using System;

namespace MenuData
{
    internal class DefaultOptionsListMenu : IOptionsListMenu
    {
        public IOption[] Items { get; internal set; }

        private int selectedIndex;

        public event EventHandler<IOption> OnActiveChanged;
        private IOption _active;
        public IOption Active
        {
            get => _active;
            private set
            {
                _active = value;
                OnActiveChanged?.Invoke(this,value);
            }
        }

        private bool showing_;
        public bool Showing
        { get => showing_;
        private set
            {
                showing_ = value;
                if (!value)
                {
                    
                    Active = null;
                } 
                OnShowing?.Invoke(showing_);
            }
        }

        public void MoveTargetDown()
        {
            if(selectedIndex != Items.Length - 1)
            {
                this.Active = Items[++selectedIndex];
            }
           
        }

        public void Show(IOption[] options, int? selected = null)
        {
            if (options.Length == 0)
            {
                throw new ArgumentException("needs to have at least some options");
            }
            this.Items = options;
            
            if (options != null && options.Length > 0)
            {

                if (selected == null)
                {
                    selectedIndex = 0;
                } else
                {
                    selectedIndex = (int) selected;
                }
               
                this.Active = options[selectedIndex];
            }
            Showing = true;
        }

        public void Hide()
        {
            Showing = false;
        }

        public void MoveTargetUp()
        {
            if(selectedIndex != 0)
            {
                this.Active = Items[--selectedIndex];

            }
        }

        public Action<bool> OnShowing { get; set; }
    }
}