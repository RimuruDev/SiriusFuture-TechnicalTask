using UnityEngine;
using System;

namespace RimuruDev.SiriusFuture
{
    public sealed class UIHandler : MonoBehaviour, IInitSystem
    {
        public Action OnUpdateScoreText;
        public Action OnAttemptsText;

        private GameDataContainer dataContainer;

        private void Awake()
        {
            if (dataContainer == null)
                dataContainer = FindObjectOfType<GameDataContainer>();
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

        public void UpdateScoreText() =>
            dataContainer.GetHeaderText.CurrentScoreText.text = $"Number of points: {dataContainer.GetHeaderValue.NumberOfScores}";

        private void UpdateAttemptsText() =>
            dataContainer.GetHeaderText.CurrentAttemptsText.text = $"Number of attempts: {dataContainer.GetHeaderValue.NumberOfAttempts}";

        public void Init()
        {
            dataContainer.GetHeaderValue.NumberOfAttempts = dataContainer.numattempt;

            if (PlayerPrefs.GetInt("Score") == 0)
                dataContainer.GetHeaderValue.NumberOfScores = dataContainer.numScore;
            else
                dataContainer.GetHeaderValue.NumberOfScores = PlayerPrefs.GetInt("Score");
        }
    }
}