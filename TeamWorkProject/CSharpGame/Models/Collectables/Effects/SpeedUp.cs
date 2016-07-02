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

    
 
       
    }
}