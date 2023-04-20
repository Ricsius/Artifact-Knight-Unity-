using Assets.Scripts.Detectors;
using Assets.Scripts.Environment;
using Assets.Scripts.Items;
using Assets.Scripts.Systems.Equipment;
using UnityEngine;

namespace Assets.Scripts.HitBoxes
{
    public class PlayerEquippedItemStealerHitBox : MonoBehaviour
    {
        [SerializeField]
        private Chest _chest;

        protected virtual void OnCollisionEnter2D(Collision2D collison)
        {
            StealFromPlayer(collison.gameObject);
        }
        protected virtual void OnTriggerEnter2D(Collider2D collider)
        {
            StealFromPlayer(collider.gameObject);
        }

        private void StealFromPlayer(GameObject gameObject)
        {
            if (SpecialGameObjectRecognition.IsPlayer(gameObject))
            {
                EquipmentSystem playerEquipmentSystem = gameObject.GetComponent<EquipmentSystem>();
                ItemBase item = playerEquipmentSystem?.EquipedItem;

                if (item != null)
                {
                    playerEquipmentSystem.RemoveItem(item);

                    _chest.Put(item);
                }
            }
        }
    }
}
