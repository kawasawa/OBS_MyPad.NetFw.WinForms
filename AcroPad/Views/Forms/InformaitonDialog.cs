using AcroBat;
using AcroBat.Views.Forms;

namespace AcroPad.Views.Forms
{
    /// <summary>
    /// バージョン情報ダイアログを表します。
    /// </summary>
    public partial class InformaitonDialog : FixedSizeDialogBase
    {
        /// <summary>
        /// インスタンスを初期化します。
        /// </summary>
        public InformaitonDialog()
        {
            this.InitializeComponent();
            this.AcceptButton = this.btnOK;
            this.CancelButton = this.btnOK;
            this.Title.ForeColor = ApplicationColor.DependOnBuild;
            this.Title.Text = AssemblyEntity.Title;
            this.VersionString.Text = $"Version {AssemblyEntity.VersionString}";
            this.Copyright.Text = AssemblyEntity.Copyright;
            this.btnOK.Select();
        }
    }
}
