namespace LevelEditor.Data
{
    using System.Collections.Generic;

    using LevelEditor.Interfaces;
    using LevelEditor.Models;

    public class Repository
    {
        public static readonly List<IDrawableGameObject> GameObjects = new List<IDrawableGameObject>();

        public static readonly List<Camera2D> Cameras = new List<Camera2D>();

        public static int SelectedCameraIndex { get; set; }

        public static Camera2D GetSelectedCamera()
        {
            return Cameras[SelectedCameraIndex];
        }
    }
}
