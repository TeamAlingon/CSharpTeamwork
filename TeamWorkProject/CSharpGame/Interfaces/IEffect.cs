namespace CSharpGame.Interfaces
{
    using Models;

    public interface IEffect : ICollectable
    {
        void ApplyEffect();

        void RemoveEffect();
    }
}