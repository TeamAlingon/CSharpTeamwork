namespace CSharpGame.CollisionHandler
{
    using Interfaces;
    using Microsoft.Xna.Framework;
    using Models;
    using Models.Collectables.Items;

    public class CollisionHandler
    {


        public bool Intersect(Character charater, ICollectable coin)
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
    }
}
