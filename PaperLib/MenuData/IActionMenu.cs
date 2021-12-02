using Battle;
using System;

namespace MenuData
{
    public interface IActionMenu: IMenu, IHasMoveableTarget
    {
        bool Showing { get;  }
        event EventHandler<IActionMenuData> OnActiveActionChanged;
        MenuData.IActionMenuData ActiveAction { get; }
        int SelectedIndex { get; }
        IActionMenuData[] Items { get;  }
        event EventHandler<bool> OnHide;


        void Hide();

        void Process();
        void Show();
    }
}