
namespace CSharpGame.Models
{

    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    public class Character
    {
        private string imageName = "Images/MainChar";
        private int x = 20;
        private int y = 20;
        public int X { get; set; }
        public int Y { get; set; }
        //private Rectangle rectangle = new Rectangle(X, Y, 1000, 200);
        //  public Rectangle Rectangle { get; set; }

        Texture2D imageTexture;
        public string GetImage()
        {
            return imageName;
        }

        public void MoveRight()
        {
            this.X += 5;
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