

namespace AcroBat
{
    using Microsoft.VisualBasic;
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Xml.Serialization;

    /// <summary>
    /// 拡張メソッドを提供します。
    /// </summary>
    public static class Extensions
    {
        #region Text
        /// <summary>
        /// 指定した文字を指定した数だけ連結した文字列を取得します
        /// </summary>
        /// <param name="text">文字列</param>
        /// <param name="number">連結する数</param>
        /// <returns>連結した文字列。</returns>
        public static string Repeat(this char text, int number) => text.ToString().Repeat(number);

        /// <summary>
        /// 指定した文字列を指定した数だけ連結した文字列を取得します
        /// </summary>
        /// <param name="text">文字列</param>
        /// <param name="number">連結する数</param>
        /// <returns>連結した文字列。</returns>
        public static string Repeat(this string text, int number) => string.Concat(Enumerable.Repeat(text, number));

        /// <summary>
        /// この文字列の先頭が、指定した文字列と一致するかどうかを判断します。
        /// </summary>
        /// <param name="text">文字列</param>
        /// <param name="values">指定した文字列</param>
        /// <returns>一致するかどうかを示す値</returns>
        public static bool StartsWith(this string text, string[] values) => 0 < values.Count(value => text.StartsWith(value));

        /// <summary>
        /// この文字列の先頭から、指定された文字列をすべて削除します。
        /// </summary>
        /// <param name="text">文字列</param>
        /// <param name="values">削除する文字列</param>
        /// <returns>文字列の先頭から、<<paramref name="values"/> で指定された文字列をすべて削除した後に残った文字列</returns>
        public static string TrimStart(this string text, string values) => text.TrimStart(new[] { values });

        /// <summary>
        /// この文字列の先頭から、配列で指定された文字列をすべて削除します。
        /// </summary>
        /// <param name="text">文字列</param>
        /// <param name="values">削除する文字列の配列</param>
        /// <returns>文字列の先頭から、<<paramref name="values"/> で指定された文字列をすべて削除した後に残った文字列</returns>
        public static string TrimStart(this string text, string[] values)
        {
            foreach (string value in values)
            {
                if (text.StartsWith(value))
                {
                    return text.Substring(value.Length);
                }
            }
            return text;
        }

        /// <summary>
        /// 指定された文字列内の各語の最初の文字を大文字に変換します。
        /// </summary>
        /// <param name="text">文字列</param>
        /// <returns>変換後の文字列</returns>
        public static string ToProperCase(this string text) => Strings.StrConv(text, VbStrConv.ProperCase);

        /// <summary>
        /// 指定された文字列内の半角文字を全角文字に変換します。
        /// </summary>
        /// <param name="text">文字列</param>
        /// <returns>変換後の文字列</returns>
        public static string ToWide(this string text) => Strings.StrConv(text, VbStrConv.Wide);

        /// <summary>
        /// 指定された文字列内の全角文字を半角文字に変換します。
        /// </summary>
        /// <param name="text">文字列</param>
        /// <returns>変換後の文字列</returns>
        public static string ToNarrow(this string text) => Strings.StrConv(text, VbStrConv.Narrow);

        /// <summary>
        /// 指定された文字列内のカタカナをひらがなに変換します。
        /// </summary>
        /// <param name="text">文字列</param>
        /// <returns>変換後の文字列</returns>
        public static string ToHiragana(this string text) => Strings.StrConv(text, VbStrConv.Hiragana);

        /// <summary>
        /// 指定された文字列内のひらがなをカタカナに変換します。
        /// </summary>
        /// <param name="text">文字列</param>
        /// <returns>変換後の文字列</returns>
        public static string ToKatakana(this string text) => Strings.StrConv(text, VbStrConv.Katakana);

        /// <summary>
        /// 指定された正規表現パターンを .NET Framework の仕様に従い整形します。
        /// </summary>
        /// <param name="pattern">正規表現パターン</param>
        /// <returns>整形後の正規表現パターン</returns>
        public static string ToDotNetRegexPattern(string pattern)
        {
            if (pattern.EndsWith("$"))
            {
                // .NET Framework の仕様で、
                // 行末位置を示す "$" はラインフィード "\n" にマッチする。
                // キャリッジリターン "\r" にはマッチしない。
                // このため、改行コードの種類に関わらず、行末位置にマッチするよう整形する。
                return $"({pattern.Substring(0, pattern.Length - "$".Length)})(?={EolCode.CR}?{EolCode.LF}|$)";
            }
            else
            {
                return pattern;
            }
        }

