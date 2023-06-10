using Assets.Scripts.Controllers;
using Assets.Scripts.Detectors;
using Assets.Scripts.Environment.Checkpoint;
using Assets.Scripts.UI.Indicators;
using Assets.Scripts.UI.Indicators.Targets;
using UnityEngine;

namespace Assets.Scripts.Systems.Health
{
    public class TransformationHealthSystem : HealthSystem
    {
        public float TimeTillTurningBack { get; private set; }
        public float TransformationDuration 
        { 
            get 
            { 
                return _transformationDuration; 
            } 
            set 
            { 
                _transformationDuration = value;
                TimeTillTurningBack = _transformationDuration;
            } 
        }
        private float _transformationDuration;

        protected virtual void Update()
        {
            TimeTillTurningBack -= Time.deltaTime;

            if (TimeTillTurningBack <= 0)
            {
                TurnBackToOriginalForm();
            }
        }

        protected override void Die()
        {
            TurnBackToOriginalForm();
        }

        private void TurnBackToOriginalForm()
        {
            GameObject originalForm = null;
            int i = 0;

            while (originalForm == null && i < transform.childCount)
            {
                originalForm = transform.GetChild(i).GetComponent<ControllerBase>().gameObject;
                ++i;
            }

            originalForm.transform.parent = null;

            originalForm.SetActive(true);

            if (SpecialGameObjectRecognition.IsPlayer(originalForm))
            {
                originalForm.AddComponent<CheckpointManagerTarget>();
            }

            Destroy(gameObject);
        }
    }
}
