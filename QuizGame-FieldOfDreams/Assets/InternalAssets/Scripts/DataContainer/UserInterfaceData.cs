using System;
using UnityEngine;

namespace RimuruDev.SiriusFuture
{
    [Serializable]
    public sealed class UserInterfaceData
    {
        private const int keyboardHeaderLength = 10;
        private const int keyboardMiddleLength = 9;
        private const int keyboardBottomLength = 7;

        [SerializeField] private Transform[] headerUserInterfaceKeyboard;
        public Transform[] HeaderUserInterfaceKeyboard { get => headerUserInterfaceKeyboard; set => headerUserInterfaceKeyboard = value; }

        [SerializeField] private Transform[] middleUserInterfaceKeyboard;
        public Transform[] MiddleUserInterfaceKeyboard { get => middleUserInterfaceKeyboard; set => middleUserInterfaceKeyboard = value; }

        [SerializeField] private Transform[] bottomUserInterfaceKeyboard;
        public Transform[] BottomUserInterfaceKeyboard { get => bottomUserInterfaceKeyboard; set => bottomUserInterfaceKeyboard = value; }
    }
}