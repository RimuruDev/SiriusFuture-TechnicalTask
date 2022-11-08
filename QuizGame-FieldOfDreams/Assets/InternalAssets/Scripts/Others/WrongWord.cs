using System;
using UnityEngine;
using UnityEngine.UI;

namespace RimuruDev.SiriusFuture
{
    [HelpURL("https://t.me/AbyssMothGames")]
    public sealed class WrongWord : MonoBehaviour
    {
        public Action<Button> OnClickWrongWord;

        [SerializeField, HideInInspector] private GameDataContainer dataContainer;
        [SerializeField, HideInInspector] private WordHandler wordHandler;
        [SerializeField, HideInInspector] private CurrentProgress currentProgress;
        [SerializeField, HideInInspector] private UIHandler uiHandler;
        private SaveUserProgress saveUserProgress;

        private void Awake() => CheckRefs();

        public void OnEnable() => OnClickWrongWord += ClickWrongWord;
        public void OnDisable() => OnClickWrongWord -= ClickWrongWord;

        [System.Diagnostics.Conditional(Tag.DEBUG)]
        private void OnValidate() => CheckRefs();

        private void ClickWrongWord(Button button)
        {
            dataContainer.GetHeaderValue.NumberOfAttempts -= 1;

            button.interactable = false;
            button.gameObject.GetComponent<Image>().color = new Color(74, 70, 69, 0);
            button.transform.GetChild(0).gameObject.SetActive(false);

            if (dataContainer.GetHeaderValue.NumberOfAttempts == 0)
            {
                PlayerPrefs.DeleteAll();
                uiHandler.OnFailurePopup(true);
            }
        }

        private void CheckRefs()
        {
            saveUserProgress = new SaveUserProgress();

            if (dataContainer == null)
                dataContainer = FindObjectOfType<GameDataContainer>();

            if (wordHandler == null)
                wordHandler = FindObjectOfType<WordHandler>();

            if (currentProgress == null)
                currentProgress = FindObjectOfType<CurrentProgress>();

            if (uiHandler == null)
                uiHandler = FindObjectOfType<UIHandler>();
        }
    }
}