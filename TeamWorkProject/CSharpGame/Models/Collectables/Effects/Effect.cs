namespace CSharpGame.Models.Collectables.Effects
{
    using Interfaces;
    using Microsoft.Xna.Framework.Graphics;

    public class Effect : IEffect
    {
        private bool isCollected;
        private int timeOfEffect;
        private string image;
        private int x;
        private int y;

        public Effect(int x, int y, int duration)
        {
            this.timeOfEffect = duration;
            isCollected = false;
            this.X = x;
            this.Y = y;
        }

        public Texture2D ImageTexture2D { get; set; }

        public int X { get; set; }

        public int Y { get; set; }


        public void Collect(Character player)
        {
            isCollected = false;
        }
        
        public bool IsAvailable()
        {
            if (isCollected)
            {
                return true;
            }

            return false;
        }

        public void Update()
        {
            if (timeOfEffect <= 0)
            {
                //TODO: if duration == 0 stop effect and bring back default state
            }
            else
            {
                timeOfEffect -= 1;
            }
        }
       
        public  string GetImage()
        {
            return this.image;
        }

        public virtual void ApplyEffect(Character player)
        {
            //TODO: apply effect to player
        }
    }
}