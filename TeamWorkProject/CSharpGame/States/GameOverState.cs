namespace CSharpGame.States
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using Microsoft.Xna.Framework.Input;

    public sealed class GameOverState : State
    {
        private GameStateManager gameStateManager;

        private Texture2D gameoverImage;

        private Vector2 gameOverImagePosition;

        public GameOverState(GameStateManager gameStateManager, Game game)
            : base(game)
        {
            this.gameStateManager = gameStateManager;
            this.LoadContent();
        }

        public override void LoadContent()
        {
            this.gameoverImage = this.Game.Content.Load<Texture2D>("Images/GameOver");
            this.gameOverImagePosition = new Vector2(160, 100);
        }

        public override void UnloadContent()
        {
            this.gameoverImage = null;
            this.gameOverImagePosition = Vector2.Zero;
        }

        public override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                this.Game.Exit();
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Enter))
            {
                this.gameStateManager.Set(new PlayState(this.gameStateManager, this.Game));
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(this.gameoverImage, this.gameOverImagePosition);
            spriteBatch.End();
        }
    }
}