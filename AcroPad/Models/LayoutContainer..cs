using AcroBat;
using AcroPad.Models.Entity;
using AcroPad.Properties;
using System.IO;
using WeifenLuo.WinFormsUI.Docking;

namespace AcroPad.Models
{
    /// <summary>
    /// アプリケーションのレイアウト情報を管理するクラスを表します。
    /// </summary>
    public class LayoutContainer
    {
        /// <summary>
        /// 設定ファイルのパスを取得します。
        /// </summary>
        private static readonly string FILE_PATH = Path.Combine(DirectoryPath.APP_DATA, Resources.CNS_FIL_LAYOUT);

        /// <summary>
        /// <see cref="LayoutContainer"/> クラスの唯一のインスタンスを表します。
        /// </summary>
        public static LayoutContainer Instance { get; private set; }

        /// <summary>
        /// インスタンスを初期化します。
        /// </summary>
        private LayoutContainer()
        {
        }

        /// <summary>
        /// 設定を取得します。
        /// </summary>
        internal static void Load()
        {
            Instance = new LayoutContainer();
        }

        /// <summary>
        /// XML ファイルからレイアウト情報を読み込みます。
        /// </summary>
        /// <param name="panel">パネル</param>
        /// <param name="contentSelector">コンテンツを選択する処理</param>
        internal void InternalLoadAsXml(DockPanel panel, DeserializeDockContent contentSelector)
        {
            try
            {
                panel.LoadFromXml(FILE_PATH, contentSelector);
            }
            catch (IOException)
            {
            }
        }

        /// <summary>
        /// XML ファイルからレイアウト情報を保存します。
        /// </summary>
        /// <param name="panel">パネル</param>
        /// <returns>保存に成功したかどうかを示す値</returns>
        internal bool InternalSaveAsXml(DockPanel panel)
        {
            try
            {
                Directory.CreateDirectory(Path.GetDirectoryName(FILE_PATH));
                panel.SaveAsXml(FILE_PATH, DotNETFrameworkDependencyValue.Encoding);
            }
            catch (IOException)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// レイアウト設定を取得または設定します。
        /// </summary>
        public DockingPanelLayout DockingPanelLayout { get; set; } = new DockingPanelLayout();
    }
}
