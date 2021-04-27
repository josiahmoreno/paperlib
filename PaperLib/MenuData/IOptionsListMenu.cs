using System;

namespace MenuData
{
    public interface IOptionsListMenu
    {
        bool Showing { get; }

        IOption[] Items { get; }

        event EventHandler<IOption> OnActiveChanged;
        IOption Active { get; }

        void Show(IOption[] options, int? selected = null);
        void MoveTargetDown();
        void Hide();
        void MoveTargetUp();
        
        Action<bool> OnShowing { get ; set ; }
    }
}