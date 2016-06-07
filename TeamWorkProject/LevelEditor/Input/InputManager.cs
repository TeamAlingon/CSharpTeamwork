namespace LevelEditor.Input
{
    using System;

    using LevelEditor.EventHandlers;

    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Input;

    public static class InputManager
    {
        public static event PointerEventHandler OnPress;

        public static event PointerEventHandler OnDrag;

        public static event PointerEventHandler OnRelease;

        private static Vector2 LastMouseClick { get; set; }

        private static bool Pressed { get; set; }

        public static void UpdateMouse(GameTime gameTime, MouseState mState)
        {
            if (mState.LeftButton == ButtonState.Pressed)
            {
                var curretnPosition = mState.Position.ToVector2();
                var delta = Vector2.Zero;
                if (!LastMouseClick.Equals(Vector2.Zero))
                {
                    delta = mState.Position.ToVector2() - LastMouseClick;
                }

                if (!Pressed)
                {
                    OnPress?.Invoke(new PointerEventDataArgs(curretnPosition, delta));
                    Pressed = true;
                }

                LastMouseClick = mState.Position.ToVector2();

                if (delta != Vector2.Zero)
                {
                    OnDrag?.Invoke(new PointerEventDataArgs(curretnPosition, delta));
                }

                if (LastMouseClick.Equals(Vector2.Zero))
                {
                    LastMouseClick = mState.Position.ToVector2();
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
