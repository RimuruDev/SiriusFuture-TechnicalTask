using UnityEngine;
using System;

namespace RimuruDev.SiriusFuture
{
    [Serializable]
    public sealed class LoadUserProgress
    {
        public Action<GameDataContainer> OnLoadScore;

        public void OnEnabled() => OnLoadScore += LoadScore;

        public void OnDisabled() => OnLoadScore -= LoadScore;

        private void LoadScore(GameDataContainer dataContainer)
        {
            dataContainer.GetHeaderValue.NumberOfAttempts = dataContainer.GameplaySettings.NumberOfAttempts;

            if (PlayerPrefs.GetInt(Tag.Score) == 0)
                dataContainer.GetHeaderValue.NumberOfPoints = 0;
            else
                dataContainer.GetHeaderValue.NumberOfPoints = PlayerPrefs.GetInt(Tag.Score);
        }
    }
}