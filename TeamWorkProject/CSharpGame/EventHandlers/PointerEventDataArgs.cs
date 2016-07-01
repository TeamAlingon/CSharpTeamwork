namespace CSharpGame.EventHandlers
{
    using System;

    using Microsoft.Xna.Framework;

    public delegate void PointerEventHandler(PointerEventDataArgs args);

    public class PointerEventDataArgs : EventArgs
    {
        public Vector2 Position { get; }

        public Vector2 Delta { get; }

        public PointerEventDataArgs(Vector2 position, Vector2 delta)
        {
            this.Position = position;
            this.Delta = delta;
        }
    }
}
