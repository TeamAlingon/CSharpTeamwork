
namespace CSharpGame.Models
{

    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    public class Character
    {
        private string imageName = "Images/maincharacter";
        //private Texture2D imageTexture;
        public int X { get; set; }
        public int Y { get; set; }
        //private Rectangle rectangle = new Rectangle(X, Y, 1000, 200);
        //  public Rectangle Rectangle { get; set; }
        public SpriteEffects Orientation { get; set; }
        public int HorizontalSquareMove { get; set; }
        public Character()
        {
            Orientation = SpriteEffects.None;
        }
        public string GetImage()
        {
            return imageName;
        }

        public void MoveRight()
        {
            this.X += 5;
            HorizontalSquareMove += 260;
            if (HorizontalSquareMove > 2000)
            {
                HorizontalSquareMove = 0;
            }
        }

        public void MoveLeft()
        {
            this.X -= 5;
        }

        public void Jump()
        {
            this.Y -= 10;
        }
    }
}