namespace CSharpGame.Interfaces
{
    using System.Collections.Generic;

    public interface IInventory
    {
        IList<ICollectable> mainInvenroty { get; set; }

        long CollectedCoins { get; set; }
    }
}
