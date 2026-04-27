using IHFM.VAF.Resident.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

//PRAN NOTES
//FUTURE TO POSSIBLY CHANGE THIS TO A DB TABLE INCASE ITEMS FAIL DURING PROCESSING, causing them to be lost from the queue since it's file based.
//Database can have a Processed flag for marking completed items incase a failure happens during processing
//Any failed items can be retried by setting the flag back to false(more control over processing)
namespace IHFM.VAF.Resident.Services
{
    /// <summary>
    /// Manages a file-backed JSON queue of pending Site property updates
    /// triggered by a Resident's site change.
    /// </summary>
    public static class ResidentSiteChangeQueueService
    {
        private static readonly string QueueFilePath = Path.Combine(
            Path.GetTempPath(),
            "IHFM_VAF_ResidentSiteChangeQueue.json"
        );

        private static readonly object _lock = new object();

        /// <summary>
        /// Adds a batch of items to the end of the queue.
        /// </summary>
        public static void Enqueue(IEnumerable<ResidentSiteChangeQueueItem> items)
        {
            lock (_lock)
            {
                var queue = ReadQueue();
                queue.AddRange(items);
                WriteQueue(queue);
            }
        }

        /// <summary>
        /// Removes and returns the first item from the queue, or null if empty.
        /// </summary>
        public static ResidentSiteChangeQueueItem Dequeue()
        {
            lock (_lock)
            {
                var queue = ReadQueue();
                if (queue.Count == 0)
                    return null;

                var item = queue[0];
                queue.RemoveAt(0);
                WriteQueue(queue);
                return item;
            }
        }

        /// <summary>
        /// Returns the number of items currently in the queue.
        /// </summary>
        public static int Count()
        {
            lock (_lock)
            {
                return ReadQueue().Count;
            }
        }

        private static List<ResidentSiteChangeQueueItem> ReadQueue()
        {
            if (!File.Exists(QueueFilePath))
                return new List<ResidentSiteChangeQueueItem>();

            try
            {
                string json = File.ReadAllText(QueueFilePath);
                return JsonConvert.DeserializeObject<List<ResidentSiteChangeQueueItem>>(json)
                    ?? new List<ResidentSiteChangeQueueItem>();
            }
            catch (Exception)
            {
                return new List<ResidentSiteChangeQueueItem>();
            }
        }

        private static void WriteQueue(List<ResidentSiteChangeQueueItem> queue)
        {
            string json = JsonConvert.SerializeObject(queue, Formatting.Indented);
            File.WriteAllText(QueueFilePath, json);
        }
    }
}
