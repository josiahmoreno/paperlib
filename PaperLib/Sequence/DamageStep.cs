using Attacks;
using Enemies;
using Heroes;
using PaperLib.Sequence;
using System;

namespace PaperLib.Sequence
{
    internal class DamageStep : ISequenceStep
    {
        private readonly ILogger logger;

        public IAttack Attack { get; }

        private readonly IEntity hero;
        private readonly IEntity target;
        private readonly Func<bool> successfulQuicktime;
        private IDamageTarget damageTarget;
        public DamageStep(ILogger logger, IDamageTarget damageTarget)
        {
            this.logger = logger;
            this.damageTarget = damageTarget ?? throw new NullReferenceException("damage target null");
            this.Attack = damageTarget.Attack ?? throw new NullReferenceException("attack is null");
            this.hero = damageTarget.hero ?? throw new NullReferenceException("hero is null");
            this.target = damageTarget.target ?? throw new NullReferenceException("target is null");
            this.successfulQuicktime = damageTarget.successfulQuicktime ?? throw new NullReferenceException("successfulQuicktime is null");
        }

        public event EventHandler OnComplete;

        public void Start(ISequenceable sequenceable)
        {
            logger?.Log(this+ $" attack = { Attack}, hero {hero}, enemy {target}");
            bool attWasSuc = hero.Attack(Attack, target, successfulQuicktime());
            OnComplete?.Invoke(this, EventArgs.Empty);

        }
    }
}