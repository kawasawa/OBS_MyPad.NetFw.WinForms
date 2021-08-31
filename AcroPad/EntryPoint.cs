namespace AcroPad
{
    using AcroPad.Models;
    using AcroPad.Views.Forms;
    using System;
    using System.Diagnostics;
    using System.Windows.Forms;

    /// <summary>
    /// アプリケーションのエントリーポイントとなるクラスを表します。
    /// </summary>
    public static class EntryPoint
    {
        /// <summary>
        /// アプリケーションのメインメソッドです。
        /// </summary>
        /// <param name="args">コマンドライン引数</param>
        [STAThread]
        public static void Main(string[] args)
        {
            AppDomain.CurrentDomain.UnhandledException += UnhandledException;
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.ThrowException);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            if (EntryPointHelper.SendArgs(Process.GetCurrentProcess(), args))
            {
                Environment.Exit(ExitCode.SUCCESS);
                return;
            }

            ContainerManager.Load();
            using (MainForm form = new MainForm())
            {
                Array.ForEach(args, path => form.InvokeAddDocument(path, false));
                Application.Run(form);
            }
            ContainerManager.Save();
        }

        /// <summary>
        /// 処理されていない例外を受け取ったときに発生します。
        /// </summary>
        /// <param name="sender">イベントの発生源</param>
        /// <param name="e">イベントの情報</param>
        private static void UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            try
            {
                Process process = Process.GetCurrentProcess();
                process.Refresh();
                string message = EntryPointHelper.GetExceptionMessage((Exception)e.ExceptionObject, process);
                EntryPointHelper.WriteLog(message, DirectoryPath.LOG, "ERROR", DateTime.Now);
                using (ExceptionForm form = new ExceptionForm(message))
                {
                    form.ShowDialog();
                }
            }
            finally
            {
                Environment.Exit(ExitCode.UNHANDLED_EXCEPTION_OCCURED);
            }
        }
    }
}

namespace AcroPad
{
    using AcroBat;
    using AcroBat.WindowsAPI;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Management;
    using System.Text;

    /// <summary>
    /// <see cref="AcroPad.EntryPoint"/> から利用するためのメソッドを提供します。
    /// </summary>
    internal static class EntryPointHelper
    {
        /// <summary>
        /// 指定したプロセスと同じ実行ファイルから起動したプロセスに対してデータを送信します。
        /// </summary>
        /// <param name="current">プロセス</param>
        /// <param name="args">コマンドライン引数</param>
        /// <returns>処理されたかどうかを示す値</returns>
        public static bool SendArgs(Process current, string[] args)
        {
            IList<IntPtr> handles = Process.GetProcessesByName(current.ProcessName)
                                           .Where(p => {
                                               try { return p.Id != current.Id &&
                                                            p.MainModule.FileName.Equals(current.MainModule.FileName); }
                                               catch (Win32Exception) { return false; }
                                            })
                                           .Select(p => p.MainWindowHandle)
                                           .ToList();
            if (handles.Count == 0)
            {
                return false;
            }

            IntPtr hWnd = GetForegroundWindowHandle(handles);
            if (0 < args.Length)
            {
                COPYDATASTRUCT lParam = new COPYDATASTRUCT();
                lParam.dwData = IntPtr.Zero;
                lParam.lpData = args.Join(SpecialChar.SendDataSeparator);
                lParam.cbData = DotNETFrameworkDependencyValue.Encoding.GetByteCount(lParam.lpData) + 1;
                User32.SendMessage(hWnd, WindowsMessages.WM_COPYDATA, current.Handle, ref lParam);
            }
            User32.ShowWindow(hWnd, nCmdShow.SW_RESTORE);
            User32.SetForegroundWindow(hWnd);
            return true;
        }
        
