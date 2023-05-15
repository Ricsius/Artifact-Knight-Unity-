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

        public void LoadNextScene()
        {
            LoadNextScene(true);
        }

        public void LoadNextScene(bool useLoadingScreen)
        {
            LoadNextScene(useLoadingScreen, null);
        }

        public void LoadNextScene(ScoreSystem score)
        {
            LoadNextScene(true, score);
        }

        public void LoadNextScene(bool useLoadingScreen, ScoreSystem score)
        {
            int nextIndex = SceneManager.GetActiveScene().buildIndex + 1;
            int index = nextIndex % SceneManager.sceneCountInBuildSettings;

            LoadScene(index, useLoadingScreen, score);
        }

        public void LoadMainMenu()
        {
            LoadScene(0, false);
        }

        private void LoadScene(int index, bool useLoadingScreen)
        {
            LoadScene(index, useLoadingScreen, null);
        }

        private void LoadScene(int index, bool useLoadingScreen, ScoreSystem score)
        {
            if (useLoadingScreen)
            {
                AsyncOperation operation = SceneManager.LoadSceneAsync(index);

                operation.allowSceneActivation = false;

                _loadingScreen.LoadingOperation = operation;
                _loadingScreen.Score = score;

                _loadingScreen.gameObject.SetActive(true);
            }
            else
            {
                SceneManager.LoadScene(index);
            }
        }
    }
}
