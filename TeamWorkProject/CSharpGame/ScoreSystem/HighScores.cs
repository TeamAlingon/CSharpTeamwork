using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace CSharpGame.ScoreSystem
{
    public class HighScores : Microsoft.Xna.Framework.DrawableGameComponent
    {
        public HighScores(Game game) : base(game)
        {
            
        }

        List<string> scores;
        SpriteFont scoreFont;
        KeyboardState oldState;
        SpriteFont itemFont;
        SpriteBatch spriteBatch;
        GraphicsDeviceManager graphics;
        Texture2D selectionArrow;

        public override void Initialize()
        {
            scores = new List<string>(10);
            const string fileName = "highscores.txt";
            if (File.Exists(fileName))
            {
                scores = File.ReadAllLines(fileName).ToList<string>();
                scores.Sort((a, b) => Convert.ToInt32(b).CompareTo(Convert.ToInt32(a)));
            }
            scoreFont = Game.Content.Load<SpriteFont>("Score");
            
            oldState = Keyboard.GetState();
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().GetPressedKeys().Length > 0 && oldState.GetPressedKeys().Length == 0)
            {
                Game.Components.Remove(this);
            }
            oldState = Keyboard.GetState();
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            Vector2 position = new Vector2(graphics.PreferredBackBufferWidth / 2 - 150, graphics.PreferredBackBufferHeight / 2 - 200);
            spriteBatch.Begin();

            for (int i = 0; i < 10; i++)
            {
                spriteBatch.DrawString(scoreFont, (i + 1).ToString() + ".", new Vector2(position.X, position.Y + (30 * i)), Color.White);
                if (i < scores.Count)
                {
                    spriteBatch.DrawString(scoreFont, scores[i], new Vector2(position.X + 50, position.Y + (30 * i)), Color.White);
                }
            }

            Vector2 itemPosition;
            itemPosition.X = (graphics.PreferredBackBufferWidth / 2) - 100;
            itemPosition.Y = (graphics.PreferredBackBufferHeight / 2) + 200;
            spriteBatch.Draw(selectionArrow, new Vector2(itemPosition.X - 50, itemPosition.Y), Color.White);
            spriteBatch.DrawString(itemFont, "Return", itemPosition, Color.Yellow);

            spriteBatch.End();
        }

    }
}