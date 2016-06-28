namespace CSharpGame
{
    using System;
    using System.Collections.Generic;

    using CSharpGame.Models;
    using CSharpGame.Models.Animations;

    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Audio;
    using Microsoft.Xna.Framework.Graphics;
    using Microsoft.Xna.Framework.Input;

    using MonoGame.Extended;
    using MonoGame.Extended.ViewportAdapters;

    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        private Texture2D background;
        private Character mainCharacter;
        private Models.Collectables.Items.RegularCoin regularCoin = new Models.Collectables.Items.RegularCoin(400, 380);
        private List<Models.Collectables.Items.RegularCoin> coins = new List<Models.Collectables.Items.RegularCoin>();
        private Camera2D camera;
        private SpriteFont font;
        private int score = 0;

        SoundEffect walkEffect;
        SoundEffectInstance walkInstance;
        SoundEffect levelTheme;
        SoundEffect jumpEffect;
        SoundEffectInstance jumpInstance;
        SoundEffect hitEffect;
        SoundEffectInstance hitInstance;

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

            var mainCharacterTexture = Content.Load<Texture2D>("Images/maincharacter");
            var mainCharacterSpriteData = LevelEditor.IO.File.ReadTextFile("maincharacter.spriteData");

            var mainCharacterAnimations = AnimationParser.ReadSpriteSheetData(
                mainCharacterTexture,
                mainCharacterSpriteData);

            mainCharacter = new Character(mainCharacterAnimations);
            background = Content.Load<Texture2D>("Images/MapSample");
            regularCoin.imageTexture = Content.Load<Texture2D>(regularCoin.GetImage());

            font = Content.Load<SpriteFont>("Score");

            walkEffect = Content.Load<SoundEffect>("Soundtrack/footstep_cut");
            walkInstance = walkEffect.CreateInstance();
            walkInstance.IsLooped = true;

            jumpEffect = Content.Load<SoundEffect>("Soundtrack/jump");
            jumpInstance = jumpEffect.CreateInstance();
            jumpInstance.IsLooped = false;

            //Hit effect for breaking or knocking enemies
            /*
            hitEffect = Content.Load<SoundEffect>("Soundtrack/scratch");
            hitInstance = hitEffect.CreateInstance();
            hitInstance.IsLooped = false;
            */

            levelTheme = Content.Load<SoundEffect>("Soundtrack/level");
            SoundEffectInstance levelThemeInstance = levelTheme.CreateInstance();
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
            this.camera.LookAt(this.mainCharacter.Position);

            var characterMovementParameters = new List<string>();
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Exit();
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Right) || Keyboard.GetState().IsKeyDown(Keys.D))
            {
                characterMovementParameters.Add("right");
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Left) || Keyboard.GetState().IsKeyDown(Keys.A))
            {
                characterMovementParameters.Add("left");
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Up) || Keyboard.GetState().IsKeyDown(Keys.W))
            {
                jumpInstance.Play();
                characterMovementParameters.Add("jump");
            }
            else if (Keyboard.GetState().IsKeyUp(Keys.Up) || Keyboard.GetState().IsKeyDown(Keys.W))
            {
                jumpInstance.Stop();
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Left) || Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                walkInstance.Play();
            }
            else if (Keyboard.GetState().IsKeyUp(Keys.Right) && Keyboard.GetState().IsKeyUp(Keys.Left))
            {
                walkInstance.Stop();
            }
            
            this.mainCharacter.Move(gameTime, characterMovementParameters.ToArray());
            
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            Vector2 origin = new Vector2(2, 3);
            var transformMatrix = camera.GetViewMatrix();
            spriteBatch.Begin(transformMatrix: transformMatrix);
            spriteBatch.Draw(
                this.background,
                new Rectangle(-500, -330, (int)(this.background.Width * 1.7), (int)(this.background.Height * 1.7)),
                Color.White);

            spriteBatch.Draw(
                this.mainCharacter.Texture,
                new Rectangle(this.mainCharacter.Position.ToPoint(), new Point(100, 150)),
                this.mainCharacter.CurrentFrame,
            Color.White,
                rotation: 0,
                origin: new Vector2(),
                effects: mainCharacter.Orientation,
                layerDepth: 0f);
            foreach (var coin in coins)
            {
                if(!coin.isCollected)
                spriteBatch.Draw(regularCoin.imageTexture, new Rectangle(coin.X, coin.Y, 80, 80), Color.White);
                if (regularCoin.Intersect(mainCharacter, coin, spriteBatch))
                {
                    
                    this.mainCharacter.Collect(coin);
                    this.score = this.mainCharacter.Inventory.Coins.Count;
                    Console.WriteLine("Collected");
                }
            }

            spriteBatch.DrawString(
                font,
                $"SCORE: {score}",
                new Vector2(-390 + this.mainCharacter.Position.X, 120),
                Color.Silver);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}