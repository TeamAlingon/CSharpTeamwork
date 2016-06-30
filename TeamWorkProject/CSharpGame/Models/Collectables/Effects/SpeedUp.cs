namespace CSharpGame.Models.Collectables.Effects
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    public class SpeedUp : Effect
    {
        private string imageSpeedUp = "Images/SpeedUp";
        private Texture2D imageTexture;

        public SpeedUp(int x, int y,int time)
            :base(x,y,time)
       {
           
       }

       public string ImageSpeedUp => this.imageSpeedUp;

       public string GetImage()
       {
           return this.ImageSpeedUp;
       }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (!base.IsAvailable())
                spriteBatch.Draw(this.ImageTexture2D, new Rectangle(this.X, this.Y, 80, 80), Color.White);
        }

        public Texture2D ImageTexture2D
        {
            get { return this.imageTexture; }
            set { this.imageTexture = value; }
        }
    }
}