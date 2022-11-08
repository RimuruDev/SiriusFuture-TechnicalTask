using System;

namespace RimuruDev.SiriusFuture
{
    [Serializable]
    public sealed class KeywordWords
    {
        public readonly char[] headerKeybardWords = { 'q', 'w', 'e', 'r', 't', 'y', 'u', 'i', 'o', 'p' };
        public readonly char[] middleKeybardWord = { 'a', 's', 'd', 'f', 'g', 'h', 'j', 'k', 'l' };
        public readonly char[] bottomKeybardWords = { 'z', 'x', 'c', 'v', 'b', 'n', 'm' };
    }
}