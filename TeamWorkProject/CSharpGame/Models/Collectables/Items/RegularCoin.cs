namespace CSharpGame.Models.Collectables.Items
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using System.Collections.Generic;
    using Interfaces;

    public class RegularCoin : Item
    {
        private const string imageCoins = "Images/Coin";

        public RegularCoin(int x, int y)
            : base(x,y, imageCoins)
        {
          
        }
   
        public void InitializeList(List<ICollectable> a)
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


    }
}
