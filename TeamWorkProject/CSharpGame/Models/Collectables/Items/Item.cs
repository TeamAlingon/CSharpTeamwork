namespace CSharpGame.Models.Collectables.Items
{
    using CSharpGame.Data;
    using CSharpGame.Models.Foundations;

    using Interfaces;

    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    public abstract class Item : TexturedGameObject, ICollectable
    {
        protected Item(int x, int y, string image, GameRepository gameRepository)
            : base(new Transform2D(new Vector2(x, y), Rectangle.Empty), image, gameRepository)
        {
            this.IsCollected = false;
        }

        public virtual Character Collector { get; set; }

        public bool IsCollected { get; set; }

        public bool HasBeenUsed { get; protected set; }

        public Rectangle BoundingBox => this.Transform.BoundingBox;

        public string GetImage()
        {
            return this.TextureFilePath;
        }

        public virtual void GetCollected(Character player)
        {
            this.IsCollected = true;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            //this.DrawBoundingBox(spriteBatch);

            if (!this.IsCollected)
            {
                spriteBatch.Draw(this.Texture, this.Transform.BoundingBox, Color.White);
            }
        }


        // Debugging collision, might come in handy again.
        private Texture2D tex;
        private void DrawBoundingBox(SpriteBatch spriteBatch)
        {
            if (this.tex == null)
            {
                if (!this.BoundingBox.Equals(Rectangle.Empty))
                {
                    this.tex = new Texture2D(
                    this.GameRepository.GraphicsDevice,
                    this.BoundingBox.Width,
                    this.BoundingBox.Height);

                    Color[] data = new Color[this.BoundingBox.Width * this.BoundingBox.Height];
                    for (int i = 0; i < data.Length; ++i) data[i] = Color.Chocolate;
                    this.tex.SetData(data);
                }
            }
            else
            {
                spriteBatch.Draw(
                    texture: this.tex,
                    destinationRectangle: this.BoundingBox,
                    color: Color.White,
                    origin: this.Transform.Origin);
            }
        }
    }
}