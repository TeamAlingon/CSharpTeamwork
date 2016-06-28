namespace CSharpGame.Models
{
    using System.Collections.Generic;
    using Interfaces;

    public class Inventory
    {
        private IList<ICollectable> coins;

        public Inventory()
        {
            InitInventory();
        }

        public void InitInventory()
        {
            this.coins = new List<ICollectable>();
        }

        public IList<ICollectable> Coins
        {

            get { return this.coins; }
            set { this.coins = value; }
        }

        public void Collect(ICollectable item)
        {
            this.Coins.Add(item);
        }
    }
}
