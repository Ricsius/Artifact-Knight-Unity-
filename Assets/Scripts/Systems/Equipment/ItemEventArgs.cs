using Assets.Scripts.Items;
using System;

namespace Assets.Scripts.Systems.Equipment
{
    public class ItemEventArgs : EventArgs
    {
        public ItemBase Item { get; }
        public ItemEventArgs(ItemBase item)
        {
            Item = item;
        }
    }
}
