namespace LevelEditor.Models
{
    using System.Xml.Serialization;

    using LevelEditor.Interfaces;

    using Microsoft.Xna.Framework;

    public abstract class GameObject : IGameObject
    {
        public Transform2D Transform { get; set; }

        protected GameObject()
        {
        }

        protected GameObject(Transform2D transform)
        {
            this.Transform = transform;
        }

        public abstract void Update(GameTime gameTime);
    }
}
