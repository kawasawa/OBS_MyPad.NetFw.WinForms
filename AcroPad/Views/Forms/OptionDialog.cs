using AcroBat;
using AcroBat.Views.Forms;
using AcroPad.Models;
using AcroPad.Models.Entity;
using AcroPad.Properties;
using System;
using System.Drawing;
using System.Drawing.Text;
using System.Reflection;
using System.Windows.Forms;

namespace AcroPad.Views.Forms
{
    /// <summary>
    /// オプションダイアログを表します。
    /// </summary>
    public partial class OptionDialog : FixedSizeDialogBase
    {
        private const decimal MIN_FONT_SIZE = 6;
        private const decimal MAX_FONT_SIZE = 72;
        private const decimal MIN_TAB_WIDTH = 1;
        private const decimal MAX_TAB_WIDTH = 64;
        private const decimal MIN_WORD_WRAP_DIGIT = 20;
        private const decimal MAX_WORD_WRAP_DIGIT = 8000;

        /// <summary>
        /// インスタンスを初期化します。
        /// </summary>
        public OptionDialog()
        {
            this.InitializeComponent();
            this.InitializeItems();
            this.AddEventHandler();

            this.AcceptButton = this.btnOK;
            this.CancelButton = this.btnCancel;
            this.btnOK.DialogResult = DialogResult.OK;
            this.btnCancel.DialogResult = DialogResult.Cancel;

            this.textEditor.Text = Resources.VAL_OPTION_PREVIEW;
            this.textEditor.IsDirty = false;
            this.textEditor.ClearHistory();
        }

        /// <summary>
        /// 特定のコントロールを初期化します。
        /// </summary>
        private void InitializeItems()
        {
            this.nmapFontSize.Minimum = MIN_FONT_SIZE;
            this.nmapFontSize.Maximum = MAX_FONT_SIZE;
            this.nmapTabWidth.Minimum = MIN_TAB_WIDTH;
            this.nmapTabWidth.Maximum = MAX_TAB_WIDTH;
            this.nmapWordWrapDigit.Minimum = MIN_WORD_WRAP_DIGIT;
            this.nmapWordWrapDigit.Maximum = MAX_WORD_WRAP_DIGIT;

            new InstalledFontCollection().Families.ForEach(f => this.emapFontName.Items.Add(f.Name));
            Enum.GetValues(typeof(LanguageKind)).ForEach<LanguageKind>(m => this.emapLanguageMode.Items.Add(m));
            Enum.GetValues(typeof(EncodingKind)).ForEach<EncodingKind>(m => this.emapEncodingMode.Items.Add(m));
            Enum.GetValues(typeof(EolKind)).ForEach<EolKind>(m => this.emapEolMode.Items.Add(m));
            Enum.GetValues(typeof(WordWrapKind)).ForEach<WordWrapKind>(m => this.emapWordWrapMode.Items.Add(m));
            Enum.GetValues(typeof(ColorSettingsKind)).ForEach<ColorSettingsKind>(m =>
            {
                switch (m)
                {
                    case ColorSettingsKind.テキスト:
                        this.lstColor.Items.Add(
                            new ColorMappingObject(m, 
                                                   nameof(TextEditorSetting.ForeColor),
                                                   nameof(TextEditorSetting.BackColor)));
                        break;
                    case ColorSettingsKind.選択範囲:
                        this.lstColor.Items.Add(
                            new ColorMappingObject(m,
                                                   nameof(TextEditorSetting.SelectionForeColor),
                                                   nameof(TextEditorSetting.SelectionBackColor)));
                        break;
                    case ColorSettingsKind.検索結果:
                        this.lstColor.Items.Add(
                            new ColorMappingObject(m,
                                                   string.Empty,
                                                   nameof(TextEditorSetting.SearchPatternsBackColor)));
                        break;
                    case ColorSettingsKind.括弧:
                        this.lstColor.Items.Add(
                            new ColorMappingObject(m,
                                                   nameof(TextEditorSetting.MatchedBracketForeColor),
                                                   nameof(TextEditorSetting.MatchedBracketBackColor)));
                        break;
                    case ColorSettingsKind.行番号:
                        this.lstColor.Items.Add(
                            new ColorMappingObject(m,
                                                   nameof(TextEditorSetting.LineNumberForeColor),
                                                   string.Empty));
                        break;
                    case ColorSettingsKind.編集済み:
                        this.lstColor.Items.Add(
                            new ColorMappingObject(m,
                                                   nameof(TextEditorSetting.DirtyLineBarColor),
                                                   string.Empty));
                        break;
                    case ColorSettingsKind.保存済み:
                        this.lstColor.Items.Add(
                            new ColorMappingObject(m,
                                                   nameof(TextEditorSetting.CleanedLineBarColor),
                                                   string.Empty));
                        break;
                    case ColorSettingsKind.現在行:
                        this.lstColor.Items.Add(
                            new ColorMappingObject(m,
                                                   nameof(TextEditorSetting.HighlightColor),
                                                   string.Empty));
                        break;
                    case ColorSettingsKind.折り返し:
                        this.lstColor.Items.Add(
                            new ColorMappingObject(m,
                                                   nameof(TextEditorSetting.RightEdgeColor),
                                                   string.Empty));
                        break;
                    case ColorSettingsKind.空白:
                        this.lstColor.Items.Add(
                            new ColorMappingObject(m,
                                                   nameof(TextEditorSetting.WhiteSpaceColor),
                                                   string.Empty));
                        break;
                    case ColorSettingsKind.改行:
                        this.lstColor.Items.Add(
                            new ColorMappingObject(m,
                                                   nameof(TextEditorSetting.EolColor),
                                                   string.Empty));
                        break;
                    case ColorSettingsKind.末尾:
                        this.lstColor.Items.Add(
                            new ColorMappingObject(m,
                                                   nameof(TextEditorSetting.EofColor),
                                                   string.Empty));
                        break;
                    case ColorSettingsKind.エリア外:
                        this.lstColor.Items.Add(
                            new ColorMappingObject(m,
                                                   string.Empty,
                                                   nameof(TextEditorSetting.LineNumberBackColor)));
                        break;
                }
            });
        }

