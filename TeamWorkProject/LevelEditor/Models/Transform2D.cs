namespace LevelEditor.Models
{
    using LevelEditor.EventHandlers;

    using Microsoft.Xna.Framework;

    public class Transform2D
    {
        private Vector2 position;

        private Transform2D parent;
        private Transform2D transform;

        public event MovementEventHandler PositionChanged;

        public Vector2 Position
        {
            get
            {
                return this.position;
            }
            set
            {
                var movement = this.position - value;
                this.position = value;
                this.PositionChanged?.Invoke(this, new TransformMovedEventArgs(movement));
            }
        }

        //TODO: resize and rotate with parent?
        public Rectangle Size { get; set; }

        public float Rotation { get; set; }

        public Transform2D Parent
        {
            get
            {
                return this.parent;
            }
            set
            {
                if (this.parent != null)
                {
                    this.parent.PositionChanged -= this.MoveWithParent;
                }

                this.parent = value;

                if (this.parent != null)
                {
                    this.Position = this.parent.position + this.position;
                    this.parent.PositionChanged += this.MoveWithParent;
                }
            }
        }

        public Transform2D()
            : this(Vector2.Zero, Rectangle.Empty)
        {
        }

        public Transform2D(Transform2D parentTransform)
            : this(Vector2.Zero, Rectangle.Empty, 1.0f, parentTransform)
        {

        }

        public Transform2D(Vector2 position, Rectangle size, float rotation = 1.0f, Transform2D parent = null)
        {
            this.Position = position;
            this.Size = size;
            this.Rotation = rotation;
            this.Parent = parent;

            this.PositionChanged += this.UpdateSizePositionWithMovement;
        }

        private void MoveWithParent(Transform2D sender, TransformMovedEventArgs args)
        {
            this.Position -= args.Movement;
        }

        private void UpdateSizePositionWithMovement(Transform2D sender, TransformMovedEventArgs args)
        {
            this.Size = new Rectangle((int)this.Position.X, (int)this.Position.Y, this.Size.Width, this.Size.Height);
        }
    }
}
