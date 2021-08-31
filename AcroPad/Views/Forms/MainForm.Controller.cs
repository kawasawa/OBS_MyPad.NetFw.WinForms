using AcroBat;
using AcroBat.WindowsAPI;
using AcroPad.Views.Contents;
using AcroPad.Views.Controls;
using Sgry.Azuki;
using System;
using System.ComponentModel;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace AcroPad.Views.Forms
{
    //
    // メインフォームのイベントハンドラを実装します。
    //
    public partial class MainForm
    {
        /// <summary>
        /// コマンドの有効状態を更新するイベントの種類を表します。
        /// </summary>
        private enum UpdateCommandEnabledEventKind
        {
            Load,
            Activated,
            ActiveDocumentChanged,
            MenuBarVisibleChanged,
            ToolBarVisibleChanged,
            StatusBarVisibleChanged,
            TopMostChanged,
            CaretMoved,
            TextChanged,
            Copied,
        }

        /// <summary>
        /// ドロップダウンアイテムのヘッダの種類を表します。
        /// </summary>
        private enum DropDownItemHeaderKind
        {
            Encoding,
            EolCode,
        }

        /// <summary>
        /// イベントハンドラを追加します。
        /// </summary>
        private void AddEventHandler()
        {
            this.Load += this.Form_Load;
            this.FormClosed += this.Form_FormClosed;
            this.Activated += this.Form_Activated;
            this.DragEnter += this.Form_DragEnter;
            this.DragDrop += this.Form_DragDrop;
            this.WindowsMessageReceived += this.Form_WindowsMessageReceived;

            this.DockingPanel.ActiveDocumentChanged += this.DockingPanel_ActiveDocumentChanged;
            this.DockingPanel.ContentRemoved += this.DockingPanel_ContentRemoved;

            this.SystemMenuOpened += this.Form_SystemMenuOpened;
            this._contextMenu.Opening += this.ContextMenu_Opening;
            this.mihEncodingMode.DropDownOpening += this.MenuHeader_DropDownOpening;
            this.mihEolMode.DropDownOpening += this.MenuHeader_DropDownOpening;
            this.stsEncodingMode.DropDownOpening += this.StatusHeader_DropDownOpening;
            this.stsEolMode.DropDownOpening += this.StatusHeader_DropDownOpening;

            this.stsLine.MouseEnter += this.CaretIndexStatusItem_MouseEnter;
            this.stsColumn.MouseEnter += this.CaretIndexStatusItem_MouseEnter;
            this.stsChar.MouseEnter += this.CaretIndexStatusItem_MouseEnter;
            this.stsLanguageMode.MouseEnter += this.StatusItem_MouseEnter;
            this.stsEncodingMode.MouseEnter += this.StatusItem_MouseEnter;
            this.stsEolMode.MouseEnter += this.StatusItem_MouseEnter;
            this.stsInputMode.MouseEnter += this.StatusItem_MouseEnter;

            this.stsLine.MouseLeave += this.CaretIndexStatusItem_MouseLeave;
            this.stsColumn.MouseLeave += this.CaretIndexStatusItem_MouseLeave;
            this.stsChar.MouseLeave += this.CaretIndexStatusItem_MouseLeave;
            this.stsLanguageMode.MouseLeave += this.StatusItem_MouseLeave;
            this.stsEncodingMode.MouseLeave += this.StatusItem_MouseLeave;
            this.stsEolMode.MouseLeave += this.StatusItem_MouseLeave;
            this.stsInputMode.MouseLeave += this.StatusItem_MouseLeave;

            this.ccmdMenuBar.Click += this.MenuBarCommand_Executed;
            this.ccmdToolBar.Click += this.ToolBarCommand_Executed;
            this.ccmdStatusBar.Click += this.StatusBarCommand_Executed;

            this.mcmdAdd.Click += this.AddCommand_Executed;
            this.mcmdOpenFolder.Click += this.OpenFolderCommand_Executed;
            this.mcmdSave.Click += this.SaveCommand_Executed;
            this.mcmdSaveAs.Click += this.SaveAsCommand_Executed;
            this.mcmdSaveAll.Click += this.SaveAllCommand_Executed;
            this.mcmdClose.Click += this.CloseCommand_Executed;
            this.mcmdCloseAllButThis.Click += this.CloseAllButThisCommand_Executed;
            this.mcmdCloseAll.Click += this.CloseAllCommand_Executed;
            this.mcmdPrintOut.Click += this.PrintOutCommand_Executed;
            this.mcmdPrintPreview.Click += this.PrintPreviewCommand_Executed;
            this.mcmdExit.Click += this.ExitCommand_Executed;
            this.mcmdUndo.Click += this.UndoCommand_Executed;
            this.mcmdRedo.Click += this.RedoCommand_Executed;
            this.mcmdCut.Click += this.CutCommand_Executed;
            this.mcmdCopy.Click += this.CopyCommand_Executed;
            this.mcmdPaste.Click += this.PasteCommand_Executed;
            this.mcmdDelete.Click += this.DeleteCommand_Executed;
            this.mcmdSelectAll.Click += this.SelectAllCommand_Executed;
            this.mcmdFind.Click += this.FindCommand_Executed;
            this.mcmdFindNext.Click += this.FindNextCommand_Executed;
            this.mcmdFindPrev.Click += this.FindPrevCommand_Executed;
            this.mcmdReplace.Click += this.ReplaceCommand_Executed;
            this.mcmdGoToLine.Click += this.GoToLineCodmand_Executed;
            this.mcmdGoToMatchedBracket.Click += this.GoToMatchedBracketCommand_Executed;
            this.mcmdMenuBar.Click += this.MenuBarCommand_Executed;
            this.mcmdToolBar.Click += this.ToolBarCommand_Executed;
            this.mcmdStatusBar.Click += this.StatusBarCommand_Executed;
            this.mcmdTopMost.Click += this.TopMostCommand_Executed;
            this.mcmdExplorer.Click += this.ExplorerCommand_Executed;
            this.mcmdActivateNext.Click += this.ActivateNextCommand_Executed;
            this.mcmdActivatePrev.Click += this.ActivatePreviousCommand_Executed;
            this.mcmdZoomIn.Click += this.ZoomInCommand_Executed;
            this.mcmdZoomOut.Click += this.ZoomOutCommand_Executed;
            this.mcmdOption.Click += this.OptionCommand_Executed;
            this.mcmdInformation.Click += this.InformationCommand_Executed;

            this.tcmdAdd.Click += this.AddCommand_Executed;
            this.tcmdOpenFolder.Click += this.OpenFolderCommand_Executed;
            this.tcmdSave.Click += this.SaveCommand_Executed;
            this.tcmdSaveAll.Click += this.SaveAllCommand_Executed;
            this.tcmdUndo.Click += this.UndoCommand_Executed;
            this.tcmdRedo.Click += this.RedoCommand_Executed;
            this.tcmdCut.Click += this.CutCommand_Executed;
            this.tcmdCopy.Click += this.CopyCommand_Executed;
            this.tcmdPaste.Click += this.PasteCommand_Executed;
            this.tcmdSelectAll.Click += this.SelectAllCommand_Executed;
            this.tcmdFind.Click += this.FindCommand_Executed;
            this.tcmdReplace.Click += this.ReplaceCommand_Executed;
            this.tcmdZoomIn.Click += this.ZoomInCommand_Executed;
            this.tcmdZoomOut.Click += this.ZoomOutCommand_Executed;
            this.tcmdTopMost.Click += this.TopMostCommand_Executed;

            this.stsLine.Click += this.GoToLineCodmand_Executed;
            this.stsColumn.Click += this.GoToLineCodmand_Executed;
            this.stsChar.Click += this.GoToLineCodmand_Executed;
            this.stsInputMode.Click += this.SwitchInputModeCommand_Executed;
        }

        /// <summary>
        /// イベントハンドラを追加します。
        /// </summary>
        /// <param name="document">ドキュメント</param>
        private void AddEventHandler(EditorDocument document)
        {
            this.AddEventHandler(document.TextEditor);
            document.TabTextChanged += this.Document_TabTextChanged;
            document.FormClosing += this.Document_FormClosing;
        }

        /// <summary>
        /// イベントハンドラを追加します。
        /// </summary>
        /// <param name="textEditor">テキストエディター</param>
        private void AddEventHandler(TextEditor textEditor)
        {
            textEditor.CaretMoved += this.TextEditor_CaretMoved;
            textEditor.SelectionChanged += this.TextEditor_SelectionChanged;
            textEditor.TextChanged += this.TextEditor_TextChanged;
            textEditor.Copied += this.TextEditor_Copied;
            textEditor.LanguageModeChanged += this.TextEditor_LanguageModeChanged;
            textEditor.EncodingModeChanged += this.TextEditor_EncodingModeChanged;
            textEditor.EolModeChanged += this.TextEditor_EolModeChanged;
            textEditor.OverwriteModeChanged += this.TextEditor_OverwriteModeChanged;
        }

        private void Form_Load(object sender, EventArgs e)
        {
            this.ApplySettings();
            this.OnSettingsAppliedOnLoad(e);
            this.UpdateCommandEnabled(UpdateCommandEnabledEventKind.Load);
            if (this.DockingPanel.DocumentsCount == 0)
            {
                this.AddDocument();
            }
        }

        private void Form_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.CloseAllDocument();
            this.SaveSettings();
        }

        private void Form_Activated(object sender, EventArgs e)
        {
            this.UpdateCommandEnabled(UpdateCommandEnabledEventKind.Activated);
        }

        private void Form_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = this.GetDragDropEffects(e.Data);
        }

        private void Form_DragDrop(object sender, DragEventArgs e)
        {
            this.ReceveDropData(e.Data);
            this.Activate();
        }

        private void Form_WindowsMessageReceived(object sender, WindowsAPIEventArgs e)
        {
            this.ReceveMessage(e.Message);
        }

        private void DockingPanel_ActiveDocumentChanged(object sender, EventArgs e)
        {
            this.UpdateWindowTitle();
            this.UpdateCommandEnabled(UpdateCommandEnabledEventKind.ActiveDocumentChanged);
            this.UpdateMessageStatus();
            this.UpdateCaretIndexStatus();
            this.UpdateLanguageModeStatus();
            this.UpdateEncodingModeStatus();
            this.UpdateEolModeStatus();
            this.UpdateWordWrapModeStatus();
            this.UpdateOverwriteModeStatus();
        }

        private void DockingPanel_ContentRemoved(object sender, DockContentEventArgs e)
        {
            if (this.IsFormClosed)
            {
                return;
            }

            if (this.DockingPanel.DocumentsCount == 0)
            {
                this.AddDocument();
            }
        }

        private void Form_SystemMenuOpened(object sender, WindowsAPIEventArgs e)
        {
            this.UpdateSystemMenuCommandEnabled();
        }

        private void ContextMenu_Opening(object sender, CancelEventArgs e)
        {
            this.UpdateContextMenuCommandEnabled();
        }

        private void MenuHeader_DropDownOpening(object sender, EventArgs e)
        {
            if (sender.Equals(this.mihEncodingMode))
            {
                this.UpdateMenuCommandEnabled(DropDownItemHeaderKind.Encoding);
            }
            else if (sender.Equals(this.mihEolMode))
            {
                this.UpdateMenuCommandEnabled(DropDownItemHeaderKind.EolCode);
            }
        }

        private void StatusHeader_DropDownOpening(object sender, EventArgs e)
        {
            if (sender.Equals(this.stsEncodingMode))
            {
                this.UpdateStatusCommandEnabled(DropDownItemHeaderKind.Encoding);
            }
            else if (sender.Equals(this.stsEolMode))
            {
                this.UpdateStatusCommandEnabled(DropDownItemHeaderKind.EolCode);
            }
        }

        private void StatusItem_MouseEnter(object sender, EventArgs e)
        {
            this.SwitchStatusLabelHighlight((ToolStripItem)sender);
        }

        private void StatusItem_MouseLeave(object sender, EventArgs e)
        {
            this.SwitchStatusLabelHighlight((ToolStripItem)sender);
        }

        private void CaretIndexStatusItem_MouseEnter(object sender, EventArgs e)
        {
            this.SwitchStatusLabelHighlight(this.stsLine);
            this.SwitchStatusLabelHighlight(this.stsColumn);
            this.SwitchStatusLabelHighlight(this.stsChar);
        }

        private void CaretIndexStatusItem_MouseLeave(object sender, EventArgs e)
        {
            this.SwitchStatusLabelHighlight(this.stsLine);
            this.SwitchStatusLabelHighlight(this.stsColumn);
            this.SwitchStatusLabelHighlight(this.stsChar);
        }

        private void MenuBarCommand_Executed(object sender, EventArgs e)
        {
            this.SwitchMenuBarVisible();
            this.UpdateCommandEnabled(UpdateCommandEnabledEventKind.MenuBarVisibleChanged);
        }

        private void ToolBarCommand_Executed(object sender, EventArgs e)
        {
            this.SwitchToolBarVisible();
            this.UpdateCommandEnabled(UpdateCommandEnabledEventKind.ToolBarVisibleChanged);
        }

        private void StatusBarCommand_Executed(object sender, EventArgs e)
        {
            this.SwitchStatusBarVisible();
            this.UpdateCommandEnabled(UpdateCommandEnabledEventKind.StatusBarVisibleChanged);
        }

        private void TopMostCommand_Executed(object sender, EventArgs e)
        {
            this.SwitchTopMost();
            this.UpdateCommandEnabled(UpdateCommandEnabledEventKind.TopMostChanged);
        }

        private void AddCommand_Executed(object sender, EventArgs e)
        {
            this.AddDocument();
        }

        private void OpenFolderCommand_Executed(object sender, EventArgs e)
        {
            this.OpenDocument();
        }

        private void SaveCommand_Executed(object sender, EventArgs e)
        {
            this.SaveDocument();
        }

        private void SaveAsCommand_Executed(object sender, EventArgs e)
        {
            this.SaveAsDocument();
        }

        private void SaveAllCommand_Executed(object sender, EventArgs e)
        {
            this.SaveAllDocument();
        }

        private void CloseCommand_Executed(object sender, EventArgs e)
        {
            this.CloseDocument();
        }

        private void CloseAllButThisCommand_Executed(object sender, EventArgs e)
        {
            this.CloseAllButThisDocument();
        }

        private void CloseAllCommand_Executed(object sender, EventArgs e)
        {
            this.CloseAllDocument();
        }

        private void PrintOutCommand_Executed(object sender, EventArgs e)
        {
            this.PrintDocument(false);
        }

        private void PrintPreviewCommand_Executed(object sender, EventArgs e)
        {
            this.PrintDocument(true);
        }

        private void ExitCommand_Executed(object sender, EventArgs e)
        {
            this.Close();
        }

        private void UndoCommand_Executed(object sender, EventArgs e)
        {
            this.ActiveDocument?.TextEditor.Undo();
        }

        private void RedoCommand_Executed(object sender, EventArgs e)
        {
            this.ActiveDocument?.TextEditor.Redo();
        }

        private void CutCommand_Executed(object sender, EventArgs e)
        {
            this.ActiveDocument?.TextEditor.Cut();
        }

        private void CopyCommand_Executed(object sender, EventArgs e)
        {
            this.ActiveDocument?.TextEditor.Copy();
        }

        private void PasteCommand_Executed(object sender, EventArgs e)
        {
            this.ActiveDocument?.TextEditor.Paste();
        }

        private void DeleteCommand_Executed(object sender, EventArgs e)
        {
            this.ActiveDocument?.TextEditor.Delete();
        }

        private void SelectAllCommand_Executed(object sender, EventArgs e)
        {
            this.ActiveDocument?.TextEditor.SelectAll();
        }

        private void FindCommand_Executed(object sender, EventArgs e)
        {
            this.Find();
        }

        private void FindNextCommand_Executed(object sender, EventArgs e)
        {
            this.FindNext();
        }

        private void FindPrevCommand_Executed(object sender, EventArgs e)
        {
            this.FindPrev();
        }

        private void ReplaceCommand_Executed(object sender, EventArgs e)
        {
            this.Replace();
        }

        private void GoToLineCodmand_Executed(object sender, EventArgs e)
        {
            this.ShowGoToLineDialog();
        }

        private void GoToMatchedBracketCommand_Executed(object sender, EventArgs e)
        {
            this.ActiveDocument?.TextEditor.GoToMatchedBracket();
        }

        private void ExplorerCommand_Executed(object sender, EventArgs e)
        {
            this.ShowExplorerContent();
        }

        private void ActivateNextCommand_Executed(object sender, EventArgs e)
        {
            this.DockingPanel.ActivateNextContent<EditorDocument>();
        }

        private void ActivatePreviousCommand_Executed(object sender, EventArgs e)
        {
            this.DockingPanel.ActivatePreviousContent<EditorDocument>();
        }

        private void ZoomInCommand_Executed(object sender, EventArgs e)
        {
            this.ActiveDocument?.TextEditor.ZoomIn();
        }

        private void ZoomOutCommand_Executed(object sender, EventArgs e)
        {
            this.ActiveDocument?.TextEditor.ZoomOut();
        }

        private void OptionCommand_Executed(object sender, EventArgs e)
        {
            this.ShowOptionDialog();
        }

        private void InformationCommand_Executed(object sender, EventArgs e)
        {
            this.ShowInformaitonDialog();
        }

        private void SwitchLanguageModeCommand_Executed(object sender, EventArgs e)
        {
            this.SwitchLanguageMode((LanguageKind)((ToolStripMenuItem)sender).Tag);
            this.UpdateLanguageModeStatus();
        }

        private void SwitchEncodingModeCommand_Executed(object sender, EventArgs e)
        {
            if (this.ReloadDocument((EncodingKind)((ToolStripMenuItem)sender).Tag))
            {
                this.UpdateEncodingModeStatus();
            }
        }

        private void SwitchEolModeCommand_Executed(object sender, EventArgs e)
        {
            this.SwitchEolMode((EolKind)((ToolStripMenuItem)sender).Tag);
            this.UpdateEolModeStatus();
        }

        private void SwitchWordWrapModeCommand_Executed(object sender, EventArgs e)
        {
            this.SwitchWordWrapMode((WordWrapKind)((ToolStripMenuItem)sender).Tag);
            this.UpdateWordWrapModeStatus();
        }

        private void SwitchInputModeCommand_Executed(object sender, EventArgs e)
        {
            this.SwitchInputMode();
            this.UpdateOverwriteModeStatus();
        }

        private void TextEditor_CaretMoved(object sender, EventArgs e)
        {
            this.UpdateCommandEnabled(UpdateCommandEnabledEventKind.CaretMoved);
            this.UpdateCaretIndexStatus();
        }

        private void TextEditor_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.UpdateMessageStatus();
        }

        private void TextEditor_TextChanged(object sender, EventArgs e)
        {
            this.UpdateCommandEnabled(UpdateCommandEnabledEventKind.TextChanged);
        }

        private void TextEditor_Copied(object sender, EventArgs e)
        {
            this.UpdateCommandEnabled(UpdateCommandEnabledEventKind.Copied);
        }

        private void TextEditor_LanguageModeChanged(object sender, EventArgs e)
        {
            this.UpdateLanguageModeStatus();
        }

        private void TextEditor_EncodingModeChanged(object sender, EventArgs e)
        {
            this.UpdateEncodingModeStatus();
        }

        private void TextEditor_EolModeChanged(object sender, EventArgs e)
        {
            this.UpdateEolModeStatus();
        }

        private void TextEditor_OverwriteModeChanged(object sender, EventArgs e)
        {
            this.UpdateOverwriteModeStatus();
        }

        private void Document_TabTextChanged(object sender, EventArgs e)
        {
            this.UpdateWindowTitle();
        }

        private void Document_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.TrySaveAndCloseOnFormClosing((EditorDocument)sender, e);
        }
    }
}
