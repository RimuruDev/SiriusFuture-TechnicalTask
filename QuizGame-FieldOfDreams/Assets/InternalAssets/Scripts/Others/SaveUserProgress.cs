using System;
using UnityEngine;

namespace RimuruDev.SiriusFuture
{
    [Serializable]
    public sealed class SaveUserProgress
    {
        public Action<int> OnSaveScore;

        public void OnEnabled() => OnSaveScore += SaveScore;

        public void OnDisabled() => OnSaveScore -= SaveScore;

        private void SaveScore(int score) => UnityEngine.PlayerPrefs.SetInt(Tag.Score, score);
    }
}