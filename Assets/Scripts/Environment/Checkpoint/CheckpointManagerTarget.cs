
using UnityEngine;

namespace Assets.Scripts.Environment.Checkpoint
{
    public class CheckpointManagerTarget : MonoBehaviour
    {
        private CheckpointManager _checkpointManager;

        protected virtual void Awake()
        {
            _checkpointManager = GameObject.Find("Checkpoint Manager").GetComponent<CheckpointManager>();
        }

        protected virtual void Start()
        {
            _checkpointManager.JoinTargets(gameObject);
        }

        protected virtual void OnDestroy()
        {
            _checkpointManager.LeaveTargets(gameObject);
        }
    }
}
