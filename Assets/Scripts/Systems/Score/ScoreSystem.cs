using Assets.Scripts.Items;
using Assets.Scripts.Systems.Equipment;
using System;
using UnityEngine;

namespace Assets.Scripts.Systems.Score
{
    public class ScoreSystem : MonoBehaviour
    {
        public event EventHandler ScoreChanged;
        public int Score { get; private set; }
        private EquipmentSystem _equipment;
        protected virtual void Awake()
        {
            _equipment= GetComponent<EquipmentSystem>();
            Score = 0;
        }

        protected virtual void OnEnable()
        {
            _equipment.NewItemAdded += OnNewItemAdded;
        }

        protected virtual void OnDisable()
        {
            _equipment.NewItemAdded -= OnNewItemAdded;
        }

        private void OnNewItemAdded(object sender, EventArgs args)
        {
            ItemEventArgs itemArgs = args as ItemEventArgs;
            int itemScoreValue = itemArgs.Item.GetComponent<ItemBase>().ScoreValue;

            Score += itemScoreValue;

            ScoreChanged?.Invoke(this, new ScoreChangeEventArgs(Score));
        }
    }
}
