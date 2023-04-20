
using UnityEngine;

namespace Assets.Scripts.Environment
{
    public class IdeaBubble : MonoBehaviour
    {
        public Sprite Sprite 
        { 
            set 
            {
                _childSpriteRenderer.sprite = value;
            } 
        }
        [SerializeField]
        private SpriteRenderer _childSpriteRenderer;
        [SerializeField]
        private float _showDuration;
        private float _timeTillDisable;

        protected virtual void Awake()
        {
            Sprite = null;
        }

        protected virtual void Update()
        {
            _timeTillDisable -= Time.deltaTime;

            if (_timeTillDisable <= 0)
            {
                gameObject.SetActive(false);
            }
        }

        protected virtual void OnEnable() 
        {
            _timeTillDisable = _showDuration;
        }
    }
}
