using AcroBat;
using AcroBat.Models;
using AcroBat.Views;
using AcroBat.Views.Forms;
using AcroBat.WindowsAPI;
using AcroPad.Models;
using AcroPad.Models.Associations;
using AcroPad.Properties;
using AcroPad.Views.Contents;
using AcroPad.Views.Controls;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace AcroPad.Views.Forms
{
    /// <summary>
    /// メインフォームを表します。
    /// </summary>
    public partial class MainForm : FormBase
    {
        #region 変数・プロパティ
        /// <summary>
        /// アクセスキーの一覧を取得します。
        /// </summary>
        private static readonly string[] ACCESS_KEYS = new[]
        {
            "1", "2", "3", "4", "5", "6", "7", "8", "9", "A",
            "B", "C", "D", "E", "F", "G", "H", "I", "J", "K",
            "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U",
            "V", "W", "X", "Y", "Z",
        };

        /// <summary>
        /// ドッキングパネルを取得します。
        /// </summary>
        private DockingPanel DockingPanel => this._dockingPanel;

        /// <summary>
        /// アクティブなドキュメントを取得します。
        /// </summary>
        private EditorDocument ActiveDocument => this.DockingPanel.ActiveDocument as EditorDocument;

        /// <summary>
        /// エクスプローラーコンテンツを取得します。
        /// </summary>
        private ExplorerContent ExplorerContent { get; } = new ExplorerContent();

        /// <summary>
        /// 検索コンテンツを取得します。
        /// </summary>
        private SearchContent SearchContent { get; } = new SearchContent();

        /// <summary>
        /// メニューバーの表示状態を取得または設定します。
        /// </summary>
        public bool MenuBarVisible
        {
            get { return this._menuBar.Visible; }
            set { this._menuBar.Visible = value; }
        }

        /// <summary>
        /// ツールバーの表示状態を取得または設定します。
        /// </summary>
        public bool ToolBarVisible
        {
            get { return this._toolBar.Visible; }
            set { this._toolBar.Visible = value; }
        }

        /// <summary>
        /// ステータスバーの表示状態を取得または設定します。
        /// </summary>
        public bool StatusBarVisible
        {
            get { return this._statusBar.Visible; }
            set { this._statusBar.Visible = value; }
        }

        /// <summary>
        /// システムメニューアイテム「メニューバー」を表します。
        /// </summary>
        private MENUITEMINFO _scmdMenuBar;

        /// <summary>
        /// システムメニューアイテム「ツールバー」を表します。
        /// </summary>
        private MENUITEMINFO _scmdToolBar;

        /// <summary>
        /// システムメニューアイテム「ステータスバー」を表します。
        /// </summary>
        private MENUITEMINFO _scmdStatusBar;

        /// <summary>
        /// 画面ロード時に設定情報が適用された直後に発生します。
        /// </summary>
        private EventHandler _settingsAppliedOnLoad;
        #endregion

        #region 初期化処理
        /// <summary>
        /// インスタンスを初期化します。
        /// </summary>
        public MainForm()
        {
            this.InitializeComponent();
            this.InitializeItems();
            this.AddEventHandler();
        }

        /// <summary>
        /// 特定のコントロールを初期化します。
        /// </summary>
        private void InitializeItems()
        {
            this.SetFormIcon();
            this.SetItemImage();
            this.SetItemToolTip();
            this.InitilizeSystemMenuItems();
            this.InitializeDropDownItems();
            this.IsMdiContainer = true;
            this.MainMenuStrip = this._menuBar;
            this._menuBar.ContextMenuStrip = this._contextMenu;
            this._toolBar.ContextMenuStrip = this._contextMenu;
            this._statusBar.BackColor = ApplicationColor.DependOnBuild;
            this.DockingPanel.DockLeftPortion = this.ExplorerContent.Width;
            this.DockingPanel.DockRightPortion = this.SearchContent.Width;
        }

        /// <summary>
        /// システムメニューの項目を初期化します。
        /// </summary>
        private void InitilizeSystemMenuItems()
        {
            this._scmdMenuBar = SystemMenuFactory.CreateMenuItem(this.mcmdMenuBar.Text);
            this._scmdToolBar = SystemMenuFactory.CreateMenuItem(this.mcmdToolBar.Text);
            this._scmdStatusBar = SystemMenuFactory.CreateMenuItem(this.mcmdStatusBar.Text);
            this.SystemMenuCustomizer.InsertMenuItem(6, this._scmdMenuBar, this.MenuBarCommand_Executed);
            this.SystemMenuCustomizer.InsertMenuItem(7, this._scmdToolBar, this.ToolBarCommand_Executed);
            this.SystemMenuCustomizer.InsertMenuItem(8, this._scmdStatusBar, this.StatusBarCommand_Executed);
            this.SystemMenuCustomizer.InsertMenuItem(9, SystemMenuFactory.CreateSeparater());
        }

        /// <summary>
        /// ドロップダウンの項目を初期化します。
        /// </summary>
        private void InitializeDropDownItems()
        {
            Action<ToolStripItemCollection, Enum, string, EventHandler> addItem =
                (target, kind, text, handler) =>
                {
                    ToolStripMenuItem item = new ToolStripMenuItem();
                    item.Tag = kind;
                    item.Text = text;
                    item.Click += handler;
                    target.Add(item);
                };

            Array kinds = Enum.GetValues(typeof(LanguageKind));
            for (int i = 0; i < kinds.Length; i++)
            {
                Enum kind = (LanguageKind)kinds.GetValue(i);
                Association assoc = LanguageContainer.Instance.GetAssociation((LanguageKind)kind);
                string text = $"{assoc.DisplayName}{(i < ACCESS_KEYS.Length ? $"(&{ACCESS_KEYS[i]})" : string.Empty)}";
                EventHandler handler = this.SwitchLanguageModeCommand_Executed;
                ToolStripItemCollection menuItems = this.mihLanguageMode.DropDownItems;
                ToolStripItemCollection statusItems = this.stsLanguageMode.DropDownItems;

                addItem(menuItems, kind, text, handler);
                addItem(statusItems, kind, text, handler);
            }

            kinds = Enum.GetValues(typeof(EncodingKind));
            for (int i = 0; i < kinds.Length; i++)
            {
                Enum kind = (EncodingKind)kinds.GetValue(i);
                string text = $"{kind.ToString()}{(i < ACCESS_KEYS.Length ? $"(&{ACCESS_KEYS[i]})" : string.Empty)}";
                EventHandler handler = this.SwitchEncodingModeCommand_Executed;
                ToolStripItemCollection menuItems = this.mihEncodingMode.DropDownItems;
                ToolStripItemCollection statusItems = this.stsEncodingMode.DropDownItems;

                addItem(menuItems, kind, text, handler);
                addItem(statusItems, kind, text, handler);
            }

            kinds = Enum.GetValues(typeof(EolKind));
            for (int i = 0; i < kinds.Length; i++)
            {
                Enum kind = (EolKind)kinds.GetValue(i);
                string text = $"{kind.ToString()}{(i < ACCESS_KEYS.Length ? $"(&{ACCESS_KEYS[i]})" : string.Empty)}";
                EventHandler handler = this.SwitchEolModeCommand_Executed;
                ToolStripItemCollection menuItems = this.mihEolMode.DropDownItems;
                ToolStripItemCollection statusItems = this.stsEolMode.DropDownItems;

                addItem(menuItems, kind, text, handler);
                addItem(statusItems, kind, text, handler);
            }

            kinds = Enum.GetValues(typeof(WordWrapKind));
            for (int i = 0; i < kinds.Length; i++)
            {
                Enum kind = (WordWrapKind)kinds.GetValue(i);
                string text = $"{kind.ToString()}{(i < ACCESS_KEYS.Length ? $"(&{ACCESS_KEYS[i]})" : string.Empty)}";
                EventHandler handler = this.SwitchWordWrapModeCommand_Executed;
                ToolStripItemCollection menuItems = this.mihWordWrapMode.DropDownItems;

                addItem(menuItems, kind, text, handler);
            }
        }
        #endregion

        #region 設定情報
        /// <summary>
        /// 設定情報をコントロールに反映します。
        /// </summary>
        private void ApplySettings()
        {
            SettingContainer.Instance.WindowSetting.SetValue(this);
            SettingContainer.Instance.DockingPanelSetting.SetValue(this.DockingPanel);
            SettingContainer.Instance.ExplorerSetting.SetValue(this.ExplorerContent);
            SettingContainer.Instance.SearchSetting.SetValue(this.SearchContent);
            HistoryContainer.Instance.SearchHistory.SetValue(this.SearchContent);
            LayoutContainer.Instance.DockingPanelLayout.SetValue(this.DockingPanel, this.DeserializeContent);
        }

        /// <summary>
        /// コントロールの設定を記録します。
        /// </summary>
        private void SaveSettings()
        {
            SettingContainer.Instance.WindowSetting.GetValue(this);
            SettingContainer.Instance.DockingPanelSetting.GetValue(this.DockingPanel);
            SettingContainer.Instance.ExplorerSetting.GetValue(this.ExplorerContent);
            SettingContainer.Instance.SearchSetting.GetValue(this.SearchContent);
            HistoryContainer.Instance.SearchHistory.GetValue(this.SearchContent);
            LayoutContainer.Instance.DockingPanelLayout.GetValue(this.DockingPanel);
        }

        /// <summary>
        /// クラス名をもとにコンテンツを取得します。
        /// </summary>
        /// <param name="fullClassName">クラス名</param>
        /// <returns>コンテンツ</returns>
        private IDockContent DeserializeContent(string fullClassName)
        {
            if (typeof(ExplorerContent).ToString().Equals(fullClassName))
            {
                return this.ExplorerContent;
            }
            else if (typeof(SearchContent).ToString().Equals(fullClassName))
            {
                return this.SearchContent;
            }
            else
            {
                return null;
            }
        }
        #endregion

        #region 汎用処理
        /// <summary>
        /// 新しいドキュメントを追加します。
        /// </summary>
        private void AddDocument() => this.AddDocument(new EditorDocument(ViewUtils.GetSequence()));

        /// <summary>
        /// 指定したファイルをドキュメントとして追加します。
        /// </summary>
        /// <param name="path">ファイルのパス</param>
        /// <param name="isReadOnly">読み取り専用かどうか</param>
        private void AddDocument(string path, bool isReadOnly)
        {
            if (string.IsNullOrEmpty(path))
            {
                throw new ArgumentException($"{nameof(path)} が null か空文字です。");
            }

            EditorDocument document = 
                this.DockingPanel.GetContents<EditorDocument>()
                .FirstOrDefault(d => d.TextEditor.FilePath.Equals(path));
            if (document != null)
            {
                document.Select();
                EncodingKind mode = 
                    EncodingAnalyzer.Analyze(document.TextEditor.FileStream)?.GetEncodingKind() ??
                    SettingContainer.Instance.TextEditorSetting.EncodingMode;
                if (document.TextEditor.EncodingMode == mode ||
                    MessageBoxUtils.ShowMessageBox(
                        MessageKind.Confirm,
                        string.Format(Resources.MSG_CFM_RELOAD, document.FileName, document.TextEditor.EncodingMode, mode)
                    ) != DialogResult.Yes)
                {
                    return;
                }
                this.ReloadDocument(document, mode);
            }
            else
            {
                FileStream stream = TextEditor.GetStreamForOpen(path, isReadOnly);
                if (stream == null)
                {
                    return;
                }
                EncodingKind mode =
                    EncodingAnalyzer.Analyze(stream)?.GetEncodingKind() ?? 
                    SettingContainer.Instance.TextEditorSetting.EncodingMode;
                this.AddDocument(new EditorDocument(stream, mode));
            }
        }

        /// <summary>
        /// 指定したファイルをドキュメントとして追加します。
        /// </summary>
        /// <param name="path">ファイルのパス</param>
        /// <param name="isReadOnly">読み取り専用かどうか</param>
        /// <param name="mode">エンコードの種類</param>
        private void AddDocument(string path, bool isReadOnly, EncodingKind mode)
        {
            if (string.IsNullOrEmpty(path))
            {
                throw new ArgumentException($"{nameof(path)} が null か空文字です。");
            }

            EditorDocument document = 
                this.DockingPanel.GetContents<EditorDocument>()
                .FirstOrDefault(d => d.TextEditor.FilePath.Equals(path));
            if (document != null)
            {
                document.Select();
                if (document.TextEditor.EncodingMode == mode ||
                    MessageBoxUtils.ShowMessageBox(
                        MessageKind.Confirm, 
                        string.Format(Resources.MSG_CFM_RELOAD, document.FileName, document.TextEditor.EncodingMode, mode)
                    ) != DialogResult.Yes)
                {
                    return;
                }
                this.ReloadDocument(document, mode);
            }
            else
            {
                FileStream stream = TextEditor.GetStreamForOpen(path, isReadOnly);
                if (stream == null)
                {
                    return;
                }
                this.AddDocument(new EditorDocument(stream, mode));
            }
        }

        /// <summary>
        /// 指定したドキュメントを追加します。
        /// </summary>
        /// <param name="document">ドキュメント</param>
        private void AddDocument(EditorDocument document)
        {
            this.AddEventHandler(document);
            this.DockingPanel.AddDocument(document);
        }

        /// <summary>
        /// ファイルを選択し、新しいドキュメントに読み込みます。
        /// </summary>
        /// <returns>読み込みに成功したかどうかを示す値</returns>
        private bool OpenDocument()
        {
            FileDialogArg arg = ViewUtils.GetDialogArgsForOpen();
            OpenFileDialogResult result = DialogUtils.ShowOpenFileDialog(arg);
            if (result.DialogResult != DialogResult.OK)
            {
                return false;
            }

            EncodingKind mode;
            if (Enum.TryParse(arg.Encodings[result.SelectedEncodingIndex], out mode))
            {
                result.FileNames.ForEach(path => this.AddDocument(path, result.ReadOnlyChecked, mode));
            }
            else
            {
                result.FileNames.ForEach(path => this.AddDocument(path, result.ReadOnlyChecked));
            }
            return true;
        }

        /// <summary>
        /// 指定したエンコードでアクティブなドキュメントを読み直します。
        /// </summary>
        /// <param name="mode">エンコードの種類</param>
        /// <returns>処理されたかどうかを示す値</returns>
        private bool ReloadDocument(EncodingKind mode)
        {
            if (this.ActiveDocument == null)
            {
                return false;
            }
            return this.ReloadDocument(this.ActiveDocument, mode);
        }

        /// <summary>
        /// 指定したエンコードでドキュメントを読み直します。
        /// </summary>
        /// <param name="document">ドキュメント</param>
        /// <param name="mode">エンコードの種類</param>
        /// <returns>処理されたかどうかを示す値</returns>
        private bool ReloadDocument(EditorDocument document, EncodingKind mode)
        {
            if (document.TextEditor.IsDirty &&
                MessageBoxUtils.ShowMessageBox(
                    MessageKind.WarningCancelable,
                    string.Format(Resources.MSG_WAR_DISPOSE_CHANGE, document.FileName)
                ) != DialogResult.OK)
            {
                return false;
            }
            document.TextEditor.Reload(mode);
            return true;
        }

        /// <summary>
        /// アクティブなドキュメントを保存します。
        /// </summary>
        /// <returns>処理されたかどうかを示す値</returns>
        private bool SaveDocument()
        {
            if (this.ActiveDocument == null)
            {
                return false;
            }
            return this.SaveDocument(this.ActiveDocument);
        }

        /// <summary>
        /// 指定したドキュメントを保存します。
        /// </summary>
        /// <param name="document">ドキュメント</param>
        /// <returns>処理されたかどうかを示す値</returns>
        private bool SaveDocument(EditorDocument document)
        {
            if (document.TextEditor.IsNewFile)
            {
                return this.SaveAsDocument(document);
            }
            else if (document.TextEditor.IsReadOnly)
            {
                return false;
            }
            return document.TextEditor.Save(document.TextEditor.FileStream);
        }

        /// <summary>
        /// ファイルを選択し、アクティブなドキュメントを保存します。
        /// </summary>
        /// <returns>処理されたかどうかを示す値</returns>
        private bool SaveAsDocument()
        {
            if (this.ActiveDocument == null)
            {
                return false;
            }
            return this.SaveAsDocument(this.ActiveDocument);
        }

        /// <summary>
        /// ファイルを選択し、指定したドキュメントを保存します。
        /// </summary>
        /// <param name="document">ドキュメント</param>
        /// <returns>処理されたかどうかを示す値</returns>
        private bool SaveAsDocument(EditorDocument document)
        {
            FileDialogArg arg = ViewUtils.GetDialogArgsForSave(document);
            SaveFileDialogResult result = DialogUtils.ShowSaveFileDialog(arg);
            if (result.DialogResult != DialogResult.OK)
            {
                return false;
            }

            string path = result.FilePath;
            EncodingKind mode = ViewUtils.GetEncodingKindFromResult(result);
            if (Path.HasExtension(path) == false)
            {
                Association assoc = LanguageContainer.Instance.GetAssociation(document.TextEditor.LanguageMode);
                if (0 < assoc.Extensions?.Length)
                {
                    path += ViewUtils.GetExtensionFromResult(result, assoc.Extensions[0]);
                }
            }

            if (document.TextEditor.IsReadOnly &&
                document.TextEditor.FilePath.Equals(path))
            {
                MessageBoxUtils.ShowMessageBox(MessageKind.Error, Resources.MSG_ERR_SAVE_FAILED_BY_READ_ONLY);
                return false;
            }
            return document.TextEditor.Save(path, mode);
        }

        /// <summary>
        /// すべてのドキュメントを保存します。
        /// </summary>
        /// <returns>処理されたかどうかを示す値</returns>
        private bool SaveAllDocument()
        {
            foreach (EditorDocument document in this.DockingPanel.GetContents<EditorDocument>())
            {
                if (this.SaveDocument(document) == false)
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// アクティブなドキュメントを閉じます。
        /// </summary>
        /// <returns>処理されたかどうかを示す値</returns>
        private bool CloseDocument()
        {
            if (this.ActiveDocument == null)
            {
                return false;
            }
            return this.CloseDocument(this.ActiveDocument);
        }

        /// <summary>
        /// 指定したドキュメントを閉じます。
        /// </summary>
        /// <param name="document">ドキュメント</param>
        /// <returns>処理されたかどうかを示す値</returns>
        private bool CloseDocument(EditorDocument document)
        {
            document.Close();
            return document.IsDisposed;
        }

        /// <summary>
        /// アクティブなドキュメントを除くすべてのドキュメントを閉じます。
        /// </summary>
        /// <returns>処理されたかどうかを示す値</returns>
        private bool CloseAllButThisDocument()
        {
            if (this.ActiveDocument == null)
            {
                return false;
            }
            return this.CloseAllButThisDocument(this.ActiveDocument);
        }

        /// <summary>
        /// 指定したドキュメントを除くすべてのドキュメントを閉じます。
        /// </summary>
        /// <param name="exceptDocument">除外するドキュメント</param>
        /// <returns>処理されたかどうかを示す値</returns>
        private bool CloseAllButThisDocument(EditorDocument exceptDocument)
        {
            foreach (EditorDocument document in this.DockingPanel.GetContents<EditorDocument>())
            {
                if (document.Equals(exceptDocument))
                {
                    continue;
                }

                if (this.CloseDocument(document) == false)
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// すべてのドキュメントを閉じます。
        /// </summary>
        /// <returns>処理されたかどうかを示す値</returns>
        private bool CloseAllDocument()
        {
            foreach (EditorDocument document in this.DockingPanel.GetContents<EditorDocument>())
            {
                if (this.CloseDocument(document) == false)
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// ドキュメントの変更内容の保存確認を行い、ドキュメントを閉じます。
        /// </summary>
        /// <param name="document">ドキュメント</param>
        /// <returns>処理されたかどうかを示す値</returns>
        private bool TrySaveAndCloseOnFormClosing(EditorDocument document)
        {
            Func<EditorDocument, string, bool> saveAndClose = (d, m) =>
            {
                d.Select();
                switch (MessageBoxUtils.ShowMessageBox(MessageKind.ConfirmCancelable, m))
                {
                    case DialogResult.Yes:
                        if (this.SaveDocument(d) == false)
                        {
                            return false;
                        }
                        break;
                    case DialogResult.Cancel:
                        return false;
                }
                d.Dispose();
                return true;
            };

            if (document.TextEditor.IsDirty == false)
            {
                return true;
            }
            if (saveAndClose(document, string.Format(Resources.MSG_CFM_SAVE_CHANGES, document.FileName)) == false)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// ドキュメントの変更内容の保存確認を行い、ドキュメントを閉じます。
        /// </summary>
        /// <param name="document">ドキュメント</param>
        /// <param name="e">イベントの情報</param>
        /// <returns>処理されたかどうかを示す値</returns>
        private bool TrySaveAndCloseOnFormClosing(EditorDocument document, CancelEventArgs e)
        {
            if (this.TrySaveAndCloseOnFormClosing(document) == false)
            {
                e.Cancel = true;
                return false;
            }
            return true;
        }
        #endregion

        #region ダイアログの表示
        /// <summary>
        /// 印刷プレビューを表示します。
        /// </summary>
        private void PrintDocument(bool isPreview)
        {
            int printCharArrayIndex = 0;
            string  printText = this.ActiveDocument.TextEditor.Text;
            Font printFont = this.ActiveDocument.TextEditor.Font;

            Action<PrintPageEventArgs> printPageAction = (e) =>
            {
                // 印刷位置を決定する
                int x = e.MarginBounds.Left;
                int y = e.MarginBounds.Top;

                // 文字を描画する
                while ((printFont.Height + y < e.MarginBounds.Bottom - printFont.Height) &&
                       (printCharArrayIndex < printText.Length))
                {
                    string line = string.Empty;
                    while (true)
                    {
                        // 印刷する文字の有無をチェックする
                        if (printText.Length <= printCharArrayIndex)
                        {
                            printCharArrayIndex++;
                            break;
                        }

                        // 改行をチェックする
                        string printString = printText[printCharArrayIndex].ToString();
                        if (printString == EolCode.LF)
                        {
                            printCharArrayIndex++;
                            break;
                        }

                        // 文字を追加する
                        // 印刷幅を超える場合は折り返す
                        line += printString;
                        if (e.MarginBounds.Width < e.Graphics.MeasureString(line, printFont).Width)
                        {
                            line = line.Substring(0, line.Length - 1);
                            break;
                        }
                        printCharArrayIndex++;
                    }

                    // 文字を描画し出力位置を更新する
                    e.Graphics.DrawString(line, printFont, Brushes.Black, x, y);
                    y += (int)printFont.GetHeight(e.Graphics);
                }

                // 次ページの有無をチェックする
                if (printText.Length <= printCharArrayIndex)
                {
                    printCharArrayIndex = 0;
                    e.HasMorePages = false;
                }
                else
                {
                    e.HasMorePages = true;
                }
            };

            using (PrintDocument document = new PrintDocument())
            {
                document.DocumentName = this.ActiveDocument.FileName;
                document.PrintPage += (sender, e) => printPageAction(e);
                if (isPreview)
                {
                    using (PrintPreviewDialogEx form = new PrintPreviewDialogEx())
                    {
                        form.ShowDialog(this);
                    }
                    //using (PrintPreviewDialog dialog = new PrintPreviewDialog())
                    //{
                    //    dialog.Document = document;
                    //    dialog.ShowIcon = false;
                    //    dialog.ShowDialog(this);
                    //}
                }
                else
                {
                    using (PrintDialog dialog = new PrintDialog())
                    {
                        dialog.Document = document;
                        dialog.AllowSelection = true;
                        dialog.AllowSomePages = true;
                        if (dialog.ShowDialog() == DialogResult.OK)
                        {
                            document.Print();
                        }
                    }
                }
            }
        }

        /// <summary>
        /// コンテンツを表示します。
        /// </summary>
        /// <param name="content">コンテンツ</param>
        /// <param name="initialState">初期状態</param>
        private void ShowContent(ContentBase content, DockState initialState)
        {
            if (content.IsAdded == false)
            {
                this.DockingPanel.AddContent(content, initialState);
            }
            else if (content.IsActiveAutoHide || content.Focused || content.ContainsFocus)
            {
                if (content.IsAnchorDock)
                {
                    content.ToAutoHide();
                }
                else
                {
                    content.ToAnchorDock();
                }
            }
            content.Select();
        }

        /// <summary>
        /// 検索処理を行います。
        /// </summary>
        private void Find()
        {
            if (this.SearchContent.IsAdded &&
                this.SearchContent.BehaviorMode != SearchContentBehaviorKind.Search)
            {
                this.SearchContent.BehaviorMode = SearchContentBehaviorKind.Search;
                this.SearchContent.Select();
            }
            else
            {
                this.ShowContent(this.SearchContent, DockState.DockRightAutoHide);
            }
        }

        /// <summary>
        /// 一つ先の検索対象を探します。
        /// </summary>
        private void FindNext()
        {
            if (this.SearchContent.IsAdded == false)
            {
                this.Find();
            }
            else
            {
                this.SearchContent.FindNext();
            }
        }

        /// <summary>
        /// 一つ前の検索対象を探します。
        /// </summary>
        private void FindPrev()
        {
            if (this.SearchContent.IsAdded == false)
            {
                this.Find();
            }
            else
            {
                this.SearchContent.FindPrev();
            }
        }

        /// <summary>
        /// 置換処理を行います。
        /// </summary>
        private void Replace()
        {
            if (this.SearchContent.IsAdded &&
                this.SearchContent.BehaviorMode != SearchContentBehaviorKind.Replace)
            {
                this.SearchContent.BehaviorMode = SearchContentBehaviorKind.Replace;
                this.SearchContent.Select();
            }
            else
            {
                this.ShowContent(this.SearchContent, DockState.DockRightAutoHide);
            }
        }
        
        /// <summary>
        /// エクスプローラーコンテンツを表示します。
        /// </summary>
        private void ShowExplorerContent()
        {
            this.ShowContent(this.ExplorerContent, DockState.DockLeftAutoHide);
        }

        /// <summary>
        /// 指定行へ移動するダイアログを表示します。
        /// </summary>
        private void ShowGoToLineDialog()
        {
            if (this.ActiveDocument == null)
            {
                return;
            }

            using (GoToLineDialog form = new GoToLineDialog(this.ActiveDocument.TextEditor))
            {
                form.ShowDialog(this);
            }
        }

        /// <summary>
        /// オプションダイアログを表示します。
        /// </summary>
        private void ShowOptionDialog()
        {
            using (OptionDialog form = new OptionDialog())
            {
                switch (form.ShowDialog(this))
                {
                    case DialogResult.OK:
                        this.DockingPanel.GetContents<EditorDocument>().ForEach(d => d.UpdateSettings());
                        break;
                }
            }
        }

        /// <summary>
        /// バージョン情報ダイアログを表示します。
        /// </summary>
        private void ShowInformaitonDialog()
        {
            using (InformaitonDialog form = new InformaitonDialog())
            {
                form.ShowDialog(this);
            }
        }
        #endregion

        #region 表示状態の更新
        /// <summary>
        /// メニューバーの表示状態を切り替えます。
        /// </summary>
        private void SwitchMenuBarVisible() => this.MenuBarVisible = !this.MenuBarVisible;

        /// <summary>
        /// ツールバーの表示状態を切り替えます。
        /// </summary>
        private void SwitchToolBarVisible() => this.ToolBarVisible = !this.ToolBarVisible;

        /// <summary>
        /// ステータスーの表示状態を切り替えます。
        /// </summary>
        private void SwitchStatusBarVisible() => this.StatusBarVisible = !this.StatusBarVisible;

        /// <summary>
        /// 最前面表示を切り替えます。
        /// </summary>
        private void SwitchTopMost() => this.TopMost = !this.TopMost;

        /// <summary>
        /// ファイルの種類を切り替えます。
        /// </summary>
        /// <param name="mode">種類</param>
        private void SwitchLanguageMode(LanguageKind mode)
        {
            if (this.ActiveDocument == null)
            {
                return;
            }
            this.ActiveDocument.TextEditor.LanguageMode = mode;
        }

        /// <summary>
        /// エンコードを切り替えます。
        /// </summary>
        /// <param name="mode">種類</param>
        private void SwitchEncodingMode(EncodingKind mode)
        {
            if (this.ActiveDocument == null)
            {
                return;
            }
            this.ActiveDocument.TextEditor.EncodingMode = mode;
            this.ReloadDocument(mode);
        }

        /// <summary>
        /// 改行コードを切り替えます。
        /// </summary>
        /// <param name="mode">種類</param>
        private void SwitchEolMode(EolKind mode)
        {
            if (this.ActiveDocument == null)
            {
                return;
            }
            this.ActiveDocument.TextEditor.EolMode = mode;
            this.ActiveDocument.TextEditor.Text = ViewUtils.UnifyEndOfLine(this.ActiveDocument.TextEditor.Text, mode);
        }

        /// <summary>
        /// 折り返し方法を切り替えます。
        /// </summary>
        /// <param name="mode">種類</param>
        private void SwitchWordWrapMode(WordWrapKind mode)
        {
            if (this.ActiveDocument == null)
            {
                return;
            }
            this.ActiveDocument.TextEditor.WordWrapMode = mode;
        }

        /// <summary>
        /// 入力方式を切り替えます。
        /// </summary>
        private void SwitchInputMode()
        {
            if (this.ActiveDocument == null)
            {
                return;
            }
            this.ActiveDocument.TextEditor.IsOverwriteMode = !this.ActiveDocument.TextEditor.IsOverwriteMode;
        }

        /// <summary>
        /// コマンドの有効状態を更新します。
        /// </summary>
        /// <param name="mode">種類</param>
        private void UpdateCommandEnabled(UpdateCommandEnabledEventKind mode)
        {
            TextEditor textEditor = this.ActiveDocument?.TextEditor;
            switch (mode)
            {
                case UpdateCommandEnabledEventKind.Load:
                    this.mcmdMenuBar.Checked = this.MenuBarVisible;
                    this.mcmdToolBar.Checked = this.ToolBarVisible;
                    this.mcmdStatusBar.Checked = this.StatusBarVisible;
                    this.mcmdTopMost.Checked = this.TopMost;
                    this.tcmdTopMost.Checked = this.TopMost;
                    break;
                case UpdateCommandEnabledEventKind.MenuBarVisibleChanged:
                    this.mcmdMenuBar.Checked = this.MenuBarVisible;
                    break;
                case UpdateCommandEnabledEventKind.ToolBarVisibleChanged:
                    this.mcmdToolBar.Checked = this.ToolBarVisible;
                    break;
                case UpdateCommandEnabledEventKind.StatusBarVisibleChanged:
                    this.mcmdStatusBar.Checked = this.StatusBarVisible;
                    break;
                case UpdateCommandEnabledEventKind.TopMostChanged:
                    this.mcmdTopMost.Checked = this.TopMost;
                    this.tcmdTopMost.Checked = this.TopMost;
                    break;
                case UpdateCommandEnabledEventKind.CaretMoved:
                    this.mcmdDelete.Enabled = textEditor?.CanDelete ?? false;
                    break;
                case UpdateCommandEnabledEventKind.TextChanged:
                    this.mcmdRedo.Enabled = textEditor?.CanRedo ?? false;
                    this.mcmdUndo.Enabled = textEditor?.CanUndo ?? false;
                    this.tcmdRedo.Enabled = textEditor?.CanRedo ?? false;
                    this.tcmdUndo.Enabled = textEditor?.CanUndo ?? false;
                    break;
                case UpdateCommandEnabledEventKind.Copied:
                    this.mcmdPaste.Enabled = textEditor?.CanPaste ?? false;
                    this.tcmdPaste.Enabled = textEditor?.CanPaste ?? false;
                    break;
                case UpdateCommandEnabledEventKind.Activated:
                case UpdateCommandEnabledEventKind.ActiveDocumentChanged:
                    this.mcmdSave.Enabled = !textEditor?.IsReadOnly ?? false;
                    this.mcmdRedo.Enabled = textEditor?.CanRedo ?? false;
                    this.mcmdUndo.Enabled = textEditor?.CanUndo ?? false;
                    this.mcmdCut.Enabled = textEditor?.CanCut ?? false;
                    this.mcmdCopy.Enabled = textEditor?.CanCopy ?? false;
                    this.mcmdPaste.Enabled = textEditor?.CanPaste ?? false;
                    this.mcmdDelete.Enabled = textEditor?.CanDelete ?? false;
                    this.mcmdSelectAll.Enabled = textEditor != null ? true : false;

                    this.tcmdSave.Enabled = !textEditor?.IsReadOnly ?? false;
                    this.tcmdRedo.Enabled = textEditor?.CanRedo ?? false;
                    this.tcmdUndo.Enabled = textEditor?.CanUndo ?? false;
                    this.tcmdCut.Enabled = textEditor?.CanCut ?? false;
                    this.tcmdCopy.Enabled = textEditor?.CanCopy ?? false;
                    this.tcmdPaste.Enabled = textEditor?.CanPaste ?? false;
                    this.tcmdSelectAll.Enabled = textEditor != null ? true : false;
                    break;
            }
        }

        /// <summary>
        /// システムメニューコマンドの有効状態を更新します。
        /// </summary>
        private void UpdateSystemMenuCommandEnabled()
        {
            this._scmdMenuBar.fState = (uint)(this.MenuBarVisible ? FState.MFS_CHECKED : FState.MFS_ENABLED);
            this._scmdToolBar.fState = (uint)(this.ToolBarVisible ? FState.MFS_CHECKED : FState.MFS_ENABLED);
            this._scmdStatusBar.fState = (uint)(this.StatusBarVisible ? FState.MFS_CHECKED : FState.MFS_ENABLED);

            IntPtr hMenu = User32.GetSystemMenu(this.Handle, false);
            User32.SetMenuItemInfo(hMenu, 6, true, ref this._scmdMenuBar);
            User32.SetMenuItemInfo(hMenu, 7, true, ref this._scmdToolBar);
            User32.SetMenuItemInfo(hMenu, 8, true, ref this._scmdStatusBar);
        }

        /// <summary>
        /// コンテキストメニューコマンドの有効状態を更新します。
        /// </summary>
        private void UpdateContextMenuCommandEnabled()
        {
            this.ccmdMenuBar.Checked = this.MenuBarVisible;
            this.ccmdToolBar.Checked = this.ToolBarVisible;
            this.ccmdStatusBar.Checked = this.StatusBarVisible;
        }

        /// <summary>
        /// メニューコマンドの有効状態を更新します。
        /// </summary>
        /// <param name="mode">種類</param>
        private void UpdateMenuCommandEnabled(DropDownItemHeaderKind mode)
        {
            TextEditor textEditor = this.ActiveDocument?.TextEditor;
            switch (mode)
            {
                case DropDownItemHeaderKind.Encoding:
                    foreach (ToolStripItem item in this.mihEncodingMode.DropDownItems)
                    {
                        if (item is ToolStripMenuItem)
                        {
                            item.Enabled = !textEditor.IsNewFile;
                        }
                    }
                    break;
                case DropDownItemHeaderKind.EolCode:
                    foreach (ToolStripItem item in this.mihEolMode.DropDownItems)
                    {
                        if (item is ToolStripMenuItem)
                        {
                            item.Enabled = !textEditor.IsReadOnly;
                        }
                    }
                    break;
            }
        }

        /// <summary>
        /// ステータスコマンドの有効状態を更新します。
        /// </summary>
        /// <param name="mode">種類</param>
        private void UpdateStatusCommandEnabled(DropDownItemHeaderKind mode)
        {
            TextEditor textEditor = this.ActiveDocument?.TextEditor;
            switch (mode)
            {
                case DropDownItemHeaderKind.Encoding:
                    foreach (ToolStripItem item in this.stsEncodingMode.DropDownItems)
                    {
                        if (item is ToolStripMenuItem)
                        {
                            item.Enabled = !textEditor.IsNewFile;
                        }
                    }
                    break;
                case DropDownItemHeaderKind.EolCode:
                    foreach (ToolStripItem item in this.stsEolMode.DropDownItems)
                    {
                        if (item is ToolStripMenuItem)
                        {
                            item.Enabled = !textEditor.IsReadOnly;
                        }
                    }
                    break;
            }
        }
        #endregion

        #region 表示内容の更新
        /// <summary>
        /// ウィンドウのタイトルを更新します。
        /// </summary>
        private void UpdateWindowTitle()
        {
            if (this.ActiveDocument == null)
            {
                this.Text = AssemblyEntity.Title;
                return;
            }
            this.Text = $"{this.ActiveDocument.TabText} - {AssemblyEntity.Title}";
        }

        /// <summary>
        /// メッセージのステータス表示を更新します。
        /// </summary>
        private void UpdateMessageStatus()
        {
            if (this.ActiveDocument == null)
            {
                this.stsMessage.Text = string.Empty;
                return;
            }
            int lenght = this.ActiveDocument.TextEditor.SelectedLength;
            this.stsMessage.Text = 0 < lenght ? $"{lenght} chars selected." : string.Empty;
        }

        /// <summary>
        /// インデックスのステータス表示を更新します。
        /// </summary>
        private void UpdateCaretIndexStatus()
        {
            if (this.ActiveDocument == null)
            {
                this.stsLine.Text = string.Empty;
                this.stsColumn.Text = string.Empty;
                this.stsChar.Text = string.Empty;
                return;
            }
            this.stsLine.Text = $"Ln: {this.ActiveDocument.TextEditor.LineIndex + 1}";
            this.stsColumn.Text = $"Col: {this.ActiveDocument.TextEditor.ColumnIndex + 1}";
            this.stsChar.Text = $"Ch: {this.ActiveDocument.TextEditor.CharIndex + 1}";
        }

        /// <summary>
        /// 言語の種類のステータス表示を更新します。
        /// </summary>
        private void UpdateLanguageModeStatus()
        {
            this.ClearDropDownChecked(this.mihLanguageMode);
            this.ClearDropDownChecked(this.stsLanguageMode);
            if (this.ActiveDocument == null)
            {
                return;
            }
            this.SelectDropDownItem(this.mihLanguageMode, this.ActiveDocument.TextEditor.LanguageMode);
            this.SelectDropDownItem(this.stsLanguageMode, this.ActiveDocument.TextEditor.LanguageMode);
        }

        /// <summary>
        /// エンコードの種類のステータス表示を更新します。
        /// </summary>
        private void UpdateEncodingModeStatus()
        {
            this.ClearDropDownChecked(this.mihEncodingMode);
            this.ClearDropDownChecked(this.stsEncodingMode);
            if (this.ActiveDocument == null)
            {
                return;
            }
            this.SelectDropDownItem(this.mihEncodingMode, this.ActiveDocument.TextEditor.EncodingMode);
            this.SelectDropDownItem(this.stsEncodingMode, this.ActiveDocument.TextEditor.EncodingMode);
        }

        /// <summary>
        /// 改行コードの種類のステータス表示を更新します。
        /// </summary>
        private void UpdateEolModeStatus()
        {
            this.ClearDropDownChecked(this.mihEolMode);
            this.ClearDropDownChecked(this.stsEolMode);
            if (this.ActiveDocument == null)
            {
                return;
            }
            this.SelectDropDownItem(this.mihEolMode, this.ActiveDocument.TextEditor.EolMode);
            this.SelectDropDownItem(this.stsEolMode, this.ActiveDocument.TextEditor.EolMode);
        }

        /// <summary>
        /// 折り返し方法の種類のステータス表示を更新します。
        /// </summary>
        private void UpdateWordWrapModeStatus()
        {
            this.ClearDropDownChecked(this.mihWordWrapMode);
            if (this.ActiveDocument == null)
            {
                return;
            }
            this.SelectDropDownItem(this.mihWordWrapMode, this.ActiveDocument.TextEditor.WordWrapMode);
        }

        /// <summary>
        /// 入力方法のステータス表示を更新します。
        /// </summary>
        private void UpdateOverwriteModeStatus()
        {
            if (this.ActiveDocument == null)
            {
                this.stsInputMode.Text = string.Empty;
                return;
            }
            this.stsInputMode.Text = this.ActiveDocument.TextEditor.IsOverwriteMode ? "OVER" : "INS";
        }

        /// <summary>
        /// ドロップダウンの選択状態をクリアします。
        /// </summary>
        /// <param name="control">ドロップダウンコントロール</param>
        private void ClearDropDownChecked(ToolStripDropDownItem control)
        {
            foreach (ToolStripMenuItem item in control.DropDownItems)
            {
                item.Checked = false;
            }
            if (control is ToolStripDropDownButton)
            {
                control.Text = string.Empty;
            }
        }

        /// <summary>
        /// 指定したドロップダウンの項目を選択状態にします。
        /// </summary>
        /// <param name="control">ドロップダウンコントロール</param>
        /// <param name="tag">項目のタグ</param>
        private void SelectDropDownItem(ToolStripDropDownItem control, Enum tag)
        {
            foreach (ToolStripMenuItem item in control.DropDownItems)
            {
                if (tag.Equals(item.Tag))
                {
                    if (control is ToolStripDropDownButton)
                    {
                        control.Text = ViewUtils.GetTextWithoutAccessKey(item.Text);
                    }
                    item.Checked = true;
                    return;
                }
            }
        }

        /// <summary>
        /// ステータスラベルのハイライト状態を切り替えます。
        /// </summary>
        /// <param name="label">ラベル</param>
        private void SwitchStatusLabelHighlight(ToolStripItem label)
        {
            ToolStrip parent = label.GetCurrentParent();
            label.BackColor = label.BackColor == parent.BackColor ? ControlPaint.Light(parent.BackColor) : parent.BackColor;
        }
        #endregion

        #region 外部データの処理
        /// <summary>
        /// ドロップされたデータによって操作の効果を切り分けます。
        /// </summary>
        /// <param name="data">ドロップされたデータ</param>
        /// <returns>ドラッグドロップ操作の効果</returns>
        private DragDropEffects GetDragDropEffects(IDataObject data)
        {
            return data.GetDataPresent(DataFormats.FileDrop) ? DragDropEffects.All : DragDropEffects.None;
        }

        /// <summary>
        /// ドロップされたデータを処理します。
        /// </summary>
        /// <param name="data">ドロップされたデータ</param>
        private void ReceveDropData(IDataObject data)
        {
            (data.GetData(DataFormats.FileDrop) as string[])?.ForEach(path => this.AddDocument(path, false));
        }

        /// <summary>
        /// 受信した Windows メッセージを処理します。
        /// </summary>
        /// <param name="m">Windows メッセージ</param>
        private void ReceveMessage(Message m)
        {
            COPYDATASTRUCT lParam = (COPYDATASTRUCT)m.GetLParam(typeof(COPYDATASTRUCT));
            if (string.IsNullOrEmpty(lParam.lpData) == false)
            {
                lParam.lpData.Split(SpecialChar.SendDataSeparator).ForEach(p => this.AddDocument(p, false));
            }
        }
        #endregion

        #region イベント処理
        /// <summary>
        /// <see cref="_settingsAppliedOnLoad"/> イベントを発生させます。
        /// </summary>
        /// <param name="e">イベントの情報</param>
        private void OnSettingsAppliedOnLoad(EventArgs e)
        {
            this._settingsAppliedOnLoad?.Invoke(this, e);
        }

        /// <summary>
        /// <see cref="Form.ProcesscmddKey"/> を呼び出します。
        /// </summary>
        /// <param name="msg">メッセージ</param>
        /// <param name="keyData">キーデータ</param>
        /// <returns>コマンドキーが処理されたかどうかを示す値</returns>
        public override bool RaiseProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (this._menuBar.RaiseProcessCmdKey(ref msg, keyData))
            {
                return true;
            }
            return base.RaiseProcessCmdKey(ref msg, keyData);
        }

        /// <summary>
        /// コマンドキーを処理します。
        /// </summary>
        /// <param name="msg">メッセージ</param>
        /// <param name="keyData">キーデータ</param>
        /// <returns>コマンドキーが処理されたかどうかを示す値</returns>
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (this.ProcessSpecialShortCutKey(keyData))
            {
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        /// <summary>
        /// 特殊なショートカットキーを処理します。
        /// </summary>
        /// <param name="keyData">キーデータ</param>
        /// <returns>コマンドキーが処理されたかどうかを示す値</returns>
        private bool ProcessSpecialShortCutKey(Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Control | Keys.Oem4:
                case Keys.Control | Keys.OemCloseBrackets:
                    this.mcmdGoToMatchedBracket.PerformClick();
                    return true;
            }
            return false;
        }
        #endregion
    }
}
