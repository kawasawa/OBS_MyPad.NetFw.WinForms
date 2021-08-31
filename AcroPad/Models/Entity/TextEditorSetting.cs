using AcroBat;
using AcroBat.Models;
using System;
using System.Drawing;
using System.Reflection;
using System.Xml.Serialization;

namespace AcroPad.Models.Entity
{
    /// <summary>
    /// テキストエディターの設定を表します。
    /// </summary>
    public class TextEditorSetting : SettingBase
    {
        /// <summary>
        /// 行番号を表示するかどうかを示す値を取得または設定します。
        /// </summary>
        public bool ShowsLineNumber { get; set; } = true;

        /// <summary>
        /// 水平ルーラーを表示するかどうかを示す値を取得または設定します。
        /// </summary>
        public bool ShowsHRuler { get; set; } = true;

        /// <summary>
        /// 編集状態マーカーを表示するかどうかを示す値を取得または設定します。
        /// </summary>
        public bool ShowsDirtBar { get; set; } = true;

        /// <summary>
        /// 最終行を越えた位置までスクロールするかどうかを示す値を取得または設定します。
        /// </summary>
        public bool ScrollsBeyondLastLine { get; set; } = true;

        /// <summary>
        /// キャレットがある行に下線を表示するかどうかを示す値を取得または設定します。
        /// </summary>
        public bool HighlightsCurrentLine { get; set; } = true;

        /// <summary>
        /// 対応する括弧を強調するかどうかを示す値を取得または設定します。
        /// </summary>
        public bool HighlightsMatchedBracket { get; set; } = false;

        /// <summary>
        /// 半角の空白を表示するかどうかを示す値を取得または設定します。
        /// </summary>
        public bool DrawsSpace { get; set; } = false;

        /// <summary>
        /// 全角の空白を表示するかどうかを示す値を取得または設定します。
        /// </summary>
        public bool DrawsFullWidthSpace { get; set; } = true;

        /// <summary>
        /// タブを表示するかどうかを示す値を取得または設定します。
        /// </summary>
        public bool DrawsTab { get; set; } = true;

        /// <summary>
        /// 改行を表示するかどうかを示す値を取得または設定します。
        /// </summary>
        public bool DrawsEolCode { get; set; } = true;

        /// <summary>
        /// ファイルの終わりを表示するかどうかを示す値を取得または設定します。
        /// </summary>
        public bool DrawsEofMark { get; set; } = true;

        /// <summary>
        /// 入力されたタブを半角空白に置き換えるかどうかを示す値を取得または設定します。
        /// </summary>
        public bool ConvertsTabToSpaces { get; set; } = false;

        /// <summary>
        /// 入力された全角空白を半角空白に置き換えるかどうかを示す値を取得または設定します。
        /// </summary>
        public bool ConvertsFullWidthSpaceToSpace { get; set; } = false;

        /// <summary>
        /// バックスペースで逆インデントするかどうかを示す値を取得または設定します。
        /// </summary>
        public bool UnindentsWithBackspace { get; set; } = false;

        /// <summary>
        /// タブによるインデントの数を取得または設定します。
        /// </summary>
        public int TabWidth { get; set; } = 4;

        /// <summary>
        /// テキストを折り返す桁数を取得または設定します。
        /// </summary>
        public int WordWrapDigit { get; set; } = 80;

        /// <summary>
        /// 言語の種類を取得または設定します。
        /// </summary>
        [ApplySettings(ApplySettingsKind.OnInitialize)]
        public LanguageKind LanguageMode { get; set; } = LanguageKind.Text;

        /// <summary>
        /// エンコードの種類を取得または設定します。
        /// </summary>
        [ApplySettings(ApplySettingsKind.OnInitialize)]
        public EncodingKind EncodingMode { get; set; } = EncodingKind.UTF8;

        /// <summary>
        /// 改行コードの種類を取得または設定します。
        /// </summary>
        [ApplySettings(ApplySettingsKind.OnInitialize)]
        public EolKind EolMode { get; set; } = EolKind.CRLF;

