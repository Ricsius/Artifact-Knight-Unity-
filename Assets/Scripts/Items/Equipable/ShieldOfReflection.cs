using Assets.Scripts.Detectors;
using UnityEngine;

namespace Assets.Scripts.Items.Equipable
{
    public class ShieldOfReflection : RangedWeapon
    {
        [field: SerializeField]
        public Sprite LoadedSprite { get; private set; }
        private SpriteRenderer _spriteRenderer;
        private Sprite _originalSprite;

        protected override void Awake()
        {
            base.Awake();

            _spriteRenderer = GetComponent<SpriteRenderer>();
            _originalSprite = _spriteRenderer.sprite;
        }

        protected override void Effect()
        {
            base.Effect();

            if (_spawner.ObjectToSpawn != null)
            {
                _spawner.ObjectToSpawn= null;
                _spriteRenderer.sprite= _originalSprite;
            }
        }

        protected virtual void OnTriggerEnter2D(Collider2D collider)
        {
            if (SpecialGameObjectRecognition.IsProjectile(collider.gameObject))
            {
                GameObject projectileObject = collider.gameObject;

                if (_spawner.ObjectToSpawn)
                {
                    Destroy(_spawner.ObjectToSpawn);
                }

                _spawner.ObjectToSpawn = Instantiate(projectileObject, Vector2.down * 1000, Quaternion.identity);
                _spriteRenderer.sprite = LoadedSprite;

                Destroy(projectileObject);
            }
        }
    }
}
