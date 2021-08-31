using System;

namespace AcroBat.WindowsAPI
{
    /// <summary>
    /// WindowsAPI イベントを処理するメソッドを表します。
    /// </summary>
    /// <param name="sender">イベントの発生源</param>
    /// <param name="e">イベントの情報</param>
    public delegate void WindowsAPIEventHandler(object sender, WindowsAPIEventArgs e);

    /// <summary>
    /// フックプロシージャを表します。
    /// </summary>
    /// <param name="nCode">フックコード</param>
    /// <param name="wParam">仮想コード</param>
    /// <param name="lParam">メッセージ情報</param>
    /// <returns>関数が成功すると、0 以外の値が返ります。</returns>
    public delegate int HOOKPROC(int nCode, IntPtr wParam, IntPtr lParam);
}
