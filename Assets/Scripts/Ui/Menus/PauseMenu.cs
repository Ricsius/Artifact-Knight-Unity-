using Assets.Scripts.Environment;
using Assets.Scripts.Environment.Checkpoint;
using UnityEngine;
using UnityEngine.UIElements;

namespace Assets.Scripts.Ui.Menus
{
    public class PauseMenu : MonoBehaviour
    {
        [SerializeField]
        private GameObject _background;
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

            _background.SetActive(false);
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

            _background.SetActive(true);
            _buttons.SetActive(true);
        }
    }
}
