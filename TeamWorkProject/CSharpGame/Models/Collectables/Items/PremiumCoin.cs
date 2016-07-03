namespace CSharpGame.Models.Collectables.Items
{
    using CSharpGame.Data;

    public  class PremuimCoin : Item
    {
        public PremuimCoin(int x, int y, string image, GameRepository gameRepository) 
            : base(x, y, image, gameRepository)
        {
        }
    }
}
