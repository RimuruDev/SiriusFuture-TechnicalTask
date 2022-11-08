using System;
using TMPro;
using UnityEngine;

namespace RimuruDev.SiriusFuture
{
    [Serializable]
    public sealed class HeaderText
    {
        [SerializeField] private TMP_Text currentAttemptsText;
        public TMP_Text CurrentAttemptsText { get => currentAttemptsText; set => currentAttemptsText = value; }

        [SerializeField] private TMP_Text currentScoreText;
        public TMP_Text CurrentScoreText { get => currentScoreText; set => currentScoreText = value; }
    }
}