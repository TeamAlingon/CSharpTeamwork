namespace LevelEditor.Models.Level
{
    using System.Collections.Generic;

    using LevelEditor.Interfaces;

    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    public class Level : GameObject, IDrawableGameObject
    {
        public List<IDrawableGameObject> LevelObjects { get; set; }

        public override void Update(GameTime gameTime)
        {
            foreach (IDrawableGameObject levelObject in this.LevelObjects)
            {
                levelObject.Update(gameTime);
            }
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            foreach (IDrawableGameObject levelObject in this.LevelObjects)
            {
                levelObject.Draw(gameTime, spriteBatch);
            }
        }
    }
}
