using System;
using UnityEngine;
using UnityEngine.UI;

namespace RimuruDev.SiriusFuture
{
    [DisallowMultipleComponent]
    [HelpURL("https://t.me/AbyssMothGames")]
    public sealed class WordElementSwitcher : MonoBehaviour, IInitSystem
    {
        public Action OnCloseAllWordTextElement;
        public Action OnHideAllEmptyWordUIElement;
        public Action OnEnableAnswerWordTextElement;

        [SerializeField, HideInInspector] private GameDataContainer dataContainer;
        [SerializeField, HideInInspector] private WordHandler wordHandler;
        [SerializeField, HideInInspector] private UIHandler uiHandler;

        private void Awake() => CheckRefs();

        private void OnEnable()
        {
            OnCloseAllWordTextElement += CloseAllWordTextElement;
            OnHideAllEmptyWordUIElement += HideAllEmptyWordUIElement;
            OnEnableAnswerWordTextElement += EnableAnswerWordTextElement;
        }

        private void OnDisable()
        {
            OnCloseAllWordTextElement -= CloseAllWordTextElement;
            OnHideAllEmptyWordUIElement -= HideAllEmptyWordUIElement;
            OnEnableAnswerWordTextElement -= EnableAnswerWordTextElement;
        }

        [System.Diagnostics.Conditional(Tag.DEBUG)]
        private void OnValidate() => CheckRefs();

        public void Init()
        {
            CloseAllWordTextElement();
            HideAllEmptyWordUIElement();
            EnableAnswerWordTextElement();
        }

        private void CloseAllWordTextElement()
        {
            if (wordHandler.GetWordLenthNormolized <= 0) return;//{ uiHandler.OnWarningPopup(true); return; }

            int elementLength = dataContainer.GetElementContainer.Element.Length;

            for (int i = 0; i < elementLength; i++)
                dataContainer.GetElementContainer.Element[i].GetChild(0).gameObject.SetActive(false);
        }

        private void HideAllEmptyWordUIElement()
        {
            if (wordHandler.GetWordLenthNormolized <= 0) return;//{ uiHandler.OnWarningPopup(true); return; }

            int elementLength = dataContainer.GetElementContainer.Element.Length;

            for (int i = wordHandler.GetWordLenthNormolized; i < elementLength; i++)
            {
                var element = dataContainer.GetElementContainer.Element[i];
                element.GetComponent<Image>().color = new Color(74, 70, 69, 0);
            }
        }

        private void EnableAnswerWordTextElement()
        {
            if (wordHandler.GetWordLenthNormolized <= 0) return;//{ uiHandler.OnWarningPopup(true); return; }

            for (int i = 0; i < wordHandler.GetWordLenthNormolized; i++)
            {
                dataContainer.GetElementContainer.Element[i].GetComponent<Image>().color = new Color(0, 0, 0, 255);

                dataContainer.GetElementContainer.Element[i].GetChild(0).gameObject
                    .GetComponent<TMPro.TMP_Text>().text = wordHandler.GetCurrentWordChar[i].ToString().ToUpper();
            }
        }

        private void CheckRefs()
        {
            if (dataContainer == null)
                dataContainer = FindObjectOfType<GameDataContainer>();

            if (wordHandler == null)
                wordHandler = FindObjectOfType<WordHandler>();

            if (uiHandler == null)
                uiHandler = FindObjectOfType<UIHandler>();
        }
    }
}