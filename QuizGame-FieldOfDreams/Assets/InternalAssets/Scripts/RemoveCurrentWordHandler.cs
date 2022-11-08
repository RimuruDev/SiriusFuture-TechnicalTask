using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;

namespace RimuruDev.SiriusFuture
{
    [HelpURL("https://t.me/AbyssMothGames")]
    public sealed class RemoveCurrentWordHandler : MonoBehaviour
    {
        public Action OnRemoveCurrentWordHandler;

        [SerializeField, HideInInspector] private WordHandler wordHandler;
        [SerializeField, HideInInspector] private GameDataContainer dataContainer;

        private readonly string path = @$"{Application.streamingAssetsPath}/OriginalTextOfAlicesBook.txt";
        private readonly string pathOut = @$"{Application.streamingAssetsPath}/SortedTextOfAlicesBook.txt";

        private void Awake() => CheckRefs();

        private void OnEnable() => OnRemoveCurrentWordHandler += RemoveCurrentWordWithArrayList;
        private void OnDisable() => OnRemoveCurrentWordHandler -= RemoveCurrentWordWithArrayList;

        [System.Diagnostics.Conditional(Tag.DEBUG)]
        private void OnValidate() => CheckRefs();
        
        private void RemoveCurrentWordWithArrayList()
        {
            string word = wordHandler.GetCurrenWord;

            var arrayCopy = dataContainer.GetTextDataset.RemoveCurrentWordFromArray(ref wordHandler.wordArray, word);

            string controlCharacter = "\r\n";
            string pattern = @"\b[a-z]+\b";

            string resultStr = string.Join(controlCharacter, arrayCopy);

            File.WriteAllText(pathOut, string.Join(controlCharacter, Regex.Matches(resultStr, pattern, RegexOptions.IgnoreCase)
                 .Select(x => x.Value)
                 .Where(x => (x.Length >= dataContainer.GameplaySettings.MaximumWordLength && x.Length <= dataContainer.GetElementContainer.Element.Length))
                 .GroupBy(x => x)
                 .Select(x => x.Key.ToLower())
                 .OrderBy(x => x)
                 .Distinct(StringComparer.CurrentCultureIgnoreCase)));
        }

        private void CheckRefs()
        {
            if (dataContainer == null)
                dataContainer = FindObjectOfType<GameDataContainer>();

            if (wordHandler == null)
                wordHandler = FindObjectOfType<WordHandler>();
        }
    }
}