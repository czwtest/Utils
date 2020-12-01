using System;
using System.Diagnostics;
using System.Text;

namespace Utils
{
    public class SubProcess
    {
        /// <summary>
        /// 执行命令，不等待返回结果
        /// </summary>
        /// <param name="filePath">命令行</param>
        /// <param name="args">命令行参数</param>
        /// <param name="shell">是否使用shell启动</param>
        /// <param name="noWin">是否创建窗口</param>
        public static void Run(string filePath, string args, bool shell = false, bool noWin = true)
        {
            Execute(filePath, args, shell, noWin);
        }

        /// <summary>
        /// 执行命令，等待返回结果
        /// </summary>
        /// <param name="filePath">命令行</param>
        /// <param name="args">命令行参数</param>
        /// <param name="encoding"></param>
        /// <param name="timeOut">超时时间（秒）</param>
        /// <param name="shell">是否使用shell启动</param>
        /// <param name="stdIn">是否启用标准输入流</param>
        /// <param name="stdOut">是否启用标准输出流</param>
        /// <param name="stdErr">是否启用标准错误流</param>
        /// <param name="noWin">是否创建窗口</param>
        /// <returns>命令行结果</returns>
        public static string Communicate(string filePath, string args, Encoding encoding = null, int timeOut = 0, bool shell = false, bool stdIn = true, bool stdOut = true, bool stdErr = true, bool noWin = true)
        {
            return Execute(filePath, args, encoding, timeOut, shell, stdIn, stdOut, stdErr, noWin);
        }

        /// <summary>
        /// 执行命令行（无返回值）
        /// </summary>
        /// <param name="filePath">命令行</param>
        /// <param name="args">命令行参数</param>
        /// <param name="shell">是否使用shell启动</param>
        /// <param name="noWin">是否创建窗口</param>
        private static void Execute(string filePath, string args, bool shell = false, bool noWin = true)
        {
            var psi = new ProcessStartInfo(filePath, args)
            {
                UseShellExecute = shell,
                CreateNoWindow = noWin
            };
            var proc = Process.Start(psi);
            if (proc == null)
            {
                throw new NullReferenceException();
            }
        }

        /// <summary>
        /// 执行命令行（有返回值）
        /// </summary>
        /// <param name="filePath">命令行</param>
        /// <param name="args">命令行参数</param>
        /// <param name="encoding"></param>
        /// <param name="timeOut">超时时间（秒）</param>
        /// <param name="shell">是否使用shell启动</param>
        /// <param name="stdIn">是否启用标准输入流</param>
        /// <param name="stdOut">是否启用标准输出流</param>
        /// <param name="stdErr">是否启用标准错误流</param>
        /// <param name="noWin">是否创建窗口</param>
        /// <returns>命令行结果</returns>
        private static string Execute(string filePath, string args, Encoding encoding = null, int timeOut = 0, bool shell = false, bool stdIn = true, bool stdOut = true, bool stdErr = true, bool noWin = true)
        {
            var psi = new ProcessStartInfo(filePath, args)
            {
                UseShellExecute = shell,
                RedirectStandardInput = stdIn,
                RedirectStandardOutput = stdOut,
                RedirectStandardError = stdErr,
                CreateNoWindow = noWin,
                StandardOutputEncoding = encoding,
                StandardErrorEncoding = encoding
            };
            var proc = Process.Start(psi);
            if (proc == null)
            {
                throw new NullReferenceException();
            }
            string result = proc.StandardOutput.ReadToEnd();
            if (timeOut > 0)
            {
                proc.WaitForExit(timeOut);
            }
            return result;
        }
    }
}
