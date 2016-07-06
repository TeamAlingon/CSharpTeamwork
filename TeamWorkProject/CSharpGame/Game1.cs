namespace CSharpGame
{
    using CSharpGame.Audio;
    using CSharpGame.Data;

    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    public class Game1 : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        private GameRepository gameRepository;
        private PlayerAudioManager playerAudioManager;
        SpriteFont font;

        public Game1()
        {
            this.graphics = new GraphicsDeviceManager(this);
            this.Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            this.gameRepository = new GameRepository(this);
            this.playerAudioManager = new PlayerAudioManager(this, this.gameRepository.Player);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            this.spriteBatch = new SpriteBatch(this.GraphicsDevice);

            this.font = this.Content.Load<SpriteFont>("Score");
        }

        protected override void UnloadContent()
        {
            this.Content.Dispose();
        }

        protected override void Update(GameTime gameTime)
        {
            this.gameRepository.Update(gameTime);
            this.playerAudioManager.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            this.GraphicsDevice.Clear(Color.Black);
            var transformMatrix = this.gameRepository.Camera.GetViewMatrix();
            this.spriteBatch.Begin(transformMatrix: transformMatrix);

            this.gameRepository.Draw(this.spriteBatch);

            this.spriteBatch.DrawString(this.font,
                $"SCORE: {this.gameRepository.Player.ScoreCoins}",
                new Vector2(this.gameRepository.Camera.Position.X + 5, this.gameRepository.Camera.Position.Y + 5),
                Color.Silver);

            this.spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}