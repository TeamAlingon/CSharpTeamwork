namespace CSharpGame.Models.Collectables.Effects
{
    using CSharpGame.Data;
    using CSharpGame.Models.Collectables.Items;

    using Interfaces;

    using Microsoft.Xna.Framework;

    public abstract class Effect : Item, IEffect
    {
        private bool effectIsApplied;

        public Effect(int x, int y, int duration, string image, GameRepository gameRepository)
            : base(x, y, image, gameRepository)
        {
            this.Duration = duration;
            this.effectIsApplied = false;
            this.EffectIsOver = false;
        }

        public float Duration { get; private set; }

        public bool EffectIsOver { get; private set; }

        public override void Update(GameTime gameTime)
        {
            this.Duration -= (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (!this.effectIsApplied && this.Collector != null)
            {
                this.effectIsApplied = true;
                this.ApplyEffect();
            }

            if (this.Duration <= 0 && !this.HasBeenUsed)
            {
                this.HasBeenUsed = true;
                this.RemoveEffect();
            }
        }

        public abstract void RemoveEffect();

        public abstract void ApplyEffect();
    }
}