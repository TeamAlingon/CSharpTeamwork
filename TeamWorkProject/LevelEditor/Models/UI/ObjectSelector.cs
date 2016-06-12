namespace LevelEditor.Models.UI
{
    using System.Collections.Generic;

    using LevelEditor.Interfaces;

    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    class ObjectSelector : GameObject, IDrawableGameObject
    {
        public int CurrentObjectIndex { get; private set; }

        public List<IDrawableGameObject> ObjectPool { get; }

        public ObjectSelector(List<IDrawableGameObject> objectPool, Transform2D transform)
        {
            this.ObjectPool = objectPool;
            this.Transform = transform;

            foreach (var obj in this.ObjectPool)
            {
                obj.Transform.Parent = this.Transform.Parent;
            }

            var lastObjectIndex = objectPool.Count - 1;
            this.CurrentObjectIndex = lastObjectIndex;
        }

        public void SwitchToNextObject()
        {
            this.CurrentObjectIndex++;

            if (this.CurrentObjectIndex == this.ObjectPool.Count)
            {
                this.CurrentObjectIndex = 0;
            }
        }

        public void SwitchToPreviousObject()
        {
            this.CurrentObjectIndex--;

            if (this.CurrentObjectIndex == -1)
            {
                this.CurrentObjectIndex = this.ObjectPool.Count - 1;
            }
        }

        public override void Update(GameTime gameTime)
        {
            this.ObjectPool[this.CurrentObjectIndex].Update(gameTime);
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            this.ObjectPool[this.CurrentObjectIndex].Draw(gameTime, spriteBatch);
        }
    }
}
