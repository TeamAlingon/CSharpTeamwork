namespace CSharpGame
{
    using CSharpGame.Data;

    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Audio;
    using Microsoft.Xna.Framework.Graphics;
    using Microsoft.Xna.Framework.Input;

    using MonoGame.Extended.ViewportAdapters;

    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        private GameRepository gameRepository;
        SpriteFont font;

        SoundEffect walkEffect;
        SoundEffectInstance walkInstance;
        SoundEffect levelTheme;
        SoundEffect jumpEffect;
        SoundEffectInstance jumpInstance;
        SoundEffect hitEffect;
        SoundEffectInstance hitInstance;

        public Game1()
        {
            this.graphics = new GraphicsDeviceManager(this);
            this.Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            this.gameRepository = new GameRepository(this);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            this.spriteBatch = new SpriteBatch(this.GraphicsDevice);

            this.font = this.Content.Load<SpriteFont>("Score");

            this.walkEffect = this.Content.Load<SoundEffect>("Soundtrack/footstep_cut");
            this.walkInstance = this.walkEffect.CreateInstance();
            this.walkInstance.IsLooped = true;

            this.jumpEffect = this.Content.Load<SoundEffect>("Soundtrack/jump");
            this.jumpInstance = this.jumpEffect.CreateInstance();
            this.jumpInstance.IsLooped = false;

            //Hit effect for breaking or knocking enemies
            /*
            hitEffect = Content.Load<SoundEffect>("Soundtrack/scratch");
            hitInstance = hitEffect.CreateInstance();
            hitInstance.IsLooped = false;
            */

            this.levelTheme = this.Content.Load<SoundEffect>("Soundtrack/level");
            SoundEffectInstance levelThemeInstance = this.levelTheme.CreateInstance();
            levelThemeInstance.IsLooped = true;
            levelThemeInstance.Volume = 0.1f;
            levelThemeInstance.Play();
        }

        protected override void UnloadContent()
        {
            this.Content.Dispose();
        }

        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Up) || Keyboard.GetState().IsKeyDown(Keys.W))
            {
                this.jumpInstance.Play();
            }
            else if (Keyboard.GetState().IsKeyUp(Keys.Up) || Keyboard.GetState().IsKeyDown(Keys.W))
            {
                this.jumpInstance.Stop();
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Left) || Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                this.walkInstance.Play();
            }
            else if (Keyboard.GetState().IsKeyUp(Keys.Right) && Keyboard.GetState().IsKeyUp(Keys.Left))
            {
                this.walkInstance.Stop();
            }

            this.gameRepository.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            this.GraphicsDevice.Clear(Color.Black);
            var transformMatrix = this.gameRepository.Camera.GetViewMatrix();
            this.spriteBatch.Begin(transformMatrix: transformMatrix);

            this.gameRepository.Draw(gameTime, this.spriteBatch);

            this.spriteBatch.DrawString(this.font,
                $"SCORE: {this.gameRepository.MainCharacter.ScoreCoins}",
                new Vector2(this.gameRepository.Camera.Position.X + 5, this.gameRepository.Camera.Position.Y + 5),
                Color.Silver);

            this.spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}