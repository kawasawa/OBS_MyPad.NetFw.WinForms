using System;
using System.Runtime.InteropServices;

namespace AcroBat.WindowsAPI
{
    /// <summary>
    /// user32 に定義されたメソッドを呼び出します。
    /// </summary>
    public static class User32
    {
        /// <summary>
        /// DLL 名を取得します。
        /// </summary>
        public const string DLL_NAME = "user32.dll";
        
        /// <summary>
        /// 1 つまたは複数のウィンドウへ、指定されたメッセージを送信します。
        /// </summary>
        /// <param name="hWnd">送信先ウィンドウのハンドル</param>
        /// <param name="msg">メッセージ</param>
        /// <param name="wParam">メッセージの最初のパラメータ</param>
        /// <param name="lParam">メッセージの 2 番目のパラメータ</param>
        /// <returns>
        /// メッセージ処理の結果が返ります。
        /// この戻り値の意味は、送信されたメッセージにより異なります。
        /// </returns>
        [DllImport(DLL_NAME)]
        public static extern int SendMessage(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);

        /// <summary>
        /// 1 つまたは複数のウィンドウへ、<see cref="COPYDATASTRUCT"/> 型のデータを送信します。
        /// </summary>
        /// <param name="hWnd">送信先ウィンドウのハンドル</param>
        /// <param name="msg">メッセージ</param>
        /// <param name="wParam">メッセージの最初のパラメータ</param>
        /// <param name="lParam"><see cref="COPYDATASTRUCT"/> 型のデータ</param>
        /// <returns>
        /// メッセージ処理の結果が返ります。
        /// この戻り値の意味は、送信されたメッセージにより異なります。
        /// </returns>
        [DllImport(DLL_NAME)]
        public static extern int SendMessage(IntPtr hWnd, int msg, IntPtr wParam, ref COPYDATASTRUCT lParam);

        /// <summary>
        /// システム全体に関するパラメータのいずれかを取得または設定します。
        /// </summary>
        /// <param name="uiAction">取得または設定するべきシステムパラメータ</param>
        /// <param name="uiParam">実施するべき操作によって異なる</param>
        /// <param name="pvParam">実施するべき操作によって異なる</param>
        /// <param name="fWinIni">ユーザープロファイルの更新オプション</param>
        /// <returns>関数が成功すると、0 以外の値が返ります。</returns>
        [DllImport(DLL_NAME)]
        public static extern int SystemParametersInfo(uint uiAction, uint uiParam, bool pvParam, uint fWinIni);

        /// <summary>
        /// 指定されたウィンドウと指定された関係(またはオーナー)にあるウィンドウのハンドルを返します。
        /// </summary>
        /// <param name="hWnd">元ウィンドウのハンドル</param>
        /// <param name="uCmd">関係</param>
        /// <returns>
        /// 関数が成功すると、ウィンドウのハンドルが返ります。
        /// 指定した関係を持つウィンドウがない場合は、NULL が返ります。
        /// </returns>
        [DllImport(DLL_NAME, SetLastError = true)]
        public static extern IntPtr GetWindow(IntPtr hWnd, int uCmd);

        /// <summary>
        /// 指定されたウィンドウの表示状態、および通常表示のとき、最小化されたとき、最大化されたときの位置を返します。
        /// </summary>
        /// <param name="hWnd">ウィンドウのハンドル</param>
        /// <param name="lpwndpl">位置データ</param>
        /// <returns>関数が成功すると、0 以外の値が返ります。</returns>
        [DllImport(DLL_NAME)]
        public static extern bool GetWindowPlacement(IntPtr hWnd, ref WINDOWPLACEMENT lpwndpl);

        /// <summary>
        /// 指定されたウィンドウの左上端と右下端の座標をスクリーン座標で取得します。
        /// </summary>
        /// <param name="hWnd">ウィンドウのハンドル</param>
        /// <param name="lpRect">ウィンドウの座標値</param>
        /// <returns>関数が成功すると、0 以外の値が返ります。</returns>
        [DllImport(DLL_NAME)]
        public static extern int GetWindowRect(IntPtr hWnd, ref RECT lpRect);

        /// <summary>
        /// 指定されたウィンドウの表示状態を設定します。
        /// </summary>
        /// <param name="hWnd">ウィンドウのハンドル</param>
        /// <param name="nCmdShow">表示状態</param>
        /// <returns>ウィンドウが以前から表示されていた場合は、0 以外の値が返ります。</returns>
        [DllImport(DLL_NAME)]
        public static extern int ShowWindow(IntPtr hWnd, int nCmdShow);

