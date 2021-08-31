using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace AcroBat.WindowsAPI
{
    /// <summary>
    /// システムメニューをカスタマイズするためのメソッドを提供します。
    /// </summary>
    public class SystemMenuCustomizer
    {
        /// <summary>
        /// システムメニューを操作するフォームを表します。
        /// </summary>
        private Form Form { get; } = null;

        /// <summary>
        /// メニューアイテムに紐づけられたメソッドを表します。
        /// </summary>
        private Dictionary<uint, EventHandler> Handler { get; } = new Dictionary<uint, EventHandler>();

        /// <summary>
        /// インスタンスを初期化します。
        /// </summary>
        /// <param name="form">フォーム</param>
        public SystemMenuCustomizer(Form form)
        {
            this.Form = form;
        }

        /// <summary>
        /// 末尾にメニューアイテムを追加します。
        /// </summary>
        /// <param name="lpmii">メニューアイテム</param>
        public void AddMenuItem(MENUITEMINFO lpmii) => this.AddMenuItem(lpmii, null);

        /// <summary>
        /// 末尾にメニューアイテムを追加します。
        /// </summary>
        /// <param name="lpmii">メニューアイテム</param>
        /// <param name="onClick">クリック時の処理</param>
        public void AddMenuItem(MENUITEMINFO lpmii, EventHandler onClick)
        {
            IntPtr hMenu = User32.GetSystemMenu(this.Form.Handle, false);
            unchecked
            {
                User32.InsertMenuItem(hMenu, (uint)-1, false, ref lpmii);
            }
            if (onClick != null)
            {
                this.Handler.Add(lpmii.wID, onClick);
            }
        }

        /// <summary>
        /// 指定された位置にメニューアイテムを挿入します。
        /// </summary>
        /// <param name="index">インデックス</param>
        /// <param name="lpmii">メニューアイテム</param>
        public void InsertMenuItem(uint index, MENUITEMINFO lpmii) => this.InsertMenuItem(index, lpmii, null);

        /// <summary>
        /// 指定された位置にメニューアイテムを挿入します。
        /// </summary>
        /// <param name="index">インデックス</param>
        /// <param name="lpmii">メニューアイテム</param>
        /// <param name="onClick">クリック時の処理</param>
        public void InsertMenuItem(uint index, MENUITEMINFO lpmii, EventHandler onClick)
        {
            IntPtr hMenu = User32.GetSystemMenu(this.Form.Handle, false);
            unchecked
            {
                User32.InsertMenuItem(hMenu, index, true, ref lpmii);
            }
            if (onClick != null)
            {
                this.Handler.Add(lpmii.wID, onClick);
            }
        }

        /// <summary>
        /// メニューアイテムに紐づけられたメソッドを呼び出します。
        /// </summary>
        /// <param name="m">メッセージ</param>
        /// <returns>メソッドを呼び出されたかどうかを示す値</returns>
        public bool InvokeMethod(ref Message m)
        {
            uint param = (uint)m.WParam.ToInt64();
            if (m.Msg != WindowsMessages.WM_SYSCOMMAND || this.Handler.ContainsKey(param) == false)
            {
                return false;
            }
            this.Handler[param].Method?.Invoke(this.Form, new[] { this.Form, null });
            return true;
        }
    }

    /// <summary>
    /// システムメニューアイテムを構築するためのファクトリークラスを表します。
    /// </summary>
    public static class SystemMenuFactory
    {
        /// <summary>
        /// 一意なIDを表します。
        /// </summary>
        private static uint ID = 0;

        /// <summary>
        /// 新しいインスタンスを取得します。
        /// </summary>
        /// <param name="text">テキスト</param>
        /// <returns>インスタンス</returns>
        public static MENUITEMINFO CreateMenuItem(string text)
            => new MENUITEMINFO
            {
                cbSize = (uint)Marshal.SizeOf(typeof(MENUITEMINFO)),
                fMask = FMask.MIIM_STATE | FMask.MIIM_ID | FMask.MIIM_STRING,
                fType = FType.MFT_MENUBARBREAK,
                fState = FState.MFS_ENABLED,
                wID = ++ID,
                hSubMenu = IntPtr.Zero,
                hbmpChecked = IntPtr.Zero,
                hbmpUnchecked = IntPtr.Zero,
                dwItemData = 0,
                dwTypeData = text,
                cch = 0,
                hbmpItem = IntPtr.Zero
            };

        /// <summary>
        /// 新しいインスタンスを取得します。
        /// </summary>
        /// <returns>インスタンス</returns>
        public static MENUITEMINFO CreateSeparater()
            => new MENUITEMINFO
            {
                cbSize = (uint)Marshal.SizeOf(typeof(MENUITEMINFO)),
                fMask = FMask.MIIM_FTYPE,
                fType = FType.MFT_SEPARATOR,
                fState = FState.MFS_ENABLED,
                wID = 0,
                hSubMenu = IntPtr.Zero,
                hbmpChecked = IntPtr.Zero,
                hbmpUnchecked = IntPtr.Zero,
                dwItemData = 0,
                dwTypeData = string.Empty,
                cch = 0,
                hbmpItem = IntPtr.Zero
            };
    }
}
