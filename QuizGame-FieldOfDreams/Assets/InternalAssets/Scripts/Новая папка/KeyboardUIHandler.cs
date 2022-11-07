using System;
using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;
using System.IO;
using System.Linq;

namespace RimuruDev.SiriusFuture
{
    public sealed class KeyboardUIHandler : MonoBehaviour, IInitSystem
    {
        public Action OnFillingKeyboardUI;
        public Action OnNormalizationButtons;
        public Action OnRemoveCurrentWordWithArrayList;
        public Action OnSaveScore;
        public Action OnLoadScore;

        public Action OnCheckingProgress;
        public Action OnNextSestion;

        private GameDataContainer dataContainer;
        private WordElementSwitcher wordElementSwitcher;
        private WordHandler wordHandler;

        private TextDataFilteringHandler textDataset = new TextDataFilteringHandler();
        private readonly string path = @$"{Application.streamingAssetsPath}/OriginalTextOfAlicesBook.txt";
        private readonly string pathOut = @$"{Application.streamingAssetsPath}/SortedTextOfAlicesBook.txt";
        private readonly int maximumWordLength = 4; // 13 maximum length

        private int wordHit = 0;

        private void Awake()
        {
            if (dataContainer == null)
                dataContainer = FindObjectOfType<GameDataContainer>();

            if (wordHandler == null)
                wordHandler = FindObjectOfType<WordHandler>();

            if (wordElementSwitcher == null)
                wordElementSwitcher = FindObjectOfType<WordElementSwitcher>();
        }

        public void Init()
        {
            FillingKeyboardUI();
            CacheAllKeyboardButtons();
        }

        private void OnEnable()
        {
            OnFillingKeyboardUI += FillingKeyboardUI;
            OnNormalizationButtons += NormalizationButtons;
            OnRemoveCurrentWordWithArrayList += RemoveCurrentWordWithArrayList;
            OnSaveScore += SaveCurrentScore;
            OnLoadScore += LoadCurrentScore;
            OnCheckingProgress += CheckingProgress;
            OnNextSestion += NextSession;
        }

        private void OnDisable()
        {
            OnFillingKeyboardUI -= FillingKeyboardUI;
            OnNormalizationButtons -= NormalizationButtons;
            OnRemoveCurrentWordWithArrayList -= RemoveCurrentWordWithArrayList;
            OnSaveScore -= SaveCurrentScore;
            OnLoadScore -= LoadCurrentScore;
            OnCheckingProgress -= CheckingProgress;
            OnNextSestion -= NextSession;
        }

        private void FillingKeyboardUI()
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

        private void NormalizationButtons()
        {
            var length = dataContainer.KeyboardButtons.Length;

            for (int i = 0; i < length; i++)
            {
                dataContainer.KeyboardButtons[i].GetComponent<Image>().color = new Color(255, 255, 255, 255);
                dataContainer.KeyboardButtons[i].interactable = true;
                dataContainer.KeyboardButtons[i].transform.GetChild(0).gameObject.SetActive(true);
            }
        }

        private void CacheAllKeyboardButtons()
        {
            int headerKeywordLength = dataContainer.GetUserInterfaceData.HeaderUserInterfaceKeyboard.Length;
            int middleKeywordLength = dataContainer.GetUserInterfaceData.MiddleUserInterfaceKeyboard.Length;
            int bottomKeywordLength = dataContainer.GetUserInterfaceData.BottomUserInterfaceKeyboard.Length;

            Debug.Log("headerKeywordLength = " + headerKeywordLength);
            Debug.Log("dataContainer.GetUserInterfaceData.HeaderUserInterfaceKeyboard[0].name = " + dataContainer.GetUserInterfaceData.HeaderUserInterfaceKeyboard[0].name);
            for (int i = 0; i < headerKeywordLength; i++)
            {
                dataContainer.KeyboardButtons[i] = dataContainer.GetUserInterfaceData.HeaderUserInterfaceKeyboard[i]
                    .GetComponent<Button>();
            }

            for (int i = 0; i < middleKeywordLength; i++)
            {
                dataContainer.KeyboardButtons[headerKeywordLength + i] = dataContainer.GetUserInterfaceData.MiddleUserInterfaceKeyboard[i]
                   .GetComponent<Button>();
            }

            for (int i = 0; i < bottomKeywordLength; i++)
            {
                dataContainer.KeyboardButtons[headerKeywordLength + middleKeywordLength + i] = dataContainer.GetUserInterfaceData.BottomUserInterfaceKeyboard[i]
                  .GetComponent<Button>();
            }

            SubscribeToAnEvent();

            void SubscribeToAnEvent()
            {
                for (int i = 0; i < dataContainer.KeyboardButtons.Length; i++)
                {
                    var localButton = dataContainer.KeyboardButtons[i];

                    localButton.onClick.AddListener(() => OnClick(localButton));
                }
            }

            void OnClick(Button butoon)
            {
                var buttonName = butoon.name;
                string currenAnswerWord = string.Empty;
                bool isAnswerWord = default;

                for (int i = 0; i < wordHandler.GetWordLenthNormolized; i++)
                {
                    if (buttonName == wordHandler.GetCurrenWord[i].ToString().ToUpper())
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
            var currentWord = wordHandler.GetCurrentWordChar;

            for (int i = 0; i < currentWord.Length - 1; i++)
            {
                if (word == currentWord[i].ToString().ToUpper())
                {
                    dataContainer.GetElementContainer.Element[i].GetChild(0).gameObject.SetActive(true);
                    dataContainer.GetHeaderValue.NumberOfScores++;
                    button.interactable = false;
                    OnCheckingProgress?.Invoke();
                }
            }
        }

        private void CheckingProgress()
        {
            wordHit += 1;

            if (wordHandler.GetWordLenthNormolized == wordHit)
            {
                wordHit = 0;
                dataContainer.GetHeaderValue.NumberOfScores += dataContainer.GetHeaderValue.NumberOfAttempts;
                OnNextSestion?.Invoke();
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

        private void NextSession()
        {
            OnRemoveCurrentWordWithArrayList?.Invoke();
            OnSaveScore?.Invoke();
            OnNormalizationButtons?.Invoke();
            wordHandler.OnFilteringAndSetTextDataset?.Invoke();
            wordElementSwitcher.OnCloseAllWordTextElement?.Invoke();
            wordElementSwitcher.OnEnableAnswerWordTextElement?.Invoke();
            wordElementSwitcher.OnHideAllEmptyWordUIElement?.Invoke();
        }

        private void RemoveCurrentWordWithArrayList()
        {
            string word = wordHandler.GetCurrenWord;

            var arrayCopy = textDataset.Remove_(wordHandler.GetWordArray, word);

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
        }

        private void SaveCurrentScore() => PlayerPrefs.SetInt("Score", dataContainer.GetHeaderValue.NumberOfScores);

        private void LoadCurrentScore()
        {
            dataContainer.GetHeaderValue.NumberOfAttempts = dataContainer.numattempt;

            if (PlayerPrefs.GetInt("Score") == 0)
                dataContainer.GetHeaderValue.NumberOfScores = dataContainer.numScore;
            else
                dataContainer.GetHeaderValue.NumberOfScores = PlayerPrefs.GetInt("Score");
        }
    }
}