namespace CSharpGame.Interfaces
{
    using CSharpGame.Models.Foundations;

    using Microsoft.Xna.Framework;

    public interface IGameObject
    {
        Transform2D Transform { get; set; }

        void Update(GameTime gameTime);
    }
}