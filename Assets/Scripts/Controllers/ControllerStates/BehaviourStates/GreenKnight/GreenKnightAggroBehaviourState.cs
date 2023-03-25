
using Assets.Scripts.Spawners;

namespace Assets.Scripts.Controllers.ControllerStates.BehaviourStates.GreenKnight
{
    public class GreenKnightAggroBehaviourState : DefaultAggroBehaviourState
    {
        private TimedSpawner _spawner;
        public GreenKnightAggroBehaviourState() 
            : base()
        {
            
        }

        public override void OnSelect()
        {
            base.OnSelect();

            if (_spawner == null)
            {
                _spawner = _manager.Owner.GetComponent<TimedSpawner>();
            }

            
            _spawner.ResetSpawnInterval();
            _spawner.enabled = true;
        }

        public override void OnDeselect()
        {
            base.OnDeselect();

            _spawner.enabled = false;
        }
    }
}
