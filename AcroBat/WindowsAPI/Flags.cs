namespace AcroBat.WindowsAPI
{
    /// <summary>
    /// ウィンドウの関係を表します。
    /// </summary>
    public static class GetWindow_Cmd
    {
        /// <summary>
        /// Zオーダーで一番上のウインドウを取得します。
        /// </summary>
        public const int GW_HWNDFIRST = 0;

        /// <summary>
        /// Zオーダーで一番下のウインドウを取得します。
        /// </summary>
        public const int GW_HWNDLAST = 1;

        /// <summary>
        /// Zオーダーで１つ下のウインドウを取得を取得します。
        /// </summary>
        public const int GW_HWNDNEXT = 2;

        /// <summary>
        /// Zオーダーで１つ上のウインドウを取得
        /// </summary>
        public const int GW_HWNDPREV = 3;

        /// <summary>
        /// 指定ウインドウのオーナー・ウインドウを取得します。
        /// </summary>
        public const int GW_OWNER = 4;

        /// <summary>
        /// 親ウインドウに対する一番上の子ウインドウを取得します。
        /// </summary>
        public const int GW_CHILD = 5;

        /// <summary>
        /// 指定ウインドウのポップアップ・ウインドウを取得します。
        /// </summary>
        public const int GW_ENABLEDPOPUP = 6;
    }

    /// <summary>
    /// ウィンドウの表示方法を表します。
    /// </summary>
    public static class nCmdShow
    {
        /// <summary>
        /// ウィンドウを非表示にし、他のウィンドウをアクティブにします。
        /// </summary>
        public const int SW_HIDE = 0;

        /// <summary>
        /// ウィンドウを最大化します。
        /// </summary>
        public const int SW_MAXIMIZE = 3;

        /// <summary>
        /// ウィンドウを最小化し、Z オーダーが次のトップレベルウィンドウをアクティブにします。
        /// </summary>
        public const int SW_MINIMIZE = 6;

        /// <summary>
        /// ウィンドウをアクティブにして表示します。
        /// 最小化または最大化されていたウィンドウは、元の位置とサイズに戻ります。
        /// 最小化されているウィンドウを元に戻す場合は、このフラグをセットします。
        /// </summary>
        public const int SW_RESTORE = 9;

        /// <summary>
        /// ウィンドウをアクティブにして、現在の位置とサイズで表示します。
        /// </summary>
        public const int SW_SHOW = 5;

        /// <summary>
        /// アプリケーションを起動したプログラムが関数に渡した構造体で指定された SW_ フラグに従って表示状態を設定します。
        /// </summary>
        public const int SW_SHOWDEFAULT = 10;

        /// <summary>
        /// ウィンドウをアクティブにして、最大化します。
        /// </summary>
        public const int SW_SHOWMAXIMIZED = 3;

        /// <summary>
        /// ウィンドウをアクティブにして、最小化します。
        /// </summary>
        public const int SW_SHOWMINIMIZED = 2;

        /// <summary>
        /// ウィンドウを最小化します。
        /// SW_SHOWMINIMIZED と似ていますが、この値を指定した場合は、ウィンドウはアクティブ化されません。
        /// </summary>
        public const int SW_SHOWMINNOACTIVE = 7;

        /// <summary>
        /// ウィンドウを現在のサイズと位置で表示します。
        /// SW_SHOW と似ていますが、この値を指定した場合は、ウィンドウはアクティブ化されません。
        /// </summary>
        public const int SW_SHOWNA = 8;

        /// <summary>
        /// ウィンドウを直前の位置とサイズで表示します。
        /// SW_SHOWNORMAL と似ていますが、この値を指定した場合は、ウィンドウはアクティブ化されません。
        /// </summary>
        public const int SW_SHOWNOACTIVATE = 4;

        /// <summary>
        /// ウィンドウをアクティブにして表示します。
        /// ウィンドウが最小化または最大化されていた場合は、その位置とサイズを元に戻します。
        /// 初めてウィンドウを表示するときには、このフラグを指定してください。
        /// </summary>
        public const int SW_SHOWNORMAL = 1;
    }

    /// <summary>
    /// 取得または設定するメンバーを表します。
    /// </summary>
    public static class FMask
    {
        /// <summary>
        /// <see cref="MENUITEMINFO.fState"/> を有効にします。
        /// </summary>
        public const int MIIM_STATE = 0x00000001;

        /// <summary>
        /// <see cref="MENUITEMINFO.wID"/> を有効にします。
        /// </summary>
        public const int MIIM_ID = 0x00000002;

        /// <summary>
        /// <see cref="MENUITEMINFO.hSubMenu"/> を有効にします。
        /// </summary>
        public const int MIIM_SUBMENU = 0x00000004;

        /// <summary>
        /// <see cref="MENUITEMINFO.hbmpChecked"/>, <see cref="MENUITEMINFO.hbmpUnchecked"/> を有効にします。
        /// </summary>
        public const int MIIM_CHECKMARKS = 0x00000008;

        /// <summary>
        /// <see cref="MENUITEMINFO.fType"/>, <see cref="MENUITEMINFO.hbmpUnchecked"/> を有効にします。
        /// </summary>
        public const int MIIM_TYPE = 0x00000010;

        /// <summary>
        /// <see cref="MENUITEMINFO.dwItemData"/> を有効にします。
        /// </summary>
        public const int MIIM_DATA = 0x00000020;

        /// <summary>
        /// <see cref="MENUITEMINFO.dwTypeData"/> を有効にします。
        /// </summary>
        public const int MIIM_STRING = 0x00000040;

        /// <summary>
        /// <see cref="MENUITEMINFO.hbmpItem"/> を有効にします。
        /// </summary>
        public const int MIIM_BITMAP = 0x00000080;

        /// <summary>
        /// <see cref="MENUITEMINFO.fType"/> を有効にします。
        /// </summary>
        public const int MIIM_FTYPE = 0x00000100;
    }

    /// <summary>
    /// アイテムのタイプを表します。
    /// </summary>
    public static class FType
    {
        /// <summary>
        /// 文字列を使用してアイテムを表示します。 
        /// </summary>
        public const int MFT_STRING = 0x00000000;

        /// <summary>
        /// アイテムにビットマップを表示します。
        /// </summary>
        public const int MFT_BITMAP = 0x00000004;

        /// <summary>
        /// メニューバーに対してはメニューアイテムを新しい行に配置します。
        /// ドロップダウンメニュー・サブメニュー・ショートカットメニューに対してはメニューアイテムを新しい列に配置します。
        /// ドロップダウンメニュー・サブメニュー・ショートカットメニューでは新しい列との間に線が引かれます。
        /// </summary>
        public const int MFT_MENUBARBREAK = 0x00000020;

        /// <summary>
        /// メニューバーに対してはメニューアイテムを新しい行に配置します。
        /// ドロップダウンメニュー・サブメニュー・ショートカットメニューに対してはメニューアイテムを新しい列に配置します。
        /// ドロップダウンメニュー・サブメニュー・ショートカットメニューでは新しい列との間には線はが引かれません。
        /// </summary>
        public const int MFT_MENUBREAK = 0x00000040;

        /// <summary>
        /// オーナードロー・メニューアイテムであることを示します。
        /// </summary>
        public const int MFT_OWNERDRAW = 0x00000100;

        /// <summary>
        /// チェックマークの代わりにラジオボタンを表示します。
        /// </summary>
        public const int MFT_RADIOCHECK = 0x00000200;

        /// <summary>
        /// セパレーター（区切り線）を表示します。
        /// </summary>
        public const int MFT_SEPARATOR = 0x00000800;

        /// <summary>
        /// アラビア語やヘブライ語などのシステムで文字列を右から左に表示します。
        /// </summary>
        public const int MFT_RIGHTORDER = 0x00002000;

        /// <summary>
        /// 以降のアイテムをメニューバーの右側に配置します。
        /// </summary>
        public const int MFT_RIGHTJUSTIFY = 0x00004000;
    }

    /// <summary>
    /// アイテムの状態を表します。
    /// </summary>
    public static class FState
    {
        /// <summary>
        /// 通常表示にします。
        /// 
        /// 次の指定と同義です。
        /// MFS_UNCHECKED
        /// MFS_UNHILITE
        /// </summary>
        public const int MFS_ENABLED = 0x00000000;

        /// <summary>
        /// アイテムを灰色で表示し、選択不可にします。
        /// 
        /// 次の指定と同義です。
        /// MFS_DISABLED
        /// </summary>
        public const int MFS_GRAYED = 0x00000003;

        /// <summary>
        /// アイテムにチェックマークをつけます。
        /// </summary>
        public const int MFS_CHECKED = 0x00000008;

        /// <summary>
        /// アイテムをハイライト表示します。
        /// </summary>
        public const int MFS_HILITE = 0x00000080;

        /// <summary>
        /// デフォルトアイテムに設定します。
        /// 1つのメニューに対して1つまで持つことができ、アイテムは太字で表示されます。
        /// </summary>
        public const int MFS_DEFAULT = 0x00001000;
    }
}
