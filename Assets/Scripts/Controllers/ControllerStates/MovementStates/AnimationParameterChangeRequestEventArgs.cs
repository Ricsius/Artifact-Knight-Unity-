
using System;

namespace Assets.Scripts.Controllers.ControllerStates.MovementStates
{
    public class AnimationParameterChangeRequestEventArgs : EventArgs
    {
        public string ParameterName { get; }
        public AnimationParameterChangeRequestEventArgs(string parameterName)
        {
            ParameterName = parameterName;
        }
    }
}
