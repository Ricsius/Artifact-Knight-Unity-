
namespace Assets.Scripts.Items
{
    public class BonusItem : ItemBase
    {
        protected override void Awake()
        {
            base.Awake();

            Type = ItemType.Bonus;
        }
    }
}
