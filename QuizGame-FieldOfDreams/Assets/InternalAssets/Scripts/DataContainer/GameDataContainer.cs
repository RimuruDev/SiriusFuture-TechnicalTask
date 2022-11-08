using UnityEngine;
using UnityEngine.UI;

namespace RimuruDev.SiriusFuture
{
    [DisallowMultipleComponent]
    [HelpURL("https://t.me/AbyssMothGames")]
    public sealed class GameDataContainer : MonoBehaviour
    {
        private GameplaySettings gameplaySettings;
        public GameplaySettings GameplaySettings { get => gameplaySettings; set => gameplaySettings = value; }

        [SerializeField] private ElementContainer elementContainer;
        public ElementContainer GetElementContainer => elementContainer;

        [SerializeField] private UserInterfaceData userInterfaceData;
        public UserInterfaceData GetUserInterfaceData => userInterfaceData;

        [HideInInspector] public KeywordWords keywordWords = new KeywordWords();
        public KeywordWords GetKeywordWords => keywordWords;

        [SerializeField] private HeaderText headerText;
        public HeaderText GetHeaderText => headerText;

        [Header("Popups"), Space(5)]
        [SerializeField] private GameObject winPopup;
        public GameObject WinPopup { get => winPopup; private set => winPopup = value; }

        [SerializeField] private GameObject warningPopup;
        public GameObject WarningPopup { get => warningPopup; private set => warningPopup = value; }

        [SerializeField] private GameObject failurePopup;
        public GameObject FailurePopup { get => failurePopup; private set => failurePopup = value; }

        [SerializeField, HideInInspector] private Button[] keyboardButtons = new Button[26]; // TODO: Add Enam Keyboard Type. English = 26 buttons
        public Button[] KeyboardButtons => keyboardButtons;

        [SerializeField, HideInInspector] private HeaderValue headerValue;
        public HeaderValue GetHeaderValue => headerValue;

        [SerializeField, HideInInspector] private WordHandler wordHandler;
        public WordHandler WordHandler { get => wordHandler; set => wordHandler = value; }

        [SerializeField, HideInInspector] private TextDataFilteringHandler textDataset;
        public TextDataFilteringHandler GetTextDataset => textDataset;

        private void Awake()
        {
            textDataset = new TextDataFilteringHandler(this);
            headerValue = new HeaderValue(FindObjectOfType<UIHandler>());

            CheckRefs();
        }

        [System.Diagnostics.Conditional(Tag.DEBUG)]
        private void OnValidate() => CheckRefs();

        private void CheckRefs()
        {
            if (headerValue == null)
                headerValue = new HeaderValue(FindObjectOfType<UIHandler>());

            if (wordHandler == null)
                wordHandler = FindObjectOfType<WordHandler>();
        }
    }
}