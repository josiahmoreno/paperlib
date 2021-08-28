using Attacks;
using PaperLib.Sequence;
using Tests;

namespace Heroes
{
    public interface IEntity
    {
        ISequenceable Sequenceable { get; }
        bool TakeDamage(IAttack enemyAttack, IProtection protection,bool successfulActionCommand = false);
        bool Attack(IAttack attack, IEntity target, bool succ);
    }
}