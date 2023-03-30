
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

        protected override void Effect()
        {
            _spawner.Spawn();
        }
    }
}