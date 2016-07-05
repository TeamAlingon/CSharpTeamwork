namespace CSharpGame.Interfaces
{
    using Microsoft.Xna.Framework;

    using Models;

    public interface ICollectable : IDrawableGameObject
    {
        Character Collector { get; set; }

        bool IsCollected { get; set; }

        bool HasBeenUsed { get; }

        Rectangle BoundingBox { get; }

        void GetCollected(Character player);
    }
}