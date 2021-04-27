using Attacks;
using Battle;
using Heroes;
using MenuData;
namespace Items
{
    public class Item: IItem
    {
        private TargetType all;



        public Item(string name, int? damage = null, TargetType all = default(TargetType)) 
        {
            this.Name = name;
            this.Damage = damage;
            this.TargetType = all;
        }

        public string Name { get; private set; }

        public int? Damage { get; private set; }

        public TargetType TargetType { get; private set; }
    }
}