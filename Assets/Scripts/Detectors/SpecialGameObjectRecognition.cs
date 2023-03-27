
using UnityEngine;

namespace Assets.Scripts.Detectors
{
    public static class SpecialGameObjectRecognition
    {
        public static bool IsPlayer(GameObject gameObject)
        {
            return gameObject.tag.Equals("Player");
        }

        public static bool IsItem(GameObject gameObject)
        {
            return gameObject.tag.Equals("Item");
        }

        public static bool IsLadder(GameObject gameObject)
        {
            return gameObject.tag.Equals("Ladder");
        }

        public static bool IsDoor(GameObject gameObject)
        {
            return gameObject.tag.Equals("Door");
        }

        public static bool IsProjectile(GameObject gameObject)
        {
            return gameObject.tag.Equals("Projectile");
        }

        public static bool IsProfessor(GameObject gameObject)
        {
            return gameObject.tag.Equals("Professor");
        }
    }
}
