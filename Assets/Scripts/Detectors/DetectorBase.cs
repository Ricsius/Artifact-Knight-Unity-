
using UnityEngine;

namespace Assets.Scripts.Detectors
{
    //ToDo: Fix detection item gathering
    public abstract class DetectorBase<T> : MonoBehaviour
    {
        public abstract T Detect();
    }
}
