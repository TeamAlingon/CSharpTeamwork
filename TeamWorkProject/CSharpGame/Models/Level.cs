namespace CSharpGame.Models
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using CSharpGame.CollisionHandler;
    using CSharpGame.Data;
    using CSharpGame.Interfaces;
    using CSharpGame.Models.Foundations;

    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    public class Level : GameObject, IDrawableGameObject
    {
        private readonly CollisionHandler collisionHandler;

        public Character Player { get; set; }

        public List<Character> Enemies { get; private set; }

        private List<TexturedGameObject> DrawableLevelObjects { get; set; }

        private List<GameObject> AllLevelObjects { get; set; }

        private List<ICollectable> CollectableObjects { get; set; }

        public Level(GameRepository gameRepository)
            : base(gameRepository)
        {
            this.Enemies = new List<Character>();
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
            this.Player.Update(gameTime);

            foreach (IGameObject gameObject in this.AllLevelObjects)
            {
                gameObject.Update(gameTime);
            }

            foreach (ICollectable collectableObject in this.CollectableObjects)
            {
                this.collisionHandler.CollectableCollision(this.Player, collectableObject);
            }

            foreach (Character enemy in this.Enemies)
            {
                enemy.Update(gameTime);

                if (this.collisionHandler.EnemyCollision(this.Player, enemy))
                {
                    this.GameRepository.PlayerDied = true;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (IDrawableGameObject levelObject in this.DrawableLevelObjects)
            {
                levelObject.Draw(spriteBatch);
            }

            foreach (Character character in this.Enemies)
            {
                character.Draw(spriteBatch);
            }

            this.Player.Draw(spriteBatch);
        }
    }
}