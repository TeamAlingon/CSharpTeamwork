namespace CSharpGame.EventHandlers
{
    using System;

    using Microsoft.Xna.Framework;

    using Transform2D = CSharpGame.Models.Foundations.Transform2D;

    public delegate void MovementEventHandler(Transform2D sender, TransformMovedEventArgs args);

    public class TransformMovedEventArgs : EventArgs
    {
        public Vector2 Movement { get; }

        public TransformMovedEventArgs(Vector2 movement)
        {
            this.Movement = movement;
        }
    }
}
