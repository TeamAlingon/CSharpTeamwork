namespace CSharpGame.Models.Collectables.Items
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using System.Collections.Generic;

    public class RegularCoin : Item
    {
        private const string imageCoins = "Images/Coin";

        public RegularCoin(int x, int y)
            : base(x,y, imageCoins)
        {
          
        }

        public bool Intersect(Character charater, RegularCoin coin)
        {
            Rectangle characterRectangle = new Rectangle(charater.Position.ToPoint(), new Point(125, 125));
            Rectangle coinRectangle = new Rectangle(coin.X, coin.Y, 80, 80);
            if (!coin.IsAvailable())
            {
                if (coinRectangle.Intersects(characterRectangle))
                {
                    coin.IsCollected = true;
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
            if (!this.IsAvailable())
                spriteBatch.Draw(regularCoin.ImageTexture2D, new Rectangle(this.X, this.Y, 80, 80), Color.White);
        }

    }
}
