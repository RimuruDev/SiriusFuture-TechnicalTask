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
        public Action<bool> OnWinPopup;
        public Action<bool> OnFailurePopup;
        public Action<bool> OnWarningPopup;

        [SerializeField, HideInInspector] private GameDataContainer dataContainer;
        [SerializeField, HideInInspector] private LoadUserProgress loadUserProgress = new LoadUserProgress();

        private void Awake() => CheckRefs();

        private void OnEnable()
        {
            OnUpdateScoreText += UpdateNumberOfPointsText;
            OnAttemptsText += UpdateAttemptsText;
            loadUserProgress.OnEnabled();
            OnFailurePopup += ShowFailurePopup;
            OnWinPopup += ShowWinPopup;
            OnWarningPopup += ShowWarningPopup;
        }

        private void OnDisable()
        {
            OnUpdateScoreText -= UpdateNumberOfPointsText;
            OnAttemptsText -= UpdateAttemptsText;
            loadUserProgress.OnDisabled();
            OnFailurePopup -= ShowFailurePopup;
            OnWinPopup -= ShowWinPopup;
            OnWarningPopup -= ShowWarningPopup;
        }

        [System.Diagnostics.Conditional(Tag.DEBUG)]
        private void OnValidate() => CheckRefs();

        public void UpdateNumberOfPointsText() =>
            dataContainer.GetHeaderText.CurrentScoreText.text = $"Number of points: {dataContainer.GetHeaderValue.NumberOfPoints}";

        private void UpdateAttemptsText() =>
            dataContainer.GetHeaderText.CurrentAttemptsText.text = $"Number of attempts: {dataContainer.GetHeaderValue.NumberOfAttempts}";

        private void ShowWinPopup(bool isActive) => dataContainer.WinPopup.SetActive(isActive);

        private void ShowWarningPopup(bool isActive) => dataContainer.WarningPopup.SetActive(true);

        private void ShowFailurePopup(bool isActive) => dataContainer.FailurePopup.SetActive(true);

        public void Init() => loadUserProgress.OnLoadScore(dataContainer);

        private void CheckRefs()
        {
            if (dataContainer == null)
                dataContainer = FindObjectOfType<GameDataContainer>();
        }
    }
}