namespace CSharpGame.Models.Collectables.Items
{
    using Interfaces;
    using Microsoft.Xna.Framework.Graphics;

    public  class Item : ICollectable
    {
        private bool isCollected;
        private int x;
        private int y;
        private string image;
        private Texture2D imageTexture;

        public Item(int x, int y, string image)
        {
            this.X = x;
            this.Y = y;
            this.image = image;
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
            return this.image;
        }

      
        public void Collect(Character player)
        {
            isCollected = true;
        }

        
        public  bool IsAvailable()
        {
            if (IsCollected)
            {
                return true;
            }
            return false;
        }

        public Texture2D ImageTexture2D
        {
            get { return this.imageTexture; }
            set { this.imageTexture = value; }
        }

        public void Update()
        {
            throw new System.NotImplementedException();
        }

        protected bool IsCollected
        {
            get { return this.isCollected; }
            set { this.isCollected = value; }
        }

       
    }
}