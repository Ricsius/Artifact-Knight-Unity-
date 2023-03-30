using Assets.Scripts.Items;
using Assets.Scripts.Systems.Equipment;
using Assets.Scripts.Systems.Score;
using UnityEngine;


namespace Assets.Scripts.Environment
{
    public class Professor : MonoBehaviour
    {
        [field: SerializeField]
        public ItemBase RequiredItem { get; private set; }
        private SceneLoader _sceneLoader;

        protected virtual void Awake()
        {
            tag = "Professor";
            _sceneLoader = GetComponent<SceneLoader>();
        }

        public void CheckEquipment(EquipmentSystem equipment)
        {
            bool hasRequiredItem = equipment.ContainsItem(RequiredItem);

            if (hasRequiredItem)
            {
                ScoreSystem score = equipment.GetComponent<ScoreSystem>();

                _sceneLoader.LoadNextScene(score);
                
            }
            else
            {
                //ToDo: Remind player to the required item
            }
        }
    }
}
