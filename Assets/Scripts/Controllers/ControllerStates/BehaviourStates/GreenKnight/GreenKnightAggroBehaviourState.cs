
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

        protected override void Init()
        {
            base.Init();

            _spawner = _manager.Owner.GetComponent<TimedSpawner>();
        }

        public override void OnSelect()
        {
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