        /// <summary>
        /// 折り返し方法の種類を取得または設定します。
        /// </summary>
        [ApplySettings(ApplySettingsKind.OnInitialize)]
        public WordWrapKind WordWrapMode { get; set; } = WordWrapKind.UnWrap;

        /// <summary>
        /// フォント情報を取得または設定します。
        /// </summary>
        [SetPropertyIgnore]
        public FontInfo FontInfo { get; set; } = new FontInfo(SystemFonts.MenuFont);

        /// <summary>
        /// 文字の色を取得または設定します。
        /// </summary>
        [SetPropertyIgnore]
        public ColorInfo ForeColorInfo { get; set; } = new ColorInfo(Color.Black);

        /// <summary>
        /// 背景の色を取得または設定します。
        /// </summary>
        [SetPropertyIgnore]
        public ColorInfo BackColorInfo { get; set; } = new ColorInfo(Color.White);

        /// <summary>
        /// 選択範囲に含まれる文字の色を取得または設定します。
        /// </summary>
        [SetPropertyIgnore]
        public ColorInfo SelectionForeColorInfo { get; set; } = new ColorInfo(Color.Transparent);

        /// <summary>
        /// 選択範囲の色を取得または設定します。
        /// </summary>
        [SetPropertyIgnore]
        public ColorInfo SelectionBackColorInfo { get; set; } = new ColorInfo(Color.LightSkyBlue);

        /// <summary>
        /// 検索パターンに合致した範囲の色を取得または設定します。
        /// </summary>
        [SetPropertyIgnore]
        public ColorInfo SearchPatternsBackColorInfo { get; set; } = new ColorInfo(Color.LightCyan);

        /// <summary>
        /// 強調された括弧の文字の色を取得または設定します。
        /// </summary>
        [SetPropertyIgnore]
        public ColorInfo MatchedBracketForeColorInfo { get; set; } = new ColorInfo(Color.Transparent);

        /// <summary>
        /// 強調された括弧の背景の色を取得または設定します。
        /// </summary>
        [SetPropertyIgnore]
        public ColorInfo MatchedBracketBackColorInfo { get; set; } = new ColorInfo(Color.Cyan);

        /// <summary>
        /// 行番号の文字の色を取得または設定します。
        /// </summary>
        [SetPropertyIgnore]
        public ColorInfo LineNumberForeColorInfo { get; set; } = new ColorInfo(Color.SteelBlue);

        /// <summary>
        /// 行番号が表示される領域の色を取得または設定します。
        /// </summary>
        [SetPropertyIgnore]
        public ColorInfo LineNumberBackColorInfo { get; set; } = new ColorInfo(SystemColors.Control);

        /// <summary>
        /// 編集済みであることを示すマーカーの色を取得または設定します。
        /// </summary>
        [SetPropertyIgnore]
        public ColorInfo DirtyLineBarColorInfo { get; set; } = new ColorInfo(Color.Gold);

        /// <summary>
        /// 保存済みであることを示すマーカーの色を取得または設定します。
        /// </summary>
        [SetPropertyIgnore]
        public ColorInfo CleanedLineBarColorInfo { get; set; } = new ColorInfo(Color.LightGreen);

        /// <summary>
        /// キャレットが置かれている行を示す下線の色を取得または設定します。
        /// </summary>
        [SetPropertyIgnore]
        public ColorInfo HighlightColorInfo { get; set; } = new ColorInfo(Color.SteelBlue);

        /// <summary>
        /// 折り返し線の色を取得または設定します。
        /// </summary>
        [SetPropertyIgnore]
        public ColorInfo RightEdgeColorInfo { get; set; } = new ColorInfo(Color.LightGray);

        /// <summary>
        /// 空白を表す文字の色を取得または設定します。
        /// </summary>
        [SetPropertyIgnore]
        public ColorInfo WhiteSpaceColorInfo { get; set; } = new ColorInfo(Color.LightGray);

        /// <summary>
        /// 改行を表す文字の色を取得または設定します。
        /// </summary>
        [SetPropertyIgnore]
        public ColorInfo EolColorInfo { get; set; } = new ColorInfo(Color.LightBlue);

