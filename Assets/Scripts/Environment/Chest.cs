using Assets.Scripts.Items;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Environment
{
    public class Chest : MonoBehaviour
    {
        private Collider2D _collider;
        private Rigidbody2D _rigidbody;
        private SpriteRenderer _spriteRenderer;
        private List<ItemBase> _items;
        protected virtual void Awake()
        {
            tag = "Chest";

            _collider = GetComponent<Collider2D>();
            _rigidbody= GetComponent<Rigidbody2D>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _items = new List<ItemBase>();

            _collider.enabled = false;
            _rigidbody.simulated = false;
            _spriteRenderer.enabled = false;
        }
        public void Open()
        {
            if (!_items.Any())
            {
                return;
            }

            foreach (ItemBase item in _items)
            {
                item.Reveal();
            }

            _items.Clear();

            _collider.enabled = false;
            _rigidbody.simulated = false;
            _spriteRenderer.enabled = false;
        }

        public void Put(ItemBase item)
        {
            _items.Add(item);

            if (_items.Count == 1)
            {
                _collider.enabled = true;
                _rigidbody.simulated = true;
                _spriteRenderer.enabled = true;
            }

            item.transform.position = transform.position;

            item.Hide();
        }
    }
}
