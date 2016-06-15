namespace LevelEditor.Models
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using Microsoft.Xna.Framework.Input;

    public class Camera2D : GameObject
    {
        public float Speed { get; set; }

        public float Zoom { get; set; }

        public Vector2 Origin { get; set; }

        public Camera2D(Viewport viewport)
        {
            this.Transform = new Transform2D(Vector2.Zero, viewport.Bounds, 0);

            this.Speed = 250;
            this.Zoom = 1;
            this.Origin = new Vector2(viewport.Width / 2f, viewport.Height / 2f);
        }

        public Matrix GetViewMatrix()
        {
            return
                Matrix.CreateTranslation(new Vector3(-this.Transform.Position, 0.0f)) *
                Matrix.CreateTranslation(new Vector3(-this.Origin, 0.0f)) *
                Matrix.CreateRotationZ(this.Transform.Rotation) *
                Matrix.CreateScale(this.Zoom, this.Zoom, 1) *
                Matrix.CreateTranslation(new Vector3(this.Origin, 0.0f));
        }

        public override void Update(GameTime gameTime)
        {
            var deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            var keyboardState = Keyboard.GetState();

            // TODO: use the InputManager after the keyboard is implemented.
            if (keyboardState.IsKeyDown(Keys.W))
            {
                this.Transform.Position -= new Vector2(0, this.Speed) * deltaTime;
            }

            if (keyboardState.IsKeyDown(Keys.S))
            {
                this.Transform.Position += new Vector2(0, this.Speed) * deltaTime;
            }

            if (keyboardState.IsKeyDown(Keys.A))
            {
                this.Transform.Position -= new Vector2(this.Speed, 0) * deltaTime;
            }

            if (keyboardState.IsKeyDown(Keys.D))
            {
                this.Transform.Position += new Vector2(this.Speed, 0) * deltaTime;
            }
        }
    }
}
