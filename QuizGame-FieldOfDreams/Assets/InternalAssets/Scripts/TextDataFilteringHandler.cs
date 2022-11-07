using System;
using System.IO;
using System.Linq;
using System.Diagnostics;
using System.Text.RegularExpressions;
using UnityEngine;

namespace RimuruDev.SiriusFuture
{
    public sealed class TextDataFilteringHandler
    {
        private readonly string path = @$"{Application.streamingAssetsPath}/OriginalTextOfAlicesBook.txt";
        private readonly string pathOut = @$"{Application.streamingAssetsPath}/SortedTextOfAlicesBook.txt";

        private readonly int maximumWordLength = 4; // 13 maximum length

        public void FilteringByUniqueWords()
        {
            Stopwatch stopwatch = new();
            stopwatch.Start();
            {
                if (!File.Exists(path)) File.Create(path);
                if (!File.Exists(pathOut)) File.Create(pathOut);

                string controlCharacter = "\r\n";
                string pattern = @"\b[a-z]+\b";

                File.WriteAllText(pathOut, string.Join(controlCharacter, Regex.Matches(File.ReadAllText(path), pattern, RegexOptions.IgnoreCase)
                     .Select(x => x.Value)
                     .Where(x => x.Length > maximumWordLength)
                     .GroupBy(x => x)
                     .Select(x => x.Key.ToLower())
                     .OrderBy(x => x)
                     .Distinct(StringComparer.CurrentCultureIgnoreCase)));
            }
            stopwatch.Stop();

            UnityEngine.Debug.Log($"Seconds: [{stopwatch.Elapsed.Seconds}]. Milliseconds: [{stopwatch.Elapsed.Milliseconds}].");
        }

        public string GetFilteringByUniqueWords()
        {
            string result = default;

            Stopwatch stopwatch = new();
            stopwatch.Start();
            {
                if (!File.Exists(path)) File.Create(path);
                if (!File.Exists(pathOut)) File.Create(pathOut);

                string controlCharacter = "\r\n";
                string pattern = @"\b[a-z]+\b";

                result = string.Join(controlCharacter, Regex.Matches(File.ReadAllText(path), pattern, RegexOptions.IgnoreCase)
                    .Select(x => x.Value)
                    .Where(x => x.Length > maximumWordLength)
                    .GroupBy(x => x)
                    .Select(x => x.Key.ToLower())
                    .OrderBy(x => x)
                    .Distinct(StringComparer.CurrentCultureIgnoreCase));
            }
            stopwatch.Stop();

            UnityEngine.Debug.Log($"Seconds: [{stopwatch.Elapsed.Seconds}]. Milliseconds: [{stopwatch.Elapsed.Milliseconds}].");

            return result;
        }
    }
}
