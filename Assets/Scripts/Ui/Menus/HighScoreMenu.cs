
using Assets.Scripts.Systems.Score.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.UI.Menus
{
    public class HighScoreMenu : MonoBehaviour
    {
        [SerializeField]
        private GameObject _mainMenu;
        [SerializeField]
        private Transform _entries;
        [SerializeField]
        private GameObject _entryPrefab;

        protected virtual void OnEnable()
        {
            for (int i = 0; i < _entries.childCount; i++)
            {
                Destroy(_entries.GetChild(i).gameObject);
            }
            
            IEnumerable<PlayerScore> scores = ScoreStorage.GetScores().OrderBy(s => s.LevelName);

            foreach (PlayerScore score in scores)
            {
                Transform entryTransform = Instantiate(_entryPrefab, _entries).transform;

                entryTransform.GetChild(0).GetComponent<TextMeshProUGUI>().text = score.LevelName;
                entryTransform.GetChild(1).GetComponent<TextMeshProUGUI>().text = score.PlayerName;
                entryTransform.GetChild(2).GetComponent<TextMeshProUGUI>().text = score.Score.ToString();
                entryTransform.GetChild(3).GetComponent<TextMeshProUGUI>().text = score.Date.ToString("yyyy. MM. dd.");
            }
        }

        public void ReturnToMainMenu()
        {
            _mainMenu.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}
