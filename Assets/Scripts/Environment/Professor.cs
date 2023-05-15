
using Assets.Scripts.Items;
using Assets.Scripts.Systems.Equipment;
using Assets.Scripts.Systems.Score;
using UnityEngine;


namespace Assets.Scripts.Environment
{
    public class Professor : MonoBehaviour
    {
        [field: SerializeField]
        private ItemBase _requiredItem;
        private IdeaBubble _ideaBubble;
        private SceneLoader _sceneLoader;

        protected virtual void Awake()
        {
            tag = "Professor";
            _ideaBubble = GetComponentInChildren<IdeaBubble>();
            _ideaBubble.Sprite = _requiredItem.GetComponent<SpriteRenderer>().sprite;
            _sceneLoader = GetComponent<SceneLoader>();

            _ideaBubble.gameObject.SetActive(false);
        }

        public void CheckEquipment(EquipmentSystem equipment)
        {
            bool hasRequiredItem = equipment.ContainsItem(_requiredItem);

            if (hasRequiredItem)
            {
                ScoreSystem score = equipment.GetComponent<ScoreSystem>();

                _sceneLoader.LoadNextScene(score);

                equipment.gameObject.SetActive(false);
            }
            else
            {
                _ideaBubble.gameObject.SetActive(true);
            }
        }
    }
}
