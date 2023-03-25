using Assets.Scripts.Items.Key;
using UnityEngine;

namespace Assets.Scripts.Environment
{
    public class Door : MonoBehaviour
    {
        [field: SerializeField]
        public KeyType KeyTypeToOpen { get; private set; }

        protected virtual void Awake()
        {
            tag = "Door";
        }

        public bool TryOpen(KeyItem key)
        {
            bool ret = key.KeyType == KeyTypeToOpen;

            if (ret)
            {
                Destroy(gameObject);
            }

            return ret;
        }
    }
}
