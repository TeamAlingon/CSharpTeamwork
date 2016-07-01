namespace CSharpGame
{
    using System.Collections.Generic;

    using CSharpGame.Input;

    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Audio;
    using Microsoft.Xna.Framework.Graphics;
    using Microsoft.Xna.Framework.Input;
    using Models;
    using Models.Animations;
    using Models.Collectables.Effects;
    using Models.Collectables.Items;
    using MonoGame.Extended;
    using MonoGame.Extended.ViewportAdapters;

    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        private InputManager input;
        private Texture2D background;
        private Character mainCharacter;
        private RegularCoin regularCoin = new RegularCoin(400, 380);
        private List<RegularCoin> coins = new List<RegularCoin>();
        private SpeedUp speedUp=new SpeedUp(100,280, 20);
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
            this.input = new InputManager(this, this.camera);
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

            mainCharacter = new Character(mainCharacterAnimations, this.input);
            background = Content.Load<Texture2D>("Images/MapSample");

            regularCoin.ImageTexture2D = Content.Load<Texture2D>(regularCoin.GetImage());
            speedUp.ImageTexture2D = Content.Load<Texture2D>(speedUp.GetImage());

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

            if (Keyboard.GetState().IsKeyDown(Keys.Up) || Keyboard.GetState().IsKeyDown(Keys.W))
            {
                jumpInstance.Play();
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
            
            this.mainCharacter.Update(gameTime);
            

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
            this.mainCharacter.Draw(spriteBatch);
            this.speedUp.Draw(spriteBatch);

            foreach (var coin in coins)
            {
                coin.Draw(regularCoin, spriteBatch);

                if (regularCoin.Intersect(mainCharacter, coin))
                {
                    
                    this.mainCharacter.Collect(coin);
                    this.score = this.mainCharacter.Inventory.ScoreConins;
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