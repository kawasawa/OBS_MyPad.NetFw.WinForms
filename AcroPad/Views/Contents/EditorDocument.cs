using AcroPad.Models;
using AcroPad.Models.Associations;
using AcroPad.Views.Controls;
using AcroPad.Views.Forms;
using System;
using System.ComponentModel;
using System.IO;

namespace AcroPad.Views.Contents
{
    /// <summary>
    /// テキストを編集するためのドキュメントを表します。
    /// </summary>
    public partial class EditorDocument : DocumentBase
    {
        /// <summary>
        /// 読み取り専用を示す記号を取得します。
        /// </summary>
        private const string READ_ONLY_MARK = "[R/O]";

        /// <summary>
        /// 編集済みを示す記号を取得します。
        /// </summary>
        private const string DIRTY_MARK = "*";

        /// <summary>
        /// メインフォームを取得します。
        /// </summary>
        private MainForm MainForm => this.DockPanel.FindForm() as MainForm;

        /// <summary>
        /// テキストエディターを取得します。
        /// </summary>
        public TextEditor TextEditor => this.textEditor;

        /// <summary>
        /// シーケンス番号を取得します。
        /// </summary>
        public uint Sequence { get; }

        /// <summary>
        /// 編集中のファイル名を取得します。
        /// </summary>
        public string FileName
        {
            get
            {
                if (this.TextEditor.IsNewFile)
                {
                    Association assoc = LanguageContainer.Instance.GetAssociation(this.TextEditor.LanguageMode);
                    string name = $"{LanguageContainer.Instance.InitialName}({this.Sequence})";
                    if (0 < assoc.Extensions?.Length)
                    {
                        name += assoc.Extensions[0];
                    }
                    return name;
                }
                return Path.GetFileName(this.TextEditor.FilePath);
            }
        }

        /// <summary>
        /// インスタンスを初期化します。
        /// </summary>
        private EditorDocument()
        {
            this.InitializeComponent();
            this.InitializeItems();
            this.AddEventHandler();
            this.ApplySettings();
        }

        /// <summary>
        /// インスタンスを初期化します。
        /// </summary>
        /// <param name="sequence">シーケンス番号</param>
        public EditorDocument(uint sequence) : this()
        {
            this.Sequence = sequence;
            this.UpdateTabText();
        }

        /// <summary>
        /// インスタンスを初期化します。
        /// </summary>
        /// <param name="stream">ストリーム</param>
        /// <param name="mode">エンコードの種類</param>
        public EditorDocument(FileStream stream, EncodingKind mode) : this()
        {
            this.TextEditor.Load(stream, mode);
            Association info = LanguageContainer.Instance.GetAssociation(Path.GetExtension(this.FileName));
            if (info != null)
            {
                this.TextEditor.LanguageMode = info.LanguageMode;
            }
        }

        /// <summary>
        /// 特定のコントロールを初期化します。
        /// </summary>
        private void InitializeItems()
        {
            this.SetFormIcon();
            this.SetItemImage();
            this.TabPageContextMenuStrip = this.cmsTabPage;
            this.TextEditor.ContextMenuStrip = this.cmsTextEditor;
        }

        /// <summary>
        /// イベントハンドラを追加します。
        /// </summary>
        private void AddEventHandler()
        {
            this.Load += this.Form_Load;
            this.TextEditor.Loaded += this.UpdateDisplayInfoCommand_Executed;
            this.TextEditor.Saved += this.UpdateDisplayInfoCommand_Executed;
            this.cmsTextEditor.Opening += this.UpdateCommandEnabled_Executed;
            this.cmdClose.Click += this.CloseCommand_Executed;
            this.cmdCloseAllButThis.Click += this.CloseAllButThisCommand_Executed;
            this.cmdCloseAll.Click += this.CloseAllCommand_Executed;
            this.cmdUndo.Click += this.UndoCommand_Executed;
            this.cmdRedo.Click += this.RedoCommand_Executed;
            this.cmdCut.Click += this.CutCommand_Executed;
            this.cmdCopy.Click += this.CopyCommand_Executed;
            this.cmdPaste.Click += this.PasteCommand_Executed;
            this.cmdDelete.Click += this.DeleteCommand_Executed;
            this.cmdSelectAll.Click += this.SelectAllCommand_Executed;
        }

