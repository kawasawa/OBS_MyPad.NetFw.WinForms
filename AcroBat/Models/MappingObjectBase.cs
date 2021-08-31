using System;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace AcroBat.Models
{
    /// <summary>
    /// プロパティ値をマッピングするためのオブジェクトの基底クラスを表します。
    /// </summary>
    [Serializable]
    public abstract class MappingObjectBase : ISerializable
    {
        /// <summary>
        /// 指定したオブジェクトから同名のプロパティの値を転記します。
        /// </summary>
        /// <param name="target">オブジェクト</param>
        public virtual void GetValue(object target) => Extensions.SetProperties(target, this);

        /// <summary>
        /// 指定したオブジェクトに同名のプロパティの値を転記します。
        /// </summary>
        /// <param name="target">オブジェクト</param>
        public virtual void SetValue(object target) => Extensions.SetProperties(this, target);

        /// <summary>
        /// <see cref="SerializationInfo"/> に、オブジェクトをシリアル化するために必要なデータを設定します。
        /// </summary>
        /// <param name="info">データを読み込む先の <see cref="SerializationInfo"/></param>
        /// <param name="context">このシリアル化のシリアル化先</param>
        [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
        void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
        {
            if (info == null)
            {
                throw new ArgumentNullException(nameof(info));
            }

            foreach (var p in this.GetType().GetProperties().Where(p => p.CanRead && p.CanWrite))
            {
                info.AddValue(p.Name, p.GetValue(this));
            }
        }
    }
}
