using System;
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
        public Action OnCheckingProgress;
        public Action OnNextSestion;

        private GameDataContainer dataContainer;
        private WordElementSwitcher wordElementSwitcher;
        private WordHandler wordHandler;
        private Buttonhandler buttonhandler;

        private readonly SaveUserProgress saveUserProgress = new SaveUserProgress();
        private readonly LoadUserProgress loadUserProgress = new LoadUserProgress();
        private RemoveCurrentWordHandler removeCurrentWordHandler;

        private int wordHit = 0;

        private void Awake()
        {
            if (dataContainer == null)
                dataContainer = FindObjectOfType<GameDataContainer>();

            if (wordHandler == null)
                wordHandler = FindObjectOfType<WordHandler>();

            if (wordElementSwitcher == null)
                wordElementSwitcher = FindObjectOfType<WordElementSwitcher>();

            buttonhandler = FindObjectOfType<Buttonhandler>();

            // removeCurrentWordHandler = new RemoveCurrentWordHandler(dataContainer, wordHandler);
        }

        public void Init()
        {
            FillingKeyboardUI();
            //CacheAllKeyboardButtons();
            buttonhandler.OnCacheAndSubscribeAllKeyboardButtons?.Invoke();
        }

        private void OnEnable()
        {
            OnFillingKeyboardUI += FillingKeyboardUI;
            OnNormalizationButtons += NormalizationButtons;
           // removeCurrentWordHandler.OnEnabled();
            saveUserProgress.OnEnabled();
            loadUserProgress.OnEnabled();
         //   OnCheckingProgress += CheckingProgress;
          //  OnNextSestion += NextSession;
        }

        private void OnDisable()
        {
            OnFillingKeyboardUI -= FillingKeyboardUI;
            OnNormalizationButtons -= NormalizationButtons;
            //removeCurrentWordHandler.OnDisabledd();
            saveUserProgress.OnDisabled();
            loadUserProgress.OnDisabled();
          //  OnCheckingProgress -= CheckingProgress;
            //OnNextSestion -= NextSession;
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
        /*
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

                    button.GetComponent<Image>().color = new Color(74, 70, 69, 0);
                    button.transform.GetChild(0).gameObject.SetActive(false);

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
                dataContainer.GetHeaderValue.NumberOfPoints += dataContainer.GetHeaderValue.NumberOfAttempts;
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
            {
                saveUserProgress.OnSaveScore(0);
                UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
            }
        }

        private void NextSession()
        {
            removeCurrentWordHandler.OnRemoveCurrentWordHandler?.Invoke();

            wordHandler.OnFilteringAndSetTextDataset();
            saveUserProgress.OnSaveScore(dataContainer.GetHeaderValue.NumberOfPoints);
            OnNormalizationButtons?.Invoke();
            wordHandler.OnFilteringAndSetTextDataset?.Invoke();

            wordElementSwitcher.OnCloseAllWordTextElement?.Invoke();
            wordElementSwitcher.OnEnableAnswerWordTextElement?.Invoke();
            wordElementSwitcher.OnHideAllEmptyWordUIElement?.Invoke();

            dataContainer.GetHeaderValue.NumberOfAttempts = dataContainer.GameplaySettings.NumberOfAttempts;
        }

        */
    }
}