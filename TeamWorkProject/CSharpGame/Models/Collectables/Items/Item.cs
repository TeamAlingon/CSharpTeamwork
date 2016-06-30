namespace CSharpGame.Models.Collectables.Items
{
    using Interfaces;
    using Microsoft.Xna.Framework.Graphics;

    public  class Item : ICollectable
    {
        private bool isCollected;
        private int x;
        private int y;
        private string Image;

        public Item(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        public int Y
        {
            get { return this.y; }
            set { this.y = value; }

        }

        public int X
        {
            get { return this.x; }
            set { this.x = value; }

        }

        public string GetImage()
        {
            return this.Image;
        }

        public Texture2D ImageTexture2D { get; set; }
        

        public void Collect(Character player)
        {
            isCollected = true;
        }

        
        public  bool IsAvailable()
        {
            if (isCollected)
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