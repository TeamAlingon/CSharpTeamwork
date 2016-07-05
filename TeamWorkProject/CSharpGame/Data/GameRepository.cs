namespace CSharpGame.Data
{
    using System;

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
        public readonly Level Level;

        public Camera2D Camera { get; private set; }

        public GameRepository(Game game)
            : base(game)
        {
            this.Level = new Level(this);

            this.InitializeLevel();
        }

        public ContentManager ContentManager => this.Game.Content;

        public Character MainCharacter => this.Level.MainCharacter;

        public T LoadContent<T>(string path)
        {
            return this.ContentManager.Load<T>(path);
        }

        public Camera2D GetSelectedCamera()
        {
            return this.Camera;
        }

        public void AddGameObject(TexturedGameObject drawableGameObject)
        {
            this.Level.AddGameObject(drawableGameObject);
        }

        public void AddGameObject(GameObject gameObject)
        {
            this.Level.AddGameObject(gameObject);
        }

        public override void Update(GameTime gameTime)
        {
            this.GetSelectedCamera().LookAt(this.Level.MainCharacter.Position);
            this.Level.Update(gameTime);
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            this.Level.Draw(gameTime, spriteBatch);
        }

        private void InitializeLevel()
        {
            Camera2D camera = new Camera2D(this, this.Game.GraphicsDevice.Viewport);
            this.Camera = camera;

            var background = new TexturedGameObject(this, "Images/MapSample");
            background.Transform.Scale = 2f;

            var random = new Random();
            for (int i = 0; i < 100; i++)
            {
                var coin = new RegularCoin((400 + i * 400), random.Next(300, 600), this) { Transform = { Scale = 2f } };
            }

            SpeedUp speedUp = new SpeedUp(100, 280, 20, this);
            speedUp.Texture = this.LoadContent<Texture2D>(speedUp.GetImage());
            speedUp.Transform.Scale = 0.5f;

            InputManager input = new InputManager(this.Game, camera);
            var mainCharTexture = this.LoadContent<Texture2D>("Images/maincharacter");
            var mainCharSpriteData = LevelEditor.IO.File.ReadTextFile("maincharacter.spriteData");
            var mainCharAnimations = AnimationParser.ReadSpriteSheetData(mainCharTexture, mainCharSpriteData);
            var mainCharacter = new Character(new Vector2(-500, 500), mainCharAnimations, input, this) { Transform = { Scale = 0.7f } };

            this.Level.MainCharacter = mainCharacter;
        }
    }
}