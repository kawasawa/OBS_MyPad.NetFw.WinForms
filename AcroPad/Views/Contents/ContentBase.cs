using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace AcroPad.Views.Contents
{
    /// <summary>
    /// コンテンツの基底クラスを表します。
    /// このフォームは <see cref="DockContent"/> のラッパーです。
    /// </summary>
    public class ContentBase : DockContent
    {
        /// <summary>
        /// パネルに追加されているかどうかを示す値を取得します。
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsAdded => this.DockPanel != null;

        /// <summary>
        /// 自動的に隠す状態であるかどうかを示す値を取得します。
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsAutoHide
        {
            get
            {
                switch (this.DockState)
                {
                    case DockState.DockTopAutoHide:
                    case DockState.DockLeftAutoHide:
                    case DockState.DockBottomAutoHide:
                    case DockState.DockRightAutoHide:
                        return true;
                    default:
                        return false;
                }
            }
        }

        /// <summary>
        /// パネルの端に固定された状態であるかどうかを示す値を取得します。
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsAnchorDock
        {
            get
            {
                switch (this.DockState)
                {
                    case DockState.DockTop:
                    case DockState.DockLeft:
                    case DockState.DockBottom:
                    case DockState.DockRight:
                        return true;
                    default:
                        return false;
                }
            }
        }

        /// <summary>
        /// 自動的に隠すアクティブなコンテンツであるかどうかを示す値を取得または設定します。
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsActiveAutoHide
        {
            get { return this.Equals(this.DockPanel?.ActiveAutoHideContent); }
            set
            {
                if (this.IsAdded)
                {
                    this.DockPanel.ActiveAutoHideContent = value ? this : null;
                }
            }
        }

        /// <summary>
        /// アクティブなコンテンツであるかどうかを示す値を取得します。
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsActiveContent
        {
            get { return this.Equals(this.DockPanel?.ActiveContent); }
        }

        /// <summary>
        /// タブに表示されるテキストを取得または設定します。
        /// </summary>
        [Description("タブに表示されるテキストを指定します。")]
        public new string TabText
        {
            get { return base.TabText; }
            set
            {
                if (base.TabText != value)
                {
                    base.TabText = value;
                    this.OnTabTextChanged(EventArgs.Empty);
                }
            }
        }

        /// <summary>
        /// タブのテキストが変更されたときに発生します。
        /// </summary>
        [Description("タブのテキストが変更されたときに発生します。")]
        public event EventHandler TabTextChanged;

        /// <summary>
        /// コンテンツに配置するコントロールの基底となるパネルを表します。
        /// </summary>
        protected readonly ContentBasePanel BasePanel = new ContentBasePanel();

        /// <summary>
        /// インスタンスを初期化します。
        /// </summary>
        public ContentBase()
        {
            this.Controls.Add(this.BasePanel);
        }

        /// <summary>
        /// コンテンツを選択します。
        /// </summary>
        public new virtual void Select()
        {
            this.Activate();
        }

        /// <summary>
        /// コンテンツをアクティブにします。
        /// </summary>
        protected new void Activate()
        {
            if (this.IsHidden)
            {
                this.Show();
            }
            if (this.IsAutoHide)
            {
                this.DockPanel.Parent?.Select();
                this.IsActiveAutoHide = true;
            }
            base.Activate();
            this.Focus();
        }

        /// <summary>
        /// コンテンツを固定された状態にします。
        /// </summary>
        public virtual void ToAnchorDock()
        {
            if (this.IsAnchorDock)
            {
                return;
            }

            switch (this.DockState)
            {
                case DockState.DockTopAutoHide:
                    this.DockState = DockState.DockTop;
                    break;
                case DockState.DockLeftAutoHide:
                    this.DockState = DockState.DockLeft;
                    break;
                case DockState.DockBottomAutoHide:
                    this.DockState = DockState.DockBottom;
                    break;
                case DockState.DockRightAutoHide:
                    this.DockState = DockState.DockRight;
                    break;
                default:
                    return;
            }
        }

        /// <summary>
        /// コンテンツを自動的に隠す状態にします。
        /// </summary>
        public virtual void ToAutoHide()
        {
            if (this.IsAnchorDock == false)
            {
                return;
            }

            switch (this.DockState)
            {
                case DockState.DockTop:
                    this.DockState = DockState.DockTopAutoHide;
                    break;
                case DockState.DockLeft:
                    this.DockState = DockState.DockLeftAutoHide;
                    break;
                case DockState.DockBottom:
                    this.DockState = DockState.DockBottomAutoHide;
                    break;
                case DockState.DockRight:
                    this.DockState = DockState.DockRightAutoHide;
                    break;
                default:
                    return;
            }
        }

        /// <summary>
        /// <see cref="TabTextChanged"/> イベントを発生させます。
        /// </summary>
        /// <param name="e">イベント情報</param>
        protected virtual void OnTabTextChanged(EventArgs e) => this.TabTextChanged?.Invoke(this, e);

        /// <summary>
        /// コマンドキーを処理します。
        /// </summary>
        /// <param name="msg">メッセージ</param>
        /// <param name="keyData">キーデータ</param>
        /// <returns>コマンドキーが処理されたかどうかを示す値</returns>
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Escape:
                    if (this.IsActiveAutoHide == false)
                    {
                        break;
                    }
                    this.IsActiveAutoHide = false;
                    return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        /// <summary>
        /// コンテンツに配置するコントロールの基底となるパネルを表します。
        /// </summary>
        public class ContentBasePanel : Panel
        {
            /// <summary>
            /// フォントを取得します。
            /// </summary>
            [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
            public new Font Font
            {
                get { return base.Font; }
                private set { base.Font = value; }
            }

            /// <summary>
            /// ドッキング状態を取得します。
            /// </summary>
            [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
            public new DockStyle Dock
            {
                get { return base.Dock; }
                private set { base.Dock = value; }
            }

            /// <summary>
            /// インスタンスを初期化します。
            /// </summary>
            public ContentBasePanel()
            {
                this.Font = SystemFonts.MenuFont;
                this.Dock = DockStyle.Fill;
            }
        }
    }
}
