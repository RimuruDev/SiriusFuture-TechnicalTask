using System;
using UnityEngine;

namespace RimuruDev.SiriusFuture
{
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

        public HeaderValue(UIHandler uiHandler) => this.uiHandler = uiHandler;
    }
}