        /// <summary>
        /// コレクションのメンバーを連結します。
        /// </summary>
        /// <typeparam name="T"><paramref name="values"/> のメンバーの型</typeparam>
        /// <param name="values">連結するオブジェクトを格納しているコレクション</param>
        /// <returns><paramref name="values"/>  のメンバーを連結した文字列</returns>
        public static string Concat<T>(this IEnumerable<T> values) => string.Concat<T>(values);

        /// <summary>
        /// コレクションのメンバーを連結します。各メンバーの間には、指定した区切り記号が挿入されます。
        /// </summary>
        /// <typeparam name="T"><paramref name="values"/> のメンバーの型</typeparam>
        /// <param name="values">連結するオブジェクトを格納しているコレクション</param>
        /// <param name="separator">区切り記号</param>
        /// <returns><paramref name="values"/>  のメンバーからなる、<paramref name="separator"/> で区切られた文字列</returns>
        public static string Join<T>(this IEnumerable<T> values, char separator) => values.Join(separator.ToString());

        /// <summary>
        /// コレクションのメンバーを連結します。各メンバーの間には、指定した区切り記号が挿入されます。
        /// </summary>
        /// <typeparam name="T"><paramref name="values"/> のメンバーの型</typeparam>
        /// <param name="values">連結するオブジェクトを格納しているコレクション</param>
        /// <param name="separator">区切り記号</param>
        /// <returns><paramref name="values"/>  のメンバーからなる、<paramref name="separator"/> で区切られた文字列</returns>
        public static string Join<T>(this IEnumerable<T> values, string separator) => string.Join(separator, values);
        #endregion

        #region Enum
        /// <summary>
        /// 指定した列挙値が列挙体に定義されているかどうかを判定します。
        /// </summary>
        /// <param name="value">列挙値</param>
        /// <returns>定義されているかどうかを示す値</returns>
        public static bool IsDefined(this Enum value) => Enum.IsDefined(value.GetType(), value);
        #endregion

        #region Stream

        /// <summary>
        /// ストリームからバイトのブロックを読み取り、そのデータを特定のバッファーに書き込みます。
        /// </summary>
        /// <param name="stream">ストリーム</param>
        /// <param name="array">このメソッドが返されるときに、現在のソースから読み取られたバイトに置き換えられます。</param>
        /// <returns>バッファーに読み取られた合計バイト数</returns>
        public static int Read(this FileStream stream, byte[] array) => stream.Read(array, 0, array.Length);

        /// <summary>
        /// バッファーのデータを使用して、ストリームにバイトのブロックを書き込みます。
        /// </summary>
        /// <param name="stream">ストリーム</param>
        /// <param name="array">ストリームに書き込むデータを格納しているバッファー</param>
        public static void Write(this FileStream stream, byte[] array) => stream.Write(array, 0, array.Length);
        #endregion

        #region Drawing
        /// <summary>
        /// 指定した <see cref="Color"/> 構造体と ARGB 値が等しいシステムカラーを取得します。
        /// 対応するシステムカラーが存在しない場合は <paramref name="color"/> をそのまま返します。
        /// </summary>
        /// <param name="color"><see cref="Color"/> 構造体</param>
        /// <returns>システムカラー</returns>
        public static Color GetKnownColor(this Color color)
        {
            if (color.IsKnownColor)
            {
                return color;
            }

            foreach (KnownColor c in Enum.GetValues(typeof(KnownColor)))
            {
                Color knownColor = Color.FromKnownColor(c);
                if (color.ToArgb() == knownColor.ToArgb())
                {
                    return knownColor;
                }
            }
            return color;
        }

        /// <summary>
        /// 2つの <see cref="Rectangle"/> 構造体に交差部分が存在するかどうかを判定します。
        /// </summary>
        /// <param name="rect1">1つめの四角形</param>
        /// <param name="rect2">2つめの四角形</param>
        /// <returns>交差部分が存在するかどうかを示す値</returns>
        public static bool SomeContains(this Rectangle rect1, Rectangle rect2) => !Rectangle.Intersect(rect1, rect2).IsEmpty;

        ///// <summary>
        ///// フォームのサイズが AeroSanp により拡張されたかどうかを判定します。
        ///// </summary>
        ///// <param name="form">フォーム</param>
        ///// <returns>拡張されたかどうかを示す値</returns>
        //public static bool IsAeroSnapState(this Form form)
        //{
        //    WINDOWPLACEMENT lpwndpl = new WINDOWPLACEMENT();
        //    RECT lpRect = new RECT();
        //    User32.GetWindowPlacement(form.Handle, ref lpwndpl);
        //    User32.GetWindowRect(form.Handle, ref lpRect);
        //    if (lpwndpl.rcNormalPosition.Equals(lpRect) == false)
        //    {
        //        return true;
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //}
        #endregion

