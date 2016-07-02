namespace CSharpGame.Interfaces
{
    using Microsoft.Xna.Framework.Graphics;
    using Models;

    public interface ICollectable
    {
        Texture2D ImageTexture2D { get; set; }

        int X { get; set; }

        int Y { get; set; }

        void Collect(Character player);

        bool IsAvailable();

        void Update();

        string GetImage();

        bool IsCollected { get; set; }

        void Draw(ICollectable collectable, SpriteBatch spriteBatch);

        void Draw(SpriteBatch spriteBatch);



    }
}