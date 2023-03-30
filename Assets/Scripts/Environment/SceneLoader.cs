using Assets.Scripts.Systems.Score;
using Assets.Scripts.Ui.Menus;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Environment
{
    public class SceneLoader : MonoBehaviour
    {
        [SerializeField]
        private LoadingScreen _loadingScreen;

        public void LoadNextScene(ScoreSystem score)
        {
            int nextIndex = SceneManager.GetActiveScene().buildIndex + 1;
            int index = SceneManager.sceneCountInBuildSettings > nextIndex ? nextIndex : 0;

            if (index == 0)
            {
                LoadMainMenu();

                return;
            }

            AsyncOperation operation = SceneManager.LoadSceneAsync(index);

            operation.allowSceneActivation = false;

            _loadingScreen.LoadingOperation = operation;
            _loadingScreen.Score = score;

            _loadingScreen.gameObject.SetActive(true);
        }

        public void LoadNextScene()
        {
            LoadNextScene(null);
        }

        public void LoadMainMenu()
        {
            SceneManager.LoadScene(0);
        }
    }
}
