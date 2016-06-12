using CSharpGame.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace CSharpGame
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D mainCharacterTexture;
        Character mainCharacter = new Character();

        SoundEffect walkEffect;
        SoundEffectInstance walkInstance;
        SoundEffect levelTheme;

        Character character = new Character();
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
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

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            mainCharacterTexture = Content.Load<Texture2D>(mainCharacter.GetImage());

            walkEffect = Content.Load<SoundEffect>("Soundtrack/footstep_cut");
            levelTheme = Content.Load<SoundEffect>("Soundtrack/level");
            walkInstance = walkEffect.CreateInstance();
            walkInstance.IsLooped = true;

            SoundEffectInstance levelThemeInstance = levelTheme.CreateInstance();
            levelThemeInstance.IsLooped = true;
            levelThemeInstance.Volume = 0.1f;
            levelThemeInstance.Play();
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {

        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                this.GraphicsDevice.Clear(Color.CornflowerBlue);
                character.X++;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                this.GraphicsDevice.Clear(Color.CornflowerBlue);
                character.X--;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                this.GraphicsDevice.Clear(Color.CornflowerBlue);
                character.Y--;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                this.GraphicsDevice.Clear(Color.CornflowerBlue);
                character.Y++;
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

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(mainCharacterTexture,new Rectangle(character.X, character.Y, 500,500), color:Color.White);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
