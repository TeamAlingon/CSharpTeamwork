namespace LevelEditor.Models.UI
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    public class Button : GameObject
    {
        public Texture2D Texture { get; set; }

        public Text Text { get; set; }

        //public Button()
        //{
        //    this.Texture = null;
        //    this.Text.Transform = new Transform2D(this.Transform);
        //    this.Transform.Size = new Rectangle(0, 0, 100, 50);
        //    this.Text.TextContent = "Button";
        //}

        public Button(
            Texture2D texture,
            Text text,
            Transform2D transform,
            Transform2D parentTransform = null)
        {
            this.Texture = texture;

            this.Transform = transform;
            this.Transform.Size = new Rectangle(
                Point.Zero,
                new Point(this.Texture.Width, this.Texture.Height));
            this.Transform.Parent = parentTransform;

            this.Text = text;
            this.Text.Transform.Parent = this.Transform;
        }

        public override void Update(GameTime gameTime)
        {
            this.Text.Update(gameTime);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(this.Texture, this.Transform.Size, Color.White);
            spriteBatch.End();

            this.Text.Draw(gameTime, spriteBatch);
        }
    }
}