        #region Generic
        /// <summary>
        /// 指定された配列内の各要素に対して、指定された処理を実行します。
        /// </summary>
        /// <typeparam name="T">配列要素の型</typeparam>
        /// <param name="array">要素に処理を適用する配列</param>
        /// <param name="action">配列の各要素に対して実行する <see cref="Action"/></param>
        public static void ForEach<T>(this Array array, Action<T> action) => ((T[])array).ForEach(action);

        /// <summary>
        /// 指定された配列内の各要素に対して、指定された処理を実行します。
        /// </summary>
        /// <typeparam name="T">配列要素の型</typeparam>
        /// <param name="array">要素に処理を適用する配列</param>
        /// <param name="action">配列の各要素に対して実行する <see cref="Action"/></param>
        public static void ForEach<T>(this T[] array, Action<T> action) => Array.ForEach(array, action);
        #endregion

        #region Serialization
        /// <summary>
		/// XMLファイルを読み取り、指定した型のインスタンスとして取得します。
		/// </summary>
		/// <typeparam name="T">読み込むデータの型</typeparam>
		/// <param name="path">取得先のパス</param>
		/// <returns>インスタンス</returns>
		public static T ReadXml<T>(string path)
        {
            using (StreamReader reader = new StreamReader(path, DotNETFrameworkDependencyValue.Encoding))
            {
                return reader.ReadData<T>();
            }
        }

        /// <summary>
		/// ストリームを読み込み、指定した型のインスタンスとして取得します。
        /// </summary>
		/// <typeparam name="T">読み込むデータの型</typeparam>
		/// <param name="reader">ストリームリーダー</param>
		/// <returns>インスタンス</returns>
        public static T ReadData<T>(this StreamReader reader)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            T result = (T)serializer.Deserialize(reader);
            return result;
        }

        /// <summary>
		/// 指定した型のインスタンスをXMLファイルに書き込みます。
		/// </summary>
		/// <typeparam name="T">保存するインスタンスの型</typeparam>
		/// <param name="target">保存するインスタンス</param>
		/// <param name="path">保存先のパス</param>
		public static void WriteXml<T>(this T target, string path)
            where T : class
        {
            Directory.CreateDirectory(Path.GetDirectoryName(path));
            using (StreamWriter writer = new StreamWriter(path, false, DotNETFrameworkDependencyValue.Encoding))
            {
                target.WriteXml(writer);
            }
        }

        /// <summary>
		/// 指定した型のインスタンスをストリームに書き込みます。
		/// </summary>
		/// <typeparam name="T">保存するインスタンスの型</typeparam>
		/// <param name="target">保存するインスタンス</param>
		/// <param name="writer">ストリームライター</param>
		public static void WriteXml<T>(this T target, StreamWriter writer)
            where T : class
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            serializer.Serialize(writer, target);
        }
        #endregion

        #region Reflection
        /// <summary>
        /// 指定されたインスタンス <paramref name="source"/> のパブリックプロパティの値を
        /// <paramref name="target"/> に定義された同名のパブリックプロパティに設定します。
        /// </summary>
        /// <typeparam name="TSource"><paramref name="source"/> の型</typeparam>
        /// <typeparam name="TTarget"><paramref name="target"/> の型</typeparam>
        /// <param name="source">プロパティの値を取得するインスタンス</param>
        /// <param name="target">プロパティの値を設定するインスタンス</param>
        /// <returns><paramref name="target"/> を返す</returns>
        public static TTarget SetProperties<TSource, TTarget>(TSource source, TTarget target)
        {
            source.GetType().GetProperties().ForEach(p => SetProperty(source, target, p));
            return target;
        }

        /// <summary>
        /// 指定されたインスタンス <paramref name="source"/> の
        /// 指定されたパブリックプロパティ <paramref name="property"/> の値を
        /// <paramref name="target"/> に定義された同名のパブリックプロパティに設定します。
        /// </summary>
        /// <typeparam name="TSource"><paramref name="source"/> の型</typeparam>
        /// <typeparam name="TTarget"><paramref name="target"/> の型</typeparam>
        /// <param name="source">プロパティの値を取得するインスタンス</param>
        /// <param name="target">プロパティの値を設定するインスタンス</param>
        /// <param name="property">転記するプロパティのメタデータ</param>
        public static void SetProperty<TSource, TTarget>(TSource source, TTarget target, PropertyInfo property)
        {
            if (property.GetCustomAttribute(typeof(SetPropertyIgnoreAttribute)) != null)
            {
                return;
            }
            target.GetType().GetProperty(property.Name)?.SetValue(target, property.GetValue(source));
        }
        #endregion
    }
}

