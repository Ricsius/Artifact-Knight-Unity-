
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Detectors
{
    //ToDo: Fix up detector returns.
    public abstract class DetectorBase : MonoBehaviour
    {
        public abstract IEnumerable<GameObject> Detect();
    }
}
