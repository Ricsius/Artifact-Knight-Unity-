
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Ui.Menus
{
    public class PauseMenu : MonoBehaviour
    {
        private CheckpointManager _checkpointManager;

        protected virtual void Awake() 
        {
            _checkpointManager = GameObject.Find("CheckpointManager").GetComponent<CheckpointManager>();
        }

        protected virtual void Start()
        {
            Resume();
        }

        protected virtual void OnEnable() 
        {
            Time.timeScale = 0f;
        }

        protected virtual void OnDisable()
        {
            Resume();
        }

        public void Resume()
        {
            Time.timeScale = 1f;
            gameObject.SetActive(false);
        }

        public void PutPlayerToLastCheckpoint()
        {
            _checkpointManager.PlacePlayerToTheLastCheckpoint();
        }

        public void ExitToMainMenu()
        {
            SceneManager.LoadScene(0);
        }
    }
}
