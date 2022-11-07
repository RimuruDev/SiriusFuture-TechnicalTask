using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

namespace RimuruDev.SiriusFuture
{
    public sealed class EventHandler : MonoBehaviour
    {
        public static EventHandler Instance;
        public Action OnAnsword;
        public Action OnInitUpdateUI;
        [SerializeField, HideInInspector] private GameDataContainer dataContainer;
        [SerializeField, HideInInspector] private WordDataHandler wordDataHandler;
        [SerializeField, HideInInspector] private Sturtup sturtup;
        [SerializeField] private Button[] allKeyboardButtons;// = new Button[26];

        private int wordHit = 0;

        private void Awake() => CheckRefs();

        private void OnEnable()
        {
            sturtup.OnInitializationEnd += CacheAllKeyboardButtons;
            OnInitUpdateUI += InitUpdateUI;
            OnAnsword += CheckWin;
        }

        private void InitUpdateUI()
        {
            dataContainer.GetHeaderValue.NumberOfAttempts = 10;
            dataContainer.GetHeaderValue.NumberOfScores = 0;
            Debug.Log("NumberOfAttempts += 10");
        }

        private void OnDisable()
        {
            sturtup.OnInitializationEnd -= CacheAllKeyboardButtons;
            OnInitUpdateUI -= InitUpdateUI;
            OnAnsword += CheckWin;

            int buttonsLength = allKeyboardButtons.Length;
            for (int i = 0; i < buttonsLength; i++)
            {
                var localButton = allKeyboardButtons[i];

                localButton.onClick.RemoveAllListeners();
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
        }

        private void SubscribeToAnEvent()
        {
            for (int i = 0; i < allKeyboardButtons.Length; i++)
            {
                var localButton = allKeyboardButtons[i];

                localButton.onClick.AddListener(() => OnClick(localButton));
            }
            EventHandler.Instance.OnInitUpdateUI?.Invoke();
            // OnInitUpdateUI?.Invoke();
        }

        private void OnClick(Button butoon)
        {
            var buttonName = butoon.name;
            var currentWord = wordDataHandler.GetCurrentWordChar;
            string currenAnswerWord = string.Empty;
            bool isAnswerWord = default;

            for (int i = 0; i < currentWord.Length - 1; i++)
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

            EventHandler.Instance.OnInitUpdateUI?.Invoke();
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

        private void CheckWin()
        {
            wordHit += 1;
            var currentWord = wordDataHandler.GetCurrentWordChar;

            if (currentWord.Length - 1 == wordHit)
            {
                Debug.Log("[============== Popup win ==============]");
                Debug.Log($"Filaai!!!  NumberOfAttempts == {dataContainer.GetHeaderValue.NumberOfScores} + {dataContainer.GetHeaderValue.NumberOfAttempts}");
                dataContainer.GetHeaderValue.NumberOfScores += dataContainer.GetHeaderValue.NumberOfAttempts;
            }

        }

        private void OpenAnswerWords(string word)
        {
            var currentWord = wordDataHandler.GetCurrentWordChar;

            for (int i = 0; i < currentWord.Length - 1; i++)
            {
                if (word == currentWord[i].ToString().ToUpper())
                {
                    dataContainer.GetElementContainer.Element[i].GetChild(0).gameObject.SetActive(true);
                    dataContainer.GetHeaderValue.NumberOfScores += 1;
                    Debug.Log("NumberOfScores ++");
                    OnAnsword.Invoke();
                }
            }
        }

        private void OnValidate() => CheckRefs();

        private void CheckRefs()
        {
            Instance = this;

            if (dataContainer == null)
                dataContainer = FindObjectOfType<GameDataContainer>();

            if (sturtup == null)
                sturtup = FindObjectOfType<Sturtup>();

            if (wordDataHandler == null)
                wordDataHandler = FindObjectOfType<WordDataHandler>();

            allKeyboardButtons = new Button[26];
        }
    }
}