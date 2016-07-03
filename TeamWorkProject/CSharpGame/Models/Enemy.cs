using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
namespace CSharpGame.Models
{
    using Animations;
    class Enemy
    {
        private const int enemySpeed = 2;
        private const string imageEnemys = "Images/enemies";
        private Texture2D imagetTexture;
        private int x;
        private int y;
        public Vector2 Position { get; private set; }
        private Point enemyFrameSize = new Point(127, 229);
        private Point enemySheetSize = new Point(8, 2);
        private Point enemyCurrentFrame = new Point(0, 0);
        private float timeBetweenFrames = 0.1f;
        private float timeSinceLastFrame;
        public Enemy(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }
        public Enemy()
        {
        }

        public Texture2D ImageTexture2D
        {
            get { return this.imagetTexture; }
            set
            {
                this.imagetTexture = value;
            }
        }
        public string GetImage()
        {
            return imageEnemys;
        }
        public int X { get { return this.x; } set { this.x = value; } }
        public int Y { get { return this.y; } private set { this.y = value; } }
        public static void InitializeEnemies(List<Enemy> a)
        {
            a.Add(new Enemy(1020, 280));
        }
        public virtual void Draw(Enemy enemy, SpriteBatch spriteBatch)
        {

            spriteBatch.Draw(enemy.ImageTexture2D, new Vector2(this.X, this.Y), new Rectangle(enemyCurrentFrame.X * enemyFrameSize.X,
              enemyFrameSize.Y, enemyFrameSize.X, enemyFrameSize.Y), Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0);


        }
        private static bool flag;
        static Enemy()
        {
            flag = false;
        }
        public void Update(GameTime gametime)
        {
            if (this.X == 1900)
            {
                flag = true;
            }
            if (this.X == 1020)
            {
                flag = false;
            }
            if (flag)
            {
                this.X -= enemySpeed;
            }
            else
            {
                this.X += enemySpeed;
            }
            UpdateSprites(gametime);
        }
        private void UpdateSprites(GameTime gameTime)
        {


            var deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            this.timeSinceLastFrame += deltaTime;
            if (this.timeSinceLastFrame >= this.timeBetweenFrames)
            {
                timeSinceLastFrame = 0;
                IncremntFrames();
            }


        }
        private void IncremntFrames()
        {

            enemyCurrentFrame.X += 1;
            if (enemyCurrentFrame.X>=5)
            {
                enemyCurrentFrame.X = 0;
            }
            
        }
    }
}
