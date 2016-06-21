namespace LevelEditor.Models
{
    using System.Xml.Serialization;

    using LevelEditor.Data;
    using LevelEditor.Interfaces;

    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Content;
    using Microsoft.Xna.Framework.Graphics;

    public class TexturedGameObject : GameObject, IDrawableGameObject
    {
        protected ContentManager Content { get; set; }

        public string TextureFilePath { get; set; }

        private Texture2D texture;

        [XmlIgnore]
        public Texture2D Texture
        {
            get
            {
                if (this.texture == null)
                {
                    this.texture = Repository.ContentManager.Load<Texture2D>(this.TextureFilePath);
                }
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

        public TexturedGameObject()
            : base(new Transform2D())
        {
        }

        public TexturedGameObject(Texture2D texture)
            : this()
        {
            this.Texture = texture;
            this.TextureFilePath = texture.Name;
        }

        public TexturedGameObject(string path)
            : this()
        {
            this.TextureFilePath = path;
        }

        public override void Update(GameTime gameTime)
        {
        }

        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch, Matrix viewMatrix)
        {
            spriteBatch.Begin(transformMatrix: viewMatrix);
            spriteBatch.Draw(this.Texture, this.Transform.Size, Color.White);
            spriteBatch.End();
        }
    }
}