namespace CSharpGame.Models.Collectables.Effects
{
    using CSharpGame.Data;
    using CSharpGame.Models.Collectables.Items;

    using Interfaces;

    using Microsoft.Xna.Framework;

    public class Effect : Item, IEffect
    {
        private int timeOfEffect;

        public Effect(int x, int y, int duration, string image, GameRepository gameRepository)
            : base(x, y, image, gameRepository)
        {
            this.timeOfEffect = duration;
        }

        public override void Update(GameTime gameTime)
        {
            if (this.timeOfEffect <= 0)
            {
                //TODO: if duration == 0 stop effect and bring back default state
            }
            else
            {
                this.timeOfEffect -= 1;
            }
        }

        public virtual void ApplyEffect(Character player)
        {
            //TODO: apply effect to player
        }
    }
}