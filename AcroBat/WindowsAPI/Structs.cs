using System;
using System.Runtime.InteropServices;

namespace AcroBat.WindowsAPI
{
    /// <summary>
    /// 点の x 座標と y 座標を表します。
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct POINT
    {
        public int x;
        public int y;
    }

    /// <summary>
    /// 四角形の左上隅および右下隅の座標を表します。
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct RECT
    {
        public int left;
        public int top;
        public int right;
        public int bottom;
    }

    /// <summary>
    /// ウィンドウの配置に関する情報を表します。
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct WINDOWPLACEMENT
    {
        public int length;
        public int flags;
        public int showCmd;
        public POINT ptMinPosition;
        public POINT ptMaxPosition;
        public RECT rcNormalPosition;
    }

    /// <summary>
    /// <see cref="WindowsMessages.WM_COPYDATA"/> メッセージで別のアプリケーションに渡されるデータを表します。
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct COPYDATASTRUCT
    {
        /// <summary>
        /// 送信する32ビット値のデータを表します。
        /// </summary>
        public IntPtr dwData;

        /// <summary>
        /// <see cref="lpData"/> ポインタが指すメモリブロックのサイズを表します。
        /// </summary>
        public int cbData;

        /// <summary>
        /// 送信するデータの先頭を指すポインタを表します。
        /// </summary>
        public string lpData;
    }

    /// <summary>
    /// メニューアイテムの情報を表します。
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct MENUITEMINFO
    {
        /// <summary>
        /// 構造体のサイズを表します。
        /// </summary>
        public uint cbSize;

        /// <summary>
        /// 取得または設定するメンバを表します。
        /// </summary>
        public uint fMask;

        /// <summary>
        /// アイテムのタイプを表します。
        /// このメンバを有効にするには <see cref="fMask"/> で <see cref="SystemMenuFactory.FMask.MIIM_FTYPE"/> を指定しておく必要があります。
        /// </summary>
        public uint fType;

        /// <summary>
        /// アイテムの状態を表します。
        /// このメンバを有効にするには <see cref="fMask"/> で <see cref="SystemMenuFactory.FMask.MIIM_STATE"/> を指定しておく必要があります。
        /// </summary>
        public uint fState;

        /// <summary>
        /// アイテムのIDを表します。
        /// このメンバを有効にするには <see cref="fMask"/> で <see cref="SystemMenuFactory.FMask.MIIM_ID"/> を指定しておく必要があります。
        /// </summary>
        public uint wID;

        /// <summary>
        /// サブメニューのハンドルを表します。
        /// このメンバを有効にするには <see cref="fMask"/> で <see cref="SystemMenuFactory.FMask.MIIM_SUBMENU"/> を指定しておく必要があります。
        /// </summary>
        public IntPtr hSubMenu;

        /// <summary>
        /// チェック表示時のビットマップを表します。
        /// このメンバを有効にするには <see cref="fMask"/> で <see cref="SystemMenuFactory.FMask.MIIM_CHECKMARKS"/> を指定しておく必要があります。
        /// <see cref="fType"/> で <see cref="SystemMenuFactory.FType.MFT_RADIOCHECK"/> が指定されている場合はラジオボタンになります。
        /// </summary>
        public IntPtr hbmpChecked;

        /// <summary>
        /// チェック非表示時のビットマップを表します。
        /// このメンバを有効にするには <see cref="fMask"/> で <see cref="SystemMenuFactory.FMask.MIIM_CHECKMARKS"/> を指定しておく必要があります。
        /// </summary>
        public IntPtr hbmpUnchecked;

        /// <summary>
        /// 任意の32ビット値を表します。
        /// </summary>
        public uint dwItemData;

        /// <summary>
        /// アイテムの内容を表します。
        /// このメンバの意味は <see cref="fType"/> メンバの値に依存します。
        /// このメンバを有効にするには <see cref="fMask"/> で <see cref="SystemMenuFactory.FMask.MIIM_TYPE "/> を指定しておく必要があります。
        /// </summary>
        public string dwTypeData;

        /// <summary>
        /// アイテムの文字列の長さを表します。
        /// このメンバを有効にするには <see cref="fMask"/> で <see cref="SystemMenuFactory.FMask.MIIM_TYPE "/> を指定しておく必要があります。
        /// </summary>
        public uint cch;

        /// <summary>
        /// ビットマップハンドルを表します。
        /// このメンバを有効にするには <see cref="fMask"/> で <see cref="SystemMenuFactory.FMask.MIIM_BITMAP "/> を指定しておく必要があります。
        /// </summary>
        public IntPtr hbmpItem;
    }
}