namespace CSharpGame.Models.Collectables.Items
{
    using CSharpGame.Data;

    public class RegularCoin : Item
    {
        private const string ImageCoins = "Images/Coin";

        private const int CoinValue = 1;

        public RegularCoin(int x, int y, GameRepository gameRepository)
            : base(x,y, ImageCoins, gameRepository)
        {
        }

        public override void GetCollected(Character player)
        {
            player.ScoreCoins += CoinValue;
            base.GetCollected(player);
        }
    }
}
