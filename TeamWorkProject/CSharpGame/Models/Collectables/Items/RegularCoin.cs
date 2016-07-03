namespace CSharpGame.Models.Collectables.Items
{
    using CSharpGame.Data;

    public class RegularCoin : Item
    {
        private const string imageCoins = "Images/Coin";

        public RegularCoin(int x, int y, GameRepository gameRepository)
            : base(x,y, imageCoins, gameRepository)
        {
        }
    }
}
