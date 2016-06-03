namespace CSharpGame.Interfaces
{
    using Models;

    public interface ICollectable
    {
        void Collect(Character player);

        bool isAvailable();

        void Update();
    }
}
