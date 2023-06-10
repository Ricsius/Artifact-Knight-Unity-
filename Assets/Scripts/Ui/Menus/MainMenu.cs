
using Assets.Scripts.Environment;
using UnityEngine;

namespace Assets.Scripts.UI.Menus
{
    public class MainMenu : MonoBehaviour
    {
        private SceneLoader _sceneLoader;
        [SerializeField]
        private GameObject _highScoreMenu;

        protected virtual void Awake()
        {
            _sceneLoader = GetComponent<SceneLoader>();
        }

        public void LoadFirstLevel()
        {
            _sceneLoader.LoadNextScene();
        }

        public void ShowHighScore()
        {
            _highScoreMenu.SetActive(true);
            gameObject.SetActive(false);
        }

        public void Quit()
        {
            #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
            #endif

            Application.Quit();
        }
    }
}
