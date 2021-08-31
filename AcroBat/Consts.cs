using System.Drawing;
using System.Text;

namespace AcroBat
{
    /// <summary>
    /// .NET Framework が内部的に使用する値を取得します。
    /// </summary>
    public static class DotNETFrameworkDependencyValue
    {
        /// <summary>
        /// エンコードを取得します。
        /// </summary>
        public static readonly Encoding Encoding = Encoding.UTF8;
    }

    /// <summary>
    /// アクセントカラーを定義します。
    /// </summary>
    public static class AccentColors
    {
        public static readonly Color Blue = Color.FromArgb(0, 122, 204);
        public static readonly Color DarkBlue = Color.FromArgb(14, 99, 156);
        public static readonly Color Purple = Color.FromArgb(104, 33, 122);
        public static readonly Color Orange = Color.FromArgb(202, 81, 0);
        public static readonly Color Pink = Color.FromArgb(255, 32, 80);
        public static readonly Color DeepBlue = Color.FromArgb(25, 71, 138);
        public static readonly Color DeepGreen = Color.FromArgb(10, 99, 50);
        public static readonly Color DeepOrange = Color.FromArgb(184, 59, 29);
        public static readonly Color DeepRed = Color.FromArgb(162, 58, 55);
    }

    /// <summary>
    /// ステータスカラーを定義します。
    /// </summary>
    public static class StatusColors
    {
        public static readonly Color Valid = Color.FromArgb(255, 255, 255);
        public static readonly Color InValid = Color.FromArgb(255, 228, 225);
    }

    /// <summary>
    /// コードページIDを定義します。
    /// </summary>
    public static class CodePage
    {
        public const int SJIS = 932;
        public const int JIS = 50220;
        public const int EUCJP = 51932;
        public const int UTF16 = 1200;
        public const int UTF16BE = 1201;
        public const int UTF8 = 65001;
        public const int UTF7 = 65000;
    }

    /// <summary>
    /// 改行コードを定義します。
    /// </summary>
    public static class EolCode
    {
        public const string CR = "\r";
        public const string LF = "\n";
        public const string CRLF = "\r\n";
    }

    /// <summary>
    /// デザイナーカテゴリを示す文字列を定義します。
    /// </summary>
    public static class DesignerCategory
    {
        public const string ACTION = "アクション";
        public const string APPEARANCE = "表示";
        public const string ASYNCHRONOUS = "非同期";
        public const string BEHAVIOR = "動作";
        public const string DATA = "データ";
        public const string DEFAULT = "デフォルト";
        public const string DESIGN = "デザイン";
        public const string DRAG_DROP = "ドラッグ アンド ドロップ";
        public const string FOCUS = "フォーカス";
        public const string FORMAT = "フォーマット";
        public const string KEY = "キー";
        public const string LAYOUT = "配置";
        public const string MOUSE = "マウス";
        public const string PROPERTY_CHANGED = "プロパティ変更";
        public const string WINDOW_STYLE = "ウィンドウ スタイル";
    }
}
