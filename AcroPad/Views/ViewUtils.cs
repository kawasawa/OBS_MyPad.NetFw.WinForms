using AcroBat;
using AcroBat.Views;
using AcroPad.Models;
using AcroPad.Models.Associations;
using AcroPad.Properties;
using AcroPad.Views.Contents;
using Sgry.Azuki;
using Sgry.Azuki.Highlighter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace AcroPad.Views
{
    /// <summary>
    /// ビューに関係する汎用的な処理を提供します。
    /// </summary>
    internal static class ViewUtils
    {
        /// <summary>
        /// リソースを検索するためのバインディング制約を表します。
        /// </summary>
        private const BindingFlags RESOURCES_BINDING_FLAGS = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static;

        /// <summary>
        /// コントロールを検索するためのバインディング制約を表します。
        /// </summary>
        private const BindingFlags CONTROLS_BINDING_FLAGS = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly;

        /// <summary>
        /// シーケンス番号を取得します。
        /// </summary>
        private static uint SEQUENCE = 0;

        /// <summary>
        /// シーケンス番号を発行します。
        /// </summary>
        /// <returns>シーケンス番号</returns>
        public static uint GetSequence() => ++SEQUENCE;

        /// <summary>
        /// フォームにアイコンを設定します。
        /// </summary>
        /// <param name="form">対象のフォーム</param>
        public static void SetFormIcon(this Form form)
        {
            Type formType = form.GetType();
            PropertyInfo sourceProp = typeof(Resources).GetProperty(formType.Name, RESOURCES_BINDING_FLAGS);
            if (sourceProp == null)
            {
                return;
            }
            formType.GetProperty(nameof(Form.Icon))?.SetValue(form, sourceProp.GetValue(null));
        }

        /// <summary>
        /// <see cref="ToolStripItem"/> コントロールにイメージを設定します。
        /// </summary>
        /// <param name="container">対象のコンテナ</param>
        public static void SetItemImage(this ContainerControl container)
        {
            Regex regex = new Regex($@"^.*{ControlNamePrefix.COMMAND}([^_]+)(_.+)?$");
            Type formType = container.GetType();
            foreach (FieldInfo targetField in formType.
                GetFields(CONTROLS_BINDING_FLAGS).
                Where(f => regex.IsMatch(f.Name)))
            {
                PropertyInfo sourceProp = typeof(Resources).GetProperty(
                    regex.Match(targetField.Name).Groups[1].Value,
                    RESOURCES_BINDING_FLAGS);
                if (sourceProp == null)
                {
                    continue;
                }
                targetField.FieldType.GetProperty(nameof(ToolStripItem.Image))?.SetValue(targetField.GetValue(container), sourceProp.GetValue(null));
            }
        }

        /// <summary>
        /// ツールバー内の <see cref="ToolStripItem"/> コントロールにツールチップを設定します。
        /// </summary>
        /// <param name="container">対象のコンテナ</param>
        public static void SetItemToolTip(this ContainerControl container)
        {
            Type formType = container.GetType();
            foreach (FieldInfo targetField in formType.
                GetFields(CONTROLS_BINDING_FLAGS).
                Where(f => f.Name.StartsWith(ControlNamePrefix.TOOL_COMMAND)))
            {
                // メタデータを取得する
                FieldInfo sourceField = formType.GetField(
                    $"{ControlNamePrefix.MENU_COMMAND}{targetField.Name.TrimStart(ControlNamePrefix.TOOL_COMMAND)}",
                    CONTROLS_BINDING_FLAGS);
                if (sourceField == null)
                {
                    continue;
                }
                PropertyInfo targetProp = targetField.FieldType.GetProperty(nameof(ToolStripItem.ToolTipText));
                if (targetProp == null)
                {
                    continue;
                }
                object sourceControl = sourceField.GetValue(container);

                // 機能名を取得する
                string funcText = GetTextWithoutAccessKey((string)sourceField.FieldType.GetProperty(nameof(ToolStripItem.Text)).GetValue(sourceControl));

                // 表示用の文字列が登録されている場合はそれを設定する
                string shortcutKeyDisplayString = (string)sourceField.FieldType.GetProperty(nameof(ToolStripMenuItem.ShortcutKeyDisplayString)).GetValue(sourceControl);
                if (string.IsNullOrWhiteSpace(shortcutKeyDisplayString) == false)
                {
                    targetProp.SetValue(targetField.GetValue(container), $"{funcText} ({shortcutKeyDisplayString})");
                    continue;
                }

                // ショートカットキーが未登録の場合は何もしない
                Keys shortcutKeys = (Keys)sourceField.FieldType.GetProperty(nameof(ToolStripMenuItem.ShortcutKeys)).GetValue(sourceControl);
                if (shortcutKeys == Keys.None)
                {
                    targetProp.SetValue(targetField.GetValue(container), funcText);
                    continue;
                }

                // ショートカットキーを基に表示用の文字列を設定する
                shortcutKeyDisplayString = string.Empty;
                if (shortcutKeys == (shortcutKeys | Keys.Control))
                {
                    shortcutKeyDisplayString += "Ctrl+";
                }
                if (shortcutKeys == (shortcutKeys | Keys.Shift))
                {
                    shortcutKeyDisplayString += "Shift+";
                }
                if (shortcutKeys == (shortcutKeys | Keys.Alt))
                {
                    shortcutKeyDisplayString += "Alt+";
                }
                shortcutKeyDisplayString += (shortcutKeys & ~(Keys.Control | Keys.Shift | Keys.Alt)).ToString();
                targetProp.SetValue(targetField.GetValue(container), $"{funcText} ({shortcutKeyDisplayString})");
            }
        }

        /// <summary>
        /// アクセスキーを除くテキストを取得します。
        /// </summary>
        /// <param name="text">基となるテキスト</param>
        /// <returns>アクセスキーを除くテキスト</returns>
        public static string GetTextWithoutAccessKey(string text)
        {
            Match match = new Regex(@"^(.+)\(&[a-zA-Z0-9]\)").Match(text);
            return 1 < match.Groups.Count ? match.Groups[1].Value : text;
        }

        /// <summary>
        /// 指定した文字列内の改行コードを統一します。
        /// </summary>
        /// <param name="text">文字列</param>
        /// <param name="mode">改行コード</param>
        /// <returns>改行コードが統一された文字列</returns>
        public static string UnifyEndOfLine(string text, EolKind mode)
            => new Regex($"{EolCode.CRLF}|{EolCode.CR}|{EolCode.LF}", RegexOptions.Multiline).Replace(text, mode.GetEolCode());

        /// <summary>
        /// エンコードの種類を取得します。
        /// </summary>
        /// <param name="encoding">エンコード</param>
        /// <returns>エンコードの種類</returns>
        public static EncodingKind? GetEncodingKind(this Encoding encoding)
        {
            foreach (FieldInfo field in typeof(CodePage).GetFields())
            {
                if (field.GetValue(null).Equals(encoding.CodePage))
                {
                    return (EncodingKind)typeof(EncodingKind).GetField(field.Name).GetValue(null);
                }
            }
            return null;
        }

        /// <summary>
        /// エンコードを取得します。
        /// </summary>
        /// <param name="mode">エンコードの種類</param>
        /// <returns>エンコード</returns>
        public static Encoding GetEncoding(this EncodingKind mode)
        {
            if (mode.IsDefined() == false)
            {
                throw new ArgumentOutOfRangeException(nameof(mode));
            }

            FieldInfo field = typeof(CodePage).GetField(mode.ToString());
            if (field == null)
            {
                throw new MissingFieldException(mode.ToString());
            }
            return Encoding.GetEncoding((int)field.GetValue(null));
        }

        /// <summary>
        /// 改行コードを取得します。
        /// </summary>
        /// <param name="mode">改行コードの種類</param>
        /// <returns>改行コード</returns>
        public static string GetEolCode(this EolKind mode)
        {
            if (mode.IsDefined() == false)
            {
                throw new ArgumentOutOfRangeException(nameof(mode));
            }

            FieldInfo field = typeof(EolCode).GetField(mode.ToString());
            if (field == null)
            {
                throw new MissingFieldException(mode.ToString());
            }
            return (string)field.GetValue(null);
        }

        /// <summary>
        /// インデント設定を取得します。
        /// </summary>
        /// <param name="mode">自動インデントの種類</param>
        /// <returns>インデント設定</returns>
        public static AutoIndentHook GetIndentHook(this AutoIndentKind mode)
        {
            switch (mode)
            {
                case AutoIndentKind.None:
                    return null;
                case AutoIndentKind.Smart:
                    return AutoIndentHooks.GenericHook;
                case AutoIndentKind.C:
                    return AutoIndentHooks.CHook;
                case AutoIndentKind.Python:
                    return AutoIndentHooks.PythonHook;
                default:
                    throw new ArgumentOutOfRangeException(nameof(mode));
            }
        }

        /// <summary>
        /// インデント設定を取得します。
        /// </summary>
        /// <param name="mode">ファイルの種類</param>
        /// <returns>インデント設定</returns>
        public static AutoIndentHook GetIndentHook(this LanguageKind mode)
        {
            switch (mode)
            {
                case LanguageKind.Text:
                    return null;
                case LanguageKind.Ruby:
                case LanguageKind.PlSql:
                case LanguageKind.Xml:
                case LanguageKind.BatchFile:
                    return AutoIndentHooks.GenericHook;
                case LanguageKind.Cpp:
                case LanguageKind.CSharp:
                case LanguageKind.Java:
                case LanguageKind.JavaScript:
                    return AutoIndentHooks.CHook;
                case LanguageKind.Python:
                    return AutoIndentHooks.PythonHook;
                default:
                    return null;
            }
        }

        /// <summary>
        /// ハイライト設定を取得します。
        /// </summary>
        /// <param name="mode">ファイルの種類</param>
        /// <returns>ハイライト設定</returns>
        public static IHighlighter GetHighlighter(this LanguageKind mode) => HighlighterFactory.CreateInstance(mode);

        /// <summary>
        /// 表示設定を取得します。
        /// </summary>
        /// <param name="mode">折り返し方法の種類</param>
        /// <returns>表示設定</returns>
        public static ViewType GetViewType(this WordWrapKind mode)
        {
            switch (mode)
            {
                case WordWrapKind.UnWrap:
                    return ViewType.Proportional;
                case WordWrapKind.DigitWrap:
                case WordWrapKind.EdgeWrap:
                    return ViewType.WrappedProportional;
                default:
                    throw new ArgumentOutOfRangeException(nameof(mode));
            }
        }

        /// <summary>
        /// ダイアログに渡す引数を取得します。
        /// </summary>
        /// <returns>引数</returns>
        public static FileDialogArg GetDialogArgsForOpen()
        {
            FileDialogArg args = new FileDialogArg();
            args.Filters = GetExtensionFilters();
            args.Encodings = new List<string>() { Resources.CNS_STR_AUTOMATIC_IDENTIFIER };
            args.Encodings.AddRange(Enum.GetNames(typeof(EncodingKind)));
            return args;
        }

        /// <summary>
        /// ダイアログに渡す引数を取得します。
        /// </summary>
        /// <param name="document">ドキュメント</param>
        /// <returns>引数</returns>
        public static FileDialogArg GetDialogArgsForSave(EditorDocument document)
        {
            FileDialogArg args = new FileDialogArg();
            args.DefaultFileName = document.FileName;
            args.Filters = GetExtensionFilters();
            args.Encodings = Enum.GetNames(typeof(EncodingKind)).ToList();
            args.SelectedEncodingIndex = args.Encodings.IndexOf(document.TextEditor.EncodingMode.ToString());
            return args;
        }

        /// <summary>
        /// ファイルの種類を指定するためのフィルターを取得します。
        /// </summary>
        /// <returns>フィルター</returns>
        private static List<KeyValuePair<string, string>> GetExtensionFilters()
        {
            var filters = new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>(Resources.CNS_STR_ALL_FILE_TYPE, "*.*"),
            };
            foreach (Association assoc in LanguageContainer.Instance.Associations.Where(info => 0 < info.Extensions?.Length))
            {
                StringBuilder filterString = new StringBuilder();
                assoc.Extensions.ForEach(ext => filterString.Append($"*{ext};"));
                filters.Add(new KeyValuePair<string, string>(assoc.DisplayName, filterString.ToString().TrimEnd(';')));
            }
            return filters;
        }

        /// <summary>
        /// 選択されたフィルターから拡張子を取得します。
        /// </summary>
        /// <param name="result">ダイアログの戻り値</param>
        /// <param name="defaultValue">既定値</param>
        /// <returns>フィルター</returns>
        public static string GetExtensionFromResult(FileDialogResultBase result, string defaultValue)
        {
            return result.SelectedFilterIndex == 0 ? defaultValue : result.Argument.Filters[result.SelectedFilterIndex].Value.Split(';')[0];
        }

        /// <summary>
        /// 選択されたエンコードの種類を取得します。
        /// </summary>
        /// <param name="result">ダイアログの戻り値</param>
        /// <returns>エンコードの種類</returns>
        public static EncodingKind GetEncodingKindFromResult(FileDialogResultBase result)
        {
            return (EncodingKind)Enum.Parse(typeof(EncodingKind), result.Argument.Encodings[result.SelectedEncodingIndex]);
        }
    }
}