        /// <summary>
        /// ファイルの終了位置を表す文字の色を取得または設定します。
        /// </summary>
        [SetPropertyIgnore]
        public ColorInfo EofColorInfo { get; set; } = new ColorInfo(Color.LightBlue);

        /// <summary>
        /// 数値を表示する際の文字の色を取得または設定します。
        /// </summary>
        [SetPropertyIgnore]
        public ColorInfo NumeberCharColorInfo { get; set; } = new ColorInfo(Color.Black);

        /// <summary>
        /// 文字列を表示する際の文字の色を取得または設定します。
        /// </summary>
        [SetPropertyIgnore]
        public ColorInfo StringCharColorInfo { get; set; } = new ColorInfo(Color.DarkRed);

        /// <summary>
        /// コメントを表示する際の文字の色を取得または設定します。
        /// </summary>
        [SetPropertyIgnore]
        public ColorInfo CommentCharColorInfo { get; set; } = new ColorInfo(Color.Green);

        /// <summary>
        /// ドキュメントコメントを表示する際の文字の色を取得または設定します。
        /// </summary>
        [SetPropertyIgnore]
        public ColorInfo DocCommentCharColorInfo { get; set; } = new ColorInfo(Color.Gray);

        /// <summary>
        /// キーワードを表示する際の文字の色を取得または設定します。
        /// </summary>
        [SetPropertyIgnore]
        public ColorInfo KeywordCharColorInfo { get; set; } = new ColorInfo(Color.Blue);

        /// <summary>
        /// キーワードを表示する際の文字の色を取得または設定します。
        /// </summary>
        [SetPropertyIgnore]
        public ColorInfo Keyword2CharColorInfo { get; set; } = new ColorInfo(Color.BlueViolet);

        /// <summary>
        /// キーワードを表示する際の文字の色を取得または設定します。
        /// </summary>
        [SetPropertyIgnore]
        public ColorInfo Keyword3CharColorInfo { get; set; } = new ColorInfo(Color.Navy);

        [XmlIgnore]
        public Font Font
        {
            get { return this.FontInfo.Font; }
            set { this.FontInfo.Font = value; }
        }

        [XmlIgnore]
        public Color ForeColor
        {
            get { return this.ForeColorInfo.Color; }
            set { this.ForeColorInfo.Color = value; }
        }

        [XmlIgnore]
        public Color BackColor
        {
            get { return this.BackColorInfo.Color; }
            set { this.BackColorInfo.Color = value; }
        }

        [XmlIgnore]
        public Color SelectionForeColor
        {
            get { return this.SelectionForeColorInfo.Color; }
            set { this.SelectionForeColorInfo.Color = value; }
        }

        [XmlIgnore]
        public Color SelectionBackColor
        {
            get { return this.SelectionBackColorInfo.Color; }
            set { this.SelectionBackColorInfo.Color = value; }
        }

        [XmlIgnore]
        public Color SearchPatternsBackColor
        {
            get { return this.SearchPatternsBackColorInfo.Color; }
            set { this.SearchPatternsBackColorInfo.Color = value; }
        }

        [XmlIgnore]
        public Color MatchedBracketForeColor
        {
            get { return this.MatchedBracketForeColorInfo.Color; }
            set { this.MatchedBracketForeColorInfo.Color = value; }
        }

        [XmlIgnore]
        public Color MatchedBracketBackColor
        {
            get { return this.MatchedBracketBackColorInfo.Color; }
            set { this.MatchedBracketBackColorInfo.Color = value; }
        }

        [XmlIgnore]
        public Color LineNumberForeColor
        {
            get { return this.LineNumberForeColorInfo.Color; }
            set { this.LineNumberForeColorInfo.Color = value; }
        }

        [XmlIgnore]
        public Color LineNumberBackColor
        {
            get { return this.LineNumberBackColorInfo.Color; }
            set { this.LineNumberBackColorInfo.Color = value; }
        }

        [XmlIgnore]
        public Color DirtyLineBarColor
        {
            get { return this.DirtyLineBarColorInfo.Color; }
            set { this.DirtyLineBarColorInfo.Color = value; }
        }

