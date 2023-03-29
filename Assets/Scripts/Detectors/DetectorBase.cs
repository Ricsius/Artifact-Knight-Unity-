
using UnityEngine;

namespace Assets.Scripts.Detectors
{
    //ToDo: Fix up detector returns.
    public abstract class DetectorBase<T> : MonoBehaviour
    {
        public abstract T Detect();
    }
}
