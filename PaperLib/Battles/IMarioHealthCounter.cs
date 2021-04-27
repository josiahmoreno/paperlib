using System;

namespace Battle
{
    public interface IMarioHealthCounter: IBattleView 
    {
    }

    public class BattleView: IBattleView
    {
        private bool _showing;
        public bool Showing { get => _showing; private set {
            _showing = value;
            OnShowing?.Invoke(_showing);
        }}

        public Action<bool> OnShowing { get; set; }
        public void Hide()
        {
            Showing = false ;
        }

        public void Show()
        {
            Showing = true;
        }
    }
    public interface IBattleView
    {
        bool Showing { get; }
        Action<bool> OnShowing { get; set; }
        void Hide();

        void Show();
    }
}