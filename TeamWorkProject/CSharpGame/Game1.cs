using CSharpGame.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using MonoGame.Extended.ViewportAdapters;
using System.Collections.Generic;

namespace CSharpGame
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        private Texture2D background;
        private Texture2D mainCharacterTexture;
        private Character mainCharacter = new Character();
        private Models.Collectables.Items.RegularCoin regularCoin = new Models.Collectables.Items.RegularCoin(400, 380);
        private List<Models.Collectables.Items.RegularCoin> coins = new List<Models.Collectables.Items.RegularCoin>();
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
            regularCoin.InitializeList(coins);
            camera = new Camera2D(viewPortAdapter);
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            mainCharacterTexture = Content.Load<Texture2D>(mainCharacter.GetImage());
            background = Content.Load<Texture2D>("Images/MapSample");
            regularCoin.imageTexture = Content.Load<Texture2D>(regularCoin.GetImage());
            // Load font to print the scores
            //font = Content.Load<SpriteFont>("Score");

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

            if (mainCharacter.Y < 380)
            {
                this.mainCharacter.Y += 5;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Exit();
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Right) ||
                Keyboard.GetState().IsKeyDown(Keys.D))
            {
                this.mainCharacter.MoveRight();
                mainCharacter.Orientation = SpriteEffects.None;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Left) ||
                Keyboard.GetState().IsKeyDown(Keys.A))
            {
                this.mainCharacter.MoveLeft();
                mainCharacter.Orientation = SpriteEffects.FlipHorizontally;
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
            Vector2 origin = new Vector2(2, 3);
            var transformMatrix = camera.GetViewMatrix();
            spriteBatch.Begin(transformMatrix: transformMatrix);
            this.spriteBatch.Draw(this.background, Vector2.Zero);
            spriteBatch.Draw(
                mainCharacterTexture,
                new Rectangle(mainCharacter.X, mainCharacter.Y, 100, 100),
                new Rectangle(0, 0, 160, 300),
                Color.White,
                rotation: 0,
                origin: new Vector2(),
               effects: mainCharacter.Orientation,
               layerDepth: 0f);
            foreach (var coin in coins)
            {
                if (regularCoin.Intersect(mainCharacter, coin, spriteBatch))
                {
                    spriteBatch.Draw(regularCoin.imageTexture, new Rectangle(coin.X, coin.Y, 80, 80), Color.White);
                }
            }

            // spriteBatch.DrawString(font, $"SCORE: {score}", new Vector2(10, 10), Color.Silver);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}