using AcroBat;
using AcroPad.Properties;
using System.Drawing;
using System.IO;
using System.Reflection;

namespace AcroPad
{
    public static class ExitCode
    {
        public const int SUCCESS = 0;
        public const int UNHANDLED_EXCEPTION_OCCURED = 1;
    }

    public static class SpecialChar
    {
        public const char SendDataSeparator = '|'; 
    }

    /// <summary>
    /// ディレクトリのパスを定義します。
    /// </summary>
    public static class DirectoryPath
    {
        /// <summary>
        /// 実行ファイルを格納するディレクトリのパスを取得します。
        /// </summary>
        public static readonly string APPLICATION = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

        /// <summary>
        /// アプリケーション情報を格納するディレクトリのパスを取得します。
        /// </summary>
        public static readonly string APP_DATA = Path.Combine(APPLICATION, Resources.CNS_DIR_DATA);

        /// <summary>
        /// ログファイルを格納するディレクトリのパスを取得します。
        /// </summary>
        public static readonly string LOG = Path.Combine(APPLICATION, Resources.CNS_DIR_LOG);
    }

    /// <summary>
    /// コントロール名のプレフィックスを定義します。
    /// </summary>
    public static class ControlNamePrefix
    {
        /// <summary>
        /// コマンドコントロールの名称を取得します。
        /// </summary>
        public const string COMMAND = "cmd";

        /// <summary>
        /// メニューコマンドコントロールの名称を取得します。
        /// </summary>
        public static readonly string MENU_COMMAND = $"m{COMMAND}";

        /// <summary>
        /// ツールコマンドコントロールの名称を取得します。
        /// </summary>
        public static readonly string TOOL_COMMAND = $"t{COMMAND}";

        /// <summary>
        /// 設定を反映するコントロールの名称を取得します。
        /// </summary>
        public const string MAPPED = "map";

        /// <summary>
        /// 数値コントロールの名称を取得します。
        /// </summary>
        public static readonly string NUMERIC_MAPPED = $"n{MAPPED}";

        /// <summary>
        /// 真偽値の名称を取得します。
        /// </summary>
        public static readonly string BOOLEAN_MAPPED = $"b{MAPPED}";

        /// <summary>
        /// 列挙値の名称を取得します。
        /// </summary>
        public static readonly string ENUM_MAPPED = $"e{MAPPED}";
    }

    /// <summary>
    /// アプリケーション固有のカラーを定義します。
    /// </summary>
    public static class ApplicationColor
    {
        /// <summary>
        /// ビルドバージョンにより異なる色を返します。
        /// </summary>
        public static readonly Color DependOnBuild =
#if DEBUG
            AccentColors.Purple;
#else
            AccentColors.Blue;
#endif
    }
}
