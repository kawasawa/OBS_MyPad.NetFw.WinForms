using AcroBat;
using AcroPad.Models.Entity;
using AcroPad.Properties;
using System.IO;

namespace AcroPad.Models
{
    /// <summary>
    /// アプリケーションの設定情報を管理するクラスを表します。
    /// </summary>
    public class SettingContainer
    {
        /// <summary>
        /// 設定ファイルのパスを取得します。
        /// </summary>
        private static readonly string FILE_PATH = Path.Combine(DirectoryPath.APP_DATA, Resources.CNS_FIL_SETTING);

        /// <summary>
        /// <see cref="SettingContainer"/> クラスの唯一のインスタンスを表します。
        /// </summary>
        public static SettingContainer Instance { get; private set; }

        /// <summary>
        /// インスタンスを初期化します。
        /// </summary>
        private SettingContainer()
        {
        }

        /// <summary>
        /// 設定を取得します。
        /// </summary>
        internal static void Load()
        {
            try
            {
                Instance = Extensions.ReadXml<SettingContainer>(FILE_PATH);
            }
            catch (IOException)
            {
                Instance = new SettingContainer();
            }
        }

        /// <summary>
        /// 設定を保存します。
        /// </summary>
        /// <returns>保存に成功したかどうかを示す値</returns>
        internal bool Save()
        {
            try
            {
                this.WriteXml(FILE_PATH);
            }
            catch (IOException)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// ウィンドウの設定を取得または設定します。
        /// </summary>
        public WindowSetting WindowSetting { get; set; } = new WindowSetting();

        /// <summary>
        /// ドッキングパネルの設定を取得または設定します。
        /// </summary>
        public DockingPanelSetting DockingPanelSetting { get; set; } = new DockingPanelSetting();

        /// <summary>
        /// エクスプローラーの設定を取得または設定します。
        /// </summary>
        public ExplorerSetting ExplorerSetting { get; set; } = new ExplorerSetting();

        /// <summary>
        /// 検索設定を取得または設定します。
        /// </summary>
        public SearchSetting SearchSetting { get; set; } = new SearchSetting();

        /// <summary>
        /// テキストエディターの設定を取得または設定します。
        /// </summary>
        public TextEditorSetting TextEditorSetting { get; set; } = new TextEditorSetting();
    }
}
