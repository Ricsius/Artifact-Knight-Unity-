
using Assets.Scripts.Environment;
using UnityEngine;

namespace Assets.Scripts.Ui.Menus
{
    public class MainMenu : MonoBehaviour
    {
        SceneLoader _sceneLoader;

        protected virtual void Awake()
        {
            _sceneLoader = GetComponent<SceneLoader>();
        }

        public void LoadFirstLevel()
        {
            _sceneLoader.LoadNextScene();
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
