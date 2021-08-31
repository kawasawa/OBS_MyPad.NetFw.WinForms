using System;
using System.Reflection;

namespace AcroBat
{
    /// <summary>
    /// アセンブリ情報を取得するためのクラスを表します。
    /// </summary>
    public static class AssemblyEntity
    {
        private static readonly Assembly ASSEMBLY = Assembly.GetEntryAssembly();
        private static readonly Lazy<string> TITLE =
            new Lazy<string>(() => ((AssemblyTitleAttribute)Attribute.GetCustomAttribute(ASSEMBLY, typeof(AssemblyTitleAttribute))).Title);
        private static readonly Lazy<string> DESCRIPTION = 
            new Lazy<string>(() => ((AssemblyDescriptionAttribute)Attribute.GetCustomAttribute(ASSEMBLY, typeof(AssemblyDescriptionAttribute))).Description);
        private static readonly Lazy<string> COMPANY = 
            new Lazy<string>(() => ((AssemblyCompanyAttribute)Attribute.GetCustomAttribute(ASSEMBLY, typeof(AssemblyCompanyAttribute))).Company);
        private static readonly Lazy<string> PRODUCT = 
            new Lazy<string>(() => ((AssemblyProductAttribute)Attribute.GetCustomAttribute(ASSEMBLY, typeof(AssemblyProductAttribute))).Product);
        private static readonly Lazy<string> COPYRIGHT = 
            new Lazy<string>(() => ((AssemblyCopyrightAttribute)Attribute.GetCustomAttribute(ASSEMBLY, typeof(AssemblyCopyrightAttribute))).Copyright);
        private static readonly Lazy<string> TRADEMARK = 
            new Lazy<string>(() => ((AssemblyTrademarkAttribute)Attribute.GetCustomAttribute(ASSEMBLY, typeof(AssemblyTrademarkAttribute))).Trademark);
        private static readonly Lazy<string> VERSION = 
            new Lazy<string>(() => $"{Version.ToString(3)}{(Version.Revision == 0 ? string.Empty : $" rev.{Version.Revision}")}");

        /// <summary>
        /// タイトルを取得します。
        /// </summary>
        public static string Title => TITLE.Value;

        /// <summary>
        /// 説明文を取得します。
        /// </summary>
        public static string Description => DESCRIPTION.Value;

        /// <summary>
        /// 社名を取得します。
        /// </summary>
        public static string Company => COMPANY.Value;

        /// <summary>
        /// 製品名を取得します。
        /// </summary>
        public static string Product => PRODUCT.Value;

        /// <summary>
        /// コピーライトを取得します。
        /// </summary>
        public static string Copyright => COPYRIGHT.Value;

        /// <summary>
        /// 商標を取得します。
        /// </summary>
        public static string Trademark => TRADEMARK.Value;

        /// <summary>
        /// バージョン情報を取得します。
        /// </summary>
        public static Version Version => ASSEMBLY.GetName().Version;

        /// <summary>
        /// バージョン情報を表す文字列を取得します。
        /// </summary>
        public static string VersionString =>
#if DEBUG
            $"{VERSION.Value}  [Debug Build]";
#else
            VERSION.Value;
#endif
    }
}