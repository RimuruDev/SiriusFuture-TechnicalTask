using System;

using UnityEngine;

namespace RimuruDev.SiriusFuture
{
    [HelpURL("https://t.me/AbyssMothGames")]
    public sealed class CurrentProgress : MonoBehaviour
    {
        public Action OnCheckingProgress;

        [SerializeField, HideInInspector] private GameDataContainer dataContainer;
        [SerializeField, HideInInspector] private WordHandler wordHandler;
        [SerializeField, HideInInspector] private NextGameSession nextGameSession;
        private int wordHit = 0;

        private void Awake() => CheckRefs();

        private void OnEnable() => OnCheckingProgress += CheckingProgress;
        private void OnDisable() => OnCheckingProgress -= CheckingProgress;

        [System.Diagnostics.Conditional("DEBUG")]
        private void OnValidate() => CheckRefs();

        private void CheckRefs()
        {
            if (dataContainer == null)
                dataContainer = FindObjectOfType<GameDataContainer>();

            if (wordHandler == null)
                wordHandler = FindObjectOfType<WordHandler>();

            if (nextGameSession == null)
                nextGameSession = FindObjectOfType<NextGameSession>();
        }

        private void CheckingProgress()
        {
            wordHit += 1;

            if (wordHandler.GetWordLenthNormolized == wordHit)
            {
                wordHit = 0;
                dataContainer.GetHeaderValue.NumberOfPoints += dataContainer.GetHeaderValue.NumberOfAttempts;
                nextGameSession.OnNextSession?.Invoke();
            }
        }
    }
}