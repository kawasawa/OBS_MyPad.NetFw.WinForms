using AcroBat;
using AcroPad.Models.Entity;
using AcroPad.Properties;
using System.IO;

namespace AcroPad.Models
{
    /// <summary>
    /// アプリケーションの履歴情報を管理するクラスを表します。
    /// </summary>
    public class HistoryContainer
    {
        /// <summary>
        /// 設定ファイルのパスを取得します。
        /// </summary>
        private static readonly string FILE_PATH = Path.Combine(DirectoryPath.APP_DATA, Resources.CNS_FIL_HISTORY);

        /// <summary>
        /// <see cref="HistoryContainer"/> クラスの唯一のインスタンスを表します。
        /// </summary>
        public static HistoryContainer Instance { get; private set; }

        /// <summary>
        /// インスタンスを初期化します。
        /// </summary>
        private HistoryContainer()
        {
        }

        /// <summary>
        /// 設定を取得します。
        /// </summary>
        internal static void Load()
        {
            try
            {
                Instance = Extensions.ReadXml<HistoryContainer>(FILE_PATH);
            }
            catch (IOException)
            {
                Instance = new HistoryContainer();
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
        /// 検索履歴を取得または設定します。
        /// </summary>
        public SearchHistory SearchHistory { get; set; } = new SearchHistory();

        /// <summary>
        /// 最近使用されたファイルを取得または設定します。
        /// </summary>
        //public OrderedDictionary<string, LanguageKind> RecentFiles { get; set; } = new OrderedDictionary<string, LanguageKind>();

    }
}
