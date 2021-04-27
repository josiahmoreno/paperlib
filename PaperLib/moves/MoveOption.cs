using Battle;
using MenuData;
namespace Moves
{
    abstract class MoveOption : Option
    {
        public Moves.MovesList Move { get; private set; }

        public MoveOption(Moves.MovesList move, ITextBubbleSystem system, TargetType targetType) : base(new DefaultActionMenuStore(), system, targetType)
        {
            this.Move = move;
        }

        public MoveOption(Moves.MovesList move,  TargetType targetType) : base(new DefaultActionMenuStore(), null, targetType)
        {
            this.Move = move;
        }

        
    }
}