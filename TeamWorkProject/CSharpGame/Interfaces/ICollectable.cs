namespace CSharpGame.Interfaces
{
    using Microsoft.Xna.Framework.Graphics;
    using Models;

    public interface ICollectable
    {
        void Collect(Character player);

        bool isAvailable();

        void Update();

        Texture2D ImageTexture2D { get; set; }
        int X { get; set; }
        int Y { get; set; }
        string GetImage();
    }
}