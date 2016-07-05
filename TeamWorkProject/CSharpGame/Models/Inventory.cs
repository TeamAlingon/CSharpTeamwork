namespace CSharpGame.Models
{
    using Collectables.Effects;
    using Collectables.Items;
    using Interfaces;

    public class Inventory
    {
        private bool speedUP;
        private int scoreCoins;

        public Inventory()
        {
            InitInventory();
        }

        public void InitInventory()
        {
           
        }

        public bool SpeedUp
        {

            get { return this.speedUP; }
            set { this.speedUP = value; }
        }

        public void Collect(ICollectable item)
        {
            if (item is RegularCoin)
            {
                this.scoreCoins++;
            }
            if (item is SpeedUp)
            {
                this.SpeedUp = true;
            }
        }

        public int ScoreCoins
        {
            get { return this.scoreCoins; }

            set
            {
                this.scoreCoins += value;
                
            }
        }
    }
}
