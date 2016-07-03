namespace CSharpGame.Interfaces
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    public interface IDrawableGameObject : IGameObject
    {
        void Draw(GameTime gameTime, SpriteBatch spriteBatch);
    }
}
