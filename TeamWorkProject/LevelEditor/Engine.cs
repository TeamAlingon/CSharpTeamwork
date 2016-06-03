namespace LevelEditor
{
    using LevelEditor.Models;
    using LevelEditor.Models.UI;
    using LevelEditor.Utils;

    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using Microsoft.Xna.Framework.Input;

    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Engine : Game
    {
        private Panel testPanel;


        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

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

            var panelPosition = new Vector2(100, 100);
            var panelSize = new Rectangle(0, 0, 512, 512);
            var panelTexture = this.Content.Load<Texture2D>("UiTiles/GrayTile");

            this.testPanel = new Panel(
                panelPosition,
                panelSize,
                panelTexture);

            var spriteFont = this.Content.Load<SpriteFont>("Fonts/impact");
            var levelFiles = FileUtils.GetFilenames("Level");

            var textPosition = Vector2.Zero;
            foreach (string levelFile in levelFiles)
            {
                var childTransform = new Transform2D(textPosition, Rectangle.Empty);
                this.testPanel.AddChild(new Text(levelFile, spriteFont, childTransform));

                textPosition = new Vector2(textPosition.X, textPosition.Y + 20);
            }

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            this.IsMouseVisible = true;

            this.graphics.PreferredBackBufferWidth = 1280;
            this.graphics.PreferredBackBufferHeight = 720;
            this.graphics.ApplyChanges();

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
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape)) this.Exit();

            // TODO: Add your update logic here
            this.testPanel.Update(gameTime);

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
            this.testPanel.Draw(gameTime, this.spriteBatch);

            base.Draw(gameTime);
        }
    }
}
