using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;

namespace RimuruDev.SiriusFuture
{
    [CreateAssetMenu(fileName = "new GameplaySettings", menuName = "SiriusFuture")]
    public sealed class GameplaySettings : ScriptableObject
    {
        private readonly int minWordLength = 3, maxWordLength = 13;
        private readonly int minAttemptLength = 1, maxAttemptLength = 100;

        [SerializeField, Range(3, 13)] private int maximumWordLength;
        public int MaximumWordLength { get => maximumWordLength; set => maximumWordLength = Mathf.Clamp(value, minWordLength, maxWordLength); }

        [SerializeField, Range(1, 100)] private int numberOfAttempts;
        public int NumberOfAttempts { get => numberOfAttempts; set => numberOfAttempts = Mathf.Clamp(value, minAttemptLength, maxAttemptLength); }

        private void Reset()
        {
            MaximumWordLength = 4;
            NumberOfAttempts = 10;
        }

        [Conditional(Tag.DEBUG)]
        private void OnValidate()
        {
            if (MaximumWordLength > 13)
                MaximumWordLength = 13;

            if (MaximumWordLength > 100)
                MaximumWordLength = 100;
        }
    }
}