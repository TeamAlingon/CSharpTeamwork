namespace CSharpGame.Interfaces
{
    public interface ICollectable
    {
        void Collect();

        bool isAvailable();

        void Update();
    }
}
