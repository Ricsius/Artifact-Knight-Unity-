
using UnityEngine;

namespace Assets.Scripts.Controllers.Test
{
    public class TestAction : MonoBehaviour
    {
        [field: SerializeField]
        public TestActionType Type { get; private set; }
        [field: SerializeField]
        public float Duration { get; private set; }
    }
}
