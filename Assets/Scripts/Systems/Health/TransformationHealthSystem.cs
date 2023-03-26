using Assets.Scripts.Controllers;
using UnityEngine;

namespace Assets.Scripts.Systems.Health
{
    public class TransformationHealthSystem : HealthSystem
    {
        protected override void Die()
        {
            GameObject originalForm = null;
            int i = 0;

            while (i < transform.childCount && originalForm == null) 
            {
                originalForm = transform.GetChild(i).gameObject.GetComponent<ControllerBase>()?.gameObject;
                ++i;
            }

            originalForm.transform.parent = null;

            base.Die();

            originalForm.SetActive(true);
        }
    }
}
