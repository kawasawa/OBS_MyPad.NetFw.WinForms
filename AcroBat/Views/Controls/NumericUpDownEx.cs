using System.ComponentModel;
using System.Windows.Forms;
using System;
using System.Drawing;

namespace AcroBat.Views.Controls
{
    /// <summary>
    /// 数値を表示するスピンボックスを表します。
    /// これは <see cref="NumericUpDown"/> を拡張したコントロールです。
    /// </summary>
    public class NumericUpDownEx : NumericUpDown
    {
        private bool _isValid = false;

        /// <summary>
        /// 入力値が有効あるかどうかを示す値を取得します。
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsValid
        {
            get { return this._isValid; }
            protected set
            {
                if (this._isValid != value)
                {
                    this._isValid = value;
                    this.OnValidStatusChanged(EventArgs.Empty);
                }
            }
        }

        /// <summary>
        /// 背景色を取得します。
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public new Color BackColor
        {
            get { return base.BackColor; }
            private set { base.BackColor = value; }
        }

        /// <summary>
        /// 入力値が有効な場合の背景色を取得または設定します。
        /// </summary>
        [Category(DesignerCategory.APPEARANCE)]
        [Description("入力値が有効な場合の背景色を取得または設定します。")]
        public Color ValidColor { get; set; } = StatusColors.Valid;

        /// <summary>
        /// 入力値が無効な場合の背景色を取得または設定します。
        /// </summary>
        [Category(DesignerCategory.APPEARANCE)]
        [Description("入力値が無効な場合の背景色を取得または設定します。")]
        public Color InValidColor { get; set; } = StatusColors.InValid;

        /// <summary>
        /// 上下キーの動作を反転させるかどうかを示す値を取得または設定します。
        /// </summary>
        [Category(DesignerCategory.BEHAVIOR)]
        [Description("上下キーの動作を反転させるかどうかを指定します。")]
        [DefaultValue(false)]
        public bool ReverseUpDown { get; set; } = false;

        /// <summary>
        /// 入力値の有効状態が変更されたときに発生します。
        /// </summary>
        [Category(DesignerCategory.PROPERTY_CHANGED)]
        [Description("入力値の有効状態が変更されたときに発生します。")]
        public event EventHandler ValidStatusChanged;

        /// <summary>
        /// インスタンスを初期化します。
        /// </summary>
        public NumericUpDownEx()
        {
            this.UpdateBackColor();
        }

        /// <summary>
        /// 背景色を更新します。
        /// </summary>
        protected void UpdateBackColor()
        {
            this.BackColor = this.IsValid ? this.ValidColor : this.InValidColor;
        }

        /// <summary>
        /// 上ボタンを押したときの処理を行います。
        /// </summary>
        public override void UpButton()
        {
            if (this.ReverseUpDown)
            {
                base.DownButton();
            }
            else
            {
                base.UpButton();
            }
        }

        /// <summary>
        /// 下ボタンが押されたときの処理を行います。
        /// </summary>
        public override void DownButton()
        {
            if (this.ReverseUpDown)
            {
                base.UpButton();
            }
            else
            {
                base.DownButton();
            }
        }

        /// <summary>
        /// <see cref="ValidStatusChanged"/> イベントを発生させます。
        /// </summary>
        /// <param name="e">イベント情報</param>
        protected virtual void OnValidStatusChanged(EventArgs e)
        {
            this.ValidStatusChanged?.Invoke(this, e);
            this.UpdateBackColor();
        }

        /// <summary>
        /// <see cref="Control.TextChanged"/> イベントを発生させます。
        /// </summary>
        /// <param name="e">イベント情報</param>
        protected override void OnTextChanged(EventArgs e)
        {
            base.OnTextChanged(e);

            decimal value;
            if (decimal.TryParse(this.Text, out value) &&
                this.Minimum <= value && value <= this.Maximum)
            {
                this.IsValid = true;
            }
            else
            {
                this.IsValid = false;
            }
        }

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
                case Keys.Enter:
                case Keys.Escape:
                    if (this.IsValid == false)
                    {
                        this.ValidateEditText();
                        return true;
                    }
                    break;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
