namespace CSharpGame.Models.Collectables
{
    using Interfaces;
    public abstract class AbstractItem : ICollectable
    {
        public abstract void Collect();
        
        public abstract bool isAvailable();

    }
}
