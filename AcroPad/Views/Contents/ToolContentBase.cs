using System.ComponentModel;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace AcroPad.Views.Contents
{
    /// <summary>
    /// ツールコンテンツの基底クラスを表します。
    /// </summary>
    public class ToolContentBase : ContentBase
    {
        /// <summary>
        /// エンドユーザーがドッキング状態を変更できるかどうかを示す値を取得します。
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new bool AllowEndUserDocking
        {
            get { return base.AllowEndUserDocking; }
            private set { base.AllowEndUserDocking = value; }
        }

        /// <summary>
        /// コンテンツを閉じるときにインスタンスを保持するかどうかを示します。
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new bool HideOnClose
        {
            get { return base.HideOnClose; }
            private set { base.HideOnClose = value; }
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public ToolContentBase()
        {
            this.AllowEndUserDocking = true;
            this.HideOnClose = true;
            this.DockAreas = DockAreas.DockLeft |
                             DockAreas.DockRight |
                             DockAreas.DockTop |
                             DockAreas.DockBottom |
                             DockAreas.Float;
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
                    if (this.IsFloat)
                    {
                        this.Hide();
                    }
                    else if (this.IsAnchorDock)
                    {
                        (this.DockPanel.ActiveDocument as ContentBase)?.Select();
                    }
                    else
                    {
                        break;
                    }
                    return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
