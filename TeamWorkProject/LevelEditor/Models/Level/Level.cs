namespace LevelEditor.Models.Level
{
    using System.Collections.Generic;
    using System.Xml.Serialization;

    using LevelEditor.Interfaces;

    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    [XmlRoot("Level")]
    [XmlInclude(typeof(GameObject))]
    public class Level : GameObject, IDrawableGameObject
    {
        private readonly List<IDrawableGameObject> drawableLevelObjects;

        [XmlElement("AllLevelObjects")]
        private readonly List<IGameObject> allLevelObjects;

        private List<IDrawableGameObject> DrawableLevelObjects => this.drawableLevelObjects;

        private List<IGameObject> AllLevelObjects => this.allLevelObjects;

        public Level()
        {
            this.drawableLevelObjects = new List<IDrawableGameObject>();
            this.allLevelObjects = new List<IGameObject>();
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

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch, Matrix viewMatrix)
        {
            foreach (IDrawableGameObject levelObject in this.DrawableLevelObjects)
            {
                levelObject.Draw(gameTime, spriteBatch, viewMatrix);
            }
        }
    }
}
