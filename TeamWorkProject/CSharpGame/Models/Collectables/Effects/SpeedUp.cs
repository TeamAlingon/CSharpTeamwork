namespace CSharpGame.Models.Collectables.Effects
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    public class SpeedUp : Effect
    {
        private const string imageSpeedUp = "Images/SpeedUp";
      
        public SpeedUp(int x, int y, int time)
            :base(x, y, time, imageSpeedUp)
       {
           
       }

       public string ImageSpeedUp => imageSpeedUp;

    
        public void Draw(SpriteBatch spriteBatch)
        {
            if (!base.IsAvailable())
                spriteBatch.Draw(this.ImageTexture2D, new Rectangle(this.X, this.Y, 80, 80), Color.White);
        }
       
    }
}