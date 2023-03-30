using Assets.Scripts.Environment;
using Assets.Scripts.Environment.Checkpoint;
using UnityEngine;

namespace Assets.Scripts.Ui.Menus
{
    public class PauseMenu : MonoBehaviour
    {
        [SerializeField]
        private GameObject _buttons;
        [SerializeField]
        private CheckpointManager _checkpointManager;
        private SceneLoader _sceneLoader;

        protected virtual void Awake()
        {
            _sceneLoader = GetComponent<SceneLoader>();
        }

        protected virtual void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (_buttons.activeSelf)
                {
                    Resume();
                }
                else
                {
                    Pause();
                }
            }
        }

        public void Resume()
        {
            Time.timeScale = 1f;

            _buttons.SetActive(false);
        }

        public void PutPlayerToLastCheckpoint()
        {
            _checkpointManager.PlacePlayerToTheLastCheckpoint();

            Resume();
        }

        public void ExitToMainMenu()
        {
            _sceneLoader.LoadMainMenu();
        }

        private void Pause()
        {
            Time.timeScale = 0f;

            _buttons.SetActive(true);
        }
    }
}