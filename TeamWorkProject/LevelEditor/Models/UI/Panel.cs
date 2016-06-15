namespace LevelEditor.Models.UI
{
    using System.Collections.Generic;

    using LevelEditor.EventHandlers;
    using LevelEditor.Input;
    using LevelEditor.Interfaces;
    using LevelEditor.Models;

    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    public class Panel : GameObject, IDrawableGameObject
    {
        private List<IDrawableGameObject> ChildrenObjects { get; set; }

        public Texture2D Texture { get; set; }

        public Panel(Vector2 position, Rectangle size, Texture2D backgroundTexture)
        {
            this.Transform = new Transform2D { Position = position, Size = size };

            this.Texture = backgroundTexture;

            this.ChildrenObjects = new List<IDrawableGameObject>();
            InputManager.OnDrag += this.HandleMouseDragEvent;
        }

        public void AddChild(IDrawableGameObject child)
        {
            child.Transform.Parent = this.Transform;
            this.ChildrenObjects.Add(child);
        }

        public void RemoveChild(IDrawableGameObject child)
        {
            this.ChildrenObjects.Remove(child);
        }

        public override void Update(GameTime gameTime)
        {
            foreach (var childrenObject in this.ChildrenObjects)
            {
                childrenObject.Update(gameTime);
            }
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch, Matrix viewMatrix)
        {
            spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.LinearWrap, transformMatrix: viewMatrix);

            spriteBatch.Draw(
                this.Texture,
                this.Transform.Position,
                this.Transform.Size,
                Color.White,
                0,
                Vector2.Zero,
                1f,
                SpriteEffects.None,
                0);

            spriteBatch.End();

            foreach (var childrenObject in this.ChildrenObjects)
            {
                childrenObject.Draw(gameTime, spriteBatch, viewMatrix);
            }
        }

        private void HandleMouseDragEvent(PointerEventDataArgs args)
        {
            if (this.Transform.Size.Contains(args.Position))
            {
                this.Transform.Position += args.Delta;
            }
        }
    }
}
