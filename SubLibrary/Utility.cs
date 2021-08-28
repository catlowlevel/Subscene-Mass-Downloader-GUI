using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SubLibrary
{
    public static class Utility
    {
        private static HashSet<char> _allowedChars = new HashSet<char>(" abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890_-.".ToArray());
        private static HashSet<char> _unallowedChars = new HashSet<char>("\t\n\r\b\\/:*?\"<>|".ToArray());
        public static string FilterString(string str)
        {
            // tempbuffer
            char[] buffer = new char[str.Length];
            int index = 0;

            // check each character
            foreach (var ch in str)
                if (!_unallowedChars.Contains(ch))
                    buffer[index++] = ch;

            // return the new string.
            return new String(buffer, 0, index);
        }


        public static IEnumerable<List<T>> SplitList<T>(List<T> locations, int nSize = 30)
        {
            //https://stackoverflow.com/a/11463800
            for (int i = 0; i < locations.Count; i += nSize)
            {
                yield return locations.GetRange(i, Math.Min(nSize, locations.Count - i));
            }
        }
        public static async Task WhenAllEx<T>(List<Task<T>> tasks, Action<List<Task<T>>> reportProgressAction)
        {
            // get Task which completes when all 'tasks' have completed
            var whenAllTask = Task.WhenAll(tasks);
            for (; ; )
            {
                // get Task which completes after 250ms
                var timer = Task.Delay(250); // you might want to make this configurable
                // Wait until either all tasks have completed OR 250ms passed
                await Task.WhenAny(whenAllTask, timer);

                reportProgressAction?.Invoke(tasks);
                if (whenAllTask.IsCompleted)
                {
                    return;
                }
            }
        }
    }
}
