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
            //Debug.Log("Напоминание, убери  dataContainer.GetHeaderValue.NumberOfAttempts отсюда, Single responsibility плачет");

            if (PlayerPrefs.GetInt("Score") == 0)
                dataContainer.GetHeaderValue.NumberOfPoints = dataContainer.GameplaySettings.NumberOfAttempts;
            else
                dataContainer.GetHeaderValue.NumberOfPoints = PlayerPrefs.GetInt("Score");
        }
    }
}