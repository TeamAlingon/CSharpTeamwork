namespace CSharpGame.Data
{
    using System;
    using System.Security.Policy;

    using CSharpGame.Input;
    using CSharpGame.Models;
    using CSharpGame.Models.Animations;
    using CSharpGame.Models.Collectables.Effects;
    using CSharpGame.Models.Collectables.Items;
    using CSharpGame.Models.Foundations;

    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Content;
    using Microsoft.Xna.Framework.Graphics;

    public class GameRepository : DrawableGameComponent
    {
        private readonly Level level;

        public bool PlayerDied { get; set; }

        public Camera2D Camera { get; private set; }

        public GameRepository(Game game)
            : base(game)
        {
            this.level = new Level(this);
            this.PlayerDied = false;

            this.InitializeLevel();
        }

        public ContentManager ContentManager => this.Game.Content;

        public Character Player => this.level.Player;

        public T LoadContent<T>(string path)
        {
            return this.ContentManager.Load<T>(path);
        }

        public void AddGameObject(TexturedGameObject drawableGameObject)
        {
            this.level.AddGameObject(drawableGameObject);
        }

        public void AddGameObject(GameObject gameObject)
        {
            this.level.AddGameObject(gameObject);
        }

        public override void Update(GameTime gameTime)
        {
            this.Camera.LookAt(this.level.Player.Position);
            this.level.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            this.level.Draw(spriteBatch);
        }

        private void InitializeLevel()
        {
            var cameraOfset = new Vector2(0, 150);
            Camera2D camera = new Camera2D(this, this.Game.GraphicsDevice.Viewport, cameraOfset);
            this.Camera = camera;

            var background = new TexturedGameObject(this, "Images/MapSample");
            background.Transform.Scale = 2;

            this.AddGameObject(background);

            var startX = 1500;
            var random = new Random();
            for (int i = 0; i < 100; i++)
            {
                var coin = new RegularCoin((startX + i * 400), random.Next(700, 900), this);
                this.AddGameObject(coin);
            }

            for (int i = 0; i < 3; i++)
            {
                var speedUp = new SpeedUp(startX + i * 10000, 700, 20, 7, this);
                speedUp.Texture = this.LoadContent<Texture2D>(speedUp.GetImage());
                speedUp.Transform.Scale = 0.2f;

                this.AddGameObject(speedUp);
            }

            var playerInput = new PlayerInput(this.Game, camera);
            var playerTexture = this.LoadContent<Texture2D>("Images/maincharacter");
            var playerSpriteData = LevelEditor.IO.File.ReadTextFile("maincharacter.spriteData");
            var playerAnimations = Animation.ReadSpriteSheetData(playerTexture, playerSpriteData);
            var player = new Character(new Vector2(startX, 860), playerAnimations, playerInput, this);
            player.Transform.Scale = 0.4f;

            this.level.Player = player;

            startX += 1000;
            var enemyTexture = this.LoadContent<Texture2D>("Images/enemies");
            for (int i = 0; i < 5; i++)
            {
                var guitarEnemyInput = new EnemyInput(this.Game);
                var guitarEnemySpriteData = LevelEditor.IO.File.ReadTextFile("enemyGuitar.spriteData");
                var guitarEnemyAnimations = Animation.ReadSpriteSheetData(enemyTexture, guitarEnemySpriteData, 0.2f);
                var guitarEnemy = new Character(new Vector2(startX + i * 1500, 800), guitarEnemyAnimations, guitarEnemyInput, this);
                guitarEnemy.Transform.Scale = 0.7f;

                this.level.Enemies.Add(guitarEnemy);

                var battonEnemyInput = new EnemyInput(this.Game);
                var battonEnemySpriteData = LevelEditor.IO.File.ReadTextFile("enemyBatton.spriteData");
                var battonEnemyAnimations = Animation.ReadSpriteSheetData(enemyTexture, battonEnemySpriteData, 0.2f);
                var battonEnemy = new Character(new Vector2(startX + i * 2000, 800), battonEnemyAnimations, battonEnemyInput, this);
                battonEnemy.Transform.Scale = 0.7f;

                this.level.Enemies.Add(battonEnemy);
            }
        }
    }
}