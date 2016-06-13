using CSharpGame.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using MonoGame.Extended.ViewportAdapters;

namespace CSharpGame
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        private Texture2D background;
        private Texture2D mainCharacterTexture;
        private Character mainCharacter = new Character();
        private Camera2D camera;

        private SpriteFont font;
        private int score = 0;

        SoundEffect walkEffect;
        SoundEffectInstance walkInstance;
        SoundEffect levelTheme;

        //Character character = new Character();
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }
        
        protected override void Initialize()
        {
            var viewPortAdapter = new BoxingViewportAdapter(Window, GraphicsDevice, 800, 480);
            camera = new Camera2D(viewPortAdapter);
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            mainCharacterTexture = Content.Load<Texture2D>(mainCharacter.GetImage());
            background = Content.Load<Texture2D>("Images/MapSample");
            // Load font to print the scores
            font = Content.Load<SpriteFont>("Score");
            
            walkEffect = Content.Load<SoundEffect>("Soundtrack/footstep_cut");
            levelTheme = Content.Load<SoundEffect>("Soundtrack/level");
            walkInstance = walkEffect.CreateInstance();
            walkInstance.IsLooped = true;

            SoundEffectInstance levelThemeInstance = levelTheme.CreateInstance();
            levelThemeInstance.IsLooped = true;
            levelThemeInstance.Volume = 0.1f;
            levelThemeInstance.Play();
        }

        protected override void UnloadContent()
        {

        }

        protected override void Update(GameTime gameTime)
        {
            this.camera.LookAt(new Vector2(this.mainCharacter.X, this.mainCharacter.Y));
            
            if (mainCharacter.Y < 350)
            {
                this.mainCharacter.Y+= 5;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Exit();
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Right) ||
                Keyboard.GetState().IsKeyDown(Keys.D))
            {
                this.mainCharacter.MoveRight();
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Left) ||
                Keyboard.GetState().IsKeyDown(Keys.A))
            {
                this.mainCharacter.MoveLeft();
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Up) ||
                Keyboard.GetState().IsKeyDown(Keys.W))
            {
                mainCharacter.Jump();
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                mainCharacter.Y++;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Down) || Keyboard.GetState().IsKeyDown(Keys.Up) ||
                Keyboard.GetState().IsKeyDown(Keys.Left) || Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                walkInstance.Play();
            }
            else if (Keyboard.GetState().IsKeyUp(Keys.Up) && Keyboard.GetState().IsKeyUp(Keys.Down) &&
                     Keyboard.GetState().IsKeyUp(Keys.Right) && Keyboard.GetState().IsKeyUp(Keys.Left))
            {
                walkInstance.Stop();

            }

            base.Update(gameTime);
        }
        
        protected override void Draw(GameTime gameTime)
        {
            var transformMatrix = camera.GetViewMatrix();
            spriteBatch.Begin(transformMatrix: transformMatrix);
            this.spriteBatch.Draw(this.background, Vector2.Zero);
            spriteBatch.Draw(mainCharacterTexture,new Rectangle(
                mainCharacter.X, mainCharacter.Y, 125,125), color:Color.White);
            spriteBatch.DrawString(font, $"SCORE: {score}", new Vector2(10, 10), Color.Silver);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
