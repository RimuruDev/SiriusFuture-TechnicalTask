using UnityEngine;
using System;

namespace RimuruDev.SiriusFuture
{
    [DisallowMultipleComponent]
    [HelpURL("https://t.me/AbyssMothGames")]
    public sealed class UIHandler : MonoBehaviour, IInitSystem
    {
        public Action OnUpdateScoreText;
        public Action OnAttemptsText;
        public Action OnWinPopup;
        public Action OnWarningPopup;

        private GameDataContainer dataContainer;
        private LoadUserProgress loadUserProgress = new LoadUserProgress();

        private void Awake()
        {
            if (dataContainer == null)
                dataContainer = FindObjectOfType<GameDataContainer>();
        }

        private void OnEnable()
        {
            OnUpdateScoreText += UpdateNumberOfPointsText;
            OnAttemptsText += UpdateAttemptsText;
            loadUserProgress.OnEnabled();
            OnWinPopup += ShowWinPopup;
            OnWarningPopup += ShowWarningPopup;
        }

        private void OnDisable()
        {
            OnUpdateScoreText -= UpdateNumberOfPointsText;
            OnAttemptsText -= UpdateAttemptsText;
            loadUserProgress.OnDisabled();
            OnWinPopup -= ShowWinPopup;
            OnWarningPopup -= ShowWarningPopup;
        }

        public void UpdateNumberOfPointsText() =>
            dataContainer.GetHeaderText.CurrentScoreText.text = $"Number of points: {dataContainer.GetHeaderValue.NumberOfPoints}";

        private void UpdateAttemptsText() =>
            dataContainer.GetHeaderText.CurrentAttemptsText.text = $"Number of attempts: {dataContainer.GetHeaderValue.NumberOfAttempts}";

        private void ShowWinPopup() => dataContainer.WinPopup.SetActive(true);
        private void ShowWarningPopup() => dataContainer.WarningPopup.SetActive(true);

        public void Init() => loadUserProgress.OnLoadScore(dataContainer);
    }
}