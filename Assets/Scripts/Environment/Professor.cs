using Assets.Scripts.Items;
using Assets.Scripts.Systems.Equipment;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Environment
{
    public class Professor : MonoBehaviour
    {
        [field: SerializeField]
        public ItemBase RequiredItem { get; private set; }

        protected virtual void Awake()
        {
            tag = "Professor";
        }

        public void CheckEquipment(EquipmentSystem equipment)
        {
            bool hasRequiredItem = equipment.ContainsItem(RequiredItem.gameObject);

            if (hasRequiredItem)
            {
                int nextIndex = SceneManager.GetActiveScene().buildIndex + 1;
                int index = SceneManager.sceneCount > nextIndex ? nextIndex : 0;

                //ToDo: Score summary board.
                //ToDo: Loading screen for the levels.

                SceneManager.LoadScene(index);
            }
            else
            {
                //ToDo: Remind player to the required item
            }
        }
    }
}
