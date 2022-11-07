using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

namespace RimuruDev.SiriusFuture
{
    public sealed class InitializationGame : MonoBehaviour
    {
        [Header("Container")]
        public GameObject[] container;



        [SerializeField] private GameDataContainer dataContainer;
        [SerializeField] private Sturtup sturtup;
        [SerializeField] private UIHandler UIHandler;

        [SerializeField] private string currentWord;
        public string GetCurrenWord => currentWord;
        [SerializeField] private char[] currentWordChar;
        public char[] GetCurrentWordChar => currentWordChar;
        [SerializeField, TextArea(), Space(5)] private string[] array;
        [SerializeField] private Button[] allKeyboardButtons;
        private Color normalColor = new Color(255, 255, 255, 255);
        private Color blackColor = new Color(0, 0, 0, 255);
        private Color invisibleColor = new Color(74, 70, 69, 0);
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
            //if (null == container) return;

            for (int i = 0; i < container.Length; i++)
            {
                if (container[i].GetComponent<IInitSystem>() == null)
                    container[i] = null;
            }


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
            for (int i = 0; i < container.Length; i++)
                container[i].GetComponent<IInitSystem>().Init();



            //  Initializator();
            //  SetinitialAttemptValue();
        }

        private void Update()
        {
            //dataContainer.GetHeaderText.CurrentScoreText.text = $"Number of points: {dataContainer.GetHeaderValue.NumberOfScores}";
            //    dataContainer.GetHeaderText.CurrentAttemptsText.text = $"Number of attempts: {dataContainer.GetHeaderValue.NumberOfAttempts}";
        }

        #region Old
        /*
        private bool isWin;
        public void Initializator()
        {
            if (isWin == true)
                return;

            InitWord();

            if (isWin == true)
                return;

            InitialUserInterface();
            InitialWordToUnravel();
            CacheAllKeyboardButtons();
            NormalButtons();
        }



        private bool isInit = false;
        private void InitWord()
        {
            if (isInit == false)
            {
                if (filteringHandler == null) filteringHandler = new TextDataFilteringHandler();
                filteringHandler.FilteringByUniqueWords();

                string textData = filteringHandler.GetFilteringByUniqueWords();

                if (textData.Length <= 0)
                {
                    Debug.Log("Win Game!");
                    isWin = true;
                    return;
                }

                array = textData.Split(new char[] { '\n' });
            }
            else
            {
                string textData = filteringHandler.GetFilteringByUniqueWords();

                if (textData.Length <= 0)
                {
                    Debug.Log("Win Game!");
                    isWin = true;
                    return;
                }

                array = textData.Split(new char[] { '\n' });
            }

            SetCurrentWord();

            isInit = true;

            void SetCurrentWord()
            {
                currentWord = array[GetRandomArrayElementIndex()];
                currentWordChar = currentWord.ToCharArray();
            }

            int GetRandomArrayElementIndex() => new System.Random().Next(0, array.Length);
        }



        private readonly string pathOut = @$"{Application.streamingAssetsPath}/SortedTextOfAlicesBook.txt";
        private void NextSession()
        {
            PlayerPrefs.SetInt("Score", dataContainer.GetHeaderValue.NumberOfScores);

            // Removed old element
            {
                string word = GetCurrenWord;

                var arrayCopy = filteringHandler.Remove_(array, word);

                string controlCharacter = "\r\n";
                string pattern = @"\b[a-z]+\b";

                string resultStr = string.Join(controlCharacter, arrayCopy);

                File.WriteAllText(pathOut, string.Join(controlCharacter, Regex.Matches(resultStr, pattern, RegexOptions.IgnoreCase)
                     .Select(x => x.Value)
                     .Where(x => x.Length > 4)
                     .GroupBy(x => x)
                     .Select(x => x.Key.ToLower())
                     .OrderBy(x => x)
                     .Distinct(StringComparer.CurrentCultureIgnoreCase)));

                // File.WriteAllText(@$"{Application.streamingAssetsPath}/SortedTextOfAlicesBook.txt", array);
            }

            NormalButtons();
            InitWord();
            InitialWordToUnravel();
        }

        private void InitialWordToUnravel()
        {
            int allElementsLength = dataContainer.GetElementContainer.Element.Length;

            ClosedAllWordElement();
            OpenWordElements();
            EnableEmptyElement();

            void ClosedAllWordElement()
            {
                for (int i = 0; i < allElementsLength; i++)
                    dataContainer.GetElementContainer.Element[i].GetChild(0).gameObject.SetActive(false);
            }

            void EnableEmptyElement()
            {
                for (int i = GetWordLenthNormolized; i < allElementsLength; i++)
                {
                    var element = dataContainer.GetElementContainer.Element[i];
                    element.GetComponent<Image>().color = new Color(74, 70, 69, 0);
                }
            }

            void OpenWordElements()
            {
                for (int i = 0; i < GetCurrentWordChar.Length - 1; i++)
                {
                    dataContainer.GetElementContainer.Element[i].GetComponent<Image>().color = blackColor;
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

        private void NormalButtons()
        {
            var length = allKeyboardButtons.Length;

            for (int i = 0; i < length; i++)
            {
                allKeyboardButtons[i].GetComponent<Image>().color = normalColor;
                allKeyboardButtons[i].interactable = true;
                allKeyboardButtons[i].transform.GetChild(0).gameObject.SetActive(true);
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
                    OpenAnswerWords(currenAnswerWord, butoon);
                else
                    DisableButton(butoon);
            }
        }

        private void OpenAnswerWords(string word, Button button)
        {
            var currentWord = GetCurrentWordChar;

            for (int i = 0; i < currentWord.Length - 1; i++)
            {
                if (word == currentWord[i].ToString().ToUpper())
                {
                    dataContainer.GetElementContainer.Element[i].GetChild(0).gameObject.SetActive(true);
                    dataContainer.GetHeaderValue.NumberOfScores++;
                    button.interactable = false;
                    CheckWin();
                }
            }

            void CheckWin()
            {
                wordHit += 1;

                if (GetWordLenthNormolized == wordHit)
                {
                    wordHit = 0;
                    dataContainer.GetHeaderValue.NumberOfScores += dataContainer.GetHeaderValue.NumberOfAttempts;
                    NextSession();
                }

            }
        }

        private void DisableButton(Button butoon)
        {
            dataContainer.GetHeaderValue.NumberOfAttempts -= 1;

            butoon.interactable = false;
            butoon.gameObject.GetComponent<Image>().color = new Color(74, 70, 69, 0);
            butoon.transform.GetChild(0).gameObject.SetActive(false);

            if (dataContainer.GetHeaderValue.NumberOfAttempts == 0)
                UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
        }

        private void SetinitialAttemptValue()
        {
            dataContainer.GetHeaderValue.NumberOfAttempts = numattempt;

            if (PlayerPrefs.GetInt("Score") == 0)
                dataContainer.GetHeaderValue.NumberOfScores = numScore;
            else
                dataContainer.GetHeaderValue.NumberOfScores = PlayerPrefs.GetInt("Score");
        }
        */
        #endregion
    }
}