
using Assets.Scripts.Detectors;
using UnityEngine;

namespace Assets.Scripts.Systems.Equipment
{
    public class ItemCollector : MonoBehaviour
    {
        private EquipmentSystem _equipmentSystem;
        private GameObject _collectableItem;
        private void Awake()
        {
            _equipmentSystem = GetComponent<EquipmentSystem>();
        }
        private void OnTriggerEnter2D(Collider2D collider)
        {
            if (SpecialGameObjectRecognition.IsItem(collider.gameObject))
            {
                _collectableItem = collider.gameObject;
            }
        }

        private void OnTriggerExit2D(Collider2D collider)
        {
            if (collider.gameObject == _collectableItem)
            {
                _collectableItem = null;
            }
        }

        public void CollectItem()
        {
            if (_collectableItem != null)
            {
                _equipmentSystem.AddItem(_collectableItem);
            }
        }
    }
}
