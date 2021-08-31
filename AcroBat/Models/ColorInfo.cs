using System;
using System.ComponentModel;
using System.Drawing;
using System.Xml.Serialization;

namespace AcroBat.Models
{
    /// <summary>
    /// カラー情報を表します。
    /// </summary>
    [Serializable]
    public class ColorInfo : IEquatable<ColorInfo>
    {
        /// <summary>
        /// カラーを取得または設定します。
        /// </summary>
        [XmlIgnore]
        [RefreshProperties(RefreshProperties.Repaint)]
        public Color Color { get; set; }

        /// <summary>
        /// HTMLカラーを取得または設定します。
        /// </summary>
        public string Html
        {
            get { return string.Format("#{0:X2}{1:X2}{2:X2}{3:X2}", this.Color.A, this.Color.R, this.Color.G, this.Color.B); }
            set
            {
                Color color = ColorTranslator.FromHtml(value);
                if (this.Color != color)
                {
                    this.Color = color;
                }
            }
        }

        /// <summary>
        /// インスタンスを初期化します。
        /// </summary>
        public ColorInfo() : this(Color.Empty)
        {
        }

        /// <summary>
        /// インスタンスを初期化します。
        /// </summary>
        /// <param name="color">カラー</param>
        public ColorInfo(Color color)
        {
            this.Color = color;
        }

        /// <summary>
        /// インスタンスを初期化します。
        /// </summary>
        /// <param name="html">HTMLカラー</param>
        public ColorInfo(string html)
        {
            this.Html = html;
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
            return this.Equals(obj as ColorInfo);
        }

        /// <summary>
        /// 指定したオブジェクトが、現在のオブジェクトと等しいかどうかを判断します。
        /// </summary>
        /// <param name="other">比較するオブジェクト</param>
        /// <returns>等しいかどうかを示す値</returns>
        public bool Equals(ColorInfo other)
        {
            if (other == null)
            {
                return false;
            }
            return this.Color.Equals(other.Color);
        }

        /// <summary>
        /// ハッシュ値を取得します。
        /// </summary>
        /// <returns>ハッシュ値</returns>
        public override int GetHashCode() => this.Color.GetHashCode();

        /// <summary>
        /// 現在のオブジェクトを表す文字列を返します。
        /// </summary>
        /// <returns>文字列</returns>
        public override string ToString() => this.Color.ToString();
    }
}