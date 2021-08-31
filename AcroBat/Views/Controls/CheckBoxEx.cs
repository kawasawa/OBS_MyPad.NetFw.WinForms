using AcroBat.WindowsAPI;
using System.Windows.Forms;

namespace AcroBat.Views.Controls
{
    /// <summary>
    /// チェックボックスを表します。
    /// これは <see cref="CheckBox"/> を拡張したコントロールです。
    /// </summary>
    public class CheckBoxEx : CheckBox
    {
        ///// <summary>
        ///// インスタンスの初期化時に一度だけ行われます。
        ///// </summary>
        //static CheckBoxEx()
        //{
        //    // 下記の処理により常にアクセスキーに下線が表示された状態になる。
        //    // 現状、有効な対処方法が無い。
        //    //
        //    //// Windows Vista 以降の Windows OS において、
        //    //// TABキー押下以外の方法によりフォーカスを取得した際、
        //    //// フォーカスを取得していることを示す破線枠が表示されない現象が確認されている。
        //    //// このため、常に表示するようシステムパラメータを書き換えている。
        //    //const uint SPI_SETKEYBOARDCUES = 0x100B;
        //    //const uint SPIF_UPDATEINIFILE = 0x0001;
        //    //const uint SPIF_SENDWININICHANGE = 0x0002;
        //    //User32.SystemParametersInfo(SPI_SETKEYBOARDCUES, 0, true, SPIF_UPDATEINIFILE | SPIF_SENDWININICHANGE);
        //}
    }
}
