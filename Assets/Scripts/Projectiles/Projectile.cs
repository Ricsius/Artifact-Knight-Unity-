
using UnityEngine;


namespace Assets.Scripts.Projectiles
{
    public class Projectile : MonoBehaviour
    {
        protected virtual void Awake()
        {
            tag = "Projectile";
        }
        void OnCollisionEnter2D(Collision2D collision)
        {
            Destroy(gameObject);
        }
    }
}
