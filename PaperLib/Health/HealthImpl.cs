using System;
using System.Collections.Generic;
using System.Text;
using Attacks;

namespace Attributes
{
    class HealthImpl : IHealth
    {

     
        public int Max { get; internal set; }
        public int CurrentValue { get; internal set; }


        public HealthImpl(int max)
        {
            this.CurrentValue = max;
            this.Max = max;
        }
        public HealthImpl(int currentValue,int max)
        {
            this.CurrentValue = currentValue;
            this.Max = max;
        }
        private event EventHandler _onZero;
        event EventHandler IHealth.OnZero
        {
            add
            {
                _onZero += value ;
            }

            remove
            {
                _onZero -= value;
            }
        }

        public event EventHandler<int> OnHealthChange;


        public void TakeDamage(int damage)
        {
            CurrentValue -= damage;
            if(CurrentValue <= 0)
            {
                CurrentValue = 0;
                OnHealthChange?.Invoke(this,CurrentValue);
                _onZero?.Invoke(this,null);
            }
            else
            {
                OnHealthChange?.Invoke(this,CurrentValue);
            }
        }

        public void Heal(int heal)
        {
            CurrentValue += heal;
        }
    }


}
