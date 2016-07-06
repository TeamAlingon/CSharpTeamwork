namespace CSharpGame.Input
{
    using System.Linq;

    using CSharpGame.Models;

    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Input;

    public class PlayerInput : CharacterInput
    {

        private readonly Camera2D currentCamera;

        public Keys[] MoveRightKeys { get; set; }

        public Keys[] MoveLeftKeys { get; set; }

        public Keys[] JumpKeys { get; set; }

        public Keys[] ExitKeys { get; set; }

        public PlayerInput(Game game, Camera2D camera)
            : base(game)
        {
            game.Components.Add(this);
            this.currentCamera = camera;

            this.MoveRightKeys = new[] { Keys.Right, Keys.D };
            this.MoveLeftKeys = new[] { Keys.Left, Keys.A };
            this.JumpKeys = new[] { Keys.Up, Keys.W };
            this.ExitKeys = new[] { Keys.Escape };
        }

        private Vector2 LastPointerPress { get; set; }

        private bool Pressed { get; set; }

        public override void Update(GameTime gameTime)
        {
            var mouseState = Mouse.GetState();
            var keyboardState = Keyboard.GetState();

            this.UpdateMouse(gameTime, mouseState);
            this.UpdateKeyboard(gameTime, keyboardState);
        }

        private void UpdateMouse(GameTime gameTime, MouseState mState)
        {
            if (mState.LeftButton == ButtonState.Pressed)
            {
                // Taking into account the camera position since it is causing issues when the camera is moved.
                var currentMousePosition = mState.Position.ToVector2() + this.currentCamera.Position;
                var delta = Vector2.Zero;
                if (!this.LastPointerPress.Equals(Vector2.Zero))
                {
                    delta = currentMousePosition - this.LastPointerPress;
                }

                if (!this.Pressed)
                {
                    this.InvokePointerPress(currentMousePosition, delta);
                    this.Pressed = true;
                }

                this.LastPointerPress = currentMousePosition;

                if (delta != Vector2.Zero)
                {
                    this.InvokePointerDrag(currentMousePosition, delta);
                }

                if (this.LastPointerPress.Equals(Vector2.Zero))
                {
                    this.LastPointerPress = currentMousePosition;
                }
            }
            else if (this.Pressed)
            {
                this.InvokePointerRelease(this.LastPointerPress, Vector2.Zero);
                this.Pressed = false;

                this.LastPointerPress = Vector2.Zero;
            }
        }

        private void UpdateKeyboard(GameTime gameTime, KeyboardState kState)
        {
            if (this.ExitKeys.Any(kState.IsKeyDown))
            {
                this.Game.Exit();
            }

            if (this.MoveRightKeys.Any(kState.IsKeyDown))
            {
                this.InvokeMoveRight();
            }

            if (this.MoveLeftKeys.Any(kState.IsKeyDown))
            {
                this.InvokeMoveLeft();
            }

            if (this.JumpKeys.Any(kState.IsKeyDown))
            {
                this.InvokeJump();
            }

            // TODO: Attack
        }
    }
}
