namespace CSharpGame.Models.Collectables.Items
{
    using Interfaces;

    public  class Item : ICollectable
    {
        private bool canBeCollected;

        public Item()
        {
            canBeCollected = true;
        }

        public void Collect(Character player)
        {
            canBeCollected = false;
        }

        public  bool isAvailable()
        {
            if (canBeCollected)
            {
                return true;
            }
            return false;
        }

        public void Update()
        {
            throw new System.NotImplementedException();
        }
    }
}