        /// <summary>
        /// イベントハンドラを追加します。
        /// </summary>
        private void AddEventHandler()
        {
            this.Load += this.Form_Load;
            this.FormClosed += this.Form_FormClosed;
            this.btnReset.Click += this.BtnReset_Click;
            this.lstColor.SelectedIndexChanged += this.LstColor_SelectedIndexChanged;
            this.btnForeColor.Click += this.BtnForeColor_Click;
            this.btnBackColor.Click += this.BtnBackColor_Click;
        }

        /// <summary>
        /// イベントハンドラを追加します。
        /// </summary>
        private void AddEventHandlerOnFormLoad()
        {
            foreach (TabPage page in this.tabControl1.TabPages)
            {
                this.AddEventHandlerOnFormLoad(page);
            }
        }

        /// <summary>
        /// イベントハンドラを追加します。
        /// </summary>
        /// <param name="parent">親コントロール</param>
        private void AddEventHandlerOnFormLoad(Control parent)
        {
            foreach (Control control in parent.Controls)
            {
                if (control is GroupBox)
                {
                    this.AddEventHandlerOnFormLoad(control);
                }
                else if (control is CheckBox)
                {
                    ((CheckBox)control).CheckedChanged += this.CheckBox_CheckedChanged;
                }
                else if (control is NumericUpDown)
                {
                    ((NumericUpDown)control).ValueChanged += this.NumericUpDown_ValueChanged;
                }
                else if (control is ComboBox)
                {
                    ((ComboBox)control).SelectionChangeCommitted += this.ComboBox_SelectionChangeCommitted;
                }
            }
        }

        /// <summary>
        /// 設定情報を初期化します。
        /// </summary>
        private void ResetSettings() => this.ApplySettings(new TextEditorSetting());

        /// <summary>
        /// 設定情報をコントロールに反映します。
        /// </summary>
        private void ApplySettings() => this.ApplySettings(SettingContainer.Instance.TextEditorSetting);

        /// <summary>
        /// 設定情報をコントロールに反映します。
        /// </summary>
        /// <param name="settings">設定情報</param>
        private void ApplySettings(TextEditorSetting settings)
        {
            settings.SetInnerControlValue(this);
            settings.SetValue(this.textEditor);
            this.nmapFontSize.Value = (decimal)settings.FontInfo.Size;
            this.emapFontName.SelectedItem = settings.FontInfo.Name;
            Type settingsType = settings.GetType();
            foreach (var obj in this.lstColor.Items)
            {
                ColorMappingObject item = obj as ColorMappingObject;
                if (string.IsNullOrEmpty(item.ForeColorPropertyName) == false)
                {
                    PropertyInfo property = settingsType.GetProperty(item.ForeColorPropertyName);
                    item.ForeColor = (Color)property.GetValue(settings);
                }
                if (string.IsNullOrEmpty(item.BackColorPropertyName) == false)
                {
                    PropertyInfo property = settingsType.GetProperty(item.BackColorPropertyName);
                    item.BackColor = (Color)property.GetValue(settings);
                }
            }
            this.UpdateDisplayColorInfo();
        }

