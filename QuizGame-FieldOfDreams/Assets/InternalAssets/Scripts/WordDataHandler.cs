using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using UnityEngine;
using System.Security.Cryptography;

namespace RimuruDev.SiriusFuture
{
    public sealed class WordDataHandler : MonoBehaviour
    {
        public Action OnInitializationWordDataContainerEnd;
        [SerializeField, HideInInspector] private Sturtup sturtup;
        [SerializeField] private string currentWord;
        public string GetCurrenWord => currentWord;
        [SerializeField] private char[] currentWordChar;
        public char[] GetCurrentWordChar => currentWordChar;
        [SerializeField, TextArea(), Space(5)] private string[] array;

        private void Awake()
        {
            Checkrefs();
        }

        private void OnEnable()
        {
            sturtup.OnDataPreparationEnd += InitializationWordDataContainer;
        }

        private void OnDisable()
        {
            sturtup.OnDataPreparationEnd -= InitializationWordDataContainer;
        }

        private void InitializationWordDataContainer(string allFilteredData)
        {
            Debug.Log("InitializationWordDataContainer");
            array = allFilteredData.Split(new char[] { '\n' });

            Debug.Log($"Array = {array}");

            SetCurrentWord();
        }

        private void SetCurrentWord()
        {
            currentWord = array[GetRandomArrayElementIndex()];
            currentWordChar = currentWord.ToCharArray();

            OnInitializationWordDataContainerEnd?.Invoke();
        }

        private int GetRandomArrayElementIndex() => new System.Random().Next(0, array.Length);

        private void OnValidate() => Checkrefs();

        private void Checkrefs()
        {
            if (sturtup == null)
                sturtup = FindObjectOfType<Sturtup>();
        }
    }
}