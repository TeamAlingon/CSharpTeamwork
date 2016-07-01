namespace CSharpGame.EventHandlers
{
    using System;

    using LevelEditor.Models;

    using Microsoft.Xna.Framework;

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
