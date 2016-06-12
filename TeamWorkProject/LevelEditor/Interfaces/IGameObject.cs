namespace LevelEditor.Interfaces
{
    using LevelEditor.Models;

    using Microsoft.Xna.Framework;

    public interface IGameObject
    {
        Transform2D Transform { get; set; }

        void Update(GameTime gameTime);
    }
}