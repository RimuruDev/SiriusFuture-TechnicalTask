using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RimuruDev.SiriusFuture
{
    public sealed class InitializationGame : MonoBehaviour
    {
        [SerializeField] private GameDataContainer dataContainer;
        [SerializeField] private Sturtup sturtup;
        [SerializeField] private UIHandler UIHandler;

        [SerializeField] private string currentWord;
        public string GetCurrenWord => currentWord;

        [SerializeField] private char[] currentWordChar;
        public char[] GetCurrentWordChar => currentWordChar;

        [SerializeField, TextArea(), Space(5)] private string[] array;
        [SerializeField] private Button[] allKeyboardButtons;// = new Button[26];

        // Init test value
        public int numattempt = 10;
        public int numScore = 0;
        private int wordHit = 0;

        private TextDataFilteringHandler filteringHandler = new TextDataFilteringHandler();

        public int GetWordLenthNormolized => currentWordChar.Length - 1;

        private void Awake()
        {
            if (dataContainer == null)
                dataContainer = FindObjectOfType<GameDataContainer>();

            if (sturtup == null)
                sturtup = FindObjectOfType<Sturtup>();

            if (UIHandler == null)
                UIHandler = FindObjectOfType<UIHandler>();

            allKeyboardButtons = new Button[26];
        }

        private void OnValidate()
        {
            if (dataContainer == null)
                dataContainer = FindObjectOfType<GameDataContainer>();

            if (sturtup == null)
                sturtup = FindObjectOfType<Sturtup>();

            if (UIHandler == null)
                UIHandler = FindObjectOfType<UIHandler>();

            allKeyboardButtons = new Button[26];
        }

        private void Start()
        {
            Initializator();
        }

        private void Update()
        {
            dataContainer.GetHeaderText.CurrentScoreText.text = $"Number of points: {dataContainer.GetHeaderValue.NumberOfScores}";
            dataContainer.GetHeaderText.CurrentAttemptsText.text = $"Number of attempts: {dataContainer.GetHeaderValue.NumberOfAttempts}";
        }

        public void Initializator()
        {
            InitWord();
            InitialWordToUnravel();
            InitialUserInterface();
            CacheAllKeyboardButtons();

            SetinitialAttemptValue();
        }

        private void InitWord()
        {
            // Filtred book data
            {
                if (filteringHandler == null) filteringHandler = new TextDataFilteringHandler();
                filteringHandler.FilteringByUniqueWords();
            }

            // Get filtrea string data
            string textData = filteringHandler.GetFilteringByUniqueWords();

            // Get current char[] answer word
            array = textData.Split(new char[] { '\n' });

            SetCurrentWord();

            void SetCurrentWord()
            {
                currentWord = array[GetRandomArrayElementIndex()];
                currentWordChar = currentWord.ToCharArray();
            }

            int GetRandomArrayElementIndex() => new System.Random().Next(0, array.Length);
        }

        private void InitialWordToUnravel()
        {
            ClosedAllWordElement();
            EnableEmptyElement();
            OpenWordElements();

            void ClosedAllWordElement()
            {
                for (int i = 0; i < dataContainer.GetElementContainer.Element.Length; i++)
                    dataContainer.GetElementContainer.Element[i].GetChild(0).gameObject.SetActive(false);
            }

            void EnableEmptyElement()
            {
                //  var currentWord = wordDataHandler.GetCurrentWordChar;
                //  var currentWordLengthNormalize = currentWord.Length - 1;
                var allElementsLength = dataContainer.GetElementContainer.Element.Length;

                Debug.Log($"Word length: {GetWordLenthNormolized}");
                // Debug.Log($"Word length: {currentWordLengthNormalize}");
                Debug.Log($"All element length: {allElementsLength}");
                // Temp trash solution

                for (int i = GetWordLenthNormolized; i < allElementsLength; i++)
                {
                    var element = dataContainer.GetElementContainer.Element[i];
                    element.GetComponent<Image>().color = new Color(74, 70, 69, 0);
                    element.GetChild(0).gameObject.SetActive(false);
                }
            }

            void OpenWordElements()
            {
                for (int i = 0; i < GetWordLenthNormolized; i++)
                {
                    dataContainer.GetElementContainer.Element[i].GetChild(0).gameObject
                        .GetComponent<TMPro.TMP_Text>().text = GetCurrentWordChar[i].ToString().ToUpper();
                }
            }
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

        private void CacheAllKeyboardButtons()
        {
            int headerKeywordLength = dataContainer.GetUserInterfaceData.HeaderUserInterfaceKeyboard.Length;
            int middleKeywordLength = dataContainer.GetUserInterfaceData.MiddleUserInterfaceKeyboard.Length;
            int bottomKeywordLength = dataContainer.GetUserInterfaceData.BottomUserInterfaceKeyboard.Length;

            for (int i = 0; i < headerKeywordLength; i++)
            {
                allKeyboardButtons[i] = dataContainer.GetUserInterfaceData.HeaderUserInterfaceKeyboard[i]
                    .GetComponent<Button>();
            }

            for (int i = 0; i < middleKeywordLength; i++)
            {
                allKeyboardButtons[headerKeywordLength + i] = dataContainer.GetUserInterfaceData.MiddleUserInterfaceKeyboard[i]
                   .GetComponent<Button>();
            }

            for (int i = 0; i < bottomKeywordLength; i++)
            {
                allKeyboardButtons[headerKeywordLength + middleKeywordLength + i] = dataContainer.GetUserInterfaceData.BottomUserInterfaceKeyboard[i]
                  .GetComponent<Button>();
            }

            SubscribeToAnEvent();

            void SubscribeToAnEvent()
            {
                for (int i = 0; i < allKeyboardButtons.Length; i++)
                {
                    var localButton = allKeyboardButtons[i];

                    localButton.onClick.AddListener(() => OnClick(localButton));
                }
            }

            void OnClick(Button butoon)
            {
                var buttonName = butoon.name;
                //var currentWord = wordDataHandler.GetCurrentWordChar;
                string currenAnswerWord = string.Empty;
                bool isAnswerWord = default;

                for (int i = 0; i < GetWordLenthNormolized; i++)
                {
                    if (buttonName == currentWord[i].ToString().ToUpper())
                    {
                        isAnswerWord = true;
                        currenAnswerWord = buttonName;
                        break;
                    }
                    else
                        isAnswerWord = false;
                }

                if (isAnswerWord)
                {
                    OpenAnswerWords(currenAnswerWord);
                }
                else
                    DisableButton();

                //   EventHandler.Instance.OnInitUpdateUI?.Invoke();
            }
        }

        private void OpenAnswerWords(string word)
        {
            var currentWord = GetCurrentWordChar;

            for (int i = 0; i < currentWord.Length - 1; i++)
            {
                if (word == currentWord[i].ToString().ToUpper())
                {
                    dataContainer.GetElementContainer.Element[i].GetChild(0).gameObject.SetActive(true);
                    dataContainer.GetHeaderValue.NumberOfScores++;
                    Debug.Log("NumberOfScores ++");
                    CheckWin();
                }
            }

            void CheckWin()
            {
                wordHit += 1;
                //var currentWord = wordDataHandler.GetCurrentWordChar;

                if (GetWordLenthNormolized == wordHit)
                {
                    Debug.Log("[============== Popup win ==============]");
                    Debug.Log($"Filaai!!!  NumberOfAttempts == {dataContainer.GetHeaderValue.NumberOfScores} + {dataContainer.GetHeaderValue.NumberOfAttempts}");
                    dataContainer.GetHeaderValue.NumberOfScores += dataContainer.GetHeaderValue.NumberOfAttempts;
                }

            }
        }

        private void DisableButton()
        {
            dataContainer.GetHeaderValue.NumberOfAttempts -= 1;
            Debug.Log("NumberOfAttempts --");

            if (dataContainer.GetHeaderValue.NumberOfAttempts == 0)
            {
                Debug.Log("[============== Popup failure ==============]");

            }
        }

        private void SetinitialAttemptValue()
        {
            dataContainer.GetHeaderValue.NumberOfAttempts = numattempt;
            dataContainer.GetHeaderValue.NumberOfScores = numScore;
        }
    }
}
