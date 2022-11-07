using UnityEngine;

namespace RimuruDev.SiriusFuture
{
    public sealed class InitializationGame : MonoBehaviour
    {
        [Header("Simple DI :D")]
        [SerializeField] private GameObject[] container;

        private void Start() => Init();

#if !DEBUG
        private void OnValidate()
        {
            for (int i = 0; i < container.Length; i++)
            {
                if (container[i].GetComponent<IInitSystem>() == null)
                    container[i] = null;
                else
                    Debug.LogWarning("You are trying to inject an object that has not signed a contract with IInitSystem.");
            }
        }
#endif

        private void Init()
        {
            for (int i = 0; i < container.Length; i++)
                container[i].GetComponent<IInitSystem>().Init();
        }
    }
}