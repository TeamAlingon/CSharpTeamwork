﻿namespace LevelEditor.EventHandlers
{
    using System;

    using LevelEditor.Models;

    using Microsoft.Xna.Framework;

    public delegate void MovementEventHandler(Transform2D sender, TransformMovedEventArgs args);

    public class TransformMovedEventArgs : EventArgs
    {
        public TransformMovedEventArgs(Vector2 movement)
        {
            this.Movement = movement;
        }

        public Vector2 Movement { get; }
    }
}
