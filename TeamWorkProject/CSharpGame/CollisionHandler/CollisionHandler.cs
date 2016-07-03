namespace CSharpGame.CollisionHandler
{
    using CSharpGame.Interfaces;
    using CSharpGame.Models;

    using Microsoft.Xna.Framework;

    class CollisionHandler
    {
        public bool Intersect(Character charater, ICollectable coin)
        {
            Rectangle characterRectangle = charater.BoundingBox;
            Rectangle coinRectangle = coin.Transform.BoundingBox;
            if (!coin.IsCollected)
            {
                if (coinRectangle.Intersects(characterRectangle) || characterRectangle.Intersects(coinRectangle))
                {
                    charater.Score++;
                    coin.IsCollected = true;
                    return true;
                }
            }

            return false;
        }
    }
}
