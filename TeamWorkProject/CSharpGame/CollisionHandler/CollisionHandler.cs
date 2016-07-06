namespace CSharpGame.CollisionHandler
{
    using CSharpGame.Interfaces;
    using CSharpGame.Models;

    using Microsoft.Xna.Framework;

    public class CollisionHandler
    {
        public bool EnemyCollision(Character character, Character enemy)
        {
            Rectangle characterRectangle = character.BoundingBox;
            Rectangle enemyRectangle = enemy.BoundingBox;
            if (enemyRectangle.Intersects(characterRectangle))
            {
                return true;
            }

            return false;
        }

        public bool CollectableCollision(Character charater, ICollectable collectable)
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
