namespace LevelEditor.Models.Level
{
    using System.Collections.Generic;

    using LevelEditor.Interfaces;

    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    public class Level : GameObject
    {
        public List<IGameObject> LevelObjects { get; set; }

        public override void Update(GameTime gameTime)
        {
            foreach (IGameObject levelObject in this.LevelObjects)
            {
                levelObject.Update(gameTime);
            }
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            foreach (IGameObject levelObject in this.LevelObjects)
            {
                levelObject.Draw(gameTime, spriteBatch);
            }
        }
    }
}
