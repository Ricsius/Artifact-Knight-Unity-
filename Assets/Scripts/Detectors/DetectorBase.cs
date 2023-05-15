
using UnityEngine;

namespace Assets.Scripts.Detectors
{
    public abstract class DetectorBase<T> : MonoBehaviour
    {
        public abstract T Detect();
    }
}
