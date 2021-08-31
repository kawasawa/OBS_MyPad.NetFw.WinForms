using AcroBat.Models;
using System;

namespace AcroPad.Models.Entity
{
    /// <summary>
    /// 検索履歴を表します。
    /// </summary>
    public class SearchHistory : MappingObjectBase
    {
        /// <summary>
        /// 検索語を取得または設定します。
        /// </summary>
        public string[] SearchTerm { get; set; } = Array.Empty<string>();

        /// <summary>
        /// 置換語を取得または設定します。
        /// </summary>
        public string[] ReplaceTerm { get; set; } = Array.Empty<string>();
    }
}
