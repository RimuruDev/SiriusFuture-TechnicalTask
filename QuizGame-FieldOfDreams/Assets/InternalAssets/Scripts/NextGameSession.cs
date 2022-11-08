using System;

using UnityEngine;

namespace RimuruDev.SiriusFuture
{
    [HelpURL("https://t.me/AbyssMothGames")]
    public sealed class NextGameSession : MonoBehaviour
    {
        public Action OnNextSession;

        [SerializeField, HideInInspector] private GameDataContainer dataContainer;
        [SerializeField, HideInInspector] private WordHandler wordHandler;
        [SerializeField, HideInInspector] private AnswerWord answerWord;
        [SerializeField, HideInInspector] private WrongWord wrongWord;
        [SerializeField, HideInInspector] private KeyboardUIHandler keyboardUIHandler;
        [SerializeField, HideInInspector] private WordElementSwitcher wordElementSwitcher;
        [SerializeField, HideInInspector] private Buttonhandler buttonhandler;
        [SerializeField, HideInInspector] private RemoveCurrentWordHandler removeCurrentWordHandler;

        private readonly SaveUserProgress saveUserProgress = new SaveUserProgress();
        private readonly LoadUserProgress loadUserProgress = new LoadUserProgress();

        private void Awake() => CheckRefs();

        [System.Diagnostics.Conditional("DEBUG")]
        private void OnValidate() => CheckRefs();

        private void OnEnable() => OnNextSession += NextSession;
        private void OnDisable() => OnNextSession -= NextSession;

        private void NextSession()
        {
            removeCurrentWordHandler.OnRemoveCurrentWordHandler?.Invoke();
            keyboardUIHandler.OnNormalizationButtons?.Invoke();
            wordHandler.OnFilteringAndSetTextDataset?.Invoke();
            wordElementSwitcher.OnCloseAllWordTextElement?.Invoke();
            wordElementSwitcher.OnEnableAnswerWordTextElement?.Invoke();
            wordElementSwitcher.OnHideAllEmptyWordUIElement?.Invoke();

            dataContainer.GetHeaderValue.NumberOfAttempts = dataContainer.GameplaySettings.NumberOfAttempts;
        }

        private void CheckRefs()
        {
            if (dataContainer == null)
                dataContainer = FindObjectOfType<GameDataContainer>();

            if (wordHandler == null)
                wordHandler = FindObjectOfType<WordHandler>();

            if (answerWord == null)
                answerWord = FindObjectOfType<AnswerWord>();

            if (wordElementSwitcher == null)
                wordElementSwitcher = FindObjectOfType<WordElementSwitcher>();

            if (wrongWord == null)
                wrongWord = FindObjectOfType<WrongWord>();

            if (keyboardUIHandler == null)
                keyboardUIHandler = FindObjectOfType<KeyboardUIHandler>();

            if (keyboardUIHandler == null)
                removeCurrentWordHandler = FindObjectOfType<RemoveCurrentWordHandler>();

            if (buttonhandler == null)
                buttonhandler = FindObjectOfType<Buttonhandler>();
        }
    }
}