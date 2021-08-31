using AcroBat;
using AcroBat.Views;
using AcroPad.Properties;
using Sgry.Azuki;
using Sgry.Azuki.WinForms;
using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace AcroPad.Views.Controls
{
    /// <summary>
    /// テキストエディターを表します。
    /// このコントロールは <see cref="AzukiControl"/> のラッパーです。
    /// </summary>
    public sealed class TextEditor : AzukiControl
    {
        #region フィールド
        private const int SEARCH_PATTERNS_MARKING_ID = 0;
        private int _wordWrapDigit;
        private LanguageKind _languageMode;
        private EncodingKind _encodingMode;
        private EolKind _eolMode;
        private WordWrapKind _wordWrapMode;
        #endregion

        #region プロパティ
        /// <summary>
        /// テキストエディターが編集しているファイルのストリームを取得します。
        /// 新規ファイルの場合は null を保持します。
        /// </summary>
        public FileStream FileStream { get; private set; }

        /// <summary>
        /// 新しく作成されるファイルであることを示す値を取得します。
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsNewFile => this.FileStream == null;

        /// <summary>
        /// 編集中のファイルのパスを取得します。
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string FilePath => this.FileStream?.Name ?? string.Empty;

        /// <summary>
        /// キャレットの行位置を取得します。
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int LineIndex
        {
            get
            {
                int lineIndex, columnIndex;
                this.Document.GetCaretIndex(out lineIndex, out columnIndex);
                return lineIndex;
            }
        }

        /// <summary>
        /// キャレットの列位置を取得します。
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int ColumnIndex => this.View.GetVirPosFromIndex(this.CaretIndex).X / this.View.HRulerUnitWidth;

        /// <summary>
        /// キャレットの文字位置を取得します。
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int CharIndex
        {
            get
            {
                int lineIndex, columnIndex;
                this.Document.GetCaretIndex(out lineIndex, out columnIndex);
                return columnIndex;
            }
        }

        /// <summary>
        /// 選択中の文字列を取得します。
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string SelectedText => this.GetSelectedText(this.Document.EolCode);

        /// <summary>
        /// 選択範囲の長さを取得します。
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int SelectedLength => this.GetSelectedTextLength();

        /// <summary>
        /// 選択範囲の開始位置を取得します。
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int SelectionBeginIndex
        {
            get
            {
                int begin, end;
                this.GetSelection(out begin, out end);
                return begin;
            }
        }

        /// <summary>
        /// 選択範囲の終了位置を取得します。
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int SelectionEndIndex
        {
            get
            {
                int begin, end;
                this.GetSelection(out begin, out end);
                return end;
            }
        }

        /// <summary>
        /// テキストが編集されていることを示す値を取得または設定します。
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsDirty
        {
            get { return this.Document.IsDirty; }
            set { this.Document.IsDirty = value; }
        }

        /// <summary>
        /// 削除が行えるかどうかを示す値を取得します。
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool CanDelete
        {
            get
            {
                if (this.IsReadOnly)
                {
                    return false;
                }

                if (this.Document.RectSelectRanges != null ||
                    this.Document.AnchorIndex != this.CaretIndex)
                {
                    return true;
                }

                if (this.Document.Length <= this.Document.CaretIndex)
                {
                    return false;
                }
                return true;
            }
        }

        /// <summary>
        /// テキストを折り返す桁数を取得または設定します。
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int WordWrapDigit
        {
            get { return this._wordWrapDigit; }
            set
            {
                if (this._wordWrapDigit != value)
                {
                    this._wordWrapDigit = value;
                    this.UpdateWordWrappedBar();
                }
            }
        }

        /// <summary>
        /// 文字列を検索するための正規表現パターンを取得します。
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Regex SearchPatterns => this.Document.WatchPatterns.Get(SEARCH_PATTERNS_MARKING_ID).Pattern;

        /// <summary>
        /// 言語の種類を取得または設定します。
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public LanguageKind LanguageMode
        {
            get { return this._languageMode; }
            set
            {
                if (this._languageMode != value)
                {
                    this._languageMode = value;
                    this.AutoIndentHook = value.GetIndentHook();
                    this.Highlighter = value.GetHighlighter();
                    this.OnLanguageModeChanged(EventArgs.Empty);
                }
            }
        }

        /// <summary>
        /// エンコードの種類を取得または設定します。
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public EncodingKind EncodingMode
        {
            get { return this._encodingMode; }
            set
            {
                if (this._encodingMode != value)
                {
                    this._encodingMode = value;
                    this.OnEncodingModeChanged(EventArgs.Empty);
                }
            }
        }

        /// <summary>
        /// 改行コードの種類を取得または設定します。
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public EolKind EolMode
        {
            get { return this._eolMode; }
            set
            {
                if (this._eolMode != value)
                {
                    this._eolMode = value;
                    this.Document.EolCode = value.GetEolCode();
                    this.OnEolModeChanged(EventArgs.Empty);
                }
            }
        }

        /// <summary>
        /// 折り返し方法の種類を取得または設定します。
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public WordWrapKind WordWrapMode
        {
            get { return this._wordWrapMode; }
            set
            {
                if (this._wordWrapMode != value)
                {
                    this._wordWrapMode = value;
                    this.ViewType = value.GetViewType();
                    this.UpdateWordWrappedBar();
                    this.OnWordWrapModeChanged(EventArgs.Empty);
                }
            }
        }

        /// <summary>
        /// コンテンツのレンダリング方法を取得します。
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new ViewType ViewType
        {
            get { return base.ViewType; }
            private set { base.ViewType = value; }
        }

        /// <summary>
        /// コンテンツエリアの幅を取得します。
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new int ViewWidth
        {
            get { return base.ViewWidth; }
            private set { base.ViewWidth = value; }
        }

        /// <summary>
        /// 描画オプションを取得します。
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new DrawingOption DrawingOption
        {
            get { return base.DrawingOption; }
            private set { base.DrawingOption = value; }
        }

        /// <summary>
        /// フォント情報を取得または設定します。
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new FontInfo FontInfo
        {
            get { return base.FontInfo; }
            set { base.FontInfo = value; }
        }

        /// <summary>
        /// 選択範囲に含まれる文字の色を取得または設定します。
        /// </summary>
        [Category(DesignerCategory.APPEARANCE)]
        [Description("選択範囲に含まれる文字の色を指定します。")]
        public Color SelectionForeColor
        {
            get { return this.ColorScheme.SelectionFore; }
            set { this.ColorScheme.SelectionFore = value; }
        }

        /// <summary>
        /// 選択範囲の色を取得または設定します。
        /// </summary>
        [Category(DesignerCategory.APPEARANCE)]
        [Description("選択範囲の色を指定します。")]
        public Color SelectionBackColor
        {
            get { return this.ColorScheme.SelectionBack; }
            set { this.ColorScheme.SelectionBack = value; }
        }

        /// <summary>
        /// 検索パターンに合致した範囲の色を取得または設定します。
        /// </summary>
        [Category(DesignerCategory.APPEARANCE)]
        [Description("検索パターンに合致した範囲の色を指定します。")]
        public Color SearchPatternsBackColor
        {
            get { return (this.ColorScheme.GetMarkingDecoration(SEARCH_PATTERNS_MARKING_ID) as BgColorTextDecoration)?.BackgroundColor ?? Color.Empty; }
            set { this.ColorScheme.SetMarkingDecoration(SEARCH_PATTERNS_MARKING_ID, new BgColorTextDecoration(value)); }
        }

        /// <summary>
        /// 強調表示された括弧の文字の色を取得または設定します。
        /// </summary>
        [Category(DesignerCategory.APPEARANCE)]
        [Description("強調表示された括弧の文字の色を指定します。")]
        public Color MatchedBracketForeColor
        {
            get { return this.ColorScheme.MatchedBracketFore; }
            set { this.ColorScheme.MatchedBracketFore = value; }
        }

        /// <summary>
        /// 強調表示された括弧の背景色を取得または設定します。
        /// </summary>
        [Category(DesignerCategory.APPEARANCE)]
        [Description("強調表示された括弧の背景色を指定します。")]
        public Color MatchedBracketBackColor
        {
            get { return this.ColorScheme.MatchedBracketBack; }
            set { this.ColorScheme.MatchedBracketBack = value; }
        }

        /// <summary>
        /// 行番号の文字の色を取得または設定します。
        /// </summary>
        [Category(DesignerCategory.APPEARANCE)]
        [Description("行番号の文字の色を指定します。")]
        public Color LineNumberForeColor
        {
            get { return this.ColorScheme.LineNumberFore; }
            set { this.ColorScheme.LineNumberFore = value; }
        }

        /// <summary>
        /// 行番号が表示される領域の色を取得または設定します。
        /// </summary>
        [Category(DesignerCategory.APPEARANCE)]
        [Description("行番号が表示される領域の色を指定します。")]
        public Color LineNumberBackColor
        {
            get { return this.ColorScheme.LineNumberBack; }
            set { this.ColorScheme.LineNumberBack = value; }
        }

        /// <summary>
        /// 編集済みであることを示すマーカーの色を取得または設定します。
        /// </summary>
        [Category(DesignerCategory.APPEARANCE)]
        [Description("編集済みであることを示すマーカーの色を指定します。")]
        public Color DirtyLineBarColor
        {
            get { return this.ColorScheme.DirtyLineBar; }
            set { this.ColorScheme.DirtyLineBar = value; }
        }

        /// <summary>
        /// 保存済みであることを示すマーカーの色を取得または設定します。
        /// </summary>
        [Category(DesignerCategory.APPEARANCE)]
        [Description("保存済みであることを示すマーカーの色を指定します。")]
        public Color CleanedLineBarColor
        {
            get { return this.ColorScheme.CleanedLineBar; }
            set { this.ColorScheme.CleanedLineBar = value; }
        }

        /// <summary>
        /// キャレットが置かれている行を示す下線の色を取得または設定します。
        /// </summary>
        [Category(DesignerCategory.APPEARANCE)]
        [Description("キャレットが置かれている行を示す下線の色を指定します。")]
        public Color HighlightColor
        {
            get { return this.ColorScheme.HighlightColor; }
            set { this.ColorScheme.HighlightColor = value; }
        }

        /// <summary>
        /// 折り返し線の色を取得または設定します。
        /// </summary>
        [Category(DesignerCategory.APPEARANCE)]
        [Description("折り返し線の色を指定します。")]
        public Color RightEdgeColor
        {
            get { return this.ColorScheme.RightEdgeColor; }
            set { this.ColorScheme.RightEdgeColor = value; }
        }

        /// <summary>
        /// 空白を表す文字の色を取得または設定します。
        /// </summary>
        [Category(DesignerCategory.APPEARANCE)]
        [Description("空白を表す文字の色を指定します。")]
        public Color WhiteSpaceColor
        {
            get { return this.ColorScheme.WhiteSpaceColor; }
            set { this.ColorScheme.WhiteSpaceColor = value; }
        }

        /// <summary>
        /// 改行を表す文字の色を取得または設定します。
        /// </summary>
        [Category(DesignerCategory.APPEARANCE)]
        [Description("改行を表す文字の色を指定します。")]
        public Color EolColor
        {
            get { return this.ColorScheme.EolColor; }
            set { this.ColorScheme.EolColor = value; }
        }

        /// <summary>
        /// ファイルの終了位置を表す文字の色を取得または設定します。
        /// </summary>
        [Category(DesignerCategory.APPEARANCE)]
        [Description("ファイルの終了位置を表す文字の色を指定します。")]
        public Color EofColor
        {
            get { return this.ColorScheme.EofColor; }
            set { this.ColorScheme.EofColor = value; }
        }

        /// <summary>
        /// 数値を表示する際の文字の色を取得または設定します。
        /// </summary>
        [Category(DesignerCategory.APPEARANCE)]
        [Description("数値を表示する際の文字の色を指定します。")]
        public Color NumeberCharColor
        {
            get { return this.GetForeColor(CharClass.Number); }
            set { this.SetForeColor(CharClass.Number, value); }
        }

        /// <summary>
        /// 文字列を表示する際の文字の色を取得または設定します。
        /// </summary>
        [Category(DesignerCategory.APPEARANCE)]
        [Description("文字列を表示する際の文字の色を指定します。")]
        public Color StringCharColor
        {
            get { return this.GetForeColor(CharClass.String); }
            set { this.SetForeColor(CharClass.String, value); }
        }

        /// <summary>
        /// コメントを表示する際の文字の色を取得または設定します。
        /// </summary>
        [Category(DesignerCategory.APPEARANCE)]
        [Description("コメントを表示する際の文字の色を指定します。")]
        public Color CommentCharColor
        {
            get { return this.GetForeColor(CharClass.Comment); }
            set { this.SetForeColor(CharClass.Comment, value); }
        }

        /// <summary>
        /// ドキュメントコメントを表示する際の文字の色を取得または設定します。
        /// </summary>
        [Category(DesignerCategory.APPEARANCE)]
        [Description("ドキュメントコメントを表示する際の文字の色を指定します。")]
        public Color DocCommentCharColor
        {
            get { return this.GetForeColor(CharClass.DocComment); }
            set { this.SetForeColor(CharClass.DocComment, value); }
        }

        /// <summary>
        /// キーワードを表示する際の文字の色を取得または設定します。
        /// </summary>
        [Category(DesignerCategory.APPEARANCE)]
        [Description("キーワードを表示する際の文字の色を指定します。")]
        public Color KeywordCharColor
        {
            get { return this.GetForeColor(CharClass.Keyword); }
            set { this.SetForeColor(CharClass.Keyword, value); }
        }

        /// <summary>
        /// キーワードを表示する際の文字の色を取得または設定します。
        /// </summary>
        [Category(DesignerCategory.APPEARANCE)]
        [Description("キーワードを表示する際の文字の色を指定します。")]
        public Color Keyword2CharColor
        {
            get { return this.GetForeColor(CharClass.Keyword2); }
            set { this.SetForeColor(CharClass.Keyword2, value); }
        }

        /// <summary>
        /// キーワードを表示する際の文字の色を取得または設定します。
        /// </summary>
        [Category(DesignerCategory.APPEARANCE)]
        [Description("キーワードを表示する際の文字の色を指定します。")]
        public Color Keyword3CharColor
        {
            get { return this.GetForeColor(CharClass.Keyword3); }
            set { this.SetForeColor(CharClass.Keyword3, value); }
        }
        #endregion

        #region イベント
        /// <summary>
        /// ファイル種別が変更されたときに発生します。
        /// </summary>
        [Category(DesignerCategory.PROPERTY_CHANGED)]
        [Description("ファイル種別が変更されたときに発生します。")]
        public event EventHandler LanguageModeChanged;

        /// <summary>
        /// 文字コードが変更されたときに発生します。
        /// </summary>
        [Category(DesignerCategory.PROPERTY_CHANGED)]
        [Description("文字コードが変更されたときに発生します。")]
        public event EventHandler EncodingModeChanged;

        /// <summary>
        /// 改行コードが変更されたときに発生します。
        /// </summary>
        [Category(DesignerCategory.PROPERTY_CHANGED)]
        [Description("改行コードが変更されたときに発生します。")]
        public event EventHandler EolModeChanged;

        /// <summary>
        /// 折り返し方法が変更されたときに発生します。
        /// </summary>
        [Category(DesignerCategory.PROPERTY_CHANGED)]
        [Description("折り返し方法が変更されたときに発生します。")]
        public event EventHandler WordWrapModeChanged;

        /// <summary>
        /// 編集済みを示すの値が変更されたときに発生します。
        /// </summary>
        [Category(DesignerCategory.PROPERTY_CHANGED)]
        [Description("IsDirty プロパティの値が変更されたときに発生します。")]
        public event EventHandler DirtyStateChanged;

        /// <summary>
        /// 選択範囲が変更されたときに発生します。
        /// </summary>
        [Description("選択範囲が変更されたときに発生します。")]
        public event SelectionChangedEventHandler SelectionChanged;

        /// <summary>
        /// 選択方法が変更されたときに発生します。
        /// </summary>
        [Description("選択方法が変更されたときに発生します。")]
        public event EventHandler SelectionModeChanged;

        /// <summary>
        /// ファイルが読み込まれた直後に発生します。
        /// </summary>
        [Description("ファイルが読み込まれた直後に発生します。")]
        public event EventHandler Loaded;

        /// <summary>
        /// ファイルが保存された直後に発生します。
        /// </summary>
        [Description("ファイルが保存された直後に発生します。")]
        public event EventHandler Saved;

        /// <summary>
        /// <see cref="Undo"/> が実行された直後に発生します。
        /// </summary>
        [Description("Undo() が実行された直後に発生します。")]
        public event EventHandler Undone;

        /// <summary>
        /// <see cref="Redo"/> が実行された直後に発生します。
        /// </summary>
        [Description("Redo() が実行された直後に発生します。")]
        public event EventHandler Redone;

        /// <summary>
        /// 切り取りが行われた直後に発生します。
        /// </summary>
        [Description("切り取りが行われた直後に発生します。")]
        public event EventHandler AfterCut;

        /// <summary>
        /// コピーが行われた直後に発生します。
        /// </summary>
        [Description("コピーが行われた直後に発生します。")]
        public event EventHandler Copied;

        /// <summary>
        /// 貼り付けが行われた直後に発生します。
        /// </summary>
        [Description("貼り付けが行われた直後に発生します。")]
        public event EventHandler Pasted;

        /// <summary>
        /// <see cref="DirtyStateChanged"/> イベントを発生させます。
        /// </summary>
        /// <param name="e">イベント情報</param>
        private void OnDirtyStateChanged(EventArgs e) => this.DirtyStateChanged?.Invoke(this, e);

        /// <summary>
        /// <see cref="SelectionChanged"/> イベントを発生させます。
        /// </summary>
        /// <param name="e">イベント情報</param>
        private void OnSelectionChanged(SelectionChangedEventArgs e) => this.SelectionChanged?.Invoke(this, e);

        /// <summary>
        /// <see cref="SelectionModeChanged"/> イベントを発生させます。
        /// </summary>
        /// <param name="e">イベント情報</param>
        private void OnSelectionModeChanged(EventArgs e) => this.SelectionModeChanged?.Invoke(this, e);

        /// <summary>
        /// <see cref="LanguageModeChanged"/> イベントを発生させます。
        /// </summary>
        /// <param name="e">イベント情報</param>
        private void OnLanguageModeChanged(EventArgs e) => this.LanguageModeChanged?.Invoke(this, e);

        /// <summary>
        /// <see cref="EncodingModeChanged"/> イベントを発生させます。
        /// </summary>
        /// <param name="e">イベント情報</param>
        private void OnEncodingModeChanged(EventArgs e) => this.EncodingModeChanged?.Invoke(this, e);

        /// <summary>
        /// <see cref="EolModeChanged"/> イベントを発生させます。
        /// </summary>
        /// <param name="e">イベント情報</param>
        private void OnEolModeChanged(EventArgs e) => this.EolModeChanged?.Invoke(this, e);

        /// <summary>
        /// <see cref="WordWrapModeChanged"/> イベントを発生させます。
        /// </summary>
        /// <param name="e">イベント情報</param>
        private void OnWordWrapModeChanged(EventArgs e) => this.WordWrapModeChanged?.Invoke(this, e);

        /// <summary>
        /// <see cref="Loaded"/> イベントを発生させます。
        /// </summary>
        /// <param name="e">イベント情報</param>
        private void OnLoaded(EventArgs e) => this.Loaded?.Invoke(this, e);

        /// <summary>
        /// <see cref="Saved"/> イベントを発生させます。
        /// </summary>
        /// <param name="e">イベント情報</param>
        private void OnSaved(EventArgs e) => this.Saved?.Invoke(this, e);

        /// <summary>
        /// <see cref="Undone"/> イベントを発生させます。
        /// </summary>
        /// <param name="e">イベント情報</param>
        private void OnUndone(EventArgs e) => this.Undone?.Invoke(this, e);

        /// <summary>
        /// <see cref="Redone"/> イベントを発生させます。
        /// </summary>
        /// <param name="e">イベント情報</param>
        private void OnRedone(EventArgs e) => this.Redone?.Invoke(this, e);

        /// <summary>
        /// <see cref="AfterCut"/> イベントを発生させます。
        /// </summary>
        /// <param name="e">イベント情報</param>
        private void OnAfterCut(EventArgs e) => this.AfterCut?.Invoke(this, e);

        /// <summary>
        /// <see cref="Copied"/> イベントを発生させます。
        /// </summary>
        /// <param name="e">イベント情報</param>
        private void OnCopied(EventArgs e) => this.Copied?.Invoke(this, e);

        /// <summary>
        /// <see cref="Pasted"/> イベントを発生させます。
        /// </summary>
        /// <param name="e">イベント情報</param>
        private void OnPasted(EventArgs e) => this.Pasted?.Invoke(this, e);
        #endregion

        #region メソッド
        /// <summary>
        /// 静的変数を初期化します。
        /// </summary>
        static TextEditor()
        {
            Marking.Register(new MarkingInfo(SEARCH_PATTERNS_MARKING_ID, nameof(SEARCH_PATTERNS_MARKING_ID)));
        }

        /// <summary>
        /// インスタンスを初期化します。
        /// </summary>
        public TextEditor()
        {
            this.Clear();
            this.DisposeStream();
            this.AddEventHandler();
        }

        /// <summary>
        /// このインスタンスが使用しているすべてのリソースを解放します。
        /// </summary>
        /// <param name="disposing">マネージリソースを開放するかどうかを示す値</param>
        protected override void Dispose(bool disposing)
        {
            this.DisposeStream();
            base.Dispose(disposing);
        }

        /// <summary>
        /// インスタンスをクリアします。
        /// </summary>
        public void Clear()
        {
            this.Text = string.Empty;
            this.IsReadOnly = false;
            this.IsDirty = false;
            this.LanguageMode = LanguageKind.Text;
            this.EncodingMode = EncodingKind.UTF8;
            this.EolMode = EolKind.CRLF;
            this.WordWrapMode = WordWrapKind.UnWrap;
            this.WordWrapDigit = 80;
            this.ClearHistory();
            this.ClearSearchPatterns();
        }

        /// <summary>
        /// このインスタンスが編集しているファイルのストリームを解放します。
        /// </summary>
        public void DisposeStream()
        {
            this.FileStream?.Dispose();
            this.FileStream = null;
        }

        /// <summary>
        /// イベントハンドラを追加します。
        /// </summary>
        private void AddEventHandler()
        {
            this.Document.DirtyStateChanged += this.Document_DirtyStateChanged;
            this.Document.SelectionChanged += this.Document_SelectionChanged;
            this.Document.SelectionModeChanged += this.Document_SelectionModeChanged;
        }

        /// <summary>
        /// 指定したエンコードで、指定したファイルを読み込みます。
        /// </summary>
        /// <param name="path">ファイルのパス</param>
        /// <param name="isReadOnly">読み取り専用かどうかを示す値</param>
        /// <param name="mode">エンコードの種類</param>
        /// <returns>読み込みに成功したかどうかを示す値</returns>
        public bool Load(string path, bool isReadOnly, EncodingKind mode)
        {
            FileStream stream = this.FilePath.Equals(path) ? this.FileStream : GetStreamForOpen(path, isReadOnly);
            if (stream == null)
            {
                return false;
            }
            return this.Load(stream, mode);
        }

        /// <summary>
        /// 指定したエンコードで、指定したファイルを読み込みます。
        /// </summary>
        /// <param name="stream">ストリーム</param>
        /// <param name="mode">エンコードの種類</param>
        public bool Load(FileStream stream, EncodingKind mode)
        {
            this.Clear();
            if (this.FileStream?.Equals(stream) != true)
            {
                this.DisposeStream();
                this.FileStream = stream;
            }
            byte[] byteString = new byte[this.FileStream.Length];
            this.FileStream.Position = 0;
            this.FileStream.Read(byteString);

            this.Text = mode.GetEncoding().GetString(byteString);
            this.EncodingMode = mode;
            this.IsReadOnly = !stream.CanWrite;
            this.IsDirty = false;
            this.ClearHistory();
            this.OnLoaded(EventArgs.Empty);
            return true;
        }

        /// <summary>
        /// 指定したエンコードで、現在のストリームを再読み込みします。
        /// </summary>
        /// <param name="mode">エンコードの種類</param>
        /// <returns>読み込みに成功したかどうかを示す値</returns>
        public void Reload(EncodingKind mode)
        {
            if (this.IsNewFile)
            {
                throw new InvalidOperationException(nameof(this.IsNewFile));
            }

            byte[] byteString = new byte[this.FileStream.Length];
            this.FileStream.Position = 0;
            this.FileStream.Read(byteString);

            this.Text = mode.GetEncoding().GetString(byteString);
            this.EncodingMode = mode;
            this.IsDirty = false;
            this.ClearHistory();
            return;
        }

        /// <summary>
        /// 指定したファイルに保存します。
        /// </summary>
        /// <param name="path">ファイルのパス</param>
        /// <returns>保存に成功したかどうかを示す値</returns>
        public bool Save(string path) => this.Save(path, this.EncodingMode);

        /// <summary>
        /// 指定したエンコードで、指定したファイルに保存します。
        /// </summary>
        /// <param name="path">ファイルのパス</param>
        /// <param name="mode">エンコードの種類</param>
        /// <returns>保存に成功したかどうかを示す値</returns>
        public bool Save(string path, EncodingKind mode)
        {
            FileStream stream = this.FilePath.Equals(path) ? this.FileStream : GetStreamForSave(path);
            if (stream == null)
            {
                return false;
            }
            return this.Save(stream, mode);
        }

        /// <summary>
        /// 指定したファイルに保存します。
        /// </summary>
        /// <param name="stream">ストリーム</param>
        /// <returns>保存に成功したかどうかを示す値</returns>
        public bool Save(FileStream stream) => this.Save(stream, this.EncodingMode);

        /// <summary>
        /// 指定したエンコードで、指定したファイルに保存します。
        /// </summary>
        /// <param name="stream">ストリーム</param>
        /// <param name="mode">エンコードの種類</param>
        /// <returns>保存に成功したかどうかを示す値</returns>
        public bool Save(FileStream stream, EncodingKind mode)
        {
            if (this.FileStream?.Equals(stream) != true)
            {
                this.DisposeStream();
                this.FileStream = stream;
            }
            this.FileStream.Position = 0;
            this.FileStream.SetLength(0);
            this.FileStream.Write(mode.GetEncoding().GetBytes(this.Text));
            this.FileStream.Flush();

            this.EncodingMode = mode;
            this.IsDirty = false;
            this.OnSaved(EventArgs.Empty);
            return true;
        }

        /// <summary>
        /// キャレット位置から先の方向に対して指定された文字列を検索します。
        /// </summary>
        /// <param name="value">検索する文字列</param>
        /// <param name="goRound">末尾まで検索して見つからなかった場合に、先頭から検索開始位置まで検索するかどうかを示す値</param>
        /// <param name="isCaseSensitive">大文字小文字を区別するかどうかを示す値</param>
        /// <returns>検索結果を表す値</returns>
        public FindResult FindNext(string value, bool goRound, bool isCaseSensitive)
        {
            if (goRound)
            {
                FindResult result = this.FindNext(value, Math.Max(this.CaretIndex, this.Document.AnchorIndex), this.TextLength, isCaseSensitive);
                if (result.Result)
                {
                    return result;
                }
                return this.FindNext(value, 0, Math.Min(this.CaretIndex, this.Document.AnchorIndex), isCaseSensitive);
            }
            else
            {
                return this.FindNext(value, Math.Max(this.CaretIndex, this.Document.AnchorIndex), this.TextLength, isCaseSensitive);
            }
        }

        /// <summary>
        /// 指定された範囲で指定された文字列を検索します。
        /// </summary>
        /// <param name="value">検索する文字列</param>
        /// <param name="begin">検索範囲の先頭</param>
        /// <param name="end">検索範囲の末尾</param>
        /// <param name="isCaseSensitive">大文字小文字を区別するかどうかを示す値</param>
        /// <returns>検索結果を表す値</returns>
        public FindResult FindNext(string value, int begin, int end, bool isCaseSensitive)
            => new FindResult(this.Document.FindNext(value, begin, end, isCaseSensitive));

        /// <summary>
        /// キャレット位置から先の方向に対して指定された正規表現パターンを検索します。
        /// </summary>
        /// <param name="regex">検索する正規表現パターン</param>
        /// <param name="goRound">末尾まで検索して見つからなかった場合に、先頭から検索開始位置まで検索するかどうかを示す値</param>
        /// <returns>検索結果を表す値</returns>
        public FindResult FindNext(Regex regex, bool goRound)
        {
            if (goRound)
            {
                FindResult result = this.FindNext(regex, Math.Max(this.CaretIndex, this.Document.AnchorIndex), this.TextLength);
                if (result.Result)
                {
                    return result;
                }
                return this.FindNext(regex, 0, Math.Min(this.CaretIndex, this.Document.AnchorIndex));
            }
            else
            {
                return this.FindNext(regex, Math.Max(this.CaretIndex, this.Document.AnchorIndex), this.TextLength);
            }
        }

        /// <summary>
        /// 指定された範囲で指定された正規表現パターンを検索します。
        /// </summary>
        /// <param name="regex">検索する正規表現パターン</param>
        /// <param name="begin">検索範囲の先頭</param>
        /// <param name="end">検索範囲の末尾</param>
        /// <returns>検索結果を表す値</returns>
        public FindResult FindNext(Regex regex, int begin, int end) => new FindResult(this.Document.FindNext(regex, begin, end));

        /// <summary>
        /// キャレット位置から前の方向に対して指定された文字列を検索します。
        /// </summary>
        /// <param name="value">検索する文字列</param>
        /// <param name="goRound">先頭まで検索して見つからなかった場合に、末尾から検索開始位置まで検索するかどうかを示す値</param>
        /// <param name="isCaseSensitive">大文字小文字を区別するかどうかを示す値</param>
        /// <returns>検索結果を表す値</returns>
        public FindResult FindPrev(string value, bool goRound, bool isCaseSensitive)
        {
            if (goRound)
            {
                FindResult result = this.FindPrev(value, 0, Math.Min(this.CaretIndex, this.Document.AnchorIndex), isCaseSensitive);
                if (result.Result)
                {
                    return result;
                }
                return this.FindPrev(value, Math.Max(this.CaretIndex, this.Document.AnchorIndex), this.TextLength, isCaseSensitive);
            }
            else
            {
                return this.FindPrev(value, 0, Math.Min(this.CaretIndex, this.Document.AnchorIndex), isCaseSensitive);
            }
        }

        /// <summary>
        /// 指定された範囲で指定された文字列を検索します。
        /// </summary>
        /// <param name="value">検索する文字列</param>
        /// <param name="begin">検索範囲の先頭</param>
        /// <param name="end">検索範囲の末尾</param>
        /// <param name="isCaseSensitive">大文字小文字を区別するかどうかを示す値</param>
        /// <returns>検索結果を表す値</returns>
        public FindResult FindPrev(string value, int begin, int end, bool isCaseSensitive)
            => new FindResult(this.Document.FindPrev(value, begin, end, isCaseSensitive));

        /// <summary>
        /// キャレット位置から前の方向に対して指定された正規表現パターンを検索します。
        /// </summary>
        /// <param name="regex">検索する正規表現パターン</param>
        /// <param name="goRound">先頭まで検索して見つからなかった場合に、末尾から検索開始位置まで検索するかどうかを示す値</param>
        /// <returns>検索結果を表す値</returns>
        public FindResult FindPrev(Regex regex, bool goRound)
        {
            if (goRound)
            {
                FindResult result = this.FindPrev(regex, 0, Math.Min(this.CaretIndex, this.Document.AnchorIndex));
                if (result.Result)
                {
                    return result;
                }
                return this.FindPrev(regex, Math.Max(this.CaretIndex, this.Document.AnchorIndex), this.TextLength);
            }
            else
            {
                return this.FindPrev(regex, 0, Math.Min(this.CaretIndex, this.Document.AnchorIndex));
            }
        }

        /// <summary>
        /// 指定された範囲で指定された正規表現パターンを検索します。
        /// </summary>
        /// <param name="regex">検索する正規表現パターン</param>
        /// <param name="begin">検索範囲の先頭</param>
        /// <param name="end">検索範囲の末尾</param>
        /// <returns>検索結果を表す値</returns>
        public FindResult FindPrev(Regex regex, int begin, int end) => new FindResult(this.Document.FindPrev(regex, begin, end));

        /// <summary>
        /// 選択されている範囲を指定された文字列で置き換えます。
        /// </summary>
        /// <param name="text">置き換える文字列</param>
        public void Replace(string text)
        {
            int begin, end;
            this.GetSelection(out begin, out end);
            this.Replace(text, begin, end);
        }

        /// <summary>
        /// 指定された範囲を指定された文字列で置き換えます。
        /// </summary>
        /// <param name="value">置き換える文字列</param>
        /// <param name="begin">置き換える範囲の先頭</param>
        /// <param name="end">置き換える範囲の末尾</param>
        public void Replace(string value, int begin, int end) => this.Document.Replace(value, begin, end);

        /// <summary>
        /// 検索された範囲を指定された文字列で置き換えます。
        /// </summary>
        /// <param name="value">置き換える文字列</param>
        /// <param name="begin">置き換える範囲の先頭</param>
        /// <param name="end">置き換える範囲の末尾</param>
        public void Replace(string value, FindResult result)
        {
            if (result.Result == false)
            {
                throw new ArgumentException(nameof(FindResult));
            }
            this.Replace(value, result.Range.Begin, result.Range.End);
        }

        /// <summary>
        /// 選択された範囲を大文字の文字列で置き換えます。
        /// </summary>
        public void ConvertUpperCase()
        {
            int begin, end;
            this.GetSelection(out begin, out end);
            this.ConvertUpperCase(begin, end);
            this.SetSelection(begin, end);
        }

        /// <summary>
        /// 指定された範囲を大文字の文字列で置き換えます。
        /// </summary>
        /// <param name="begin">置き換える範囲の先頭</param>
        /// <param name="end">置き換える範囲の末尾</param>
        public void ConvertUpperCase(int begin, int end) => this.Replace(this.SelectedText.ToUpper(), begin, end);

        /// <summary>
        /// 選択された範囲を小文字の文字列で置き換えます。
        /// </summary>
        public void ConvertLowerCase()
        {
            int begin, end;
            this.GetSelection(out begin, out end);
            this.ConvertLowerCase(begin, end);
            this.SetSelection(begin, end);
        }

        /// <summary>
        /// 指定された範囲を小文字の文字列で置き換えます。
        /// </summary>
        /// <param name="begin">置き換える範囲の先頭</param>
        /// <param name="end">置き換える範囲の末尾</param>
        public void ConvertLowerCase(int begin, int end) => this.Replace(this.SelectedText.ToLower(), begin, end);

        /// <summary>
        /// 選択された範囲を全角の文字列で置き換えます。
        /// </summary>
        public void ConvertWide()
        {
            int begin, end;
            this.GetSelection(out begin, out end);
            this.ConvertWide(begin, end);
            this.SetSelection(begin, end);
        }

        /// <summary>
        /// 指定された範囲を全角の文字列で置き換えます。
        /// </summary>
        /// <param name="begin">置き換える範囲の先頭</param>
        /// <param name="end">置き換える範囲の末尾</param>
        public void ConvertWide(int begin, int end) => this.Replace(this.SelectedText.ToWide(), begin, end);

        /// <summary>
        /// 選択された範囲を半角の文字列で置き換えます。
        /// </summary>
        public void ConvertNarrow()
        {
            int begin, end;
            this.GetSelection(out begin, out end);
            this.ConvertNarrow(begin, end);
            this.SetSelection(begin, end);
        }

        /// <summary>
        /// 指定された範囲を半角の文字列で置き換えます。
        /// </summary>
        /// <param name="begin">置き換える範囲の先頭</param>
        /// <param name="end">置き換える範囲の末尾</param>
        public void ConvertNarrow(int begin, int end) => this.Replace(this.SelectedText.ToNarrow(), begin, end);

        /// <summary>
        /// 選択された範囲をひらがなの文字列で置き換えます。
        /// </summary>
        public void ConvertHiragana()
        {
            int begin, end;
            this.GetSelection(out begin, out end);
            this.ConvertHiragana(begin, end);
            this.SetSelection(begin, end);
        }

        /// <summary>
        /// 指定された範囲をひらがなの文字列で置き換えます。
        /// </summary>
        /// <param name="begin">置き換える範囲の先頭</param>
        /// <param name="end">置き換える範囲の末尾</param>
        public void ConvertHiragana(int begin, int end) => this.Replace(this.SelectedText.ToHiragana(), begin, end);

        /// <summary>
        /// 選択された範囲をカタカナの文字列で置き換えます。
        /// </summary>
        public void ConvertKatakana()
        {
            int begin, end;
            this.GetSelection(out begin, out end);
            this.ConvertKatakana(begin, end);
            this.SetSelection(begin, end);
        }

        /// <summary>
        /// 指定された範囲をカタカナの文字列で置き換えます。
        /// </summary>
        /// <param name="begin">置き換える範囲の先頭</param>
        /// <param name="end">置き換える範囲の末尾</param>
        public void ConvertKatakana(int begin, int end) => this.Replace(this.SelectedText.ToKatakana(), begin, end);

        /// <summary>
        /// Undo グループへの登録を開始します。
        /// </summary>
        public void BeginUndo() => this.Document.BeginUndo();

        /// <summary>
        /// Undo グループへの登録を終了します。
        /// </summary>
        public void EndUndo() => this.Document.EndUndo();

        /// <summary>
        /// キャレットを指定したインデックスに設定します。
        /// </summary>
        /// <param name="index">インデックス</param>
        public void SetCaretIndex(int index) => this.SetSelection(index, index);

        /// <summary>
        /// 選択範囲を設定します。
        /// </summary>
        /// <param name="result">検索結果</param>
        public void SetSelection(FindResult result)
        {
            if (result.Result == false)
            {
                throw new InvalidOperationException(nameof(FindResult));
            }
            this.SetSelection(result.Range.Begin, result.Range.End);
        }

        /// <summary>
        /// 選択範囲をクリアします。
        /// </summary>
		public void ClearSelection() => this.SetSelection(this.CaretIndex, this.CaretIndex);

        /// <summary>
        /// 正規表現パターンを設定します。
        /// </summary>
        /// <param name="regex">正規表現</param>
        public void SetSearchPatterns(Regex regex)
        {
            this.Document.WatchPatterns.Register(new WatchPattern(SEARCH_PATTERNS_MARKING_ID, regex));
            this.Refresh();
        }

        /// <summary>
        /// 設定された正規表現パターンをクリアします。
        /// </summary>
        public void ClearSearchPatterns()
        {
            this.Document.WatchPatterns.Register(new WatchPattern(SEARCH_PATTERNS_MARKING_ID, new Regex(string.Empty)));
            this.Refresh();
        }

        /// <summary>
        /// 指定した種類の文字を表示するに用いる色を取得します。
        /// </summary>
        /// <param name="token">文字の種類</param>
        /// <returns>文字色</returns>
        private Color GetForeColor(CharClass token)
        {
            Color fore, back;
            this.GetColor(token, out fore, out back);
            return fore;
        }

        /// <summary>
        /// 指定した種類の文字を表示する際に用いる色を取得します。
        /// </summary>
        /// <param name="token">文字の種類</param>
        /// <param name="fore">文字色(戻り値)</param>
        /// <param name="back">背景色(戻り値)</param>
        private void GetColor(CharClass token, out Color fore, out Color back) => this.ColorScheme.GetColor(token, out fore, out back);

        /// <summary>
        /// 指定した種類の文字を表示する際に用いる色を設定します。
        /// </summary>
        /// <param name="token">文字の種類</param>
        /// <param name="fore">文字色</param>
        private void SetForeColor(CharClass token, Color fore) => this.SetColor(token, fore, Color.Transparent);

        /// <summary>
        /// 指定した種類の文字を表示する際に用いる色を設定します。
        /// </summary>
        /// <param name="token">文字の種類</param>
        /// <param name="fore">文字色</param>
        /// <param name="back">背景色</param>
        private void SetColor(CharClass token, Color fore, Color back) => this.ColorScheme.SetColor(token, fore, back);

        /// <summary>
        /// テキストの折り返し線を更新します。
        /// </summary>
        private void UpdateWordWrappedBar()
        {
            switch (this.WordWrapMode)
            {
                case WordWrapKind.UnWrap:
                    break;
                case WordWrapKind.DigitWrap:
                    this.View.TextAreaWidth = this.View.HRulerUnitWidth * this.WordWrapDigit;
                    this.View.Invalidate();
                    break;
                case WordWrapKind.EdgeWrap:
                    this.ViewWidth = this.ClientSize.Width - this.View.HRulerUnitWidth;
                    break;
            }
        }
        #endregion

        #region メソッド(アクション)
        /// <summary>
        /// 直前に行われた操作を取り消します。
        /// </summary>
        public new void Undo()
        {
            Actions.Undo(this);
            this.OnUndone(EventArgs.Empty);
        }

        /// <summary>
        /// 取り消された操作をやり直します。
        /// </summary>
        public new void Redo()
        {
            Actions.Redo(this);
            this.OnRedone(EventArgs.Empty);
        }

        /// <summary>
        /// 選択されたテキストを切り取ります。
        /// </summary>
        public new void Cut()
        {
            Actions.Cut(this);
            this.OnAfterCut(EventArgs.Empty);
        }

        /// <summary>
        /// 選択されたテキストをコピーします。
        /// </summary>
        public new void Copy()
        {
            Actions.Copy(this);
            this.OnCopied(EventArgs.Empty);
        }

        /// <summary>
        /// 保持されているテキストを貼り付けます。
        /// </summary>
        public new void Paste()
        {
            Actions.Paste(this);
            this.OnPasted(EventArgs.Empty);
        }

        /// <summary>
        /// キャレットをテキストの先頭に移動させます。
        /// </summary>
        public void MoveToFileHead()
        {
            Actions.MoveToFileHead(this);
        }

        /// <summary>
        /// キャレットをテキストの末尾に移動させます。
        /// </summary>
        public void MoveToFileEnd()
        {
            Actions.MoveToFileEnd(this);
        }

        /// <summary>
        /// キャレットを１ページ上に移動させます。
        /// </summary>
        public void MovePageUp()
        {
            Actions.MovePageUp(this);
        }

        /// <summary>
        /// キャレットを１ページ下に移動させます。
        /// </summary>
        public void MovePageDown()
        {
            Actions.MovePageDown(this);
        }

        /// <summary>
        /// キャレットを行の先頭に移動させます。
        /// </summary>
        public void MoveToLineHead()
        {
            Actions.MoveToLineHead(this);
        }

        /// <summary>
        /// キャレットを空白以外の文字が最初に出現する位置へ移動させます。
        /// </summary>
        public void MoveToLineHeadSmart()
        {
            Actions.MoveToLineHeadSmart(this);
        }

        /// <summary>
        /// キャレットを行の末尾に移動させます。
        /// </summary>
        public void MoveToLineEnd()
        {
            Actions.MoveToLineEnd(this);
        }

        /// <summary>
        /// キャレットを直前の単語の開始位置に移動させます。
        /// </summary>
        public void MoveToPrevWord()
        {
            Actions.MoveToPrevWord(this);
        }

        /// <summary>
        /// キャレットを直後の単語の開始位置に移動させます。
        /// </summary>
        public void MoveToNextWord()
        {
            Actions.MoveToNextWord(this);
        }

        /// <summary>
        /// キャレットを対応する括弧に移動させます。
        /// </summary>
        public void GoToMatchedBracket()
        {
            Actions.GoToMatchedBracket(this);
        }

        /// <summary>
        /// 選択範囲をテキストの先頭まで拡張させます。
        /// </summary>
        public void SelectToFileHead()
        {
            Actions.SelectToFileHead(this);
        }

        /// <summary>
        /// 選択範囲をテキストの末尾まで拡張させます。
        /// </summary>
        public void SelectToFileEnd()
        {
            Actions.SelectToFileEnd(this);
        }

        /// <summary>
        /// 選択範囲を１ページ上まで拡張させます。
        /// </summary>
        public void SelectToPageUp()
        {
            Actions.SelectToPageUp(this);
        }

        /// <summary>
        /// 選択範囲を１ページ下まで拡張させます。
        /// </summary>
        public void SelectToPageDown()
        {
            Actions.SelectToPageDown(this);
        }

        /// <summary>
        /// 選択範囲を行の先頭まで拡張させます。
        /// </summary>
        public void SelectToLineHead()
        {
            Actions.SelectToLineHead(this);
        }

        /// <summary>
        /// 選択範囲を空白以外の文字が最初に出現する位置まで拡張させます。
        /// </summary>
        public void SelectToLineHeadSmart()
        {
            Actions.SelectToLineHeadSmart(this);
        }

        /// <summary>
        /// 選択範囲を行の末尾まで拡張させます。
        /// </summary>
        public void SelectToLineEnd()
        {
            Actions.SelectToLineEnd(this);
        }

        /// <summary>
        /// 選択範囲を直前の単語の開始位置まで拡張させます。
        /// </summary>
        public void SelectToPrevWord()
        {
            Actions.SelectToPrevWord(this);
        }

        /// <summary>
        /// 選択範囲を直次の単語の開始位置まで拡張させます。
        /// </summary>
        public void SelectToNextWord()
        {
            Actions.SelectToNextWord(this);
        }

        /// <summary>
        /// 行選択範囲を１行上まで拡張させます。
        /// </summary>
        public void LineSelectToUp()
        {
            Actions.LineSelectToUp(this);
        }

        /// <summary>
        /// 行選択範囲を１行下まで拡張させます。
        /// </summary>
        public void LineSelectToDown()
        {
            Actions.LineSelectToDown(this);
        }

        /// <summary>
        /// 矩形選択範囲を上方向に拡張させます。
        /// </summary>
        public void RectSelectToUp()
        {
            Actions.RectSelectToUp(this);
        }

        /// <summary>
        /// 矩形選択範囲を下方向に拡張させます。
        /// </summary>
        public void RectSelectToDown()
        {
            Actions.RectSelectToDown(this);
        }

        /// <summary>
        /// 矩形選択範囲を左方向に拡張させます。
        /// </summary>
        public void RectSelectToLeft()
        {
            Actions.RectSelectToLeft(this);
        }

        /// <summary>
        /// 矩形選択範囲を右方向に拡張させます。
        /// </summary>
        public void RectSelectToRight()
        {
            Actions.RectSelectToRight(this);
        }

        /// <summary>
        /// 前の行に空行を挿入します。
        /// </summary>
        public void BreakPreviousLine()
        {
            Actions.BreakPreviousLine(this);
        }

        /// <summary>
        /// 次の行に空行を挿入します。
        /// </summary>
        public void BreakNextLine()
        {
            Actions.BreakNextLine(this);
        }

        /// <summary>
        /// 選択範囲に含まれるタブを空白に置き換えます。
        /// </summary>
        public void ConvertTabsToSpaces()
        {
            Actions.ConvertTabsToSpaces(this);
        }

        /// <summary>
        /// 選択範囲に含まれる空白をタブに置き換えます。
        /// </summary>
        public void ConvertSpacesToTabs()
        {
            Actions.ConvertSpacesToTabs(this);
        }

        /// <summary>
        /// 直前の単語を削除します。
        /// </summary>
        public void BackSpaceWord()
        {
            Actions.BackSpaceWord(this);
        }

        /// <summary>
        /// 直後の単語を削除します。
        /// </summary>
        public void DeleteWord()
        {
            Actions.DeleteWord(this);
        }

        /// <summary>
        /// 行頭の空白を削除します。
        /// </summary>
        public void TrimLeadingSpace()
        {
            Actions.TrimLeadingSpace(this);
        }

        /// <summary>
        /// 行末の空白を削除します。
        /// </summary>
        public void TrimTrailingSpace()
        {
            Actions.TrimTrailingSpace(this);
        }

        ///// <summary>
        ///// 拡大率を設定する
        ///// </summary>
        ///// <param name="font">フォント情報</param>
        //public void Zoom(Font font)
        //{
        //    this.Font = font;
        //    this.RefreshTextWrappedBar();
        //}
        #endregion

        #region staticメソッド
        /// <summary>
        /// 指定したファイルを開き、読み込み用のストリームを取得します。
        /// </summary>
        /// <param name="path">ファイルのパス</param>
        /// <param name="isReadOnly">読み取り専用かどうかを示す値</param>
        /// <returns>ストリーム</returns>
        public static FileStream GetStreamForOpen(string path, bool isReadOnly)
        {
            try
            {
                return new FileStream(
                    @path,
                    FileMode.Open,
                    isReadOnly ? FileAccess.Read : FileAccess.ReadWrite,
                    isReadOnly ? FileShare.ReadWrite : FileShare.Read
                );
            }
            catch (Exception)
            when (isReadOnly == false)
            {
                try
                {
                    FileStream stream = new FileStream(
                        @path,
                        FileMode.Open,
                        FileAccess.Read,
                        FileShare.ReadWrite
                    );
                    MessageBoxUtils.ShowMessageBox(MessageKind.Warning, string.Format(Resources.MSG_WAR_WRITE_INHIBIT, $"\"{path}\""));
                    return stream;
                }
                catch (Exception e)
                {
                    MessageBoxUtils.ShowMessageBox(MessageKind.Error, e.Message);
                    return null;
                }
            }
            catch (Exception e)
            {
                MessageBoxUtils.ShowMessageBox(MessageKind.Error, e.Message);
                return null;
            }
        }

        /// <summary>
        /// 指定したファイルを開き、書き込み用のストリームを取得します。
        /// </summary>
        /// <param name="path">ファイルのパス</param>
        /// <returns>ストリーム</returns>
        public static FileStream GetStreamForSave(string path)
        {
            try
            {
                return new FileStream(
                    @path,
                    FileMode.Create,
                    FileAccess.ReadWrite,
                    FileShare.Read
                );
            }
            catch (IOException e)
            {
                MessageBoxUtils.ShowMessageBox(MessageKind.Error, e.Message);
                return null;
            }
        }
        #endregion

        #region イベント処理
        /// <summary>
        /// 編集済みを示すの値が変更されたときに発生します。
        /// </summary>
        /// <param name="sender">イベントの発生源</param>
        /// <param name="e">イベントの情報</param>
        private void Document_DirtyStateChanged(object sender, EventArgs e) => this.OnDirtyStateChanged(e);

        /// <summary>
        /// 選択範囲が変更されたときに発生します。
        /// </summary>
        /// <param name="sender">イベントの発生源</param>
        /// <param name="e">イベントの情報</param>
        private void Document_SelectionChanged(object sender, SelectionChangedEventArgs e) => this.OnSelectionChanged(e);

        /// <summary>
        /// 選択方法が変更されたときに発生します。
        /// </summary>
        /// <param name="sender">イベントの発生源</param>
        /// <param name="e">イベントの情報</param>
        private void Document_SelectionModeChanged(object sender, EventArgs e) => this.OnSelectionModeChanged(e);

        /// <summary>
        /// <see cref="ZoomIned"/> イベントを発生させます。
        /// </summary>
        /// <param name="e">イベントの情報</param>
        protected override void OnZoomIned(EventArgs e)
        {
            this.UpdateWordWrappedBar();
            base.OnZoomIned(e);
        }

        /// <summary>
        /// <see cref="ZoomOuted"/> イベントを発生させます。
        /// </summary>
        /// <param name="e">イベントの情報</param>
        protected override void OnZoomOuted(EventArgs e)
        {
            this.UpdateWordWrappedBar();
            base.OnZoomOuted(e);
        }

        /// <summary>
        /// <see cref="Control.SizeChanged"/> イベントを発生させます。
        /// </summary>
        /// <param name="e">イベントの情報</param>
        protected override void OnSizeChanged(EventArgs e)
        {
            this.UpdateWordWrappedBar();
            base.OnSizeChanged(e);
        }

        /// <summary>
        /// コマンドキーを処理します。
        /// </summary>
        /// <param name="msg">メッセージ</param>
        /// <param name="keyData">キーデータ</param>
        /// <returns>コマンドキーが処理されたかどうかを示す値</returns>
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Escape:
                    if (0 < this.SelectedLength)
                    {
                        this.ClearSelection();
                        return true;
                    }
                    break;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
        #endregion

        #region 内部クラス
        /// <summary>
        /// 検索結果を表します。
        /// </summary>
        public class FindResult
        {
            /// <summary>
            /// 検索対象が見つかったかどうかを示す値を取得します。
            /// </summary>
            public bool Result => this.Range != null;

            /// <summary>
            /// 検索対象に合致する範囲を取得します。
            /// </summary>
            public SearchResult Range { get; }

            /// <summary>
            /// インスタンスを初期化します。
            /// </summary>
            /// <param name="range">範囲</param>
            public FindResult(SearchResult range)
            {
                this.Range = range;
            }
        }
        #endregion
    }
}
