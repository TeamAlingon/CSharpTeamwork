namespace CSharpGame.Models
{
    using CSharpGame.Data;
    using CSharpGame.Models.Foundations;

    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    public class Camera2D : GameObject
    {
        private Viewport viewport;

        public float Speed { get; set; }

        public float Zoom { get; set; }

        public Camera2D(GameRepository gameRepository, Viewport viewport)
            : base(new Transform2D(Vector2.Zero, viewport.Bounds), gameRepository)
        {
            this.viewport = viewport;

            this.Speed = 250;
            this.Zoom = 1;
        }

        public Matrix GetViewMatrix()
        {
            return
                Matrix.CreateTranslation(new Vector3(-this.Transform.Position, 0.0f)) *
                Matrix.CreateTranslation(new Vector3(-this.Transform.Origin, 0.0f)) *
                Matrix.CreateRotationZ(this.Transform.Rotation) *
                Matrix.CreateScale(this.Zoom, this.Zoom, 1) *
                Matrix.CreateTranslation(new Vector3(this.Transform.Origin, 0.0f));
        }

        public void LookAt(Vector2 targetPosition)
        {
            this.Transform.Position = targetPosition - this.Transform.BoundingBox.Size.ToVector2() / 2f;
        }
    }
}
