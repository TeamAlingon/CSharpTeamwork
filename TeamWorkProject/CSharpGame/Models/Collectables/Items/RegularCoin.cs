namespace CSharpGame.Models.Collectables 
{
    public class RegularCoin : AbstractItem
    {
        private bool canCollect;

        public RegularCoin()
        {
            canCollect = true;
        }

        public override void Collect()
        {
            canCollect = false;
        }

        public override bool isAvailable()
        {
            if (canCollect)
            {
                return true;
            }
            return false;
        }
    }
}
