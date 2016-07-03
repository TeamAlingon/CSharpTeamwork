namespace CSharpGame.Models
{
    using System.Collections.Generic;
    using System.Linq;

    using CSharpGame.CollisionHandler;
    using CSharpGame.Data;
    using CSharpGame.Interfaces;
    using CSharpGame.Models.Foundations;

    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    public class Level : GameObject, IDrawableGameObject
    {
        private CollisionHandler collisionHandler;

        public Character MainCharacter { get; set; }

        private List<TexturedGameObject> DrawableLevelObjects { get; set; }

        private List<GameObject> AllLevelObjects { get; set; }

        private List<ICollectable> CollectableObjects { get; set; }

        public Level(GameRepository gameRepository)
            : base(gameRepository)
        {
            this.DrawableLevelObjects = new List<TexturedGameObject>();
            this.AllLevelObjects = new List<GameObject>();
            this.CollectableObjects = new List<ICollectable>();
            this.collisionHandler = new CollisionHandler();
        }

        public void AddGameObject(GameObject gameObject)
        {
            this.AllLevelObjects.Add(gameObject);
            this.AllLevelObjects = this.AllLevelObjects.OrderBy(go => go.UpdateOrder).ToList();

            var collidableObject = gameObject as ICollectable;
            if (collidableObject != null)
            {
                this.CollectableObjects.Add(collidableObject);
            }
        }

        public void AddGameObject(TexturedGameObject gameObject)
        {
            this.DrawableLevelObjects.Add(gameObject);
            this.DrawableLevelObjects = this.DrawableLevelObjects.OrderBy(dgo => dgo.UpdateOrder).ToList();

            this.AddGameObject(gameObject as GameObject);
        }

        public override void Update(GameTime gameTime)
        {
            foreach (IGameObject gameObject in this.AllLevelObjects)
            {
                gameObject.Update(gameTime);
            }

            foreach (ICollectable collectableObject in CollectableObjects)
            {
                this.collisionHandler.Intersect(this.MainCharacter, collectableObject);
            }

            this.MainCharacter.Update(gameTime);
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            foreach (IDrawableGameObject levelObject in this.DrawableLevelObjects)
            {
                levelObject.Draw(gameTime, spriteBatch);
            }

            this.MainCharacter.Draw(spriteBatch);
        }
    }
}