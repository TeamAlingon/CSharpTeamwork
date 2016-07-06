namespace CSharpGame.Input
{
    using System;

    using Microsoft.Xna.Framework;

    public class EnemyInput : CharacterInput
    {
        private const int DirectionSwitchTimeMin = 3;

        private const int DirectionSwitchTimeMax = 8;

        private static readonly Random Rng = new Random();

        public float CurrentSwitchTime { get; private set; }

        public bool MovingRight { get; private set; }

        public EnemyInput(Game game)
            : base(game)
        {
            this.Game.Components.Add(this);
            this.CurrentSwitchTime = Rng.Next(DirectionSwitchTimeMin, DirectionSwitchTimeMax);
            this.MovingRight = false;
        }

        public override void Update(GameTime gameTime)
        {
            if (this.CurrentSwitchTime < 0)
            {
                this.MovingRight = !this.MovingRight;
                this.CurrentSwitchTime = Rng.Next(DirectionSwitchTimeMin, DirectionSwitchTimeMax);
            }

            if (this.MovingRight)
            {
                this.InvokeMoveRight();
            }
            else
            {
                this.InvokeMoveLeft();
            }

            this.CurrentSwitchTime -= (float)gameTime.ElapsedGameTime.TotalSeconds;
        }
    }
}
