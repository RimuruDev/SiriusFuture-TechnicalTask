﻿using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

using UnityEngine;
using UnityEngine.UI;

namespace RimuruDev.SiriusFuture
{
    [DisallowMultipleComponent]
    [HelpURL("https://t.me/AbyssMothGames")]
    public sealed class KeyboardUIHandler : MonoBehaviour, IInitSystem
    {
        public Action OnFillingKeyboardUI;
        public Action OnNormalizationButtons;
        public Action OnRemoveCurrentWordWithArrayList;
        public Action OnCheckingProgress;
        public Action OnNextSestion;

        private GameDataContainer dataContainer;
        private WordElementSwitcher wordElementSwitcher;
        private WordHandler wordHandler;

        private readonly SaveUserProgress saveUserProgress = new SaveUserProgress();
        private readonly LoadUserProgress loadUserProgress = new LoadUserProgress();
        // private TextDataFilteringHandler textDataset;

        private readonly string path = @$"{Application.streamingAssetsPath}/OriginalTextOfAlicesBook.txt";
        private readonly string pathOut = @$"{Application.streamingAssetsPath}/SortedTextOfAlicesBook.txt";

        private int wordHit = 0;

        private void Awake()
        {
            if (dataContainer == null)
                dataContainer = FindObjectOfType<GameDataContainer>();

            if (wordHandler == null)
                wordHandler = FindObjectOfType<WordHandler>();

            if (wordElementSwitcher == null)
                wordElementSwitcher = FindObjectOfType<WordElementSwitcher>();

            //textDataset = new TextDataFilteringHandler(dataContainer);
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
            saveUserProgress.OnEnabled();
            loadUserProgress.OnEnabled();
            OnCheckingProgress += CheckingProgress;
            OnNextSestion += NextSession;
        }

        private void OnDisable()
        {
            OnFillingKeyboardUI -= FillingKeyboardUI;
            OnNormalizationButtons -= NormalizationButtons;
            OnRemoveCurrentWordWithArrayList -= RemoveCurrentWordWithArrayList;
            saveUserProgress.OnDisabled();
            loadUserProgress.OnDisabled();
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

                    // TODO: Mode in scriptable object. gameObject.SetActive(false) and gameObject.interactable = false;
                    button.interactable = false;
                    //OnCheckingProgress?.Invoke();
                    CheckingProgress();
                }
            }
        }

        private void CheckingProgress()
        {
            wordHit += 1;

            if (wordHandler.GetWordLenthNormolized == wordHit)
            {
                wordHit = 0;
                dataContainer.GetHeaderValue.NumberOfPoints += dataContainer.GetHeaderValue.NumberOfAttempts;
                // OnNextSestion?.Invoke();
                NextSession();
            }
        }

        private void DisableButton(Button butoon)
        {
            dataContainer.GetHeaderValue.NumberOfAttempts -= 1;

            butoon.interactable = false;
            butoon.gameObject.GetComponent<Image>().color = new Color(74, 70, 69, 0);
            butoon.transform.GetChild(0).gameObject.SetActive(false);

            if (dataContainer.GetHeaderValue.NumberOfAttempts == 0)
            {
                saveUserProgress.OnSaveScore(0);
                UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
            }
        }

        private void NextSession()
        {
            //OnRemoveCurrentWordWithArrayList?.Invoke();
            RemoveCurrentWordWithArrayList();
            wordHandler.OnFilteringAndSetTextDataset();
            saveUserProgress.OnSaveScore(dataContainer.GetHeaderValue.NumberOfPoints);
            OnNormalizationButtons?.Invoke();
            wordHandler.OnFilteringAndSetTextDataset?.Invoke();

            wordElementSwitcher.OnCloseAllWordTextElement?.Invoke();
            wordElementSwitcher.OnEnableAnswerWordTextElement?.Invoke();
            wordElementSwitcher.OnHideAllEmptyWordUIElement?.Invoke();

            dataContainer.GetHeaderValue.NumberOfAttempts = dataContainer.GameplaySettings.NumberOfAttempts;
        }

        private void RemoveCurrentWordWithArrayList()
        {
            string word = wordHandler.GetCurrenWord;
            Debug.Log(word);
            Debug.Log(wordHandler.GetWordArray.Length);

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
    }
}