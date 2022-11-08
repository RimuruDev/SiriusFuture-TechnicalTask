using UnityEngine;

namespace RimuruDev.SiriusFuture
{
    [DisallowMultipleComponent]
    [HelpURL("https://t.me/AbyssMothGames")]
    public sealed class InitializationGame : MonoBehaviour
    {
        [Header("Gameplay Settings")]
        [SerializeField] private GameplaySettings gameplaySettings;

        [Header("Simple DI :D")]
        [SerializeField] private GameObject[] container;

        private void Awake() => FindObjectOfType<GameDataContainer>().GameplaySettings = gameplaySettings;

        private void Start() => Init();

        [System.Diagnostics.Conditional("DEBUG")]
        private void OnValidate()
        {
            for (int i = 0; i < container.Length; i++)
            {
                if (container[i].GetComponent<IInitSystem>() == null)
                    container[i] = null;
            }
        }

        private void Init()
        {
            for (int i = 0; i < container.Length; i++)
                container[i].GetComponent<IInitSystem>().Init();
        }
    }
}