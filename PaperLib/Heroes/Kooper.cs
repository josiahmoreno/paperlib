using Heroes;
using MenuData;

namespace Heroes
{
    public class Kooper: Mario
    {

        public override Heroes? Identity { get; set; } = Heroes.Goombario;


        public Kooper()
        {
            Actions = new MenuData.IActionMenuData[2];
            Actions[0] = new MenuData.ActionMenuData("Flag", new MenuData.Option("Run Away"));
            Actions[1] = new MenuData.ActionMenuData("Abilities", 
                new AttackOption("Shell Toss", new ShellToss(),TargetType.Single), 
                new AttackOption("Power Shell", new PowerShell(),TargetType.All));
        }

        public override string ToString()
        {
            return "Kooper";
        }
    }
}