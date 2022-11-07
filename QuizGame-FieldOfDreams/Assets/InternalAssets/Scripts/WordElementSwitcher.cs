using System;
using UnityEngine;
using UnityEngine.UI;

namespace RimuruDev.SiriusFuture
{
    public sealed class WordElementSwitcher : MonoBehaviour, IInitSystem
    {
        public Action OnCloseAllWordTextElement;
        public Action OnHideAllEmptyWordUIElement;
        public Action OnEnableAnswerWordTextElement;

        private GameDataContainer dataContainer;
        private WordHandler wordHandler;

        private void Awake()
        {
            if (dataContainer == null)
                dataContainer = FindObjectOfType<GameDataContainer>();

            if (wordHandler == null)
                wordHandler = FindObjectOfType<WordHandler>();
        }

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

        public void Init()
        {
            CloseAllWordTextElement();
            HideAllEmptyWordUIElement();
            EnableAnswerWordTextElement();
        }

        private void CloseAllWordTextElement()
        {
            int elementLength = dataContainer.GetElementContainer.Element.Length;

            for (int i = 0; i < elementLength; i++)
                dataContainer.GetElementContainer.Element[i].GetChild(0).gameObject.SetActive(false);
        }

        private void HideAllEmptyWordUIElement()
        {
            int elementLength = dataContainer.GetElementContainer.Element.Length;

            for (int i = wordHandler.GetWordLenthNormolized; i < elementLength; i++)
            {
                var element = dataContainer.GetElementContainer.Element[i];
                element.GetComponent<Image>().color = new Color(74, 70, 69, 0);
            }
        }

        private void EnableAnswerWordTextElement()
        {
            for (int i = 0; i < wordHandler.GetWordLenthNormolized; i++)
            {
                dataContainer.GetElementContainer.Element[i].GetComponent<Image>().color = new Color(0, 0, 0, 255);

                dataContainer.GetElementContainer.Element[i].GetChild(0).gameObject
                    .GetComponent<TMPro.TMP_Text>().text = wordHandler.GetCurrentWordChar[i].ToString().ToUpper();
            }
        }
    }
}