namespace CSharpGame.States
{
    using CSharpGame.Audio;
    using CSharpGame.Data;

    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    public sealed class PlayState : State
    {
        private GameStateManager gameStateManager;

        private GameRepository gameRepository;

        private PlayerAudioManager playerAudioManager;

        private SpriteFont font;

        public PlayState(GameStateManager gameStateManager, Game game)
            : base(game)
        {
            this.gameStateManager = gameStateManager;
            this.LoadContent();
        }

        public override void LoadContent()
        {
            this.font = this.Game.Content.Load<SpriteFont>("Score");
            this.gameRepository = new GameRepository(this.Game);
            this.playerAudioManager = new PlayerAudioManager(this.Game, this.gameRepository.Player);
        }

        public override void UnloadContent()
        {
            this.gameRepository = null;
            this.playerAudioManager.StopLevelTheme();
            this.playerAudioManager = null;
            this.font = null;
        }

        public override void Update(GameTime gameTime)
        {
            this.gameRepository.Update(gameTime);
            this.playerAudioManager.Update(gameTime);

            if (this.gameRepository.PlayerDied)
            {
                this.gameStateManager.Set(new GameOverState(this.gameStateManager, this.Game));
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            this.Game.GraphicsDevice.Clear(Color.Black);
            var transformMatrix = this.gameRepository.Camera.GetViewMatrix();
            spriteBatch.Begin(transformMatrix: transformMatrix);

            this.gameRepository.Draw(spriteBatch);

            spriteBatch.DrawString(this.font,
                $"SCORE: {this.gameRepository.Player.ScoreCoins}",
                new Vector2(this.gameRepository.Camera.Position.X + 5, this.gameRepository.Camera.Position.Y + 5),
                Color.Silver);

            spriteBatch.End();
        }
    }
}
