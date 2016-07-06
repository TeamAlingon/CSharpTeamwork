namespace CSharpGame.Models
{
    using CSharpGame.Data;
    using CSharpGame.Models.Foundations;

    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    public class Camera2D : GameObject
    {
        private Viewport viewport;

        private Vector2 offset;

        public float Speed { get; set; }

        public float Zoom { get; set; }

        public Camera2D(GameRepository gameRepository, Viewport viewport, Vector2 offset)
            : base(new Transform2D(Vector2.Zero, viewport.Bounds), gameRepository)
        {
            this.viewport = viewport;
            this.offset = offset;

            this.Speed = 0.9f;
            this.Zoom = 1f;
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

        public void Follow(Vector2 targetPosition)
        {
            targetPosition = this.CenterTargetPositionX(targetPosition);

            var speed = (targetPosition - this.Transform.Position) / 10;

            this.Transform.Position += speed * this.Speed;
        }

        public void LookAt(Vector2 targetPosition)
        {
            this.Transform.Position = this.CenterTargetPosition(targetPosition);
        }

        private Vector2 CenterTargetPositionX(Vector2 targetPosition)
        {
            var viewportSize = this.Transform.BoundingBox.Size.ToVector2();

            return new Vector2(targetPosition.X - viewportSize.X / 2, this.Transform.Position.Y);
        }

        private Vector2 CenterTargetPosition(Vector2 targetPosition)
        {
            var viewportSize = this.Transform.BoundingBox.Size.ToVector2();

            return new Vector2(targetPosition.X - viewportSize.X / 2, targetPosition.Y - viewportSize.Y + this.offset.Y);
        }
    }
}
