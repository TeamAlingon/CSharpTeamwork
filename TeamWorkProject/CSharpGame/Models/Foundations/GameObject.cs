namespace CSharpGame.Models.Foundations
{
    using System;

    using CSharpGame.Data;
    using CSharpGame.Interfaces;

    using Microsoft.Xna.Framework;

    public class GameObject : IComparable<GameObject>, IGameObject
    {
        public Transform2D Transform { get; set; }

        public int UpdateOrder { get; set; }

        public Vector2 Position => this.Transform.Position;

        protected GameRepository GameRepository { get; set; }

        protected GameObject(GameRepository gameRepository)
            : this(new Transform2D(), gameRepository)
        {
        }

        protected GameObject(Transform2D transform, GameRepository gameRepository)
        {
            this.Transform = transform;
            this.GameRepository = gameRepository;
        }

        public virtual void Update(GameTime gameTime)
        {
            
        }

        public int CompareTo(GameObject other)
        {
            return other.UpdateOrder - this.UpdateOrder;
        }
    }
}
