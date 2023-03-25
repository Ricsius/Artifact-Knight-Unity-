
using Assets.Scripts.Detectors;
using Assets.Scripts.Systems.Health;

namespace Assets.Scripts.HitBoxes
{
    public class PlayerHealthAffectingHitBox : CharacterHealthAffectingHitBox
    {
        protected override void HealthAffecting(int amount, HealthSystem healthSystem)
        {
            if (healthSystem != null && SpecialGameObjectRecognition.IsPlayer(healthSystem.gameObject))
            {
                base.HealthAffecting(amount, healthSystem);
            }
        }
    }
}
