using MenuData;
using System;

namespace TargetSystem
{
    public interface ITargetSystem
    {
        void Show(IOption active);
        Enemies.Enemy[] Actives { get; }
        bool Showing { get; }
        Action<bool> OnShowing { get; set; }

        Action<Enemies.Enemy[]> ActiveChanged { get; set; }
        int SelectedIndex { get; }
        void MoveTargetLeft();
        void Hide();
        void MoveTargetRight();
        void Cleanup();
    }
}