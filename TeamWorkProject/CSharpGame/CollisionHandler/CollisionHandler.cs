namespace CSharpGame.CollisionHandler
{
    using CSharpGame.Interfaces;
    using CSharpGame.Models;

    using Microsoft.Xna.Framework;

    class CollisionHandler
    {
        public bool Intersect(Character charater, ICollectable collectable)
        {
            Rectangle characterRectangle = charater.BoundingBox;
            Rectangle coinRectangle = collectable.BoundingBox;
            if (!collectable.IsCollected)
            {
                if (coinRectangle.Intersects(characterRectangle) || characterRectangle.Intersects(coinRectangle))
                {
                    collectable.Collector = charater;
                    charater.Collect(collectable);
                    collectable.IsCollected = true;
                    return true;
                }
            }

            return false;
        }
    }
}
