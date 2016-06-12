namespace LevelEditor.Models.UI
{
    using LevelEditor.EventHandlers;
    using LevelEditor.Input;

    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    public class Button : TexturedGameObject
    {
        public event PointerEventHandler OnPress;

        public Text Text { get; set; }

        public bool IsPressed { get; private set; }

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
            this.Transform.Size = new Rectangle(Point.Zero, new Point(this.Texture.Width / 2, this.Texture.Height / 2));
            this.Transform.Parent = parentTransform;

            this.Text = text;
            this.Text.Transform.Parent = this.Transform;

            this.CenterTextInButton();

            InputManager.OnPress += this.HandleButtonPress;
            InputManager.OnRelease += this.HandleButtonRelease;
        }

        private void CenterTextInButton()
        {
            var textSize = this.Text.SpriteFont.MeasureString(this.Text.TextContent);
            var textPositionDisplacement = 
                (new Vector2(this.Transform.Size.Width, this.Transform.Size.Height) - textSize) / 2;

            this.Text.Transform.Position += textPositionDisplacement;
        }

        public override void Update(GameTime gameTime)
        {
            this.Text.Update(gameTime);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            base.Draw(gameTime, spriteBatch);

            this.Text.Draw(gameTime, spriteBatch);
        }

        private void HandleButtonPress(PointerEventDataArgs args)
        {
            if (this.Transform.Size.Contains(args.Position))
            {
                this.IsPressed = true;
            }
        }

        private void HandleButtonRelease(PointerEventDataArgs args)
        {
            if (this.IsPressed)
            {
                if (this.Transform.Size.Contains(args.Position))
                {
                    this.IsPressed = false;
                    this.OnPress?.Invoke(args);
                }
            }
        }
    }
}