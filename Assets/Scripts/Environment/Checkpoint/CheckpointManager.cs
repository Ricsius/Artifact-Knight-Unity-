
using Assets.Scripts.Systems.Health;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Environment.Checkpoint
{
    public class CheckpointManager : MonoBehaviour
    {
        [field: SerializeField]
        public List<Checkpoint> Checkpoints { get; private set; }
        private LinkedList<GameObject> _targets;
        private GameObject _currentTarget;
        private HealthSystem _targetHealthSystem;
        private Vector2 _lastActiveChackpointPosition;

        protected void Awake()
        {
            _targets = new LinkedList<GameObject>();

            foreach (Checkpoint checkpoint in Checkpoints)
            {
                checkpoint.Activated += OnActivated;
            }
        }

        public void JoinTargets(GameObject gameObject)
        {
            _targets.AddLast(gameObject);

            if (_targets.Count == 1)
            {
                _currentTarget = gameObject;
                SetUpCurrentTarget();
            }
        }

        public void LeaveTargets(GameObject gameObject)
        {
            _targets.Remove(gameObject);

            GameObject first = _targets.FirstOrDefault();

            if (first != _currentTarget)
            {
                _currentTarget = first;
                SetUpCurrentTarget();
            }
        }

        public void PlacePlayerToTheLastCheckpoint()
        {
            if (_currentTarget != null)
            {
                _currentTarget.transform.position = _lastActiveChackpointPosition;
                _currentTarget.transform.rotation = Quaternion.identity;
                _targetHealthSystem.Heal(_targetHealthSystem.MaxHealthPoints);

                _currentTarget.SetActive(true);
            }
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

        private void SetUpCurrentTarget()
        {
            _targetHealthSystem = _currentTarget?.GetComponent<HealthSystem>();

            if (_targetHealthSystem != null)
            {
                _targetHealthSystem.Death += OnPlayerDeath;

                if (_lastActiveChackpointPosition == default(Vector2))
                {
                    _lastActiveChackpointPosition = _currentTarget.transform.position;
                }
            }
        }

        private void OnPlayerDeath(object sender, EventArgs args)
        {
            PlacePlayerToTheLastCheckpoint();
        }
    }
}
