namespace LevelEditor.Data
{
    using System.Collections.Generic;

    using LevelEditor.Interfaces;

    public class Repository
    {
        public static readonly List<IDrawableGameObject> GameObjects = new List<IDrawableGameObject>();
    }
}
