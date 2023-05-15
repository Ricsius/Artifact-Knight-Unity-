using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.TestScenario.Requirement
{
    public abstract class TestRequirementBase : MonoBehaviour
    {
        [field: SerializeField]
        public string Name { get; private set; }
        [SerializeField]
        protected GameObject _subject;
        public abstract bool Check();
    }
}
