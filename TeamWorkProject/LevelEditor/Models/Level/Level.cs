namespace LevelEditor.Models.Level
{
    using System.Collections.Generic;

    using LevelEditor.Interfaces;

    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    public class Level : GameObject, IDrawableGameObject
    {
        private List<IDrawableGameObject> DrawableLevelObjects { get; }

        private List<IGameObject> AllLevelObjects { get; }

        public Level()
        {
            this.DrawableLevelObjects = new List<IDrawableGameObject>();
            this.AllLevelObjects = new List<IGameObject>();
        }

        public void AddGameObject(IGameObject gameObject)
        {
            var drawableObject = gameObject as IDrawableGameObject;
            if (drawableObject != null)
            {
                this.DrawableLevelObjects.Add(drawableObject);
            }

            this.AllLevelObjects.Add(gameObject);
        }

        public override void Update(GameTime gameTime)
        {
            foreach (IGameObject gameObject in this.AllLevelObjects)
            {
                gameObject.Update(gameTime);
            }
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            foreach (IDrawableGameObject levelObject in this.DrawableLevelObjects)
            {
                levelObject.Draw(gameTime, spriteBatch);
            }
        }
    }
}
