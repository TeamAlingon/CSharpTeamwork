namespace LevelEditor.Models.UI
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using Microsoft.Xna.Framework.Input;

    public class Text : GameObject
    {
        public string TextContent { get; set; }

        public SpriteFont SpriteFont { get; set; }

        public Color FontColor { get; set; }

        public Text(
            string textContent,
            SpriteFont spriteFont,
            Transform2D parentTransform = null)
            : this(textContent, spriteFont, new Transform2D(), parentTransform)
        {
        }

        public Text(
            string textContent,
            SpriteFont spriteFont,
            Transform2D transform,
            Transform2D parentTransform = null)
            : this(textContent, spriteFont, Color.Black, transform, parentTransform)
        {
        }

        public Text(
            string textContent,
            SpriteFont spriteFont,
            Color fontColor,
            Transform2D transform,
            Transform2D parentTransform = null)
        {
            this.TextContent = textContent;
            this.SpriteFont = spriteFont;
            this.FontColor = fontColor;

            this.Transform = transform;
            this.Transform.Parent = parentTransform;
        }

        public override void Update(GameTime gameTime, KeyboardState kbState, MouseState mState)
        {
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.DrawString(this.SpriteFont, this.TextContent, this.Transform.Position, this.FontColor);
            spriteBatch.End();
        }
    }
}
