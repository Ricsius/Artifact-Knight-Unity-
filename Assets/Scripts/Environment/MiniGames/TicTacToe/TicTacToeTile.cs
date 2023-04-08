using Assets.Scripts.Detectors;
using Assets.Scripts.Items.Equipable;
using System;
using UnityEngine;

namespace Assets.Scripts.Environment.MiniGames.TicTacToe
{
    public class TicTacToeTile : MonoBehaviour
    {
        [SerializeField]
        public Vector2 Position { get; set; }
        public Sprite Sprite
        {
            set
            {
                _spriteRenderer.sprite = value;
            }
        }
        public int MainGamePlayerID
        {
            set
            {
                _mainGamePlayerID = value;
            }
        }
        public EventHandler<TileMarkEventArgs> MarkRequested;
        private SpriteRenderer _spriteRenderer;
        private int _mainGamePlayerID;

        protected virtual void Awake()
        {
            _spriteRenderer = transform.GetChild(0).GetComponent<SpriteRenderer>();
        }

        protected virtual void OnTriggerEnter2D(Collider2D collider)
        {
            EquipableItem item = collider.GetComponent<EquipableItem>();

            if (item != null && SpecialGameObjectRecognition.IsPlayer(item.Owner))
            {
                MarkRequest(_mainGamePlayerID);
            }
        }

        public void MarkRequest(int playerID)
        {
            MarkRequested?.Invoke(this, new TileMarkEventArgs(playerID));
        }
    }
}
