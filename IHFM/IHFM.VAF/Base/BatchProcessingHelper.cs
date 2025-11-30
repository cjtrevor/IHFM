using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IHFM.VAF.Base
{
    public static class BatchProcessingHelper
    {
        public static readonly int BatchCount = 10;

        private static readonly string BatchFileDirectory = Path.Combine(
            Path.GetTempPath(), 
            "IHFM_VAF_BatchCounters"
        );

        private static readonly object _lockObject = new object();

        /// <summary>
        /// Gets the next batch value (0-9) and increments the counter, persisting to disk.
        /// </summary>
        /// <param name="processIdentifier">Optional identifier for the process. If null, uses a default global counter.</param>
        /// <returns>The next batch value to process (0-9)</returns>
        public static int GetNextBatchValue(string processIdentifier = null)
        {
            lock (_lockObject)
            {
                int currentBatch = ReadBatchValue(processIdentifier);
                int nextBatch = (currentBatch + 1) % BatchCount;
                WriteBatchValue(nextBatch, processIdentifier);
                return currentBatch;
            }
        }

        /// <summary>
        /// Gets the current batch value without incrementing.
        /// </summary>
        /// <param name="processIdentifier">Optional identifier for the process. If null, uses a default global counter.</param>
        /// <returns>The current batch value (0-9)</returns>
        public static int GetCurrentBatchValue(string processIdentifier = null)
        {
            lock (_lockObject)
            {
                return ReadBatchValue(processIdentifier);
            }
        }

        /// <summary>
        /// Resets the batch counter to 0.
        /// </summary>
        /// <param name="processIdentifier">Optional identifier for the process. If null, resets the default global counter.</param>
        public static void ResetBatchValue(string processIdentifier = null)
        {
            lock (_lockObject)
            {
                WriteBatchValue(0, processIdentifier);
            }
        }

        private static string GetBatchFilePath(string processIdentifier)
        {
            string fileName = string.IsNullOrWhiteSpace(processIdentifier) 
                ? "BatchCounter_Global.txt" 
                : $"BatchCounter_{SanitizeFileName(processIdentifier)}.txt";
            
            return Path.Combine(BatchFileDirectory, fileName);
        }

        private static string SanitizeFileName(string identifier)
        {
            char[] invalidChars = Path.GetInvalidFileNameChars();
            string sanitized = identifier;
            foreach (char c in invalidChars)
            {
                sanitized = sanitized.Replace(c, '_');
            }
            return sanitized;
        }

        private static int ReadBatchValue(string processIdentifier)
        {
            try
            {
                string filePath = GetBatchFilePath(processIdentifier);
                if (File.Exists(filePath))
                {
                    string content = File.ReadAllText(filePath);
                    if (int.TryParse(content, out int value) && value >= 0 && value < BatchCount)
                    {
                        return value;
                    }
                }
            }
            catch (Exception)
            {
                // If read fails, default to 0
            }
            return 0;
        }

        private static void WriteBatchValue(int value, string processIdentifier)
        {
            try
            {
                if (!Directory.Exists(BatchFileDirectory))
                {
                    Directory.CreateDirectory(BatchFileDirectory);
                }

                string filePath = GetBatchFilePath(processIdentifier);
                File.WriteAllText(filePath, value.ToString());
            }
            catch (Exception)
            {
                // Silently fail - batch processing will continue with in-memory value
            }
        }
    }
}
