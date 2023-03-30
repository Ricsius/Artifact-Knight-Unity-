
using Assets.Scripts.Systems.Score;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Ui.Menus
{
    public class LoadingScreen : MonoBehaviour
    {
        public AsyncOperation LoadingOperation { private get; set; }
        public ScoreSystem Score { private get; set; }
        [SerializeField]
        private Slider _slider;
        [SerializeField]
        private Transform _scoreEntries;
        [SerializeField]
        private GameObject _scoreEntryPrefab;
        [SerializeField]
        private GameObject _scoreSum;
        [SerializeField]
        private GameObject _continueButton;

        protected virtual void Update()
        {
            if (LoadingOperation != null)
            {
                _slider.value = LoadingOperation.progress + .1f;

                if (LoadingOperation.progress == .9f)
                {
                    _continueButton.SetActive(true);
                }
            }
        }

        protected virtual void OnEnable()
        {
            for (int i = 0; i < _scoreEntries.childCount; i++)
            {
                Destroy(_scoreEntries.GetChild(i).gameObject);
            }

            if (Score != null)
            {
                foreach (ScoreEntry entry in Score.Entries)
                {
                    GameObject entryObject = Instantiate(_scoreEntryPrefab);
                    TextMeshProUGUI entryText = entryObject.GetComponentInChildren<TextMeshProUGUI>();

                    entryText.text = entry.ToString();
                    entryObject.transform.SetParent(_scoreEntries);
                }

                _scoreSum.GetComponentInChildren<TextMeshProUGUI>().text = Score.ScoreSum.ToString();

                _scoreSum.SetActive(true);
            }
        }

        public void StartNextLevel()
        {
            LoadingOperation.allowSceneActivation = true;
        }
    }
}
