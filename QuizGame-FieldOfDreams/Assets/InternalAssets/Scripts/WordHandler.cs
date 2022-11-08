using System;
using Unity.VisualScripting;
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

        public int GetWordLenthNormolized => currentWordChar.Length - 1;
        public string GetCurrenWord => currentWord;
        public char[] GetCurrentWordChar => currentWordChar;

        private GameDataContainer dataContainer;
        private UIHandler uiHandler;
        private bool isFilteringByUniqueWords = false;

        private void Awake()
        {
            if (dataContainer == null)
                dataContainer = FindObjectOfType<GameDataContainer>();

            if (uiHandler == null)
                uiHandler = FindObjectOfType<UIHandler>();
        }
           
        private void OnEnable() => OnFilteringAndSetTextDataset += FilteringAndSetTextDataset;

        private void OnDisable() => OnFilteringAndSetTextDataset -= FilteringAndSetTextDataset;

        public void Init() => FilteringAndSetTextDataset();

        private void FilteringAndSetTextDataset()
        {
            if (!isFilteringByUniqueWords)
            {
                //  textDataset.FilteringByUniqueWords();
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

            if (dataset.Length <= 0 && !isFilteringByUniqueWords)
                uiHandler.OnWarningPopup?.Invoke();

            if (dataset.Length <= 0 && isFilteringByUniqueWords)
                uiHandler.OnWinPopup?.Invoke();

            wordArray = dataset.Split(new char[] { '\n' });
        }

        private void SetCurrentWord()
        {
            currentWord = wordArray[GetRandomArrayElementIndex()];
            currentWordChar = currentWord.ToCharArray();
        }

        private int GetRandomArrayElementIndex() => new System.Random().Next(0, wordArray.Length);
    }
}