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
    public sealed class GameDataContainer : MonoBehaviour
    {
        [Header("[===== Word to unravel =====]"), Space(5)]
        [SerializeField] private ElementContainer elementContainer;
        public ElementContainer GetElementContainer => elementContainer;

        [Header("[===== User interface data =====]"), Space(5)]
        [SerializeField] private UserInterfaceData userInterfaceData;
        public UserInterfaceData GetUserInterfaceData => userInterfaceData;
        public KeywordWords keywordWords = new KeywordWords();
        public KeywordWords GetKeywordWords => keywordWords;

        [Header("[===== Header text =====]"), Space(5)]
        [SerializeField] private HeaderText headerText;
        public HeaderText GetHeaderText => headerText;

        [Header("[===== Scores text =====]"), Space(5)]
        [SerializeField] private HeaderValue headerValue = new HeaderValue();
        public HeaderValue GetHeaderValue => headerValue;

        [Header(" ")]
        [SerializeField] private Button[] keyboardButtons = new Button[26];
        public Button[] KeyboardButtons => keyboardButtons;

        public int numattempt = 10;
        public int numScore = 0;
    }

    [Serializable]
    public sealed class ElementContainer
    {
        private const int maximumWordLength = 13;

        [SerializeField] private Transform[] element;

        public Transform[] Element { get => element; set => element = value; }
        /* {
             get => element;
             set
             {
                 if (element.Length < maximumWordLength)
                     element = value;
                 else
                     Debug.Log("Element is full!");
             }
         }*/
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
        [SerializeField] private TMP_Text currentScoreText;

        public TMP_Text CurrentAttemptsText { get => currentAttemptsText; set => currentAttemptsText = value; }
        public TMP_Text CurrentScoreText { get => currentScoreText; set => currentScoreText = value; }
    }

    [Serializable]
    public sealed class HeaderValue
    {
        [SerializeField] private UIHandler uiHandler;

        [SerializeField] private int numberOfAttempts;
        [SerializeField] private int numberOfScores;

        public int NumberOfAttempts
        {
            get => numberOfAttempts;
            set
            {
                numberOfAttempts = Mathf.Clamp(value, 0, ushort.MaxValue);
                uiHandler.OnAttemptsText?.Invoke();
            }
        }

        public int NumberOfScores
        {
            get => numberOfScores;
            set
            {
                numberOfScores = Mathf.Clamp(value, 0, ushort.MaxValue);
                uiHandler.OnUpdateScoreText?.Invoke();
            }
        }

        public HeaderValue() { }

        public HeaderValue(UIHandler uiHandler) => this.uiHandler = uiHandler;
    }
}