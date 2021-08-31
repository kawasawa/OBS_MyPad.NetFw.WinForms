using System;

namespace AcroPad.Models.Entity
{
    /// <summary>
    /// エクスプローラーの設定を表します。
    /// </summary>
    public class ExplorerSetting : SettingBase
    {
        /// <summary>
        /// ルートノードのディレクトリパスを取得または設定します。
        /// </summary>
        public string RootDirectoryPath { get; set; } = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
    }
}
