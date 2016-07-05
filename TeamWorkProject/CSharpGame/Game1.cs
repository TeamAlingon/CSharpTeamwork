namespace CSharpGame
{
    using System.Collections.Generic;

    using CSharpGame.Input;
    using Interfaces;
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
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private InputManager input;
        private Texture2D background;
        private Character mainCharacter;
        private RegularCoin regularCoin;
        private List<ICollectable> coins;
        private Enemy enemy;
        private List<Enemy> enemys;
        private SpeedUp speedUp;
        private Camera2D camera;
        private SpriteFont font;
        private CollisionHandler.CollisionHandler collisionHandler;
        private SoundEffect walkEffect;
        private SoundEffectInstance walkInstance;
        private SoundEffect levelTheme;
        private SoundEffect jumpEffect;
        private SoundEffectInstance jumpInstance;
        private SoundEffect hitEffect;
        private SoundEffectInstance hitInstance;

     
        public Game1()
        {
            this.graphics = new GraphicsDeviceManager(this);
            this.Content.RootDirectory = "Content";
            this.regularCoin = new RegularCoin(400, 380);
            this.enemy = new Enemy();
            this.enemys = new List<Enemy>();
            this.speedUp = new SpeedUp(200, 280, 20);
        }

        protected override void Initialize()
        {
            Enemy.InitializeEnemies(this.enemys);
            var viewPortAdapter = new BoxingViewportAdapter(this.Window, this.GraphicsDevice, 800, 480);
            this.regularCoin = new RegularCoin(400, 380);
            this.coins = new List<ICollectable>();
            this.regularCoin.InitializeList(this.coins);
            this.coins.Add(this.speedUp);
            this.camera = new Camera2D(viewPortAdapter);
            this.input = new InputManager(this, this.camera);
            this.collisionHandler = new CollisionHandler.CollisionHandler();
            
            base.Initialize();
        }

        protected override void LoadContent()
        {
            this.spriteBatch = new SpriteBatch(this.GraphicsDevice);

            var mainCharacterTexture = this.Content.Load<Texture2D>("Images/maincharacter");
            var mainCharacterSpriteData = LevelEditor.IO.File.ReadTextFile("maincharacter.spriteData");
            var mainCharacterAnimations = AnimationParser.ReadSpriteSheetData(
                mainCharacterTexture,
                mainCharacterSpriteData);

            this.enemy.ImageTexture2D = this.Content.Load<Texture2D>(this.enemy.GetImage());

            this.mainCharacter = new Character(mainCharacterAnimations, this.input);
            this.background = this.Content.Load<Texture2D>("Images/MapSample");
            this.regularCoin.ImageTexture2D = this.Content.Load<Texture2D>(this.regularCoin.GetImage());
            this.speedUp.ImageTexture2D = this.Content.Load<Texture2D>(this.speedUp.GetImage());
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
            this.camera.LookAt(this.mainCharacter.Position);

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
            
           foreach (var item in this.enemys)
           {
                try
                {
                    item.Update(gameTime, item, this.mainCharacter);
                }
                catch(System.Exception)
                {
                    Exit();
                }
           }
            this.mainCharacter.Update(gameTime);
            foreach (var collectable in this.coins)
            {
             if  (this.collisionHandler.Intersect(this.mainCharacter, collectable))
                {

                    this.mainCharacter.Collect(collectable);
                }
            }
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            this.GraphicsDevice.Clear(Color.Black);
           // Vector2 origin = new Vector2(2, 3);
            var transformMatrix = this.camera.GetViewMatrix();
            this.spriteBatch.Begin(transformMatrix: transformMatrix);
            this.spriteBatch.Draw(
                this.background,
                new Rectangle(-500, -330, (int)(this.background.Width * 1.7), (int)(this.background.Height * 1.7)),
                Color.White);
            this.mainCharacter.Draw(this.spriteBatch);
            this.speedUp.Draw(this.spriteBatch);
            foreach (var item in this.enemys)
            {
                item.Draw(this.enemy,this.spriteBatch);
            }
            foreach (var coin in this.coins)
            {
                coin.Draw(this.regularCoin, this.spriteBatch);

            }

            this.spriteBatch.DrawString(
                this.font,
                $"SCORE: {this.mainCharacter.Inventory.ScoreCoins}",
                new Vector2(-390 + this.mainCharacter.Position.X, 120),
                Color.Silver);
            this.spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}