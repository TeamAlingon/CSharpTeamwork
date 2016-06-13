namespace CSharpGame.Models.Collectables.Items
{
    using Interfaces;
    using Microsoft.Xna.Framework.Graphics;

    public  class Item : ICollectable
    {
        private bool canBeCollected;
        private int x;
        private int y;
        private string Image;

        public Item(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        public int Y { get; set; }
        public string GetImage()
        {
            return this.Image;
        }

        public int X { get; set; }

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

        public Texture2D ImageTexture2D { get; set; }
    }
}