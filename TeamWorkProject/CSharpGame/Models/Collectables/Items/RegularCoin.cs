namespace CSharpGame.Models.Collectables.Items
{
    using Microsoft.Xna.Framework.Graphics;

    public class RegularCoin : Item
    {
        private int x;
        private int y;
        public Texture2D imageTexture;
        private const string imageCoins = "Images/Coins";
        //private Rectangle rectangle = new Rectangle(X, Y, 1000, 200);
        //  public Rectangle Rectangle { get; set; }



        public RegularCoin(int x, int y) : base(x,y)
        {
            
        }

        public string GetImage()
        {
            return imageCoins;
        }
        public int X { get; set; }
        public int Y { get; set; }
        public Texture2D ImageTexture2D { get; set; }
    }
}
