
using Assets.Scripts.Controllers.ControllerStates.BehaviourStates.GreenKnight;
using Assets.Scripts.Spawners;


namespace Assets.Scripts.Controllers
{
    public class GreenKnightController : DefaultMobController
    {
        protected override void Awake()
        {
            base.Awake();

            TimedSpawner spawner = GetComponent<TimedSpawner>();
            spawner.enabled = false;

            _behaviorStateManager.Manage<GreenKnightAggroBehaviourState>();
        }
    }
}
