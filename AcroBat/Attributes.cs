using System;

namespace AcroBat
{
    /// <summary>
    /// <see cref="Extensions.SetProperty{TSource, TTarget}(TSource, TTarget, System.Reflection.PropertyInfo)"/> メソッドに対して、
    /// パブリックプロパティの値を設定しないように指示します。
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public class SetPropertyIgnoreAttribute : Attribute
    {
        /// <summary>
        /// インスタンスを初期化します。
        /// </summary>
        public SetPropertyIgnoreAttribute()
        {
        }
    }
}
