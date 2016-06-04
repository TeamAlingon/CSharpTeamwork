namespace LevelEditor.Interfaces
{
    using LevelEditor.Models;

    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using Microsoft.Xna.Framework.Input;

    public interface IGameObject
    {
        Transform2D Transform { get; set; }

        void Update(GameTime gameTime);

        void Draw(GameTime gameTime, SpriteBatch spriteBatch);
    }
}