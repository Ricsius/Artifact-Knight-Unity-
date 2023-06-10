using Assets.Scripts.Detectors;
using Assets.Scripts.Systems.Equipment;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Controllers.ControllerStates.BehaviourStates.Dragon
{
    public class DragonAggroBehaviourState : ControllerBehaviourStateBase
    {
        private EquipmentSystem _equipmentSystem;
        
        public DragonAggroBehaviourState()
            : base(ControllerBehaviourStateType.Aggro)
        {
        }

        public override void Behaviour()
        {
            Transform playerTransform = DetectGameObjectsFront().FirstOrDefault(o => SpecialGameObjectRecognition.IsPlayer(o))?.transform;

            if (playerTransform != null && !_equipmentSystem.EquipedItem.IsOnCooldown)
            {
                _equipmentSystem.UseEquippedItem();
                _equipmentSystem.StopUseEquippedItem();
            }
        }

        protected override void Init()
        {
            base.Init();

            _equipmentSystem = _manager.Owner.GetComponent<EquipmentSystem>();
        }
    }
}
