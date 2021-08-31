using System.ComponentModel;
using WeifenLuo.WinFormsUI.Docking;

namespace AcroPad.Views.Contents
{
    /// <summary>
    /// ドキュメントの基底クラスを表します。
    /// </summary>
    public class DocumentBase : ContentBase
    {
        /// <summary>
        /// ドッキング可能な領域を取得します。
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new DockAreas DockAreas
        {
            get { return base.DockAreas; }
            private set { base.DockAreas = value; }
        }

        /// <summary>
        /// インスタンスを初期化します。
        /// </summary>
        public DocumentBase()
        {
            this.DockAreas = DockAreas.Document;
        }
    }
}
