namespace CSharpGame.Input
{
    using System.Linq;

    using CSharpGame.EventHandlers;

    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Input;

    using MonoGame.Extended;

    public class InputManager : GameComponent
    {
        public event PointerEventHandler PointerPress;

        public event PointerEventHandler PointerDrag;

        public event PointerEventHandler PointerRelease;

        public event GameActionEventHandler MoveRight;

        public event GameActionEventHandler MoveLeft;

        public event GameActionEventHandler Jump;

        public event GameActionEventHandler Attack;

        private Camera2D currentCamera;

        public Keys[] MoveRightKeys { get; set; }

        public Keys[] MoveLeftKeys { get; set; }

        public Keys[] JumpKeys { get; set; }

        public Keys[] ExitKeys { get; set; }

        public InputManager(Game game, Camera2D camera)
            : base(game)
        {
            game.Components.Add(this);
            this.currentCamera = camera;

            this.MoveRightKeys = new[] { Keys.Right, Keys.D };
            this.MoveLeftKeys = new[] { Keys.Left, Keys.A };
            this.JumpKeys = new[] { Keys.Up, Keys.W };
            this.ExitKeys = new[] { Keys.Escape };
        }

        private Vector2 LastMouseClick { get; set; }

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
                if (!this.LastMouseClick.Equals(Vector2.Zero))
                {
                    delta = currentMousePosition - this.LastMouseClick;
                }

                if (!this.Pressed)
                {
                    this.PointerPress?.Invoke(new PointerEventDataArgs(currentMousePosition, delta));
                    this.Pressed = true;
                }

                this.LastMouseClick = currentMousePosition;

                if (delta != Vector2.Zero)
                {
                    this.PointerDrag?.Invoke(new PointerEventDataArgs(currentMousePosition, delta));
                }

                if (this.LastMouseClick.Equals(Vector2.Zero))
                {
                    this.LastMouseClick = currentMousePosition;
                }
            }
            else if (this.Pressed)
            {
                this.PointerRelease?.Invoke(new PointerEventDataArgs(this.LastMouseClick, Vector2.Zero));
                this.Pressed = false;

                this.LastMouseClick = Vector2.Zero;
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
                this.MoveRight?.Invoke();
            }

            if (this.MoveLeftKeys.Any(kState.IsKeyDown))
            {
                this.MoveLeft?.Invoke();
            }

            if (this.JumpKeys.Any(kState.IsKeyDown))
            {
                this.Jump?.Invoke();
            }

            // TODO: Attack
        }
    }
}
