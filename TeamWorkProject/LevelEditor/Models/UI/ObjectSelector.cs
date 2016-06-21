namespace LevelEditor.Models.UI
{
    using System.Collections.Generic;

    using LevelEditor.Interfaces;
    using LevelEditor.IO;
    using LevelEditor.Models.Level;

    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    class ObjectSelector : GameObject, IDrawableGameObject
    {
        public Level CurrentLevel { get; }

        public int CurrentObjectIndex { get; private set; }

        public List<IDrawableGameObject> ObjectPool { get; }

        public ObjectSelector(List<IDrawableGameObject> objectPool, Transform2D transform, Level currentLevel)
        {
            this.ObjectPool = objectPool;
            this.Transform = transform;
            this.CurrentLevel = currentLevel;

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

        public void PlaceGameObjectInLevel()
        {
            var currentObjectPlacement = this.ObjectPool[this.CurrentObjectIndex].Transform.Position;
            var currentTexturedGameObject = this.ObjectPool[this.CurrentObjectIndex] as TexturedGameObject;
            if (currentTexturedGameObject != null)
            {
                var levelObject = new TexturedGameObject(currentTexturedGameObject.Texture);
                levelObject.Transform.Parent = this.CurrentLevel.Transform;
                levelObject.Transform.Position = currentObjectPlacement;

                this.CurrentLevel.AddGameObject(levelObject);
            }
        }

        public void SaveLevel()
        {
            var test = this.ObjectPool[this.CurrentObjectIndex];
            File.Save("WatIsDis.xml", test);
        }

        public override void Update(GameTime gameTime)
        {
            if (this.CurrentObjectIndex >= 0 && this.CurrentObjectIndex < this.ObjectPool.Count)
            {
                this.ObjectPool[this.CurrentObjectIndex].Update(gameTime);
            }
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch, Matrix viewMatrix)
        {
            if (this.CurrentObjectIndex >= 0 && this.CurrentObjectIndex < this.ObjectPool.Count)
            {
                this.ObjectPool[this.CurrentObjectIndex].Draw(gameTime, spriteBatch, viewMatrix);
            }
        }
    }
}
