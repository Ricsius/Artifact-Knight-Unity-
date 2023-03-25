
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Detectors
{
    public abstract class DetectorBase : MonoBehaviour
    {
        public abstract IEnumerable<GameObject> Detect();
    }
}
