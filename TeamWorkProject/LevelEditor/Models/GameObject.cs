namespace LevelEditor.Models
{
    using System.Xml.Serialization;

    using LevelEditor.Interfaces;

    using Microsoft.Xna.Framework;

    [XmlType("GameObject")]
    public abstract class GameObject : IGameObject
    {
        [XmlElement("Transform")]
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
