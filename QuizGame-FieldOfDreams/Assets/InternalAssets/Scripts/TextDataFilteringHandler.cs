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

        public void RemovedWard(string ward)
        {
            Stopwatch stopwatch = new();
            stopwatch.Start();
            {


                /*
                string controlCharacter = "\r\n";
                string pattern = @"\b[a-z]+\b";

                var originSortData = string.Join(controlCharacter, Regex.Matches(File.ReadAllText(pathOut), pattern, RegexOptions.IgnoreCase)
                    .Select(x => x.Value)
                    .Where(x => x.Length > maximumWordLength)
                    .GroupBy(x => x)
                    .Select(x => x.Key.ToLower())
                    .OrderBy(x => x)
                    .Distinct(StringComparer.CurrentCultureIgnoreCase));
                */
                /*
                File.WriteAllText(pathOut, string.Join(controlCharacter, Regex.Matches(originSortData, @$"\b[{ward}]+\b", RegexOptions.IgnoreCase)
                    .Select(x => x.Value)
                    .Contains(!ward.ToLower())));
                */

                // originSortData.Where(s => !s.Contains(ward.ToLower()));

                /*
                var lines = File.ReadAllLines(pathOut).ToList();
                lines.RemoveAt(ward.IndexOf(ward));
                File.WriteAllLines(pathOut, lines);
                */
                // string controlCharacter = "\r\n";
                //  string pattern = @"\b[a-z]+\b";

                // var tmp = string.Join(controlCharacter, Regex.Matches(File.ReadAllText(pathOut), pattern, RegexOptions.IgnoreCase);
                //  tmp.Replace(tmp, ward);
                /*

                string controlCharacter = "\r\n";
                string pattern = @"\b[a-z]+\b";

               string result = string.Join(controlCharacter, Regex.Matches(File.ReadAllText(path), pattern, RegexOptions.IgnoreCase)
                    .Select(x => x.Value)
                    .Where(x => x.Length > maximumWordLength)
                    .GroupBy(x => x)
                    .Select(x => x.Key.ToLower())
                    .OrderBy(x => x)
                    .Distinct(StringComparer.CurrentCultureIgnoreCase));
                */
                // File.WriteAllLines(pathOut, File.ReadAllLines(pathOut).Where(v => v.Trim().IndexOf(ward) == -1));

                //   var re = File.ReadAllLines(pathOut, Encoding.Default).Where(s => !s.Contains(ward));
                // File.WriteAllLines(pathOut, re, Encoding.Default);

                //var re = File.ReadAllLines(pathOut, Encoding.Default).Where(s => !s.Contains(ward.ToLower()));
                // File.WriteAllLines(pathOut, re, Encoding.Default);


                // string[] deluser = System.IO.File.ReadAllLines(pathOut, Encoding.Default);
                //deluser = (string[])deluser.Where(line => line != ward);
                // System.IO.File.WriteAllLines(pathOut, deluser, Encoding.Default);

                //var array = File.ReadAllText(path);
                //var result = string.Join(ward, array.Split(array, StringSplitOptions.RemoveEmptyEntries));
                // File.WriteAllText(pathOut, result);
            }
            stopwatch.Stop();
            UnityEngine.Debug.Log("Remove word");
            UnityEngine.Debug.Log($"Seconds: [{stopwatch.Elapsed.Seconds}]. Milliseconds: [{stopwatch.Elapsed.Milliseconds}].");
        }
        public string[] Remove_(ref string[] array, string item)
        {
            int remInd = -1;

            for (int i = 0; i < array.Length; ++i)
            {
                if (array[i] == item)
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

    /*
    public static class Extensions
    {
        public static string[] RemoveAt<T>(this string[] source, string index)
        {
            string[] dest = new string[source.Length - 1];

            for (int i = 0, j = 0; i < source.Length; i++)
            {
                if (source[i] != index)
                {
                    dest[j] = source[i];
                    j++;
                }
            }

            return dest;
        }
    }*/
}