        [XmlIgnore]
        public Color CleanedLineBarColor
        {
            get { return this.CleanedLineBarColorInfo.Color; }
            set { this.CleanedLineBarColorInfo.Color = value; }
        }

        [XmlIgnore]
        public Color HighlightColor
        {
            get { return this.HighlightColorInfo.Color; }
            set { this.HighlightColorInfo.Color = value; }
        }

        [XmlIgnore]
        public Color RightEdgeColor
        {
            get { return this.RightEdgeColorInfo.Color; }
            set { this.RightEdgeColorInfo.Color = value; }
        }

        [XmlIgnore]
        public Color WhiteSpaceColor
        {
            get { return this.WhiteSpaceColorInfo.Color; }
            set { this.WhiteSpaceColorInfo.Color = value; }
        }

        [XmlIgnore]
        public Color EolColor
        {
            get { return this.EolColorInfo.Color; }
            set { this.EolColorInfo.Color = value; }
        }

        [XmlIgnore]
        public Color EofColor
        {
            get { return this.EofColorInfo.Color; }
            set { this.EofColorInfo.Color = value; }
        }

        [XmlIgnore]
        public Color NumeberCharColor
        {
            get { return this.NumeberCharColorInfo.Color; }
            set { this.NumeberCharColorInfo.Color = value; }
        }

        [XmlIgnore]
        public Color StringCharColor
        {
            get { return this.StringCharColorInfo.Color; }
            set { this.StringCharColorInfo.Color = value; }
        }

        [XmlIgnore]
        public Color CommentCharColor
        {
            get { return this.CommentCharColorInfo.Color; }
            set { this.CommentCharColorInfo.Color = value; }
        }

        [XmlIgnore]
        public Color DocCommentCharColor
        {
            get { return this.DocCommentCharColorInfo.Color; }
            set { this.DocCommentCharColorInfo.Color = value; }
        }

        [XmlIgnore]
        public Color KeywordCharColor
        {
            get { return this.KeywordCharColorInfo.Color; }
            set { this.KeywordCharColorInfo.Color = value; }
        }

        [XmlIgnore]
        public Color Keyword2CharColor
        {
            get { return this.Keyword2CharColorInfo.Color; }
            set { this.Keyword2CharColorInfo.Color = value; }
        }

        [XmlIgnore]
        public Color Keyword3CharColor
        {
            get { return this.Keyword3CharColorInfo.Color; }
            set { this.Keyword3CharColorInfo.Color = value; }
        }

        /// <summary>
        /// 指定したオブジェクトに同名のプロパティの値を転記します。
        /// </summary>
        /// <param name="target">オブジェクト</param>
        public void UpdateValue(object target)
        {
            foreach (PropertyInfo property in this.GetType().GetProperties())
            {
                bool cancel = false;
                ApplySettingsAttribute attribute = property.GetCustomAttribute(typeof(ApplySettingsAttribute)) as ApplySettingsAttribute;
                if (attribute != null)
                {
                    switch (attribute.SettingsUsageMode)
                    {
                        case ApplySettingsKind.OnInitialize:
                            cancel = true;
                            break;
                    }
                }
                if (cancel)
                {
                    continue;
                }
                Extensions.SetProperty(this, target, property);
            }
        }

        /// <summary>
        /// 設定項目の反映方法を指定する属性を表します。
        /// </summary>
        [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
        private class ApplySettingsAttribute : Attribute
        {
            /// <summary>
            /// 反映方法を取得します。
            /// </summary>
            public ApplySettingsKind SettingsUsageMode { get; }

            /// <summary>
            /// インスタンスを初期化します。
            /// </summary>
            /// <param name="mode">反映方法</param>
            public ApplySettingsAttribute(ApplySettingsKind mode)
            {
                this.SettingsUsageMode = mode;
            }
        }

        /// <summary>
        /// 反映方法の種類を表します。
        /// </summary>
        private enum ApplySettingsKind
        {
            /// <summary>
            /// 指定されていません。
            /// </summary>
            none = 0,

            /// <summary>
            /// 初期化の際のみ反映されることを示します。
            /// </summary>
            OnInitialize = 1,
        }
    }
}
