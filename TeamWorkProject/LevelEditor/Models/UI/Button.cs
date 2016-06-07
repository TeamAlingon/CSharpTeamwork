namespace LevelEditor.Models.UI
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    public class Button : GameObject
    {
        public Text Text { get; set; }

        public Button()
        {
            this.Text.Transform = new Transform2D(this.Transform);
            this.Transform.Size = new Rectangle(0, 0, 100, 50);
            this.Text.TextContent = "Button";
        }

        public Button(
            Text text,
            Transform2D transform,
            Transform2D parentTransform = null)
        {
            this.Text = text;
            this.Text.Transform.Parent = this.Transform;

            this.Transform = transform;
            this.Transform.Parent = parentTransform;
        }

        public override void Update(GameTime gameTime)
        {
            throw new System.NotImplementedException();
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            throw new System.NotImplementedException();
        }
    }
}