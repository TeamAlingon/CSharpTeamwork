namespace CSharpGame.Models.Collectables.Items
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using System.Collections.Generic;

    public class RegularCoin : Item
    {
        private int x;
        private int y;
        private Texture2D imageTexture;
        private const string imageCoins = "Images/Coin";
        
        public RegularCoin(int x, int y) : base(x,y)
        {
            X = x;
            Y = y;
        }
        public bool isCollected;
       
        public string GetImage()
        {
            return imageCoins;
        }
        public int X { get; set; }
        public int Y { get; set; }

        public Texture2D ImageTexture2D
        {
            get { return this.imageTexture; }
            set { this.imageTexture = value; }
        }

        public bool Intersect(Character charater, RegularCoin coin)
        {
            Rectangle characterRectangle = new Rectangle(charater.Position.ToPoint(), new Point(125, 125));
            Rectangle coinRectangle = new Rectangle(coin.X, coin.Y, 80, 80);
            if (!coin.isCollected)
            {
                if (coinRectangle.Intersects(characterRectangle))
                {
                    coin.isCollected = true;
                    return true;
                }
            }

            return false;
        }

        public void InitializeList(List<RegularCoin> a)
        {
            for (int i = 0; i < 100; i++)
            {
                if (i == 1)
                {
                    a.Add(new RegularCoin((400 + i * 400), 300));
                    continue;
                }
                if (i == 5)
                {
                    a.Add(new RegularCoin((400 + i * 400), 300));
                    continue;
                }
                if (i == 6)
                {
                    a.Add(new RegularCoin((400 + i * 400), 250));
                    continue;
                }
                a.Add(new RegularCoin((400 + i * 400), 380));
            }
        }

        public void Draw(RegularCoin regularCoin,SpriteBatch spriteBatch)
        {
            if (!this.isCollected)
                spriteBatch.Draw(regularCoin.imageTexture, new Rectangle(this.X, this.Y, 80, 80), Color.White);
        }

    }
}