namespace AcroBat
{
    using Microsoft.VisualBasic.FileIO;
    using System;
    using System.Collections.Specialized;
    using System.IO;
    using System.Windows.Forms;

    public static class FileEx
    {
        public static void ClipboardCopy(string[] fileNames)
        {
            StringCollection files = new StringCollection();
            fileNames.ForEach(name => files.Add(name));
            Clipboard.SetFileDropList(files);
        }

        /// <summary>
        /// 既存のファイルを新しいファイルにコピーします。
        /// </summary>
        /// <param name="sourceFileName">コピーするファイル</param>
        /// <param name="destFileName">コピー先ファイルの名前</param>
        public static void Copy(string sourceFileName, string destFileName)
        {
            FileSystem.CopyFile(@sourceFileName, @destFileName, UIOption.AllDialogs, UICancelOption.DoNothing);
        }

        /// <summary>
        /// 指定されたファイルまたはディレクトリを削除します。
        /// </summary>
        /// <param name="name">削除するファイルまたはディレクトリの名前</param>
        public static void Delete(string name)
        {
            if (File.Exists(name))
            {
                FileSystem.DeleteFile(name, UIOption.AllDialogs, RecycleOption.SendToRecycleBin, UICancelOption.DoNothing);
            }
            else if (Directory.Exists(name))
            {
                FileSystem.DeleteDirectory(name, UIOption.AllDialogs, RecycleOption.SendToRecycleBin, UICancelOption.DoNothing);
            }
        }

        /// <summary>
        /// 指定したフォルダにコピーまたは切り取られたファイルを貼り付ける
        /// </summary>
        public static void PasteFiles(string destDir)
        {
            //クリップボードのデータを取得する
            IDataObject data = Clipboard.GetDataObject();
            //クリップボードにファイルドロップ形式のデータがあるか確認
            if (data != null && data.GetDataPresent(DataFormats.FileDrop))
            {
                //コピーされたファイルのリストを取得する
                string[] files = (string[])data.GetData(DataFormats.FileDrop);
                //DragDropEffectsを取得する
                DragDropEffects dde = GetPreferredDropEffect(data);
                if (dde == (DragDropEffects.Copy | DragDropEffects.Link))
                {
                    //ファイルがコピーされていた時
                    CopyFilesToDirectory(files, destDir, false);
                }
                else if (dde == DragDropEffects.Move)
                {
                    //ファイルが切り取られていた時
                    CopyFilesToDirectory(files, destDir, true);
                }
            }
        }

        /// <summary>
        /// クリップボードの"Preferred DropEffect"を調べる
        /// </summary>
        public static DragDropEffects GetPreferredDropEffect(IDataObject data)
        {
            DragDropEffects dde = DragDropEffects.None;

            if (data != null)
            {
                //Preferred DropEffect形式のデータを取得する
                MemoryStream ms =
                    (System.IO.MemoryStream)data.GetData("Preferred DropEffect");
                if (ms != null)
                {
                    //先頭のバイトからDragDropEffectsを取得する
                    dde = (DragDropEffects)ms.ReadByte();

                    if (dde == (DragDropEffects.Copy | DragDropEffects.Link))
                    {
                        Console.WriteLine("コピー");
                    }
                    else if (dde == DragDropEffects.Move)
                    {
                        Console.WriteLine("切り取り");
                    }
                }
            }

            return dde;
        }

        /// <summary>
        /// 複数のファイルを指定したフォルダにコピーまたは移動する
        /// </summary>
        public static void CopyFilesToDirectory(string[] sourceFiles, string destDir, bool move)
        {
            foreach (string sourcePath in sourceFiles)
            {
                //コピー先のパスを決定する
                string destName = System.IO.Path.GetFileName(sourcePath);
                string destPath = System.IO.Path.Combine(destDir, destName);
                if (!move)
                {
                    //ファイルをコピーする
                    System.IO.File.Copy(sourcePath, destPath);
                }
                else
                {
                    //ファイルを移動する
                    System.IO.File.Move(sourcePath, destPath);
                }
            }
        }
    }
}
