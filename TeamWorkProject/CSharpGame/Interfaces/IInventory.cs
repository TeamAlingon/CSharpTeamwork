namespace CSharpGame.Interfaces
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Configuration;

    public interface IInventory
    {
        IList<ICollectable> mainInvenroty { get; set; }

        long CollectedCoins { get; set; }

    }
}
