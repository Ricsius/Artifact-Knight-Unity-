
using Assets.Scripts.Items.Equipable;
using System;
using UnityEngine;

namespace Assets.Scripts.Environment.MiniGames.TicTacToe
{
    public class TicTacToeTile : MonoBehaviour
    {
        [SerializeField]
        public Position Position { get; set; }
        public Sprite Sprite
        {
            set
            {
                _spriteRenderer.sprite = value;
            }
        }

        public event EventHandler<TileMarkEventArgs> MarkRequested;
        private SpriteRenderer _spriteRenderer;

        protected virtual void Awake()
        {
            _spriteRenderer = transform.GetChild(0).GetComponent<SpriteRenderer>();
        }

        protected virtual void OnTriggerEnter2D(Collider2D collider)
        {
            EquipableItem item = collider.GetComponent<EquipableItem>();
            
            if (item != null)
            {
                MarkRequest(item.Owner);
            }
        }

        public void MarkRequest(GameObject player)
        {
            MarkRequested?.Invoke(this, new TileMarkEventArgs(player));
        }
    }
}
