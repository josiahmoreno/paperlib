using Tests;
using Attacks;
using System;

namespace Attributes
{
    public interface IHealth
    {
        int CurrentValue { get; }

        void TakeDamage( int damage);

        event EventHandler OnZero;

        event EventHandler<int> OnHealthChange;

        void Heal(int heal);
    }
}