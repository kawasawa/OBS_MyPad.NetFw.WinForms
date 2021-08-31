using AcroBat;
using AcroPad.Models.Associations;
using AcroPad.Properties;
using System.IO;
using System.Linq;

namespace AcroPad.Models
{
    /// <summary>
    /// 言語情報を管理するクラスを表します。
    /// </summary>
    public class LanguageContainer
    {
        /// <summary>
        /// 設定ファイルのパスを取得します。
        /// </summary>
        private static readonly string FILE_PATH = Path.Combine(DirectoryPath.APP_DATA, Resources.CNS_FIL_LANGUAGE);

        /// <summary>
        /// <see cref="LanguageContainer"/> クラスの唯一のインスタンスを表します。
        /// </summary>
        public static LanguageContainer Instance { get; private set; }

        /// <summary>
        /// インスタンスを初期化します。
        /// </summary>
        private LanguageContainer()
        {
        }

        /// <summary>
        /// 設定を取得します。
        /// </summary>
        internal static void Load()
        {
            try
            {
                Instance = Extensions.ReadXml<LanguageContainer>(FILE_PATH);
            }
            catch (IOException)
            {
                Instance = new LanguageContainer();
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
        /// デフォルトのファイル名を取得または設定します。
        /// </summary>
        public string InitialName { get; set; } = "NoName";

        /// <summary>
        /// ファイル形式を取得または設定します。
        /// </summary>
        public Association[] Associations { get; set; } = new[]
        {
            AssociationFactory.CreateInstance(LanguageKind.Text),
            AssociationFactory.CreateInstance(LanguageKind.Cpp),
            AssociationFactory.CreateInstance(LanguageKind.CSharp),
            AssociationFactory.CreateInstance(LanguageKind.Java),
            AssociationFactory.CreateInstance(LanguageKind.Python),
            AssociationFactory.CreateInstance(LanguageKind.Ruby),
            AssociationFactory.CreateInstance(LanguageKind.JavaScript),
            AssociationFactory.CreateInstance(LanguageKind.PlSql),
            AssociationFactory.CreateInstance(LanguageKind.Xml),
            AssociationFactory.CreateInstance(LanguageKind.BatchFile),
            AssociationFactory.CreateInstance(LanguageKind.Ini),
        };

        /// <summary>
        /// ファイルの種類から関連付け情報を取得します。
        /// </summary>
        /// <param name="kind">ファイルの種類</param>
        /// <returns>関連付け情報</returns>
        public Association GetAssociation(LanguageKind kind)
        {
            return this.Associations.FirstOrDefault(info => info.LanguageMode == kind);
        }

        /// <summary>
        /// 拡張子から関連付け情報を取得します。
        /// </summary>
        /// <param name="extension">拡張子</param>
        /// <returns>関連付け情報</returns>
        public Association GetAssociation(string extension)
        {
            if (string.IsNullOrWhiteSpace(extension))
            {
                return null;
            }
            return this.Associations.FirstOrDefault(info => info.Extensions?.Contains(extension) == true);
        }
    }
}