        /// <summary>
        /// コントロールの設定を記録します。
        /// </summary>
        private void SaveSettings()
        {
            SettingContainer.Instance.TextEditorSetting.GetInnerControlValue(this);
            SettingContainer.Instance.TextEditorSetting.FontInfo.Name = (string)this.emapFontName.SelectedItem;
            SettingContainer.Instance.TextEditorSetting.FontInfo.Size = (float)this.nmapFontSize.Value;
            foreach (var obj in this.lstColor.Items)
            {
                ColorMappingObject item = obj as ColorMappingObject;
                if (string.IsNullOrEmpty(item.ForeColorPropertyName) == false)
                {
                    PropertyInfo property = typeof(TextEditorSetting).GetProperty(item.ForeColorPropertyName);
                    property.SetValue(SettingContainer.Instance.TextEditorSetting, item.ForeColor);
                }
                if (string.IsNullOrEmpty(item.BackColorPropertyName) == false)
                {
                    PropertyInfo property = typeof(TextEditorSetting).GetProperty(item.BackColorPropertyName);
                    property.SetValue(SettingContainer.Instance.TextEditorSetting, item.BackColor);
                }
            }
        }

        /// <summary>
        /// カラーダイアログを表示し前景色を選択します。
        /// </summary>
        private void SelectForeColorByDialog()
        {
            using (ColorDialog dialog = new ColorDialog())
            {
                dialog.Color = this.emapForeColor.SelectedValue;
                dialog.FullOpen = true;
                DialogResult result = dialog.ShowDialog(this);
                if (result != DialogResult.OK)
                {
                    return;
                }
                this.emapForeColor.SelectedValue = dialog.Color;
                this.emapForeColor.RaiseSelectionChangeCommitted(EventArgs.Empty);
            }
        }

        /// <summary>
        /// カラーダイアログを表示し背景色を選択します。
        /// </summary>
        private void SelectBackColorByDialog()
        {
            using (ColorDialog dialog = new ColorDialog())
            {
                dialog.Color = this.emapBackColor.SelectedValue;
                dialog.FullOpen = true;
                DialogResult result = dialog.ShowDialog(this);
                if (result != DialogResult.OK)
                {
                    return;
                }
                this.emapBackColor.SelectedValue = dialog.Color;
                this.emapBackColor.RaiseSelectionChangeCommitted(EventArgs.Empty);
            }
        }

        /// <summary>
        /// カラー情報を一時保存します。
        /// </summary>
        private void TemporarilySaveColorInfo()
        {
            ColorMappingObject obj = (ColorMappingObject)this.lstColor.SelectedItem;
            obj.ForeColor = this.emapForeColor.SelectedValue;
            obj.BackColor = this.emapBackColor.SelectedValue;

            if (string.IsNullOrEmpty(obj.ForeColorPropertyName) == false)
            {
                PropertyInfo property = this.textEditor.GetType().GetProperty(obj.ForeColorPropertyName);
                property.SetValue(this.textEditor, obj.ForeColor);
            }
            if (string.IsNullOrEmpty(obj.BackColorPropertyName) == false)
            {
                PropertyInfo property = this.textEditor.GetType().GetProperty(obj.BackColorPropertyName);
                property.SetValue(this.textEditor, obj.BackColor);
            }
            this.textEditor.Refresh();
        }

        /// <summary>
        /// カラー情報の表示状態を更新します。
        /// </summary>
        private void UpdateDisplayColorInfo()
        {
            if (this.lstColor.SelectedIndex < 0)
            {
                this.lstColor.SelectedIndex = 0;
            }

            ColorMappingObject obj = (ColorMappingObject)this.lstColor.SelectedItem;
            this.emapForeColor.SelectedValue = obj.ForeColor;
            this.emapBackColor.SelectedValue = obj.BackColor;
            this.lblForeColor.Enabled = this.emapForeColor.Enabled
                                      = this.btnForeColor.Enabled
                                      = !obj.ForeColor.IsEmpty;
            this.lblBackColor.Enabled = this.emapBackColor.Enabled
                                      = this.btnBackColor.Enabled
                                      = !obj.BackColor.IsEmpty;
        }

        private void Form_Load(object sender, EventArgs e)
        {
            this.ApplySettings();
            this.AddEventHandlerOnFormLoad();
        }

        private void Form_FormClosed(object sender, FormClosedEventArgs e)
        {
            switch (this.DialogResult)
            {
                case DialogResult.OK:
                    this.SaveSettings();
                    break;
            }
        }

        private void BtnReset_Click(object sender, EventArgs e)
        {
            this.ResetSettings();
        }

        private void LstColor_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.UpdateDisplayColorInfo();
        }

        private void BtnForeColor_Click(object sender, EventArgs e)
        {
            this.SelectForeColorByDialog();
        }

