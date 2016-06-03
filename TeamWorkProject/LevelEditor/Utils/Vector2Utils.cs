namespace LevelEditor.Utils
{
    using System;

    using Microsoft.Xna.Framework;

    public class Vector2Utils
    {
        public static Vector2 Abs(Vector2 vector)
        {
            var result = new Vector2(Math.Abs(vector.X), Math.Abs(vector.Y));

            return result;
        }
    }
}
