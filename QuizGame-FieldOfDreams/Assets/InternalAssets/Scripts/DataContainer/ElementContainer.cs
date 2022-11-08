using System;
using UnityEngine;

namespace RimuruDev.SiriusFuture
{
    [Serializable]
    public sealed class ElementContainer
    {
        [SerializeField] private Transform[] element;
        public Transform[] Element { get => element; set => element = value; }
    }
}