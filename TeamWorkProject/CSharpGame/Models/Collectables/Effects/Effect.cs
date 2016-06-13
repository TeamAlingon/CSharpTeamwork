namespace CSharpGame.Models.Collectables.Effects
{
    using Interfaces;
    using Microsoft.Xna.Framework.Graphics;

    public class Effect : IEffect
    {
        private bool canBeCollected;
        private int timeOfEffect;

        public Effect(int duration)
        {
            this.timeOfEffect = duration;
            canBeCollected = true;
        }

        public void Collect(Character player)
        {
            canBeCollected = false;
        }

        public bool isAvailable()
        {
            if (canBeCollected)
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

        public Texture2D ImageTexture2D { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public string GetImage()
        {
            throw new System.NotImplementedException();
        }

        public virtual void ApplyEffect(Character player)
        {
            //TODO: apply effect to player
        }
    }
}