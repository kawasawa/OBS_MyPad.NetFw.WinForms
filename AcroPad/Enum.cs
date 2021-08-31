namespace AcroPad
{
    /// <summary>
    /// テーマの種類を表します。
    /// </summary>
    public enum ThemeKind
    {
        Modern,
        Classic,
    }

    /// <summary>
    /// 検索コンテンツの種類を表します。
    /// </summary>
    public enum SearchContentBehaviorKind
    {
        Search,
        Replace,
    }

    /// <summary>
    /// 自動インデントの種類を表します。
    /// </summary>
    public enum AutoIndentKind
    {
        None,
        Smart,
        C,
        Python,
    }

    /// <summary>
    /// 言語の種類を表します。
    /// </summary>
    public enum LanguageKind
    {
        Text,
        Cpp,
        CSharp,
        Java,
        Python,
        Ruby,
        JavaScript,
        PlSql,
        Xml,
        BatchFile,
        Ini,
    }

    /// <summary>
    /// エンコードの種類を表します。
    /// </summary>
    public enum EncodingKind
    {
        SJIS,
        JIS,
        EUCJP,
        UTF16,
        UTF16BE,
        UTF8,
        UTF7,
    }

    /// <summary>
    /// 改行コードの種類を表します。
    /// </summary>
    public enum EolKind
    {
        CRLF,
        CR,
        LF,
    }

    /// <summary>
    /// テキストの折り返し方法の種類を表します。
    /// </summary>
    public enum WordWrapKind
    {
        UnWrap,
        DigitWrap,
        EdgeWrap,
    }
}
