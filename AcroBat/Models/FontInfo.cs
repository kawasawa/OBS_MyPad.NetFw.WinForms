using System;
using System.Drawing;
using System.Xml.Serialization;

namespace AcroBat.Models
{
    /// <summary>
    /// フォント情報を表します。
    /// </summary>
    [Serializable]
    public class FontInfo : IEquatable<FontInfo>
    {
        /// <summary>
        /// フォントを取得または設定します。
        /// </summary>
        [XmlIgnore]
        public Font Font { get; set; }

        /// <summary>
        /// フォント名を取得または設定します。
        /// </summary>
        public string Name
        {
            get { return this.Font.Name; }
            set
            {
                if (this.Font.Name != value)
                {
                    this.Font = new Font(value, this.Size);
                }
            }
        }

        /// <summary>
        /// フォントサイズを取得または設定します。
        /// </summary>
        public float Size
        {
            get { return this.Font.Size; }
            set
            {
                if (this.Font.Size != value)
                {
                    this.Font = new Font(this.Font.Name, value);
                }
            }
        }

        // <summary>
        /// インスタンスを初期化します。
        /// </summary>
        public FontInfo() : this(SystemFonts.DefaultFont)
        {
        }

        /// <summary>
        /// インスタンスを初期化します。
        /// </summary>
        /// <param name="font">フォント</param>
        public FontInfo(Font font)
        {
            this.Font = font;
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
            return this.Equals(obj as FontInfo);
        }

        /// <summary>
        /// 指定したオブジェクトが、現在のオブジェクトと等しいかどうかを判断します。
        /// </summary>
        /// <param name="other">比較するオブジェクト</param>
        /// <returns>等しいかどうかを示す値</returns>
        public bool Equals(FontInfo other)
        {
            if (other == null)
            {
                return false;
            }
            return this.Font.Equals(other.Font);
        }

        /// <summary>
        /// ハッシュ値を取得します。
        /// </summary>
        /// <returns>ハッシュ値</returns>
        public override int GetHashCode() => this.Font.GetHashCode();

        /// <summary>
        /// 現在のオブジェクトを表す文字列を返します。
        /// </summary>
        /// <returns>文字列</returns>
        public override string ToString() => this.Font.ToString();
    }
}
