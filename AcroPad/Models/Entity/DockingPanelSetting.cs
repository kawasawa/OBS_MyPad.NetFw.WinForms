namespace AcroPad.Models.Entity
{
    /// <summary>
    /// ドッキングパネルの設定を表します。
    /// </summary>
    public class DockingPanelSetting : SettingBase
    {
        /// <summary>
        /// テーマの種類を取得または設定します。
        /// </summary>
        public ThemeKind ThemeMode { get; set; } = ThemeKind.Modern;

        /// <summary>
        /// マウスカーソルをタブの上に重ねた際に、
        /// 自動的に隠すコンテンツを展開表示するかどうかを示す値を取得または設定します。
        /// </summary>
        public bool ShowAutoHideContentOnHover { get; set; } = true;

        /// <summary>
        /// タブを底に表示するかどうかを示す値を取得または設定します。
        /// </summary>
        public bool ShowDocumentTabOnBottom { get; set; } = false;

        /// <summary>
        /// ドキュメントが一つだけの場合にタブを表示するかどうかを示す値を取得または設定します。
        /// </summary>
        public bool ShowDocumentTabOnlyOne { get; set; } = true;
    }
}
