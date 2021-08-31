using AcroBat;
using AcroBat.Views.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AcroPad.Views.Forms
{
    public partial class PrintPreviewDialogEx : DialogBase
    {
        /// <summary>
        /// インスタンスを初期化します。
        /// </summary>
        public PrintPreviewDialogEx()
        {
            this.InitializeComponent();
            this.InitializeItems();
            this.AddEventHandler();
            this._statusBar.BackColor = AccentColors.DarkBlue;
        }

        /// <summary>
        /// 特定のコントロールを初期化します。
        /// </summary>
        private void InitializeItems()
        {
            this.SetItemImage();
            this.SetItemToolTip();
        }

        /// <summary>
        /// イベントハンドラを追加します。
        /// </summary>
        private void AddEventHandler()
        {
            this.mcmdClose.Click += this.CloseCommand_Executed;
        }

        private void CloseCommand_Executed(object sender, EventArgs e)
        {
            this.Cancel();
        }

        private void Cancel()
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Escape:
                    this.mcmdClose.PerformClick();
                    return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
