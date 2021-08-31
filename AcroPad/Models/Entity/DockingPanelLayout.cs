using WeifenLuo.WinFormsUI.Docking;

namespace AcroPad.Models.Entity
{
    /// <summary>
    /// ドッキングパネルのレイアウト情報を表します。
    /// </summary>
    public class DockingPanelLayout
    {
        /// <summary>
        /// 指定したパネルのレイアウト情報を保存します。
        /// </summary>
        /// <param name="panel">パネル</param>
        public void GetValue(DockPanel panel)
            => LayoutContainer.Instance.InternalSaveAsXml(panel);

        /// <summary>
        /// 指定したパネルにレイアウト情報を読み込みます。
        /// </summary>
        /// <param name="panel">パネル</param>
        /// <param name="contentSelector">コンテンツを選択する処理</param>
        public void SetValue(DockPanel panel, DeserializeDockContent deserializeContent) 
            => LayoutContainer.Instance.InternalLoadAsXml(panel, deserializeContent); 
    }
}
