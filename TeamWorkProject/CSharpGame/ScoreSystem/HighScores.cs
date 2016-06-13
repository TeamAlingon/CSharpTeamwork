using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
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





    }
}