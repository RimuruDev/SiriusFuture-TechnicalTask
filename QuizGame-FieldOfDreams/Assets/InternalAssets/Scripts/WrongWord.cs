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
        private SaveUserProgress saveUserProgress;

        private void Awake() => CheckRefs();

        public void OnEnable() => OnClickWrongWord += ClickWrongWord;
        public void OnDisable() => OnClickWrongWord -= ClickWrongWord;

        [System.Diagnostics.Conditional("DEBUG")]
        private void OnValidate() => CheckRefs();

        private void CheckRefs()
        {
            saveUserProgress = new SaveUserProgress();

            if (dataContainer == null)
                dataContainer = FindObjectOfType<GameDataContainer>();

            if (wordHandler == null)
                wordHandler = FindObjectOfType<WordHandler>();

            if (currentProgress == null)
                currentProgress = FindObjectOfType<CurrentProgress>();
        }

        private void ClickWrongWord(Button button)
        {
            dataContainer.GetHeaderValue.NumberOfAttempts -= 1;

            button.interactable = false;
            button.gameObject.GetComponent<Image>().color = new Color(74, 70, 69, 0);
            button.transform.GetChild(0).gameObject.SetActive(false);

            if (dataContainer.GetHeaderValue.NumberOfAttempts == 0)
            {
                saveUserProgress.OnSaveScore(0);
                UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
            }
        }
    }
}