        /// <summary>
        /// イベントハンドラを追加します。
        /// </summary>
        private void AddEventHandlerOnFormLoad()
        {
            this.TextEditor.DirtyStateChanged += this.UpdateTabTextCommand_Executed;
            this.TextEditor.LanguageModeChanged += this.UpdateTabTextCommand_Executed;
        }

        /// <summary>
        /// 設定情報をコントロールに反映します。
        /// </summary>
        public void ApplySettings() => SettingContainer.Instance.TextEditorSetting.SetValue(this.TextEditor);

        /// <summary>
        /// 設定情報をコントロールに反映します。
        /// </summary>
        public void UpdateSettings() => SettingContainer.Instance.TextEditorSetting.UpdateValue(this.TextEditor);

        /// <summary>
        /// タブに表示するテキストを更新します。
        /// </summary>
        private void UpdateTabText()
        {
            string text = this.FileName;
            if (this.TextEditor.IsReadOnly)
            {
                text = $"{READ_ONLY_MARK} {text}";
            }
            if (this.TextEditor.IsDirty)
            {
                text = $"{text}{DIRTY_MARK}";
            }
            this.TabText = text;
        }

        /// <summary>
        /// ツールチップを更新します。
        /// </summary>
        private void UpdateToolTip()
        {
            this.ToolTipText = this.TextEditor.FilePath;
        }

        /// <summary>
        /// コマンドの有効状態を更新します。
        /// </summary>
        private void UpdateCommandEnabled()
        {
            this.cmdRedo.Enabled = this.TextEditor.CanRedo;
            this.cmdUndo.Enabled = this.TextEditor.CanUndo;
            this.cmdCut.Enabled = this.TextEditor.CanCut;
            this.cmdCopy.Enabled = this.TextEditor.CanCopy;
            this.cmdPaste.Enabled = this.TextEditor.CanPaste;
            this.cmdDelete.Enabled = this.TextEditor.CanDelete;
        }

        private void Form_Load(object sender, EventArgs e)
        {
            this.AddEventHandlerOnFormLoad();
        }

        private void UpdateDisplayInfoCommand_Executed(object sender, EventArgs e)
        {
            this.UpdateToolTip();
            this.UpdateTabText();
        }

        private void UpdateTabTextCommand_Executed(object sender, EventArgs e)
        {
            this.UpdateTabText();
        }

        private void UpdateCommandEnabled_Executed(object sender, CancelEventArgs e)
        {
            this.UpdateCommandEnabled();
        }

        private void CloseCommand_Executed(object sender, EventArgs e)
        {
            this.MainForm.InvokeCloseDocument();
        }

        private void CloseAllButThisCommand_Executed(object sender, EventArgs e)
        {
            this.MainForm.InvokeCloseAllButThisDocument();
        }

        private void CloseAllCommand_Executed(object sender, EventArgs e)
        {
            this.MainForm.InvokeCloseAllDocument();
        }

        private void UndoCommand_Executed(object sender, EventArgs e)
        {
            this.TextEditor.Undo();
        }

        private void RedoCommand_Executed(object sender, EventArgs e)
        {
            this.TextEditor.Redo();
        }

        private void CutCommand_Executed(object sender, EventArgs e)
        {
            this.TextEditor.Cut();
        }

        private void CopyCommand_Executed(object sender, EventArgs e)
        {
            this.TextEditor.Copy();
        }

        private void PasteCommand_Executed(object sender, EventArgs e)
        {
            this.TextEditor.Paste();
        }

        private void DeleteCommand_Executed(object sender, EventArgs e)
        {
            this.TextEditor.Delete();
        }

        private void SelectAllCommand_Executed(object sender, EventArgs e)
        {
            this.TextEditor.SelectAll();
        }
    }
}
