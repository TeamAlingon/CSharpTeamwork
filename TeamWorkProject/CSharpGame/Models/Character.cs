namespace CSharpGame.Models
{
    using System.Collections.Generic;

    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    public class Character
    {
        private const float Speed = 5;

        private readonly Dictionary<string, Vector2> movements;

        private const string ImageName = "Images/maincharacter";

        //private Texture2D imageTexture;
        public Vector2 Position { get; set; }

        //private Rectangle rectangle = new Rectangle(X, Y, 1000, 200);
        //  public Rectangle Rectangle { get; set; }
        public SpriteEffects Orientation { get; set; }

        //public int HorizontalSquareMove { get; set; }
        public Character()
        {
            this.movements = new Dictionary<string, Vector2>
                                 {
                                     { "right", new Vector2(Speed, 0) },
                                     { "left", new Vector2(-Speed, 0) },
                                     { "up", new Vector2(0, -Speed) },
                                     { "down", new Vector2(0, Speed) },
                                     { "jump", new Vector2(0, -Speed * 2) }
                                 };

            this.Orientation = SpriteEffects.None;
        }

        public string GetImage()
        {
            return ImageName;
        }

        public void MoveRight()
        {
            this.Position += this.movements["right"];
        }

        public void MoveLeft()
        {
            this.Position += this.movements["left"];
        }

        public void Jump()
        {
            this.Position += this.movements["jump"];
        }

        public void MoveUp()
        {
            this.Position += this.movements["up"];
        }

        public void MoveDown()
        {
            this.Position += this.movements["down"];
        }
    }
}