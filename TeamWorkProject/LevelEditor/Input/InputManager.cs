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

        private static Vector2 LastMouseClick { get; set; }

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

                OnPress?.Invoke(new PointerEventDataArgs(curretnPosition, delta));
                
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
            else
            {
                LastMouseClick = Vector2.Zero;
            }
        }

        public static void UpdateKeyboard(GameTime gameTime)
        {
            throw new NotImplementedException();
        }
    }
}
