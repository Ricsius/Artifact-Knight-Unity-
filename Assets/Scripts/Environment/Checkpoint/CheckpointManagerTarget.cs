
using UnityEngine;

namespace Assets.Scripts.Environment.Checkpoint
{
    public class CheckpointManagerTarget : MonoBehaviour
    {
        private CheckpointManager _checkpointManager;

        protected virtual void Awake()
        {
            _checkpointManager = GameObject.Find("CheckpointManager").GetComponent<CheckpointManager>();
        }

        protected virtual void OnEnable()
        {
            _checkpointManager.Player = gameObject;
        }

        protected virtual void OnDestroy()
        {
            if (_checkpointManager.Player == gameObject)
            {
                _checkpointManager.Player = null;
            }
        }
    }
}
