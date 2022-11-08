using System;
using UnityEngine;

namespace RimuruDev.SiriusFuture
{
    [DisallowMultipleComponent]
    [HelpURL("https://t.me/AbyssMothGames")]
    public sealed class WordHandler : MonoBehaviour, IInitSystem
    {
        public Action OnFilteringAndSetTextDataset;

        [SerializeField, Space] public string[] wordArray; // Temp opened acccessor
        public string[] GetWordArray => wordArray;
        [SerializeField, Space] private string currentWord;
        [SerializeField, Space] private char[] currentWordChar;

        [SerializeField, HideInInspector] private GameDataContainer dataContainer;
        [SerializeField, HideInInspector] private UIHandler uiHandler;

        public int GetWordLenthNormolized => currentWordChar.Length - 1;
        public string GetCurrenWord => currentWord;
        public char[] GetCurrentWordChar => currentWordChar;

        private bool isFilteringByUniqueWords = false;

        private void Awake() => CheckRefs();

        private void OnEnable() => OnFilteringAndSetTextDataset += FilteringAndSetTextDataset;

        private void OnDisable() => OnFilteringAndSetTextDataset -= FilteringAndSetTextDataset;

        private void OnValidate() => CheckRefs();

        public void Init() => FilteringAndSetTextDataset();

        private void FilteringAndSetTextDataset()
        {
            if (!isFilteringByUniqueWords)
            {
                dataContainer.GetTextDataset.FilteringByUniqueWords();
                FillindWordArray();
            }
            else
                FillindWordArray();

            SetCurrentWord();

            isFilteringByUniqueWords = true;
        }

        private void FillindWordArray()
        {
            var dataset = dataContainer.GetTextDataset.GetFilteringByUniqueWords();

            if (dataset.Length <= 0 && isFilteringByUniqueWords == false)
            {
                Debug.Log($"Warning: [{isFilteringByUniqueWords} == false]");
                uiHandler.OnWarningPopup(true);
                //uiHandler.OnWinPopup(false);
            }

            if (dataset.Length <= 0 && isFilteringByUniqueWords == true)
            {
                Debug.Log($"Win: [{isFilteringByUniqueWords} == true]");
                uiHandler.OnWinPopup(true);
                // uiHandler.OnWarningPopup(false);
            }

            wordArray = dataset.Split(new char[] { '\n' });
        }

        private void SetCurrentWord()
        {
            currentWord = wordArray[GetRandomArrayElementIndex()];
            currentWordChar = currentWord.ToCharArray();
        }

        private int GetRandomArrayElementIndex() => new System.Random().Next(0, wordArray.Length);

        private void CheckRefs()
        {
            if (dataContainer == null)
                dataContainer = FindObjectOfType<GameDataContainer>();

            if (uiHandler == null)
                uiHandler = FindObjectOfType<UIHandler>();
        }
    }
}