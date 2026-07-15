using System;
using System.IO;

namespace IHFM.VAF
{
    /// <summary>
    /// Simple thread-safe file logger for DailyCare performance diagnostics.
    /// Writes timestamped entries to C:\Logs\IHFM_DailyCare.log
    /// </summary>
    public static class DailyCareLogger
    {
        private static readonly string LogFilePath = @"C:\Logs\IHFM_DailyCare.log";
        private static readonly object _lock = new object();

        public static void Log(string message)
        {
            try
            {
                string entry = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff}] {message}";
                lock (_lock)
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(LogFilePath));
                    File.AppendAllText(LogFilePath, entry + Environment.NewLine);
                }
            }
            catch
            {
                // Swallow logging errors so they never interrupt vault operations
            }
        }
    }
}