        /// <summary>
        /// 指定されたウィンドウを作成したスレッドをフォアグラウンドにし、そのウィンドウをアクティブにします。
        /// </summary>
        /// <param name="hWnd">ウィンドウのハンドル</param>
        /// <returns>ウィンドウがフォアグラウンドになったら、0 以外の値が返ります。</returns>
        [DllImport(DLL_NAME)]
        public static extern int SetForegroundWindow(IntPtr hWnd);

        /// <summary>
        /// アプリケーション定義のフックプロシージャをフックチェーン内にインストールします。
        /// </summary>
        /// <param name="idHook">フックタイプ</param>
        /// <param name="lpfn">フックプロシージャ</param>
        /// <param name="hMod">アプリケーションインスタンスのハンドル</param>
        /// <param name="dwThreadId">スレッドの識別子</param>
        /// <returns>関数が成功すると、フックプロシージャのハンドルが返ります。</returns>
        [DllImport(DLL_NAME)]
        public static extern IntPtr SetWindowsHookEx(int idHook, HOOKPROC lpfn, IntPtr hMod, IntPtr dwThreadId);

        /// <summary>
        /// <see cref="SetWindowsHookEx"/> 関数を使ってフックチェーン内にインストールされたフックプロシージャを削除します。
        /// </summary>
        /// <param name="hhk">削除対象のフックプロシージャのハンドル</param>
        /// <returns>関数が成功すると、0 以外の値が返ります。</returns>
        [DllImport(DLL_NAME)]
        public static extern bool UnhookWindowsHookEx(IntPtr hhk);

        /// <summary>
        /// 現在のフックチェーン内の次のフックプロシージャに、フック情報を渡します。
        /// </summary>
        /// <param name="hhk">現在のフックのハンドル</param>
        /// <param name="nCode">フックプロシージャに渡すフックコード</param>
        /// <param name="wParam">フックプロシージャに渡す値</param>
        /// <param name="lParam">フックプロシージャに渡す値</param>
        /// <returns>フックチェーン内の次のフックプロシージャの戻り値が返ります。</returns>
        [DllImport(DLL_NAME)]
        public static extern int CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);

        /// <summary>
        /// ダイアログボックス内のコントロールのタイトルまたはテキストを設定します。
        /// </summary>
        /// <param name="hWnd">ダイアログボックスのハンドル</param>
        /// <param name="nIDDlgItem">コントロールの識別子</param>
        /// <param name="lpString">設定したいテキスト</param>
        /// <returns>関数が成功すると、0 以外の値が返ります。</returns>
        [DllImport(DLL_NAME)]
        public static extern bool SetDlgItemText(IntPtr hWnd, int nIDDlgItem, string lpString);

        /// <summary>
        /// ウィンドウメニューのハンドルを取得します。
        /// </summary>
        /// <param name="hWnd">ウィンドウのハンドル</param>
        /// <param name="bRevert">元へ戻すかどうか</param>
        /// <returns>
        /// bRevert パラメータが FALSE の場合、ウィンドウメニューが内部でコピーされ、コピー先のメニューハンドルが返ります。
        /// bRevert パラメータが TRUE の場合、NULL が返ります。
        /// </returns>
        [DllImport(DLL_NAME)]
        public static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);

        /// <summary>
        /// メニュー内の指定された位置に、新しいメニュー項目を挿入します。
        /// </summary>
        /// <param name="hMenu">メニューのハンドル</param>
        /// <param name="uItem">識別子または位置</param>
        /// <param name="fByPosition">uItem パラメータの意味</param>
        /// <param name="lpmii">メニュー項目の情報</param>
        /// <returns>関数が成功すると、0 以外の値が返ります。</returns>
        [DllImport(DLL_NAME)]
        public static extern bool InsertMenuItem(IntPtr hMenu, uint uItem, bool fByPosition, ref MENUITEMINFO lpmii);

        /// <summary>
        /// メニュー項目に関する情報を変更します。
        /// </summary>
        /// <param name="hMenu">メニューのハンドル</param>
        /// <param name="uItem">識別子または位置</param>
        /// <param name="fByPosition">uItem パラメータの意味</param>
        /// <param name="lpmii">メニュー項目の情報</param>
        /// <returns>関数が成功すると、0 以外の値が返ります。</returns>
        [DllImport(DLL_NAME)]
        public static extern bool SetMenuItemInfo(IntPtr hMenu, uint uItem, bool fByPosition, ref MENUITEMINFO lpmii);
    }
}
