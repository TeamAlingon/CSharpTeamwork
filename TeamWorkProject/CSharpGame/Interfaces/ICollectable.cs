namespace CSharpGame.Interfaces
{
    using Models;

    public interface ICollectable : IDrawableGameObject
    {
        bool IsCollected { get; set; }

        void Collect(Character player);
    }
}