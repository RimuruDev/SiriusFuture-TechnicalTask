using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RimuruDev.SiriusFuture
{
    public sealed class Sturtup : MonoBehaviour
    {
        public Action<string> OnDataPreparationEnd;
        public Action OnInitializationEnd;
        [SerializeField, HideInInspector] private GameDataContainer dataContainer;
        [SerializeField, HideInInspector] private WordDataHandler wordDataHandler;
        private readonly TextDataFilteringHandler textDataFilteringHandler = null;

        public int numattempt = 10;
        public int numScore = 0;

        private void Awake()
        {
            CheckRef();
            DataPreparation(textDataFilteringHandler);
        }

        private void Start()
        {
            InitialWordToUnravel();
            InitialUserInterface();

            OnInitializationEnd?.Invoke();
        }

        private void OnEnable()
        {
            wordDataHandler.OnInitializationWordDataContainerEnd += InitialWordToUnravel;
        }

        private void OnDisable()
        {
            wordDataHandler.OnInitializationWordDataContainerEnd -= InitialWordToUnravel;
        }

        private void DataPreparation(TextDataFilteringHandler filteringHandler)
        {
            if (filteringHandler == null)
                filteringHandler = new TextDataFilteringHandler();

            filteringHandler.FilteringByUniqueWords();

            Debug.Log($"DataPreparation: => {filteringHandler.GetFilteringByUniqueWords()}");

            var str = filteringHandler.GetFilteringByUniqueWords();

            OnDataPreparationEnd?.Invoke(str);
        }

        private void InitialUserInterface()
        {
            int headerKeywordLength = dataContainer.GetUserInterfaceData.HeaderUserInterfaceKeyboard.Length;
            int middleKeywordLength = dataContainer.GetUserInterfaceData.MiddleUserInterfaceKeyboard.Length;
            int bottomKeywordLength = dataContainer.GetUserInterfaceData.BottomUserInterfaceKeyboard.Length;

            for (int i = 0; i < headerKeywordLength; i++)
            {
                dataContainer.GetUserInterfaceData.HeaderUserInterfaceKeyboard[i].GetChild(0)
                    .GetComponent<TMPro.TMP_Text>()
                    .text = dataContainer.GetKeywordWords.headerKeybardWords[i].ToString().ToUpper();

                dataContainer.GetUserInterfaceData.HeaderUserInterfaceKeyboard[i].name = dataContainer.GetKeywordWords.headerKeybardWords[i].ToString().ToUpper();
            }

            for (int i = 0; i < middleKeywordLength; i++)
            {
                dataContainer.GetUserInterfaceData.MiddleUserInterfaceKeyboard[i].GetChild(0)
                   .GetComponent<TMPro.TMP_Text>()
                   .text = dataContainer.GetKeywordWords.middleKeybardWord[i].ToString().ToUpper();

                dataContainer.GetUserInterfaceData.MiddleUserInterfaceKeyboard[i].name = dataContainer.GetKeywordWords.middleKeybardWord[i].ToString().ToUpper();
            }

            for (int i = 0; i < bottomKeywordLength; i++)
            {
                dataContainer.GetUserInterfaceData.BottomUserInterfaceKeyboard[i].GetChild(0)
                   .GetComponent<TMPro.TMP_Text>()
                   .text = dataContainer.GetKeywordWords.bottomKeybardWords[i].ToString().ToUpper();

                dataContainer.GetUserInterfaceData.BottomUserInterfaceKeyboard[i].name = dataContainer.GetKeywordWords.bottomKeybardWords[i].ToString().ToUpper();
            }
        }

        private void InitialWordToUnravel()
        {
            ClosedAllWordElement();
            EnableEmptyElement();
            OpenWordElements();

            void ClosedAllWordElement()
            {
                for (int i = 0; i < dataContainer.GetElementContainer.Element.Length; i++)
                {
                    dataContainer.GetElementContainer.Element[i].GetChild(0).gameObject.SetActive(false);
                }
            }

            void EnableEmptyElement()
            {
                var currentWord = wordDataHandler.GetCurrentWordChar;
                var currentWordLengthNormalize = currentWord.Length - 1;
                var allElementsLength = dataContainer.GetElementContainer.Element.Length;

                Debug.Log($"Word length: {currentWordLengthNormalize}");
                // Debug.Log($"Word length: {currentWordLengthNormalize}");
                Debug.Log($"All element length: {allElementsLength}");
                // Temp trash solution

                for (int i = currentWordLengthNormalize; i < allElementsLength; i++)
                {
                    var element = dataContainer.GetElementContainer.Element[i];
                    element.GetComponent<Image>().color = new Color(74, 70, 69, 0);
                    element.GetChild(0).gameObject.SetActive(false);
                }


            }

            void OpenWordElements()
            {
                for (int i = 0; i < wordDataHandler.GetCurrenWord.Length - 1; i++)
                {
                    dataContainer.GetElementContainer.Element[i].GetChild(0).gameObject
                        .GetComponent<TMPro.TMP_Text>().text = wordDataHandler.GetCurrentWordChar[i].ToString().ToUpper();
                }
            }
        }

        private void OnValidate() => CheckRef();

        private void CheckRef()
        {
            if (dataContainer == null)
                dataContainer = FindObjectOfType<GameDataContainer>();

            if (wordDataHandler == null)
                wordDataHandler = FindObjectOfType<WordDataHandler>();
        }
    }
}