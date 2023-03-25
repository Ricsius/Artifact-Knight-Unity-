
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Ui.Menus
{
    public class MainMenu : MonoBehaviour
    {
        public void LoadFirstLevel()
        {
            int index = SceneManager.GetActiveScene().buildIndex + 1;

            SceneManager.LoadScene(index);
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
