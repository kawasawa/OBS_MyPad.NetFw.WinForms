using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace AcroBat.Views.Controls
{
    /// <summary>
    /// 色を選択し表示できるコンボボックスを表します。
    /// </summary>
    public class ColorPicker : ComboBoxEx
    {
        private bool _visibleCustomColor = false;
        private readonly List<ColorItem> _colorItems = new List<ColorItem>()
        {
            { new ColorItem(Color.Black) },
            { new ColorItem(Color.White) },
            { new ColorItem(Color.Brown) },
            { new ColorItem(Color.Green) },
            { new ColorItem(Color.Olive) },
            { new ColorItem(Color.Navy) },
            { new ColorItem(Color.Purple) },
            { new ColorItem(Color.Teal) },
            { new ColorItem(Color.Silver) },
            { new ColorItem(Color.Gray) },
            { new ColorItem(Color.Red) },
            { new ColorItem(Color.Lime) },
            { new ColorItem(Color.Yellow) },
            { new ColorItem(Color.Blue) },
            { new ColorItem(Color.Magenta) },
            { new ColorItem(Color.Cyan) },
        };

        /// <summary>
        /// 描画の方法を取得します。
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public new DrawMode DrawMode
        {
            get { return base.DrawMode; }
            private set { base.DrawMode = value; }
        }

        /// <summary>
        /// コンボボックスのスタイルを取得します。
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public new ComboBoxStyle DropDownStyle
        {
            get { return base.DropDownStyle; }
            private set { base.DropDownStyle = value; }
        }

        /// <summary>
        /// 現在選択されている項目を取得または設定します。
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new ColorItem SelectedItem
        {
            get { return base.SelectedItem as ColorItem; }
            set
            {
                if (value == null)
                {
                    this.SelectedIndex = -1;
                    return;
                }

                if (this.Items.Contains(value) == false)
                {
                    this.SetCustomColor(value);
                }
                base.SelectedItem = value;
            }
        }

        /// <summary>
        /// 現在選択されている項目を示す値を取得または設定します。
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new Color SelectedValue
        {
            get { return this.SelectedItem?.Color ?? Color.Empty; }
            set
            {
                if (value.IsEmpty)
                {
                    this.SelectedIndex = -1;
                    return;
                }

                foreach (object item in this.Items)
                {
                    ColorItem colorItem = item as ColorItem;
                    if (colorItem == null ||
                        colorItem.Color.ToArgb() != value.ToArgb())
                    {
                        continue;
                    }
                    this.SelectedItem = colorItem;
                    return;
                }
                this.SelectedItem = new ColorItem(value.GetKnownColor());
            }
        }

        /// <summary>
        /// 現在選択されている項目を示すテキストを取得します。
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new string SelectedText => this.SelectedItem?.Text ?? string.Empty;

        /// <summary>
        /// カスタムカラーを指定しているインデックスを取得します。
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        protected int CustomColorIndex => this._colorItems.Count;

        /// <summary>
        /// インスタンスを初期化します。
        /// </summary>
        public ColorPicker()
        {
            this.InitializeItems();
            this.DrawMode = DrawMode.OwnerDrawFixed;
            this.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        /// <summary>
        /// コンボボックスの項目を初期化します。
        /// </summary>
        private void InitializeItems()
        {
            this.Items.Clear();
            this._colorItems.ForEach(c => this.Items.Add(c));
        }

        /// <summary>
        /// カスタムカラーを設定します。
        /// </summary>
        /// <param name="item">色情報</param>
        protected void SetCustomColor(ColorItem item)
        {
            if (this._visibleCustomColor == false)
            {
                this.Items.Add(item);
                this._visibleCustomColor = true;
            }
            else
            {
                this.Items[this.CustomColorIndex] = item;
            }
            this.RefreshItems();
        }

        /// <summary>
        /// カスタムカラーを削除します。
        /// </summary>
        protected void RemoveCustomColor()
        {
            if (this._visibleCustomColor)
            {
                this.Items.RemoveAt(this.CustomColorIndex);
                this._visibleCustomColor = false;
                this.RefreshItems();
            }
        }

        /// <summary>
        /// <see cref="ComboBox.DrawItem"/> を発生させます。
        /// </summary>
        /// <param name="e">イベントの情報</param>
        protected override void OnDrawItem(DrawItemEventArgs e)
        {
            e.DrawBackground();
            try
            {
                if (e.Index < 0)
                {
                    return;
                }

                ColorItem item = (ColorItem)this.Items[e.Index];
                Rectangle colorBoxRect = new Rectangle();
                colorBoxRect.X = e.Bounds.X + 2;
                colorBoxRect.Y = e.Bounds.Y + 2;
                colorBoxRect.Height = e.Bounds.Height - 5;
                colorBoxRect.Width = colorBoxRect.Height;
                float x = e.Bounds.X + colorBoxRect.X + colorBoxRect.Width + 5;
                float y = e.Bounds.Y + ((e.Bounds.Height - e.Font.Height) / 2);

                using (Brush colorBoxBrush = new SolidBrush(item.Color))
                {
                    e.Graphics.FillRectangle(colorBoxBrush, colorBoxRect);
                }
                e.Graphics.DrawRectangle(SystemPens.WindowText, colorBoxRect);

                if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
                {
                    e.Graphics.DrawString(item.Text, e.Font, SystemBrushes.HighlightText, x, y);
                }
                else
                {
                    e.Graphics.DrawString(item.Text, e.Font, SystemBrushes.WindowText, x, y);
                }
            }
            finally
            {
                e.DrawFocusRectangle();
                base.OnDrawItem(e);
            }
        }

        /// <summary>
        /// 色項目を表します。
        /// </summary>
        public class ColorItem : IEquatable<ColorItem>
        {
            /// <summary>
            /// 色を取得します。
            /// </summary>
            public Color Color { get; }

            /// <summary>
            /// 表示するテキストを取得します。
            /// </summary>
            public string Text { get; }

            /// <summary>
            /// インスタンスを初期化します。
            /// </summary>
            /// <param name="color">色</param>
            public ColorItem(Color color) : this(color, color.Name)
            {
            }

            /// <summary>
            /// インスタンスを初期化します。
            /// </summary>
            /// <param name="color">色</param>
            /// <param name="text">テキスト</param>
            public ColorItem(Color color, string text)
            {
                this.Color = color;
                this.Text = text;
            }

            /// <summary>
            /// 指定したオブジェクトが、現在のオブジェクトと等しいかどうかを判断します。
            /// </summary>
            /// <param name="obj">比較するオブジェクト</param>
            /// <returns>等しいかどうかを示す値</returns>
            public override bool Equals(object obj)
            {
                if (obj == null || this.GetType() != obj.GetType())
                {
                    return false;
                }
                return this.Equals(obj as ColorItem);    
            }

            /// <summary>
            /// 指定したオブジェクトが、現在のオブジェクトと等しいかどうかを判断します。
            /// </summary>
            /// <param name="other">比較するオブジェクト</param>
            /// <returns>等しいかどうかを示す値</returns>
            public bool Equals(ColorItem other)
            {
                if (other == null)
                {
                    return false;
                }
                return (this.Color.ToArgb() == other.Color.ToArgb()) && (this.Text.Equals(other.Text));
            }

            /// <summary>
            /// ハッシュ値を取得します。
            /// </summary>
            /// <returns>ハッシュ値</returns>
            public override int GetHashCode()
            {
                return this.Color.GetHashCode() ^ this.Text.GetHashCode();
            }

            /// <summary>
            /// 現在のオブジェクトを表す文字列を返します。
            /// </summary>
            /// <returns>文字列</returns>
            public override string ToString()
            {
                return this.Text;
            }
        }
    }
}
