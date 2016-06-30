namespace CSharpGame.Models
{
    using System.Collections.Generic;
    using Collectables.Effects;
    using Collectables.Items;
    using Interfaces;

    public class Inventory
    {
        private IList<ICollectable> powerUps;
        private int scoreCoins;

        public Inventory()
        {
            InitInventory();
        }

        public void InitInventory()
        {
            this.powerUps = new List<ICollectable>();
        }

        public IList<ICollectable> PowerUps
        {

            get { return this.powerUps; }
            set { this.powerUps = value; }
        }

        public void Collect(ICollectable item)
        {
            if (item is RegularCoin)
            {
                this.scoreCoins++;
            }
            if (item is SpeedUp)
            {
                this.PowerUps.Add(item);
            }
        }

        public int ScoreConins
        {
            get { return this.scoreCoins; }

            set
            {
                this.scoreCoins += value;
                
            }
        }
    }
}
