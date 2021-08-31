using AcroBat;
using AcroBat.Models;
using System;
using System.Reflection;
using System.Windows.Forms;

namespace AcroPad.Models.Entity
{
    /// <summary>
    /// 設定を保管するための基底クラスを表します。
    /// </summary>
    public abstract class SettingBase : MappingObjectBase
    {
        /// <summary>
        /// 指定したオブジェクトから同名のプロパティの値を転記します。
        /// </summary>
        /// <param name="target">オブジェクト</param>
        public override void GetValue(object target)
        {
            base.GetValue(target);
            ContainerControl container = target as ContainerControl;
            if (container != null)
            {
                this.GetInnerControlValue(container);
            }
        }

        /// <summary>
        /// 指定したオブジェクトに同名のプロパティの値を転記します。
        /// </summary>
        /// <param name="target">オブジェクト</param>
        public override void SetValue(object target)
        {
            base.SetValue(target);
            ContainerControl container = target as ContainerControl;
            if (container != null)
            {
                this.SetInnerControlValue(container);
            }
        }

        /// <summary>
        /// 指定したコンテナ内のコントロールから設定情報を取得します。
        /// </summary>
        /// <param name="container">コンテナ</param>
        public virtual void GetInnerControlValue(ContainerControl container)
        {
            foreach (PropertyInfo property in this.GetType().GetProperties())
            {
                object value = property.GetValue(this);
                if (value is bool)
                {
                    FieldInfo field = container.GetType().GetField($"{ControlNamePrefix.BOOLEAN_MAPPED}{property.Name}",
                                                                   BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly);
                    CheckBox control = field?.GetValue(container) as CheckBox;
                    if (control == null)
                    {
                        continue;
                    }
                    property.SetValue(this, control.Checked);
                }
                else if (this.IsInteger(value))
                {
                    FieldInfo field = container.GetType().GetField($"{ControlNamePrefix.NUMERIC_MAPPED}{property.Name}",
                                                                   BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly);
                    NumericUpDown control = field?.GetValue(container) as NumericUpDown;
                    if (control == null)
                    {
                        continue;
                    }
                    property.SetValue(this, Convert.ChangeType(control.Value, value.GetType()));
                }
                else if (value is Enum)
                {
                    FieldInfo field = container.GetType().GetField($"{ControlNamePrefix.ENUM_MAPPED}{property.Name}",
                                                                   BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly);
                    ComboBox control = field?.GetValue(container) as ComboBox;
                    if (control == null)
                    {
                        continue;
                    }
                    property.SetValue(this, Convert.ChangeType(control.SelectedItem, value.GetType()));
                }
            }
        }

        /// <summary>
        /// 指定したコンテナ内のコントロールに設定情報を適用します。
        /// </summary>
        /// <param name="container">コンテナ</param>
        public virtual void SetInnerControlValue(ContainerControl container)
        {
            foreach (PropertyInfo property in this.GetType().GetProperties())
            {
                object value = property.GetValue(this);
                if (value is bool)
                {
                    FieldInfo field = container.GetType().GetField($"{ControlNamePrefix.BOOLEAN_MAPPED}{property.Name}",
                                                                   BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly);
                    CheckBox control = field?.GetValue(container) as CheckBox;
                    if (control == null)
                    {
                        continue;
                    }
                    field.FieldType.GetProperty(nameof(CheckBox.Checked)).SetValue(control, Convert.ChangeType(value, typeof(bool)));
                }
                else if (this.IsInteger(value))
                {
                    FieldInfo field = container.GetType().GetField($"{ControlNamePrefix.NUMERIC_MAPPED}{property.Name}",
                                                                   BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly);
                    NumericUpDown control = field?.GetValue(container) as NumericUpDown;
                    if (control == null)
                    {
                        continue;
                    }
                    field.FieldType.GetProperty(nameof(NumericUpDown.Value)).SetValue(control, Convert.ChangeType(value, typeof(decimal)));
                }
                else if (value is Enum)
                {
                    FieldInfo field = container.GetType().GetField($"{ControlNamePrefix.ENUM_MAPPED}{property.Name}",
                                                                   BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly);
                    ComboBox control = field?.GetValue(container) as ComboBox;
                    if (control == null)
                    {
                        continue;
                    }
                    field.FieldType.GetProperty(nameof(ComboBox.SelectedItem)).SetValue(control, Convert.ChangeType(value, typeof(object)));
                }
            }
        }

        /// <summary>
        /// 指定したオブジェクトが整数型であるかどうかを判定します。
        /// </summary>
        /// <param name="obj">オブジェクト</param>
        /// <returns>整数型であるかどうかを示す値</returns>
        protected bool IsInteger(object obj)
            => obj is sbyte ||
               obj is byte ||
               obj is short ||
               obj is ushort ||
               obj is int ||
               obj is uint ||
               obj is long ||
               obj is ulong;
    }
}
