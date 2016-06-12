namespace LevelEditor.Models
{
    using LevelEditor.Interfaces;

    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    public class TexturedGameObject : GameObject, IDrawableGameObject
    {
        private Texture2D texture;

        public Texture2D Texture
        {
            get
            {
                return this.texture;
            }
            set
            {
                this.texture = value;

                this.Transform.Size = new Rectangle(
                    this.Transform.Position.ToPoint(),
                    new Point(value.Width, value.Height));
            }
        }

        protected TexturedGameObject()
            :base (new Transform2D())
        {
        }

        public TexturedGameObject(Texture2D texture)
            : this()
        {
            this.Texture = texture;
        }

        public override void Update(GameTime gameTime)
        {
        }

        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(this.Texture, this.Transform.Size, Color.White);
            spriteBatch.End();
        }
    }
}
