using Assets.Scripts.Detectors;
using Assets.Scripts.Spawners;
using Assets.Scripts.Systems.Health;
using System;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Controllers.ControllerStates.BehaviourStates.Eyenomancer
{
    public class EyenomancerAggroBehaviourState : ControllerBehaviourStateBase
    {
        private EyenomancerController _controller;
        private SpawnerBase _spawner;
        private float _timeTillSummon;
        private int _activeEyes;

        public EyenomancerAggroBehaviourState()
            : base(ControllerBehaviourStateType.Aggro)
        {
        }

        public override void Behaviour()
        {

            GameObject player = DetectGameObjectsBehind().FirstOrDefault(o => SpecialGameObjectRecognition.IsPlayer(o));

            player = player == null ? DetectGameObjectsFront().FirstOrDefault(o => SpecialGameObjectRecognition.IsPlayer(o)) : player;

            if (player != null)
            {
                float newDirectionX = player.transform.position.x < _transform.position.x ? 1 : -1;
                Vector2 newDirection = new Vector2(newDirectionX, 0);

                MovementStateManager.CurrentMovementState.Move(newDirection);
            }
            else
            {
                InvokeBehaviourStateChangeRequest(ControllerBehaviourStateType.Idle);
            }

            if (_timeTillSummon > 0)
            {
                _timeTillSummon -= Time.deltaTime;
            }

            if (_timeTillSummon <= 0 && _activeEyes < _controller.SummonLimit)
            {
                Summon();
            }
        }

        protected override void Init()
        {
            base.Init();

            if (_controller == null)
            {
                _controller = _manager.Owner.GetComponent<EyenomancerController>();
                _spawner = _transform.Find("SpawnLocation").GetComponent<SpawnerBase>();
                _spawner.Spawned += (object sender, EventArgs args) =>
                {
                    SpawnedEventArgs spawnedArgs = args as SpawnedEventArgs;

                    spawnedArgs.SpawnedObject.GetComponent<HealthSystem>().Death += OnSummonedEyeDeath;
                };
                _activeEyes = 0;
            }

            _timeTillSummon = _controller.SummonCooldown;
        }

        private void Summon()
        {
            ++_activeEyes;
            _spawner.Spawn();

            _timeTillSummon = _controller.SummonCooldown;
        }

        private void OnSummonedEyeDeath(object sender, EventArgs args)
        {
            --_activeEyes;
        }
    }
}
