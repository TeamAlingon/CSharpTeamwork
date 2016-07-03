namespace CSharpGame.Models.Collectables.Effects
{
    using CSharpGame.Data;

    public class SpeedUp : Effect
    {
        private const string ImageSpeedUp = "Images/SpeedUp";

        public SpeedUp(int x, int y, int time, GameRepository gameRepository)
            :base(x, y, time, ImageSpeedUp, gameRepository)
       {
       }
    }
}