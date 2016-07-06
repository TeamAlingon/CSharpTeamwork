namespace CSharpGame.Models.Foundations
{
    using System.Xml.Serialization;

    using CSharpGame.Data;
    using CSharpGame.Interfaces;

    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    public class TexturedGameObject : GameObject, IDrawableGameObject
    {
        public string TextureFilePath { get; set; }

        private Texture2D texture;

        [XmlIgnore]
        public Texture2D Texture
        {
            get
            {
                return this.texture ?? (this.Texture = this.GameRepository.LoadContent<Texture2D>(this.TextureFilePath));
            }
            set
            {
                if (this.Transform.BoundingBox.Equals(Rectangle.Empty))
                {
                    this.Transform.BoundingBox = new Rectangle(
                        this.Transform.Position.ToPoint(),
                        new Point(value.Width, value.Height));
                }

                this.texture = value;
            }
        }

        public TexturedGameObject(GameRepository gameRepository)
            : this(new Transform2D(), null, gameRepository)
        {
        }

        public TexturedGameObject(GameRepository gameRepository, Texture2D texture)
            : this(new Transform2D(), null, gameRepository)
        {
            this.Texture = texture;
            this.TextureFilePath = texture.Name;
        }

        public TexturedGameObject(GameRepository gameRepository, string path)
            : this(new Transform2D(), path, gameRepository)
        {
            this.TextureFilePath = path;
        }

        public TexturedGameObject(Transform2D transform, string path, GameRepository gameRepository)
            : base(transform, gameRepository)
        {
            this.TextureFilePath = path;
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(this.Texture, this.Transform.BoundingBox, Color.White);
        }
    }
}