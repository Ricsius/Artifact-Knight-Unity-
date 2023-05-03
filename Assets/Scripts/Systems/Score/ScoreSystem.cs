
using Assets.Scripts.Items;
using Assets.Scripts.Systems.Equipment;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Systems.Score
{
    public class ScoreSystem : MonoBehaviour
    {
        public IEnumerable<ScoreEntry> Entries => _entries;
        public int ScoreSum => _entries.Sum(e => e.Score);
        public event EventHandler<ScoreChangeEventArgs> ScoreChanged;
        private EquipmentSystem _equipment;
        private List<ScoreEntry> _entries;
        protected virtual void Awake()
        {
            _equipment= GetComponent<EquipmentSystem>();
            _entries = new List<ScoreEntry>();
        }

        protected virtual void OnEnable()
        {
            _equipment.NewItemAdded += OnNewItemAdded;
            _equipment.ItemRemoved += OnItemRemoved;
        }

        protected virtual void OnDisable()
        {
            _equipment.NewItemAdded -= OnNewItemAdded;
            _equipment.ItemRemoved -= OnItemRemoved;
        }

        private void OnNewItemAdded(object sender, ItemEventArgs args)
        {
            ItemBase item= args.Item;

            if (item.ScoreValue != 0)
            {
                ScoreEntry entry = new ScoreEntry(item.name, item.ScoreValue);

                _entries.Add(entry);

                ScoreChanged?.Invoke(this, new ScoreChangeEventArgs(ScoreSum));
            }
        }

        private void OnItemRemoved(object sender, ItemEventArgs args)
        {
            ItemBase item = args.Item;
            ScoreEntry entry = _entries.FirstOrDefault(e => e.Name == item.name && e.Score == item.ScoreValue);
            bool removed = _entries.Remove(entry);

            if (removed)
            {
                ScoreChanged?.Invoke(this, new ScoreChangeEventArgs(ScoreSum));
            }
        }
    }
}
