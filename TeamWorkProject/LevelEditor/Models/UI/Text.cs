namespace LevelEditor.Models.UI
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    public class Text : GameObject
    {
        public string Content { get; set; }

        public SpriteFont SpriteFont { get; set; }

        public Text(string content, SpriteFont spriteFont, Transform2D parentTransform = null)
            : this(content, spriteFont, new Transform2D(), parentTransform)
        {
        }

        public Text(string content, SpriteFont spriteFont, Transform2D transform, Transform2D parentTransform = null)
        {
            this.Transform = transform;
            this.Transform.Parent = parentTransform;
            this.Content = content;
            this.SpriteFont = spriteFont;
        }

        public override void Update(GameTime gameTime)
        {
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.DrawString(this.SpriteFont, this.Content, this.Transform.Position, Color.Black);
            spriteBatch.End();
        }
    }
}
