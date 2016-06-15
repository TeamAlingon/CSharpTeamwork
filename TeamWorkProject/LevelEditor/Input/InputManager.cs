namespace LevelEditor.Input
{
    using System;

    using LevelEditor.Data;
    using LevelEditor.EventHandlers;
    using LevelEditor.Models;

    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Input;

    public static class InputManager
    {
        public static event PointerEventHandler OnPress;

        public static event PointerEventHandler OnDrag;

        public static event PointerEventHandler OnRelease;

        private static Camera2D currentCamera;

        private static Vector2 LastMouseClick { get; set; }

        private static bool Pressed { get; set; }

        public static void UpdateMouse(GameTime gameTime, MouseState mState)
        {
            if (currentCamera == null)
            {
                currentCamera = Repository.GetSelectedCamera();
            }

            if (mState.LeftButton == ButtonState.Pressed)
            {
                // Taking into account the camera position since it is causing issues when the camera is moved.
                var currentMousePosition = mState.Position.ToVector2() + currentCamera.Transform.Position;
                var delta = Vector2.Zero;
                if (!LastMouseClick.Equals(Vector2.Zero))
                {
                    delta = currentMousePosition - LastMouseClick;
                }

                if (!Pressed)
                {
                    OnPress?.Invoke(new PointerEventDataArgs(currentMousePosition, delta));
                    Pressed = true;
                }

                LastMouseClick = currentMousePosition;

                if (delta != Vector2.Zero)
                {
                    OnDrag?.Invoke(new PointerEventDataArgs(currentMousePosition, delta));
                }

                if (LastMouseClick.Equals(Vector2.Zero))
                {
                    LastMouseClick = currentMousePosition;
                }
            }
            else if (Pressed)
            {
                OnRelease?.Invoke(new PointerEventDataArgs(LastMouseClick, Vector2.Zero));
                Pressed = false;

                LastMouseClick = Vector2.Zero;
            }
        }

        public static void UpdateKeyboard(GameTime gameTime)
        {
            throw new NotImplementedException();
        }
    }
}
