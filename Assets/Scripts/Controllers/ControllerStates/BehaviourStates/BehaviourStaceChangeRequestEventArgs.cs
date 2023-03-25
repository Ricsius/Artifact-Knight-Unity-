using System;

namespace Assets.Scripts.Controllers.ControllerStates.BehaviourStates
{
    public class BehaviourStaceChangeRequestEventArgs : EventArgs
    {
        public ControllerBehaviourStateType BehaviourStateType { get; }
        public BehaviourStaceChangeRequestEventArgs(ControllerBehaviourStateType type)
        {
            BehaviourStateType = type;
        }
    }
}