        /// <summary>
        /// 指定したウィンドウのうちZオーダーが最大のウィンドウのハンドルを取得します。
        /// </summary>
        /// <param name="handles">ウィンドウのハンドル</param>
        /// <returns>Zオーダーが最大のウィンドウのハンドル</returns>
        private static IntPtr GetForegroundWindowHandle(IList<IntPtr> handles)
        {
            IntPtr topWindowHandle = IntPtr.Zero;
            if (0 < handles?.Count)
            {
                IntPtr hwnd = handles[0];
                while (hwnd != IntPtr.Zero)
                {
                    if (handles.Contains(hwnd))
                    {
                        topWindowHandle = hwnd;
                    }
                    hwnd = User32.GetWindow(hwnd, GetWindow_Cmd.GW_HWNDPREV);
                }
            }
            return topWindowHandle;
        }

        /// <summary>
        /// 例外の詳細を示すメッセージを取得します。
        /// </summary>
        /// <param name="e">例外の情報</param>
        /// <param name="p">プロセスの情報</param>
        /// <returns>メッセージ</returns>
        public static string GetExceptionMessage(Exception e, Process p)
        {
            const string WIN32_OPERATING_SYSTEM = "Win32_OperatingSystem";
            const string WIN32_PROCESSOR = "Win32_Processor";

            StringBuilder msg = new StringBuilder();
            msg.AppendLine($"[Message]");
            msg.AppendLine($"  {e.Message}");

            msg.AppendLine($"[Exception]");
            msg.AppendLine($"  Type: {e.GetType().FullName}");
            msg.AppendLine($"  Method: {e.TargetSite}");
            msg.AppendLine($"  StackTrace:");
            e.StackTrace.Split(new[] { Environment.NewLine }, StringSplitOptions.None).ForEach(s => msg.AppendLine($"    {s.Trim()}"));

            msg.AppendLine($"[Process]");
            msg.AppendLine($"  ID: {p.Id}");
            msg.AppendLine($"  MemorySize: {(p.PrivateMemorySize64 / 1000).ToString("#,##0")} KB");
            msg.AppendLine($"  StartTime: {p.StartTime}");

            msg.AppendLine($"[Environment]");
            using (ManagementClass management = new ManagementClass(WIN32_OPERATING_SYSTEM))
            using (ManagementObjectCollection objects = management.GetInstances())
            {
                foreach (ManagementObject obj in objects)
                {
                    msg.AppendLine($"  OSVersion: {obj["Caption"]} (ver. {obj["Version"]})");
                    msg.AppendLine($"  MemorySize: {Convert.ToInt64(obj["TotalVisibleMemorySize"]).ToString("#,##0")} KB");
                    obj.Dispose();
                    break;
                }
            }
            using (ManagementClass management = new ManagementClass(WIN32_PROCESSOR))
            using (ManagementObjectCollection objects = management.GetInstances())
            {
                foreach (ManagementObject obj in objects)
                {
                    msg.AppendLine($"  Processor: {obj["Name"]}");
                    msg.AppendLine($"  DataWidth: {obj["DataWidth"]} bit");
                    msg.AppendLine($"  ClockSpeed: {Convert.ToInt64(obj["MaxClockSpeed"]).ToString("#,##0")} MHz");
                    obj.Dispose();
                    break;
                }
            }
            return msg.ToString();
        }

        /// <summary>
        /// 指定したディレクトリにログを出力します。
        /// </summary>
        /// <param name="message">メッセージ</param>
        /// <param name="directoryPath">ディレクトリのパス</param>
        /// <param name="fixedFileName">ファイル名の固定部</param>
        /// <param name="time">書き込み日時</param>
        public static void WriteLog(string message, string directoryPath, string fixedFileName, DateTime time)
        {
            Directory.CreateDirectory(directoryPath);
            string path = Path.Combine(directoryPath, $"{fixedFileName}_{time.ToString("yyyyMMdd")}.log");
            using (StreamWriter writer = new StreamWriter(path, true, DotNETFrameworkDependencyValue.Encoding))
            {
                writer.WriteLine($"--------------- {time.ToString("yyyy/MM/dd HH:mm:ss.fff")} ---------------");
                writer.WriteLine(message);
                writer.Flush();
            }
        }
    }
}
