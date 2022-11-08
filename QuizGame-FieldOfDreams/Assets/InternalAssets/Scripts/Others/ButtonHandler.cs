using System;
using UnityEngine;
using UnityEngine.UI;

namespace RimuruDev.SiriusFuture
{
    [DisallowMultipleComponent]
    [HelpURL("https://t.me/AbyssMothGames")]
    [RequireComponent(typeof(AnswerWord))]
    [RequireComponent(typeof(WrongWord))]
    [RequireComponent(typeof(CurrentProgress))]
    [RequireComponent(typeof(NextGameSession))]
    [RequireComponent(typeof(RemoveCurrentWordHandler))]
    public sealed class ButtonHandler : MonoBehaviour
    {
        public Action OnCacheAndSubscribeAllKeyboardButtons;

        [SerializeField, HideInInspector] private GameDataContainer dataContainer;
        [SerializeField, HideInInspector] private WordHandler wordHandler;
        [SerializeField, HideInInspector] private AnswerWord answerWord;
        [SerializeField, HideInInspector] private WrongWord wrongWord;

        private void Awake() => CheckRefs();

        private void OnEnable() => OnCacheAndSubscribeAllKeyboardButtons += CacheAllKeyboardButtons;

        [System.Diagnostics.Conditional(Tag.DEBUG)]
        public void OnDisable()
        {
            OnCacheAndSubscribeAllKeyboardButtons -= CacheAllKeyboardButtons;

            for (int i = 0; i < dataContainer.KeyboardButtons.Length; i++)
            {
                var localButton = dataContainer.KeyboardButtons[i];

                localButton.onClick.RemoveListener(() => OnClick(localButton));
            }
        }

        private void OnValidate() => CheckRefs();

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
        }

        private void SubscribeToAnEvent()
        {
            for (int i = 0; i < dataContainer.KeyboardButtons.Length; i++)
            {
                var localButton = dataContainer.KeyboardButtons[i];

                localButton.onClick.AddListener(() => OnClick(localButton));
            }
        }

        private void OnClick(Button butoon)
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
                answerWord.OnClickAnswerWords(currenAnswerWord, butoon);
            else
                wrongWord.OnClickWrongWord(butoon);
        }

        private void CheckRefs()
        {
            if (dataContainer == null)
                dataContainer = FindObjectOfType<GameDataContainer>();

            if (wordHandler == null)
                wordHandler = FindObjectOfType<WordHandler>();

            if (answerWord == null)
                answerWord = FindObjectOfType<AnswerWord>();

            if (wrongWord == null)
                wrongWord = FindObjectOfType<WrongWord>();
        }
    }
}