using Assets.Scripts.Environment;
using Assets.Scripts.TestScenario.Requirement;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.TestScenario
{
    public class TestScenario : MonoBehaviour
    {
        [SerializeField]
        private string _name;
        [SerializeField]
        private float _duration;
        private IEnumerable<TestRequirementBase> _requirements;
        private SceneLoader _sceneLoader;

        protected virtual void Awake()
        {
            _sceneLoader = GetComponent<SceneLoader>();
            _requirements = GetComponentsInChildren<TestRequirementBase>();
        }

        protected virtual void Update()
        {
            _duration -= Time.deltaTime;

            if (_duration <= 0)
            {
                StringBuilder stringBuilder = new StringBuilder();

                foreach (TestRequirementBase requirement in _requirements) 
                {
                    stringBuilder.Append($"\"{_name}\" scenario \"{requirement.Name}\" requirement ");

                    if (requirement.Check())
                    {
                        stringBuilder.Append("passed");

                        Debug.Log(stringBuilder);
                    }
                    else
                    {
                        stringBuilder.Append("failed");

                        Debug.LogError(stringBuilder);
                    }

                    stringBuilder.Clear();
                }
                
                _sceneLoader.LoadNextScene(false);
            }
        }
    }
}
