namespace CSharpGame.Input
{
    using Microsoft.Xna.Framework;

    public class EnemyInput : CharacterInput
    {
        private const float DirectionSwitchTime = 5;

        public float CurrentSwitchTime { get; private set; }

        public bool MovingRight { get; private set; }

        public EnemyInput(Game game)
            : base(game)
        {
            this.Game.Components.Add(this);
            this.CurrentSwitchTime = DirectionSwitchTime;
            this.MovingRight = false;
        }

        public override void Update(GameTime gameTime)
        {
            if (this.CurrentSwitchTime > DirectionSwitchTime)
            {
                this.MovingRight = !this.MovingRight;
                this.CurrentSwitchTime = 0;
            }

            if (this.MovingRight)
            {
                this.InvokeMoveRight();
            }
            else
            {
                this.InvokeMoveLeft();
            }

            this.CurrentSwitchTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
        }
    }
}
