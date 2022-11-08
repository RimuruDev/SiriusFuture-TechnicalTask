using UnityEngine;
using UnityEngine.SceneManagement;

namespace RimuruDev.SiriusFuture
{
    [DisallowMultipleComponent]
    [HelpURL("https://t.me/AbyssMothGames")]
    public sealed class SceneHelper : MonoBehaviour
    {
        public void RestartGamplayScene() => SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);

        public void LoadDefaultProjectSettings()
        {
            var dataContainer = FindObjectOfType<GameDataContainer>();
            dataContainer.GameplaySettings.MaximumWordLength = 4;
            dataContainer.GameplaySettings.NumberOfAttempts = 10;
            //GameObject.FindGameObjectWithTag("WorningPopup").SetActive(false);
            RestartGamplayScene();
        }
    }
}