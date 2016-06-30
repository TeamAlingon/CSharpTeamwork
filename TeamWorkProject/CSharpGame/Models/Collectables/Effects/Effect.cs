namespace CSharpGame.Models.Collectables.Effects
{
    using Interfaces;
    using Microsoft.Xna.Framework.Graphics;

    public class Effect : IEffect
    {
        private bool isCollected;
        private int timeOfEffect;
        private string image;
        private Texture2D imageTexture;
        private int x;
        private int y;

        public Effect(int x, int y, int duration , string image)
        {
            this.timeOfEffect = duration;
            isCollected = false;
            this.X = x;
            this.Y = y;
            this.image = image;
        }

        public Texture2D ImageTexture2D
        {
            get { return this.imageTexture; }
            set { this.imageTexture = value; }
        }
        public int X
        {
            get { return this.x; }
            set { this.x = value;}
        }

        public int Y
        {
            get { return this.y; }
            set { this.y = value; }
        }

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

        public bool IsCollected
        {
            get { return this.isCollected; }
            protected set { this.isCollected = value; }
        }
        public virtual void ApplyEffect(Character player)
        {
            //TODO: apply effect to player
        }
    }
}