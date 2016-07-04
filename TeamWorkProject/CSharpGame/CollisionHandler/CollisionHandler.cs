namespace CSharpGame.CollisionHandler
{
    using Interfaces;
    using Microsoft.Xna.Framework;
    using Models;
    using Models.Collectables.Items;

    public class CollisionHandler
    {

        public static bool EnemyColide(Character character,Enemy enemy)
        {
            Rectangle characterRectangle = new Rectangle(character.Position.ToPoint(), new Point(100, 100));
            Rectangle enemyRectangle = new Rectangle(enemy.X, enemy.Y, 100, 220);
            if(enemyRectangle.Intersects(characterRectangle))
            {
                throw new System.Exception();
            }
            if(enemy.X==1020)
            {
                return true;
            }
            if(enemy.X==1900)
            {
                return true;
            }
            return false;
        }
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
