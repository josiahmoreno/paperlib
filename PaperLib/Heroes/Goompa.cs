namespace Heroes
{
    public class Goompa: Mario
    {
        public override Heroes? Identity { get; set; } = Heroes.Goompa;
        public Goompa()
        {
            Actions = new MenuData.IActionMenuData[0];
            
        }
    }
}