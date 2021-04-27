using System;
using System.Collections.Generic;
using System.Linq;
using Heroes;
using Tests;

namespace MenuData
{
    internal class DefaultActionMenu : IActionMenu
    {
        private ITurnSystem turnSystem;
        public int SelectedIndex { get; private set; } = 2;
        private Hero CurrentHero;
        public IActionMenuData[] Items { get; private set; }
        private Dictionary<object, IActionMenuData> history = new Dictionary<object, IActionMenuData>();
        private bool showing_;
        public bool Showing { get => showing_; private set {
                showing_ = value;
                OnHide?.Invoke(showing_);
            }
        }

        public event EventHandler<IActionMenuData> OnActiveActionChanged;

        private IActionMenuData _activeAction;

        public IActionMenuData ActiveAction
        {
            get => _activeAction;
            private set
            {
                _activeAction = value;
                OnActiveActionChanged?.Invoke(this,value);
            }
        }

        public Action<bool> OnHide { get ; set ; }
        public DefaultActionMenu(ITurnSystem turnSystem)
        {
            Console.WriteLine($"{GetType().Name} -  init");
            this.turnSystem = turnSystem;
            OnActiveChanged(turnSystem.Active);
            this.turnSystem.OnActiveChanged += OnActiveChanged;
            
        }

        private void OnActiveChanged(object obj)
        {
            Console.WriteLine($"{GetType().Name} - 1OnActiveChanged  {obj}");
           

            Console.WriteLine($"{GetType().Name} - 2OnActiveChanged STARTED  {obj?.ToString()} - {(Showing ? "Showing" : "Hidden" )}, SelectedIndex {SelectedIndex} ");
            if (obj is Hero)
            {

                var change = CurrentHero != obj;
                CurrentHero = obj as Hero;
                Console.WriteLine($"{GetType().Name} - 3OnActiveChanged  {CurrentHero} {CurrentHero.Actions.Length} ");
                Items = CurrentHero.Actions;
                if (history.ContainsKey(obj))
                {
                    SelectedIndex = Items.ToList().IndexOf(history[obj]);
                }
                else if (SelectedIndex >= Items.Length)
                {
                    SelectedIndex = Items.Length - 1;
                }
                else if(Items.Length < 3)
                {
                    SelectedIndex = Items.Length - 1;
                }

                SetActiveAction(obj,SelectedIndex);
                //ActiveAction = Items[SelectedIndex];
                //history[obj] = ActiveAction;
            } else
            {
                CurrentHero = null;
            }
            Console.WriteLine($"{GetType().Name} - 4OnActiveChanged ENDED {obj} - {(Showing ? "Showing" : "Hidden")}, SelectedIndex {SelectedIndex}");
        }

        public void Start()
        {
           Showing = CurrentHero is Hero;
        }

        private void SetActiveAction(object obj, int selectedIndex)
        {
            if (obj == null)
            {
                throw new NullReferenceException("active action cant be set on a non-hero");
            }
            Console.WriteLine($"{GetType().Name} - SetActiveAction Items.Length {Items.Length} selectedIndex {selectedIndex}");
            
                if (selectedIndex > Items.Length)
                {
                    throw new IndexOutOfRangeException($"333selected index {{{selectedIndex}}} is out of bounds of items {{{Items.Length}}}");
                }

                if (selectedIndex < 0)
                {
                    throw new IndexOutOfRangeException($"333selected index {{{selectedIndex}}} is out of bounds of items {{{Items.Length}}}");
                }
               
            
            Console.WriteLine($"{GetType().Name} - SetActiveAction {Items.Length} {Items[selectedIndex].Name}");
            ActiveAction = Items[selectedIndex];
            history[obj] = ActiveAction;
        }

        public void MoveTargetUp()
        {
            if (SelectedIndex - 1 >= 0)
            {
                SelectedIndex -= 1;
                SetActiveAction(CurrentHero, SelectedIndex);
            }
        }

        public void MoveTargetDown()
        {
            if (SelectedIndex + 1 < Items.Length)
            {
                SelectedIndex += 1;
                SetActiveAction(CurrentHero, SelectedIndex);
            }
        }

        public void Hide()
        {
            Console.WriteLine("Action Menu Hidden");
            Showing = false;
           
        }

        public void Process()
        {
            Console.WriteLine($"{GetType().Name} - Process {CurrentHero}");
            Showing = CurrentHero is Hero;
        }

        public void Show()
        {
            Showing = CurrentHero is Hero;
        }

        
    }
}