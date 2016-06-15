namespace LevelEditor
{
    using LevelEditor.Data;
    using LevelEditor.Input;
    using LevelEditor.Models;
    using LevelEditor.Models.UI;

    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using Microsoft.Xna.Framework.Input;

    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Engine : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public static Text DebugText { get; set; }

        public Engine()
        {
            this.graphics = new GraphicsDeviceManager(this);
            this.Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            this.graphics.PreferredBackBufferWidth = 1280;
            this.graphics.PreferredBackBufferHeight = 720;
            this.graphics.ApplyChanges();

            Factory.Factory.GenerateCamera(this.graphics.GraphicsDevice.Viewport);
            Factory.Factory.GenerateLevelBuildingPanel(this.Content);
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            this.IsMouseVisible = true;

            DebugText = new Text("Debug", this.Content.Load<SpriteFont>("Fonts/impact"));
            DebugText.Transform.Parent = Repository.GetSelectedCamera().Transform;
            Repository.GameObjects.Add(DebugText);

            // Create a new SpriteBatch, which can be used to draw textures.
            this.spriteBatch = new SpriteBatch(this.GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            var keyboardState = Keyboard.GetState();
            var mouseState = Mouse.GetState();
            InputManager.UpdateMouse(gameTime, mouseState);

            foreach (var gameObject in Repository.GameObjects)
            {
                gameObject.Update(gameTime);
            }

            foreach (var camera in Repository.Cameras)
            {
                camera.Update(gameTime);
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            this.GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            foreach (var gameObject in Repository.GameObjects)
            {
                gameObject.Draw(gameTime, this.spriteBatch, Repository.GetSelectedCamera().GetViewMatrix());
            }

            base.Draw(gameTime);
        }
    }
}
