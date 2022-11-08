using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
using Unity.VisualScripting;
using System.Xml.Linq;
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

        [SerializeField] private GameObject winPopup;
        public GameObject WinPopup { get => winPopup; private set => winPopup = value; }

        [SerializeField] private GameObject warningPopup;
        public GameObject WarningPopup { get => warningPopup; private set => warningPopup = value; }

        [SerializeField, HideInInspector] private Button[] keyboardButtons = new Button[26];
        public Button[] KeyboardButtons => keyboardButtons;

        private HeaderValue headerValue;
        public HeaderValue GetHeaderValue => headerValue;

        private WordHandler wordHandler;
        public WordHandler WordHandler { get => wordHandler; set => wordHandler = value; }

        private TextDataFilteringHandler textDataset;
        public TextDataFilteringHandler GetTextDataset => textDataset;

        private void Awake()
        {
            textDataset = new TextDataFilteringHandler(this);
            headerValue = new HeaderValue(FindObjectOfType<UIHandler>());

            if (wordHandler == null)
                wordHandler = FindObjectOfType<WordHandler>();
        }
    }

    [Serializable]
    public sealed class ElementContainer
    {
        [SerializeField] private Transform[] element;
        public Transform[] Element { get => element; set => element = value; }
    }

    [Serializable]
    public sealed class UserInterfaceData
    {
        private const int keyboardHeaderLength = 10;
        private const int keyboardMiddleLength = 9;
        private const int keyboardBottomLength = 7;

        [SerializeField] private Transform[] headerUserInterfaceKeyboard;
        [SerializeField] private Transform[] middleUserInterfaceKeyboard;
        [SerializeField] private Transform[] bottomUserInterfaceKeyboard;

        public Transform[] HeaderUserInterfaceKeyboard { get => headerUserInterfaceKeyboard; set => headerUserInterfaceKeyboard = value; }
        public Transform[] MiddleUserInterfaceKeyboard { get => middleUserInterfaceKeyboard; set => middleUserInterfaceKeyboard = value; }
        public Transform[] BottomUserInterfaceKeyboard { get => bottomUserInterfaceKeyboard; set => bottomUserInterfaceKeyboard = value; }

    }

    [Serializable]
    public sealed class KeywordWords
    {
        public readonly char[] headerKeybardWords = { 'q', 'w', 'e', 'r', 't', 'y', 'u', 'i', 'o', 'p' };
        public readonly char[] middleKeybardWord = { 'a', 's', 'd', 'f', 'g', 'h', 'j', 'k', 'l' };
        public readonly char[] bottomKeybardWords = { 'z', 'x', 'c', 'v', 'b', 'n', 'm' };
    }

    [Serializable]
    public sealed class HeaderText
    {
        [SerializeField] private TMP_Text currentAttemptsText;
        public TMP_Text CurrentAttemptsText { get => currentAttemptsText; set => currentAttemptsText = value; }

        [SerializeField] private TMP_Text currentScoreText;
        public TMP_Text CurrentScoreText { get => currentScoreText; set => currentScoreText = value; }
    }

    [Serializable]
    public sealed class HeaderValue
    {
        [SerializeField] private UIHandler uiHandler;

        [SerializeField] private int numberOfAttempts;
        [SerializeField] private int numberOfPoints;

        public int NumberOfAttempts
        {
            get => numberOfAttempts;
            set
            {
                numberOfAttempts = Mathf.Clamp(value, 0, ushort.MaxValue);
                uiHandler.OnAttemptsText?.Invoke();
            }
        }

        public int NumberOfPoints
        {
            get => numberOfPoints;
            set
            {
                numberOfPoints = Mathf.Clamp(value, 0, ushort.MaxValue);
                uiHandler.OnUpdateScoreText?.Invoke();
            }
        }

        //public HeaderValue() { }

        public HeaderValue(UIHandler uiHandler) => this.uiHandler = uiHandler;
    }
}