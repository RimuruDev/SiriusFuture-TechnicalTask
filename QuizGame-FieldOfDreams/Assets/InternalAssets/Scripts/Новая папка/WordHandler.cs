using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using System.Text.RegularExpressions;
using System.IO;
using System.Linq;

namespace RimuruDev.SiriusFuture
{
    public sealed class WordHandler : MonoBehaviour, IInitSystem
    {
        public Action OnFilteringAndSetTextDataset;

        [SerializeField, Space] private string[] wordArray;
        public string[] GetWordArray => wordArray;
        [SerializeField, Space] private string currentWord;
        [SerializeField, Space] private char[] currentWordChar;

        public int GetWordLenthNormolized => currentWordChar.Length - 1;
        public string GetCurrenWord => currentWord;
        public char[] GetCurrentWordChar => currentWordChar;

        private TextDataFilteringHandler textDataset = new TextDataFilteringHandler();
        private bool isFilteringByUniqueWords = false;

        private void OnEnable()
        {
            OnFilteringAndSetTextDataset += FilteringAndSetTextDataset;
        }

        private void OnDisable()
        {
            OnFilteringAndSetTextDataset -= FilteringAndSetTextDataset;
        }

        public void Init()
        {
            FilteringAndSetTextDataset();
        }

        private void FilteringAndSetTextDataset()
        {
            if (!isFilteringByUniqueWords)
            {
                textDataset.FilteringByUniqueWords();
                FillindWordArray();
            }
            else
                FillindWordArray();

            SetCurrentWord();
        }

        private void FillindWordArray()
        {
            var dataset = textDataset.GetFilteringByUniqueWords();

            if (dataset.Length < 0)
                Debug.Log("Win Game! Заглушка!");

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