        private void BtnBackColor_Click(object sender, EventArgs e)
        {
            this.SelectBackColorByDialog();
        }

        private void CheckBox_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox control = (CheckBox)sender;
            PropertyInfo property = this.textEditor.GetType().GetProperty(control.Name.TrimStart(ControlNamePrefix.BOOLEAN_MAPPED));
            property?.SetValue(this.textEditor, control.Checked);
        }

        private void NumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            NumericUpDown control = (NumericUpDown)sender;
            if (control.Equals(this.nmapFontSize))
            {
                this.textEditor.Font = new Font(this.textEditor.Font.FontFamily, (float)control.Value);
            }
            else
            {
                PropertyInfo property = this.textEditor.GetType().GetProperty(control.Name.TrimStart(ControlNamePrefix.NUMERIC_MAPPED));
                property?.SetValue(this.textEditor, Convert.ChangeType(control.Value, property.PropertyType));
            }
        }

        private void ComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            ComboBox control = (ComboBox)sender;
            if (control.Equals(this.emapFontName))
            {
                this.textEditor.Font = new Font((string)control.SelectedItem, this.textEditor.Font.Size);
            }
            else if (control.Equals(this.emapForeColor) ||
                     control.Equals(this.emapBackColor))
            {
                this.TemporarilySaveColorInfo();
            }
            else
            {
                PropertyInfo property = this.textEditor.GetType().GetProperty(control.Name.TrimStart(ControlNamePrefix.ENUM_MAPPED));
                property?.SetValue(this.textEditor, Convert.ChangeType(control.SelectedItem, property.PropertyType));
            }
        }

        #region 内部クラス
        /// <summary>
        /// 色設定をマッピングするためのオブジェクトを表します。
        /// </summary>
        private class ColorMappingObject : IEquatable<ColorMappingObject>
        {
            /// <summary>
            /// 色設定の分類を取得します。
            /// </summary>
            public ColorSettingsKind ColorSettngsMode { get; }

            /// <summary>
            /// 前景色のプロパティ名を取得します。
            /// </summary>
            public string ForeColorPropertyName { get; }

            /// <summary>
            /// 背景色のプロパティ名を取得します。
            /// </summary>
            public string BackColorPropertyName { get; }

            /// <summary>
            /// 前景色を取得または設定します。
            /// </summary>
            public Color ForeColor { get; set; }

            /// <summary>
            /// 背景色を取得または設定します。
            /// </summary>
            public Color BackColor { get; set; }

            /// <summary>
            /// インスタンスを初期化します。
            /// </summary>
            /// <param name="kind">色設定の分類</param>
            /// <param name="foreColorPropertyName">前景色のプロパティ名</param>
            /// <param name="backColorPropertyName">背景色のプロパティ名</param>
            public ColorMappingObject(ColorSettingsKind kind, string foreColorPropertyName, string backColorPropertyName)
            {
                this.ColorSettngsMode = kind;
                this.ForeColorPropertyName = foreColorPropertyName;
                this.BackColorPropertyName = backColorPropertyName;
                this.ForeColor = Color.Empty;
                this.BackColor = Color.Empty;
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
                return this.Equals(obj as ColorMappingObject);
            }

            /// <summary>
            /// 指定したオブジェクトが、現在のオブジェクトと等しいかどうかを判断します。
            /// </summary>
            /// <param name="other">比較するオブジェクト</param>
            /// <returns>等しいかどうかを示す値</returns>
            public bool Equals(ColorMappingObject other)
            {
                if (other == null)
                {
                    return false;
                }
                return (this.ColorSettngsMode.Equals(other.ColorSettngsMode)) &&
                       (this.ForeColor.Equals(other.ForeColor)) &&
                       (this.BackColor.Equals(other.BackColor));
            }

            /// <summary>
            /// ハッシュ値を取得します。
            /// </summary>
            /// <returns>ハッシュ値</returns>
            public override int GetHashCode() 
                => this.ColorSettngsMode.GetHashCode() ^ this.ForeColor.GetHashCode() ^ this.BackColor.GetHashCode();

            /// <summary>
            /// 現在のオブジェクトを表す文字列を返します。
            /// </summary>
            /// <returns>文字列</returns>
            public override string ToString() => this.ColorSettngsMode.ToString();
        }
        #endregion

        #region 列挙値
        /// <summary>
        /// 色設定の分類を表します。
        /// </summary>
        private enum ColorSettingsKind
        {
            テキスト,
            選択範囲,
            検索結果,
            括弧,
            行番号,
            編集済み,
            保存済み,
            現在行,
            折り返し,
            空白,
            改行,
            末尾,
            エリア外,
        }
        #endregion
    }
}
