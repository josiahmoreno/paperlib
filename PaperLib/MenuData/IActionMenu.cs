using System;

namespace MenuData
{
    public interface IActionMenu
    {
        bool Showing { get;  }
        event EventHandler<IActionMenuData> OnActiveActionChanged;
        MenuData.IActionMenuData ActiveAction { get; }
        int SelectedIndex { get; }
        IActionMenuData[] Items { get;  }
        Action<bool> OnHide { get; set; }

        void MoveTargetUp();
        void Hide();
        void MoveTargetDown();
        void Start();
        void Process();
        void Show();
    }
}