
using Assets.Scripts.Detectors;
using Assets.Scripts.Systems.Health;

namespace Assets.Scripts.HitBoxes
{
    public class PlayerHealthAffectingHitBox : CharacterHealthAffectingHitBox
    {
        protected override void HealthAffecting(HealthSystem healthSystem)
        {
            if (healthSystem != null && SpecialGameObjectRecognition.IsPlayer(healthSystem.gameObject))
            {
                base.HealthAffecting(healthSystem);
            }
        }
    }
}
