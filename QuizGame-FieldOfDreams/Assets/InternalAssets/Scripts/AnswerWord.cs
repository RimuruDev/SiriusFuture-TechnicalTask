using System;

using UnityEngine;
using UnityEngine.UI;

namespace RimuruDev.SiriusFuture
{
    [HelpURL("https://t.me/AbyssMothGames")]
    public sealed class AnswerWord : MonoBehaviour
    {
        public Action<string, Button> OnClickAnswerWords;

        [SerializeField, HideInInspector] private GameDataContainer dataContainer;
        [SerializeField, HideInInspector] private WordHandler wordHandler;
        [SerializeField, HideInInspector] private CurrentProgress currentProgress;

        private void Awake() => CheckRefs();

        private void OnEnable() => OnClickAnswerWords += ClickAnswerWords;
        private void OnDisable() => OnClickAnswerWords += ClickAnswerWords;

        [System.Diagnostics.Conditional("DEBUG")]
        private void OnValidate() => CheckRefs();

        private void CheckRefs()
        {
            if (dataContainer == null)
                dataContainer = FindObjectOfType<GameDataContainer>();

            if (wordHandler == null)
                wordHandler = FindObjectOfType<WordHandler>();

            if (currentProgress == null)
                currentProgress = FindObjectOfType<CurrentProgress>();
        }

        private void ClickAnswerWords(string currentAnswerWord, Button button)
        {
            var currentWord = wordHandler.GetCurrentWordChar;

            for (int i = 0; i < currentWord.Length - 1; i++)
            {
                if (currentAnswerWord == currentWord[i].ToString().ToUpper())
                {
                    dataContainer.GetElementContainer.Element[i].GetChild(0).gameObject.SetActive(true);

                    button.GetComponent<Image>().color = new Color(74, 70, 69, 0);
                    button.transform.GetChild(0).gameObject.SetActive(false);

                    currentProgress.OnCheckingProgress.Invoke();
                }
            }
        }
    }
}