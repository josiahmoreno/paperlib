using Heroes;
using Moves;

namespace Heroes
{
    public class Goombario : Mario
    {
        public override Heroes? Identity { get; set; } = Heroes.Goombario;
        public Goombario(Battle.ITextBubbleSystem textBubbleSystem)
        {
            Actions = new MenuData.IActionMenuData[2];
            Actions[0] = new MenuData.ActionMenuData("Flag", new MenuData.Option("Run Away"));
            Actions[1] = new MenuData.ActionMenuData("Abilities", new HeadbonkOption(),new Tattle( textBubbleSystem));
        }

        public Goombario()
        {
            Actions = new MenuData.IActionMenuData[2];
            Actions[0] = new MenuData.ActionMenuData("Flag", new MenuData.Option("Run Away"));
            Actions[1] = new MenuData.ActionMenuData("Abilities", new HeadbonkOption(), new Tattle());
        }

        public override string ToString()
        {
            return "Goombario";
        }
    }
}