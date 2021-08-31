using System;
using System.Runtime.InteropServices;

namespace AcroBat.WindowsAPI
{
    /// <summary>
    /// karnel32 に定義されたメソッドを呼び出します。
    /// </summary>
    public static class Karnel32
    {
        /// <summary>
        /// DLL 名を取得します。
        /// </summary>
        public const string DLL_NAME = "kernel32.dll";

        /// <summary>
        /// 呼び出し側スレッドのスレッド識別子を取得します。
        /// </summary>
        /// <returns>呼び出し側スレッドのスレッド識別子が返ります。</returns>
        [DllImport(DLL_NAME)]
        public static extern IntPtr GetCurrentThreadId();
    }
}
