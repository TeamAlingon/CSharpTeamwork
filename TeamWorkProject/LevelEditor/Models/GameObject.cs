namespace LevelEditor.Models
{
    using LevelEditor.Interfaces;

    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using Microsoft.Xna.Framework.Input;

    public abstract class GameObject : IGameObject
    {
        public Transform2D Transform { get; set; }

        public abstract void Update(GameTime gameTime);

        public abstract void Draw(GameTime gameTime, SpriteBatch spriteBatch);
    }
}
