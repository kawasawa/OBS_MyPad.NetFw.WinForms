using AcroBat.Views.Forms;
using AcroPad.Views.Controls;
using System;
using System.Windows.Forms;

namespace AcroPad.Views.Forms
{
    /// <summary>
    /// 指定行へ移動するダイアログを表します。
    /// </summary>
    public partial class GoToLineDialog : FixedSizeDialogBase
    {
        /// <summary>
        /// テキストエディターを取得または設定します。
        /// </summary>
        private TextEditor TextEditor { get; set; }

        /// <summary>
        /// インスタンスを初期化します。
        /// </summary>
        /// <param name="textEditor">テキストエディター</param>
        public GoToLineDialog(TextEditor textEditor)
        {
            this.InitializeComponent();
            this.AddEventHandler();
            this.AcceptButton = this.btnOK;
            this.CancelButton = this.btnCancel;
            this.btnOK.DialogResult = DialogResult.OK;
            this.btnCancel.DialogResult = DialogResult.Cancel;
            this.TextEditor = textEditor;
        }

        /// <summary>
        /// このインスタンスが使用しているすべてのリソースを解放します。
        /// </summary>
        /// <param name="disposing">マネージリソースを開放するかどうかを示す値</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.components?.Dispose();
                this.TextEditor = null;
            }
            base.Dispose(disposing);
        }

        /// <summary>
        /// イベントハンドラを追加します。
        /// </summary>
        private void AddEventHandler()
        {
            this.Load += this.Form_Load;
            this.FormClosed += this.Form_Closed;
            this.numRowIndex.ValidStatusChanged += this.NumRowIndex_ErrorStateChanged;
        }

        private void Form_Load(object sender, EventArgs e)
        {
            this.lblRowIndex.Text = $"行番号 (1 - {this.TextEditor.LineCount}) (&L):";
            this.numRowIndex.Minimum = 1;
            this.numRowIndex.Maximum = this.TextEditor.LineCount;
            this.numRowIndex.Value = this.TextEditor.LineIndex + 1;
        }

        private void Form_Closed(object sender, FormClosedEventArgs e)
        {
            switch (this.DialogResult)
            {
                case DialogResult.OK:
                    int index = this.TextEditor.GetLineHeadIndex((int)this.numRowIndex.Value - 1);
                    this.TextEditor.SetSelection(index, index);
                    this.TextEditor.ScrollToCaret();
                    break;
            }
        }

        private void NumRowIndex_ErrorStateChanged(object sender, EventArgs e)
        {
            this.btnOK.Enabled = this.numRowIndex.IsValid;
        }
    }
}
