using AcroBat;
using AcroBat.Views;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace AcroPad.Views.Controls
{
    /// <summary>
    /// ドッキングパネルを表します。
    /// このコントロールは <see cref="DockPanel"/> のラッパーです。
    /// </summary>
    public class DockingPanel : DockPanel
    {
        private readonly ThemeBase _vs2005Theme = new VS2005Theme();
        private readonly ThemeBase _vs2012Theme = new VS2012LightTheme();

        /// <summary>
        /// コントロールのテーマを取得します。
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new ThemeBase Theme
        {
            get { return base.Theme; }
            private set { base.Theme = value; }
        }

        /// <summary>
        /// ドキュメントタブの表示位置を取得します。
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public new DocumentTabStripLocation DocumentTabStripLocation
        {
            get { return base.DocumentTabStripLocation; }
            private set { base.DocumentTabStripLocation = value; }
        }

        /// <summary>
        /// ドキュメントスタイルを取得します。
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public new DocumentStyle DocumentStyle
        {
            get { return base.DocumentStyle; }
            private set { base.DocumentStyle = value; }
        }

        /// <summary>
        /// テーマの種類を取得または設定します。
        /// </summary>
        [Category(DesignerCategory.APPEARANCE)]
        [DefaultValue(ThemeKind.Modern)]
        [Description("テーマの種類を指定します。")]
        public ThemeKind ThemeMode
        {
            get
            {
                if (this.Theme.Equals(this._vs2012Theme))
                {
                    return ThemeKind.Modern;
                }
                else
                {
                    return ThemeKind.Classic;
                }
            }
            set
            {
                switch (value)
                {
                    case ThemeKind.Modern:
                        this.Theme = this._vs2012Theme;
                        break;
                    case ThemeKind.Classic:
                        this.Theme = this._vs2005Theme;
                        break;
                }
            }
        }

        /// <summary>
        /// タブを底に表示するかどうかを示す値を取得または設定します。
        /// </summary>
        [Category(DesignerCategory.BEHAVIOR)]
        [DefaultValue(false)]
        [Description("タブを底に表示するかどうかを指定します。")]
        public bool ShowDocumentTabOnBottom
        {
            get
            {
                switch (this.DocumentTabStripLocation)
                {
                    case DocumentTabStripLocation.Bottom:
                        return true;
                    default:
                        return false;
                }
            }
            set
            {
                this.DocumentTabStripLocation = value ? DocumentTabStripLocation.Bottom : DocumentTabStripLocation.Top;
            }
        }

        /// <summary>
        /// ドキュメントが一つだけの場合にタブを表示するかどうかを示す値を取得または設定します。
        /// </summary>
        [Category(DesignerCategory.BEHAVIOR)]
        [DefaultValue(true)]
        [Description("ドキュメントが一つだけの場合にタブを表示するかどうかを指定します。")]
        public bool ShowDocumentTabOnlyOne
        {
            get
            {
                switch (this.DocumentStyle)
                {
                    case DocumentStyle.DockingMdi:
                    case DocumentStyle.DockingWindow:
                    case DocumentStyle.SystemMdi:
                        return true;
                    default:
                        return false;
                }
            }
            set
            {
                this.DocumentStyle = value ? DocumentStyle.DockingMdi : DocumentStyle.DockingSdi;
            }
        }

        /// <summary>
        /// インスタンスを初期化します。
        /// </summary>
        public DockingPanel()
        {
            this.ThemeMode = ThemeKind.Modern;
            this.DocumentStyle = DocumentStyle.DockingMdi;
            this.DocumentTabStripLocation = DocumentTabStripLocation.Top;
            this.Extender.FloatWindowFactory = new FloatingWindowFactory();
        }

        /// <summary>
        /// 指定したドキュメントを追加します。
        /// </summary>
        /// <param name="document">ドキュメント</param>
        public void AddDocument(DockContent document)
        {
            this.AddContent(document, DockState.Document);
        }

        /// <summary>
        /// 指定したコンテンツを指定した状態で追加します。
        /// </summary>
        /// <param name="content">コンテンツ</param>
        /// <param name="dockState">ドッキングの状態</param>
        public void AddContent(DockContent content, DockState dockState)
        {
            content.Show(this, dockState);
        }

        /// <summary>
        /// 指定した型のコンテンツをすべて取得します。
        /// </summary>
        /// <typeparam name="T">取得するコンテンツの型</typeparam>
        /// <returns>コンテンツのリスト</returns>
        public List<T> GetContents<T>()
            where T : IDockContent
        {
            return this.Contents.Where(c => c is T).Cast<T>().ToList();
        }
        
        /// <summary>
        /// 一つ後のコンテンツをアクティブにします。
        /// </summary>
        /// <typeparam name="T">コンテンツの型</typeparam>
        public void ActivateNextContent<T>()
            where T : IDockContent
        {
            // アクティブなコンテンツを取得する
            int paneIndex = this.GetActivePaneIndex();
            if (paneIndex < 0)
            {
                return;
            }
            int contentIndex = this.GetActiveContentIndex(paneIndex);
            if (contentIndex < 0)
            {
                return;
            }

            // 次のコンテンツをアクティブにする
            if (this.ActivateNextContent<T>(paneIndex, contentIndex + 1))
            {
                return;
            }

            // 次のペインの先頭のコンテンツをアクティブにする
            int start = this.GetNextPaneIndex(paneIndex);
            if (start < 0)
            {
                return;
            }
            int current = start;
            do
            {
                if (this.ActivateNextContent<T>(current, 0))
                {
                    break;
                }
                current = this.GetNextPaneIndex(current);
            }
            while (current != start);
        }

        /// <summary>
        /// ペイン内のコンテンツを走査し、最初に見つかった妥当なコンテンツをアクティブにします。
        /// </summary>
        /// <typeparam name="T">コンテンツの型</typeparam>
        /// <param name="paneIndex">ペインのインデックス</param>
        /// <param name="startContentIndex">走査を始めるコンテンツのインデックス</param>
        /// <returns>アクティブにされたかどうかを示す値</returns>
        private bool ActivateNextContent<T>(int paneIndex, int startContentIndex)
        {
            // コンテンツの状態がペインの状態と等しければ
            // そのコンテンツはペインの中に含まれている
            for (int i = startContentIndex; i < this.Panes[paneIndex].Contents.Count; i++)
            {
                if (this.Panes[paneIndex].Contents[i] is T &&
                    this.Panes[paneIndex].DockState == this.Panes[paneIndex].Contents[i].DockHandler.DockState)
                {
                    this.Panes[paneIndex].Contents[i].DockHandler.Activate();
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 一つ前のコンテンツをアクティブにします。
        /// </summary>
        /// <typeparam name="T">コンテンツの型</typeparam>
        public void ActivatePreviousContent<T>()
        {
            // アクティブなコンテンツを取得する
            int paneIndex = this.GetActivePaneIndex();
            if (paneIndex < 0)
            {
                return;
            }
            int contentIndex = this.GetActiveContentIndex(paneIndex);
            if (contentIndex < 0)
            {
                return;
            }

            // 前のコンテンツをアクティブにする
            if (this.ActivatePreviousContent<T>(paneIndex, contentIndex - 1))
            {
                return;
            }

            // 前のペインの最後のコンテンツをアクティブにする
            int startIndex = this.GetPreviousPaneIndex(paneIndex);
            if (startIndex < 0)
            {
                return;
            }
            int current = startIndex;
            do
            {
                if (this.ActivatePreviousContent<T>(current, this.Panes[current].Contents.Count - 1))
                {
                    break;
                }
                current = GetPreviousPaneIndex(current);
            }
            while (current != startIndex);
        }

        /// <summary>
        /// ペイン内のコンテンツを走査し、最初に見つかった妥当なコンテンツをアクティブにします。
        /// </summary>
        /// <typeparam name="T">コンテンツの型</typeparam>
        /// <param name="paneIndex">ペインのインデックス</param>
        /// <param name="startContentIndex">走査を始めるコンテンツのインデックス</param>
        /// <returns>アクティブにされたかどうかを示す値</returns>
        private bool ActivatePreviousContent<T>(int paneIndex, int startContentIndex)
        {
            // コンテンツの状態がペインの状態と等しければ
            // そのコンテンツはペインの中に含まれている
            for (int i = startContentIndex; 0 <= i; i--)
            {
                if (this.Panes[paneIndex].Contents[i] is T &&
                    this.Panes[paneIndex].DockState == this.Panes[paneIndex].Contents[i].DockHandler.DockState)
                {
                    this.Panes[paneIndex].Contents[i].DockHandler.Activate();
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// アクティブなペインのインデックスを取得します。
        /// </summary>
        /// <returns>アクティブなペインのインデックス</returns>
        private int GetActivePaneIndex()
        {
            for (int i = 0; i < this.Panes.Count; i++)
            {
                if (this.Panes[i].IsActivated)
                {
                    return i;
                }
            }
            return -1;
        }

        /// <summary>
        /// 指定したペイン内のアクティブなコンテンツのインデックスを取得します。
        /// </summary>
        /// <param name="paneIndex">ペインのインデックス</param>
        /// <returns>アクティブなコンテンツのインデックス</returns>
        private int GetActiveContentIndex(int paneIndex)
        {
            for (int i = 0; i < this.Panes[paneIndex].Contents.Count; i++)
            {
                if (this.Panes[paneIndex].Contents[i].DockHandler.IsActivated)
                {
                    return i;
                }
            }
            return -1;
        }

        /// <summary>
        /// ペインを走査し、最初に見つかった妥当なペインのインデックスを取得します。
        /// </summary>
        /// <param name="startPaneIndex">開始インデックス</param>
        /// <returns>インデックス</returns>
        private int GetNextPaneIndex(int startPaneIndex)
        {
            int i = startPaneIndex + 1;
            if (this.Panes.Count <= i)
            {
                i = 0;
            }
            while (this.Panes[i].IsHidden)
            {
                i++;
                if (this.Panes.Count <= i)
                {
                    i = 0;
                }

                if (i == startPaneIndex)
                {
                    break;
                }
            }

            // 表示状態であれば妥当なペインとする
            return this.Panes[i].IsHidden ? int.MinValue : i;
        }

        /// <summary>
        /// ペインを走査し、最初に見つかった妥当なペインのインデックスを取得します。
        /// </summary>
        /// <param name="startPaneIndex">開始インデックス</param>
        /// <returns>インデックス</returns>
        private int GetPreviousPaneIndex(int startPaneIndex)
        {
            int i = startPaneIndex - 1;
            if (i < 0)
            {
                i = this.Panes.Count - 1;
            }

            while (this.Panes[i].IsHidden)
            {
                i--;
                if (i < 0)
                {
                    i = this.Panes.Count - 1;
                }

                if (i == startPaneIndex)
                {
                    break;
                }
            }

            // 表示状態であれば妥当なペインとする
            return this.Panes[i].IsHidden ? int.MinValue : i;
        }

        /// <summary>
        /// フローティングウィンドウを表します。
        /// これは <see cref="FloatWindow"/> を拡張したコントロールです。
        /// </summary>
        private class FloatingWindow : FloatWindow
        {
            public FloatingWindow(DockPanel dockPanel, DockPane pane)
                : base(dockPanel, pane)
            {
                this.InitializeComponent();
            }

            public FloatingWindow(DockPanel dockPanel, DockPane pane, Rectangle bounds)
                : base(dockPanel, pane, bounds)
            {
                this.InitializeComponent();
            }

            private void InitializeComponent()
            {
                this.ShowInTaskbar = false;
                this.ShowIcon = false;
                this.MinimizeBox = false;
                this.MaximizeBox = false;
                this.SizeGripStyle = SizeGripStyle.Show;
                this.FormBorderStyle = FormBorderStyle.Sizable;
            }

            protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
            {
                if ((this.DockPanel.Parent as IControlProcess)?.RaiseProcessCmdKey(ref msg, keyData) == true)
                {
                    return true;
                }
                return base.ProcessCmdKey(ref msg, keyData);
            }
        }

        /// <summary>
        /// フローティングウィンドウを生成するためのファクトリークラスを表します。
        /// </summary>
        private class FloatingWindowFactory : DockPanelExtender.IFloatWindowFactory
        {
            public FloatWindow CreateFloatWindow(DockPanel dockPanel, DockPane pane) => new FloatingWindow(dockPanel, pane);
            public FloatWindow CreateFloatWindow(DockPanel dockPanel, DockPane pane, Rectangle bounds) => new FloatingWindow(dockPanel, pane, bounds);
        }
    }
}
