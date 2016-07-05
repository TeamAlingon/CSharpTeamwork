﻿namespace CSharpGame
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
    using System.Threading.Tasks;
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        private InputManager input;
        private Texture2D background;
        private Character mainCharacter;
        private RegularCoin regularCoin = new RegularCoin(400, 380);
        private List<ICollectable> coins = new List<ICollectable>();
        private Enemy enemy = new Enemy();
        private List<Enemy> enemys = new List<Enemy>();
        private SpeedUp speedUp = new SpeedUp(100, 280, 20);
        private Camera2D camera;
        private SpriteFont font;
        private SpriteFont fontGameOver;
        private CollisionHandler.CollisionHandler collisionHandler;

        SoundEffect walkEffect;
        SoundEffectInstance walkInstance;
        SoundEffect levelTheme;
        SoundEffect jumpEffect;
        SoundEffectInstance jumpInstance;
        SoundEffect hitEffect;
        SoundEffectInstance hitInstance;

        enum GameState
        {
            MainMenu,
            Gameplay,
            EndOfGame,
        }
        GameState stateOfGame;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            var viewPortAdapter = new BoxingViewportAdapter(Window, GraphicsDevice, 800, 480);
            regularCoin.InitializeList(coins);
            Enemy.InitializeEnemies(enemys);
            coins.Add(this.speedUp);
            camera = new Camera2D(viewPortAdapter);
            this.input = new InputManager(this, this.camera);
            this.collisionHandler = new CollisionHandler.CollisionHandler();
            base.Initialize();
            stateOfGame = GameState.Gameplay;
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            var mainCharacterTexture = Content.Load<Texture2D>("Images/maincharacter");
            var mainCharacterSpriteData = LevelEditor.IO.File.ReadTextFile("maincharacter.spriteData");
            var mainCharacterAnimations = AnimationParser.ReadSpriteSheetData(
                mainCharacterTexture,
                mainCharacterSpriteData);

            this.enemy.ImageTexture2D = Content.Load<Texture2D>(this.enemy.GetImage());

            this.mainCharacter = new Character(mainCharacterAnimations, this.input);
            this.background = this.Content.Load<Texture2D>("Images/MapSample");

            this.regularCoin.ImageTexture2D = Content.Load<Texture2D>(this.regularCoin.GetImage());
            this.speedUp.ImageTexture2D = this.Content.Load<Texture2D>(this.speedUp.GetImage());

            this.font = this.Content.Load<SpriteFont>("Score");
            this.fontGameOver = this.Content.Load<SpriteFont>("Fonts/game_over");

            this.walkEffect = this.Content.Load<SoundEffect>("Soundtrack/footstep_cut");
            this.walkInstance = this.walkEffect.CreateInstance();
            this.walkInstance.IsLooped = true;

            this.jumpEffect = Content.Load<SoundEffect>("Soundtrack/jump");
            this.jumpInstance = jumpEffect.CreateInstance();
            this.jumpInstance.IsLooped = false;

            //Hit effect for breaking or knocking enemies
            /*
            hitEffect = Content.Load<SoundEffect>("Soundtrack/scratch");
            hitInstance = hitEffect.CreateInstance();
            hitInstance.IsLooped = false;
            */

            this.levelTheme = Content.Load<SoundEffect>("Soundtrack/level");
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
            switch (stateOfGame)
            {
                case GameState.MainMenu:

                    break;
                case GameState.Gameplay:
                    base.Update(gameTime);
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

                    foreach (var item in enemys)
                    {
                        try
                        {
                            item.Update(gameTime, item, mainCharacter);
                        }
                        catch (System.Exception)
                        {
                            stateOfGame = GameState.EndOfGame;
                        }
                    }
                    this.mainCharacter.Update(gameTime);
                    foreach (var collectable in this.coins)
                    {
                        if (this.collisionHandler.Intersect(this.mainCharacter, collectable))
                        {

                            this.mainCharacter.Collect(collectable);
                        }
                    }
                    base.Update(gameTime);
                    break;
                case GameState.EndOfGame:
                    if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                    {
                        Exit();
                    }
                    break;
            }
        }

        protected override void Draw(GameTime gameTime)
        {
            switch (stateOfGame)
            {
                case GameState.MainMenu:

                    break;
                case GameState.Gameplay:
                    GraphicsDevice.Clear(Color.Black);
                    // Vector2 origin = new Vector2(2, 3);
                    var transformMatrix = camera.GetViewMatrix();
                    spriteBatch.Begin(transformMatrix: transformMatrix);
                    spriteBatch.Draw(
                        this.background,
                        new Rectangle(-500, -330, (int)(this.background.Width * 1.7), (int)(this.background.Height * 1.7)),
                        Color.White);
                    this.mainCharacter.Draw(this.spriteBatch);
                    this.speedUp.Draw(this.spriteBatch);
                    foreach (var item in this.enemys)
                    {
                        item.Draw(enemy, this.spriteBatch);
                    }
                    foreach (var coin in this.coins)
                    {
                        coin.Draw(regularCoin, spriteBatch);
                    }

                    spriteBatch.DrawString(
                        font,
                        $"SCORE: {this.mainCharacter.Inventory.ScoreCoins}",
                        new Vector2(this.mainCharacter.Position.X - 390, 120),
                        Color.Silver);
                    spriteBatch.End();
                    base.Draw(gameTime);
                    break;
                case GameState.EndOfGame:
                    spriteBatch.Begin();
                    spriteBatch.DrawString(fontGameOver, $"GAME OVER", new Vector2(210, 180), Color.DarkRed);
                    spriteBatch.End();
                    break;
            }
        }
    }
}