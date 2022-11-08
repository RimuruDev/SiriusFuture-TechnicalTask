using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;

namespace RimuruDev.SiriusFuture
{
    [Serializable]
    public sealed class TextDataFilteringHandler
    {
        private readonly GameDataContainer dataContainer = null;
        private readonly string path = @$"{Application.streamingAssetsPath}/OriginalTextOfAlicesBook.txt";
        private readonly string pathOut = @$"{Application.streamingAssetsPath}/SortedTextOfAlicesBook.txt";

        public TextDataFilteringHandler(GameDataContainer dataContainer) => this.dataContainer = dataContainer;

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
                     .Where(x => (x.Length >= dataContainer.GameplaySettings.MaximumWordLength && x.Length <= dataContainer.GetElementContainer.Element.Length))
                     .GroupBy(x => x)
                     .Select(x => x.Key.ToLower())
                     .OrderBy(x => x)
                     .Distinct(StringComparer.CurrentCultureIgnoreCase)));
            }
            stopwatch.Stop();

            //  UnityEngine.Debug.Log($"Seconds: [{stopwatch.Elapsed.Seconds}]. Milliseconds: [{stopwatch.Elapsed.Milliseconds}].");
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

                result = string.Join(controlCharacter, Regex.Matches(File.ReadAllText(pathOut), pattern, RegexOptions.IgnoreCase)
                    .Select(x => x.Value)
                    .Where(x => (x.Length >= dataContainer.GameplaySettings.MaximumWordLength && x.Length <= dataContainer.GetElementContainer.Element.Length))
                    .GroupBy(x => x)
                    .Select(x => x.Key.ToLower())
                    .OrderBy(x => x)
                    .Distinct(StringComparer.CurrentCultureIgnoreCase));
            }
            stopwatch.Stop();

            //  UnityEngine.Debug.Log($"Seconds: [{stopwatch.Elapsed.Seconds}]. Milliseconds: [{stopwatch.Elapsed.Milliseconds}].");

            return result;
        }

        public string[] RemoveCurrentWordFromArray(ref string[] array, string item)
        {
            int remInd = -1;

            for (int i = 0; i < array.Length; ++i)
            {
                if (array[i] == item.ToLower())
                {
                    remInd = i;
                    break;
                }
            }

            string[] retVal = new string[array.Length - 1];

            for (int i = 0, j = 0; i < retVal.Length; ++i, ++j)
            {
                if (j == remInd)
                    ++j;

                retVal[i] = array[j];
            }

            return retVal;
        }
    }
}