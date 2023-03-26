
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Ui.Menus
{
    public class PauseMenu : MonoBehaviour
    {
        private CheckpointManager _checkpointManager;
        private bool _isHidden;

        protected virtual void Awake() 
        {
            _checkpointManager = GameObject.Find("CheckpointManager").GetComponent<CheckpointManager>();
        }

        protected virtual void Start()
        {
            Hide();
        }

        protected virtual void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (_isHidden)
                {
                    Hide();
                }
                else
                {
                    Pause();
                }
            }
        }

        public void Hide()
        {
            Time.timeScale = 1f;

            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }

            _isHidden = false;
        }

        public void PutPlayerToLastCheckpoint()
        {
            _checkpointManager.PlacePlayerToTheLastCheckpoint();
        }

        public void ExitToMainMenu()
        {
            SceneManager.LoadScene(0);
        }

        private void Pause()
        {
            Time.timeScale = 0f;

            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(true);
            }

            _isHidden = true;
        }
    }
}
