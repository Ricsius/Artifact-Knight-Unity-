using Assets.Scripts.Detectors;
using UnityEngine;

namespace Assets.Scripts.Controllers
{
    public class StealingPigeonController : EvilEyeController
    {
        protected override void OnCollisionEnter2D(Collision2D collision)
        {
            if (SpecialGameObjectRecognition.IsPlayer(collision.gameObject))
            {
                Destroy(gameObject);
            }
        }
    }
}
