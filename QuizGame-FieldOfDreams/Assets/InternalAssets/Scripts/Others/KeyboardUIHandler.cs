using System;
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

        [SerializeField, HideInInspector] private GameDataContainer dataContainer;
        [SerializeField, HideInInspector] private ButtonHandler buttonhandler;

        private void Awake() => CheckRefs();

        public void Init()
        {
            FillingKeyboardUI();
            buttonhandler.OnCacheAndSubscribeAllKeyboardButtons?.Invoke();
        }

        private void OnEnable()
        {
            OnFillingKeyboardUI += FillingKeyboardUI;
            OnNormalizationButtons += NormalizationButtons;
        }

        private void OnDisable()
        {
            OnFillingKeyboardUI -= FillingKeyboardUI;
            OnNormalizationButtons -= NormalizationButtons;
        }

        [System.Diagnostics.Conditional(Tag.DEBUG)]
        private void OnValidate() => CheckRefs();

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

        private void CheckRefs()
        {
            if (dataContainer == null)
                dataContainer = FindObjectOfType<GameDataContainer>();

            if (buttonhandler == null)
                buttonhandler = FindObjectOfType<ButtonHandler>();
        }
    }
}