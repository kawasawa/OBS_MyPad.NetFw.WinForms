using Microsoft.WindowsAPICodePack.Dialogs;
using Microsoft.WindowsAPICodePack.Dialogs.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace AcroBat.Views
{
    /// <summary>
    /// ダイアログを表示する処理を提供します。
    /// </summary>
    public static class DialogUtils
    {
        /// <summary>
        /// 「ファイルを開く」ダイアログを表示します。
        /// </summary>
        /// <param name="arg">パラメータ</param>
        /// <returns>ダイアログの戻り値</returns>
        public static OpenFileDialogResult ShowOpenFileDialog(FileDialogArg arg)
        {
            CommonFileDialogCheckBox chkReadOnly = new CommonFileDialogCheckBox("読み取り専用で開く(&R)");
            CommonFileDialogGroupBox grpEncoding = new CommonFileDialogGroupBox("文字コード(&E):");
            CommonFileDialogComboBox cmbEncoding = new CommonFileDialogComboBox();

            arg.Encodings?.ForEach(e => cmbEncoding.Items.Add(new CommonFileDialogComboBoxItem(e)));
            cmbEncoding.SelectedIndex = 0 <= arg.SelectedEncodingIndex ? arg.SelectedEncodingIndex : 0;
            grpEncoding.Items.Add(cmbEncoding);

            using (CommonOpenFileDialog dialog = new CommonOpenFileDialog())
            {
                arg.Filters?.ForEach(f => dialog.Filters.Add(new CommonFileDialogFilter(f.Key, f.Value)));
                dialog.DefaultFileName = arg.DefaultFileName;
                dialog.Multiselect = true;
                dialog.Controls.Add(chkReadOnly);
                dialog.Controls.Add(grpEncoding);

                switch (dialog.ShowDialog())
                {
                    case CommonFileDialogResult.Ok:
                        return new OpenFileDialogResult()
                        {
                            Argument = arg,
                            DialogResult = DialogResult.OK,
                            FileNames = dialog.FileNames.ToArray(),
                            SelectedFilterIndex = dialog.SelectedFileTypeIndex - 1,
                            SelectedEncodingIndex = cmbEncoding.SelectedIndex,
                            ReadOnlyChecked = chkReadOnly.IsChecked,
                        };

                    default:
                        return new OpenFileDialogResult()
                        {
                            Argument = arg,
                            DialogResult = DialogResult.Cancel,
                            FileNames = Array.Empty<string>(),
                        };
                }
            }
        }

        /// <summary>
        /// 「ファイルを保存する」ダイアログを表示します。
        /// </summary>
        /// <param name="arg">パラメータ</param>
        /// <returns>ダイアログの戻り値</returns>
        public static SaveFileDialogResult ShowSaveFileDialog(FileDialogArg arg)
        {
            CommonFileDialogGroupBox grpEncoding = new CommonFileDialogGroupBox("文字コード(&E):");
            CommonFileDialogComboBox cmbEncoding = new CommonFileDialogComboBox();
            arg.Encodings?.ForEach(e => cmbEncoding.Items.Add(new CommonFileDialogComboBoxItem(e)));
            cmbEncoding.SelectedIndex = 0 <= arg.SelectedEncodingIndex ? arg.SelectedEncodingIndex : 0;
            grpEncoding.Items.Add(cmbEncoding);

            using (CommonSaveFileDialog dialog = new CommonSaveFileDialog())
            {
                arg.Filters?.ForEach(f => dialog.Filters.Add(new CommonFileDialogFilter(f.Key, f.Value)));
                dialog.DefaultFileName = arg.DefaultFileName;
                dialog.Controls.Add(grpEncoding);

                switch (dialog.ShowDialog())
                {
                    case CommonFileDialogResult.Ok:
                        return new SaveFileDialogResult()
                        {
                            Argument = arg,
                            DialogResult = DialogResult.OK,
                            FilePath = dialog.FileName,
                            SelectedFilterIndex = dialog.SelectedFileTypeIndex - 1,
                            SelectedEncodingIndex = cmbEncoding.SelectedIndex,
                        };

                    default:
                        return new SaveFileDialogResult()
                        {
                            Argument = arg,
                            DialogResult = DialogResult.Cancel,
                            FilePath = string.Empty,
                        };
                }
            }
        }
    }

    #region DialogArgs
    /// <summary>
    /// ファイル選択ダイアログに渡される引数を表します。
    /// </summary>
    public class FileDialogArg
    {
        /// <summary>
        /// デフォルトで表示されるファイル名を取得または設定します。
        /// </summary>
        public string DefaultFileName { get; set; } = string.Empty;

        /// <summary>
        /// ファイル名のフィルターを取得または設定します。
        /// </summary>
        public List<KeyValuePair<string, string>> Filters { get; set; } = null;

        /// <summary>
        /// エンコードの一覧を取得または設定します。
        /// </summary>
        public List<string> Encodings { get; set; } = null;

        /// <summary>
        /// 選択状態のエンコードのインデックスを取得または設定します。
        /// </summary>
        public int SelectedEncodingIndex { get; set; } = 0;
    }
    #endregion

    #region DialogResult
    /// <summary>
    /// ダイアログの戻り値の基底クラスを表します。
    /// </summary>
    public abstract class FileDialogResultBase
    {
        /// <summary>
        /// ダイアログを表示する際に渡された引数を取得します。
        /// </summary>
        public FileDialogArg Argument { get; internal set; } = null;

        /// <summary>
        /// ダイアログの戻り値を取得または設定します。
        /// </summary>
        public DialogResult DialogResult { get; internal set; } = DialogResult.None;

        /// <summary>
        /// 選択状態のフィルターのインデックスを取得または設定します。
        /// </summary>
        public int SelectedFilterIndex { get; internal set; } = 0;

        /// <summary>
        /// 選択状態のエンコードのインデックスを取得または設定します。
        /// </summary>
        public int SelectedEncodingIndex { get; internal set; } = 0;
    }

    /// <summary>
    /// <see cref="MessageBoxUtils.ShowOpenFileDialog()"/> の戻り値を表します。
    /// </summary>
    public class OpenFileDialogResult : FileDialogResultBase
    {
        /// <summary>
        /// 選択されたすべてのファイルの名前を取得または設定します。
        /// </summary>
        public string[] FileNames { get; internal set; } = Array.Empty<string>();

        /// <summary>
        /// 読み取り専用として開くかどうかを取得または設定します。
        /// </summary>
        public bool ReadOnlyChecked { get; internal set; } = false;
    }

    /// <summary>
    /// <see cref="MessageBoxUtils.ShowSaveFileDialog()"/> の戻り値を表します。
    /// </summary>
    public class SaveFileDialogResult : FileDialogResultBase
    {
        /// <summary>
        /// 選択されたファイルの名前を取得または設定します。
        /// </summary>
        public string FilePath { get; internal set; } = string.Empty;
    }
    #endregion
}
