
using Assets.Scripts.Systems.Health;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Environment.Checkpoint
{
    public class CheckpointManager : MonoBehaviour
    {
        [field: SerializeField]
        public List<Checkpoint> Checkpoints { get; private set; }
        public GameObject Player 
        {
            get
            {
                return _player;
            }
            set 
            {
                if (_player != null)
                {
                    _playerHealthSystem.Death -= OnPlayerDeath;
                }

                _player = value;
                _playerHealthSystem = _player?.GetComponent<HealthSystem>();

                if (_player != null)
                {
                    _playerHealthSystem.Death += OnPlayerDeath;

                    if (_lastActiveChackpointPosition == default(Vector2))
                    {
                        _lastActiveChackpointPosition = _player.transform.position;
                    }
                }
            }
        }
        private HealthSystem _playerHealthSystem;
        private Vector2 _lastActiveChackpointPosition;
        private GameObject _player;
        protected void Awake()
        {
            foreach (Checkpoint checkpoint in Checkpoints)
            {
                checkpoint.Activated += OnActivated;
            }
        }

        public void PlacePlayerToTheLastCheckpoint()
        {
            _player.transform.position = _lastActiveChackpointPosition;
            _player.transform.rotation = Quaternion.identity;
            _playerHealthSystem.Heal(_playerHealthSystem.MaxHealthPoints);

            _player.SetActive(true);
        }

        private void OnActivated(object sender, EventArgs args)
        {
            Checkpoint checkpoint = sender as Checkpoint;
            int index = Checkpoints.IndexOf(checkpoint);
            List<Checkpoint> checkpointsToRemove = new List<Checkpoint>();

            for (int i = 0; i < index; i++)
            {
                Checkpoint c = Checkpoints[i];

                c.Activated -= OnActivated;
                c.Activate();

                checkpointsToRemove.Add(c);
            }

            foreach (Checkpoint c in checkpointsToRemove)
            {
                Checkpoints.Remove(c);
            }

            checkpoint.Activated -= OnActivated;
            Checkpoints.Remove(checkpoint);

            _lastActiveChackpointPosition = checkpoint.transform.position;
        }

        private void OnPlayerDeath(object sender, EventArgs args)
        {
            PlacePlayerToTheLastCheckpoint();
        }
    }
}
