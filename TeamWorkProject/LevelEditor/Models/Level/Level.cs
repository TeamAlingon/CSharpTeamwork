namespace LevelEditor.Models.Level
{
    using System.Collections.Generic;

    using LevelEditor.Interfaces;

    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    public class Level : GameObject, IDrawableGameObject
    {
        public List<TexturedGameObject> DrawableLevelObjects { get; }
        
        public List<GameObject> AllLevelObjects { get; }

        public Level()
        {
            this.DrawableLevelObjects = new List<TexturedGameObject>();
            this.AllLevelObjects = new List<GameObject>();
        }

        public void AddGameObject(IGameObject gameObject)
        {
            var drawableObject = gameObject as TexturedGameObject;
            if (drawableObject != null)
            {
                this.DrawableLevelObjects.Add(drawableObject);
            }

            var castedGameObject = gameObject as GameObject;
            if (castedGameObject != null)
            {
                this.AllLevelObjects.Add(castedGameObject);
            }
        }

        public override void Update(GameTime gameTime)
        {
            foreach (IGameObject gameObject in this.AllLevelObjects)
            {
                gameObject.Update(gameTime);
            }
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch, Matrix viewMatrix)
        {
            foreach (IDrawableGameObject levelObject in this.DrawableLevelObjects)
            {
                levelObject.Draw(gameTime, spriteBatch, viewMatrix);
            }
        }
    }
}
