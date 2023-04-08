
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
        public event EventHandler ScoreChanged;
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
        }

        protected virtual void OnDisable()
        {
            _equipment.NewItemAdded -= OnNewItemAdded;
        }

        private void OnNewItemAdded(object sender, EventArgs args)
        {
            ItemEventArgs itemArgs = args as ItemEventArgs;
            ItemBase item= itemArgs.Item;
            ScoreEntry entry = new ScoreEntry(item.name, item.ScoreValue);

            _entries.Add(entry);
            //ToDo: Fix this!
            ScoreChanged?.Invoke(this, new ScoreChangeEventArgs(ScoreSum));
        }
    }
}
