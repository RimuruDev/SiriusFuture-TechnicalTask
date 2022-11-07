using UnityEngine;
using System;

namespace RimuruDev.SiriusFuture
{
    public sealed class UIHandler : MonoBehaviour
    {
        public Action OnUpdateScoreText;
        public Action OnAttemptsText;

        [SerializeField, HideInInspector] private GameDataContainer dataContainer;
        [SerializeField, HideInInspector] private WordDataHandler wordDataHandler;

        private void Awake() => CheckRefs();

        private void Start()
        {
           
        }

        private void OnEnable()
        {
            OnUpdateScoreText += UpdateScoreText;
            OnAttemptsText += UpdateAttemptsText;
        }

        private void OnDisable()
        {
            OnUpdateScoreText -= UpdateScoreText;
            OnAttemptsText -= UpdateAttemptsText;
        }

        public void UpdateScoreText()
        {
            Debug.Log("Update:: UpdateScoreText");
            dataContainer.GetHeaderText.CurrentScoreText.text = $"Number of points: {dataContainer.GetHeaderValue.NumberOfScores}";
        }

        private void UpdateAttemptsText()
        {
            Debug.Log("Update:: UpdateAttemptsTexts");
            dataContainer.GetHeaderText.CurrentAttemptsText.text = $"Number of attempts: {dataContainer.GetHeaderValue.NumberOfAttempts}";
        }
            

        private void OnValidate() => CheckRefs();

        private void CheckRefs()
        {
            if (dataContainer == null)
                dataContainer = FindObjectOfType<GameDataContainer>();

            // if (sturtup == null) sturtup = FindObjectOfType<Sturtup>();

            if (wordDataHandler == null)
                wordDataHandler = FindObjectOfType<WordDataHandler>();
        }
    }
}