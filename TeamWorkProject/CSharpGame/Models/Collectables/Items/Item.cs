namespace CSharpGame.Models.Collectables.Items
{
    using CSharpGame.Data;
    using CSharpGame.Models.Foundations;

    using Interfaces;

    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    public  class Item : TexturedGameObject, ICollectable
    {
        protected bool isCollected;

        public Item(int x, int y, string image, GameRepository gameRepository)
            : base(new Transform2D(new Vector2(x, y), Rectangle.Empty), image, gameRepository)
        {
            this.isCollected = false;
        }
        public bool IsCollected
        {
            get { return this.isCollected; }
            set { this.isCollected = value; }
        }

        public string GetImage()
        {
            return this.TextureFilePath;
        }

        public void Collect(Character player)
        {
            this.isCollected = true;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (!this.isCollected)
            {
                spriteBatch.Draw(this.Texture, this.Transform.BoundingBox, Color.White);
            }
        }
    }
}