
using Assets.Scripts.Detectors;
using Assets.Scripts.Projectiles;
using Assets.Scripts.Spawners;

namespace Assets.Scripts.Items.Equipable
{
    public class RangedWeapon : EquipableItem
    {
        protected SpawnerBase _spawner;

        protected override void Awake()
        {
            base.Awake();
            _spawner = GetComponentInChildren<SpawnerBase>();
        }

        protected virtual void Start()
        {
            bool isShootingHomingProjectile = _spawner.ObjectToSpawn?.GetComponent<HomingProjectile>() != null;

            _spawner.Spawned += OnProjectileSpawned;
        }

        protected override void Effect()
        {
            _spawner.Spawn();
        }

        private void OnProjectileSpawned(object sender, SpawnedEventArgs args)
        {
            HomingProjectile homingProjectile = args.SpawnedObject.GetComponent<HomingProjectile>();

            if (homingProjectile != null)
            {
                if (_isOwnedByPlayer)
                {
                    homingProjectile.TargetSelector = o => SpecialGameObjectRecognition.IsMob(o);
                }
                else
                {
                    homingProjectile.TargetSelector = o => SpecialGameObjectRecognition.IsPlayer(o);
                }
            }
        }
    }
}
