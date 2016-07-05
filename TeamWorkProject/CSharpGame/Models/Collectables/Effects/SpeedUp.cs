namespace CSharpGame.Models.Collectables.Effects
{
    using CSharpGame.Data;

    public class SpeedUp : Effect
    {
        private const string ImageSpeedUp = "Images/SpeedUp";

        private readonly float speedBonus;

        public SpeedUp(int x, int y, int time, float speedBonus, GameRepository gameRepository)
            :base(x, y, time, ImageSpeedUp, gameRepository)
        {
            this.speedBonus = speedBonus;
        }

        public override void RemoveEffect()
        {
            this.Collector.CurrentSpeed -= this.speedBonus;
        }

        public override void ApplyEffect()
        {
            this.Collector.CurrentSpeed += this.speedBonus;
        }
    }
}