namespace CSharpGame.Models.Collectables.Effects
{
    using Interfaces;

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

        public virtual void ApplyEffect(Character player)
        {
            //TODO: apply effect to player
        }
    }
}