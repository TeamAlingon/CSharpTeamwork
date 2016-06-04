namespace LevelEditor.Models.UI
{
    using System.Collections.Generic;

    using LevelEditor.EventHandlers;
    using LevelEditor.Input;
    using LevelEditor.Interfaces;
    using LevelEditor.Models;

    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using Microsoft.Xna.Framework.Input;

    public class Panel : GameObject
    {
        private List<IGameObject> ChildrenObjects { get; set; }

        private Texture2D BackgroundTexture { get; set; }

        // For testing purposes. TODO: When done should be separated in a proper class.
        private Point LastMouseClick { get; set; }

        public Panel(Vector2 position, Rectangle size, Texture2D backgroundTexture)
        {
            this.Transform = new Transform2D { Position = position, Size = size };

            this.BackgroundTexture = backgroundTexture;

            this.ChildrenObjects = new List<IGameObject>();
            InputManager.OnDrag += this.HandleMouseDragEvent;
        }

        public void AddChild(IGameObject child)
        {
            child.Transform.Parent = this.Transform;
            this.ChildrenObjects.Add(child);
        }

        public void RemoveChild(IGameObject child)
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

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.LinearWrap);

            spriteBatch.Draw(
                this.BackgroundTexture,
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
                childrenObject.Draw(gameTime, spriteBatch);
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
