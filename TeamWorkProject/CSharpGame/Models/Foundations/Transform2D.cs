namespace CSharpGame.Models.Foundations
{
    using CSharpGame.EventHandlers;

    using Microsoft.Xna.Framework;

    public class Transform2D
    {
        private Transform2D parent;
        private Vector2 position;
        private Vector2 origin;
        private Rectangle boundingBox;

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

        //TODO: resize/rotate/scale with parent?
        public Rectangle BoundingBox
        {
            get
            {
                return this.ScaleIfNeeded(this.boundingBox);
            }
            set
            {
                this.boundingBox = value;
            }
        }

        public Vector2 Origin
        {
            get
            {
                return this.origin;
            }
            set
            {
                if (this.origin == Vector2.Zero)
                {
                    this.origin = this.BoundingBox.Size.ToVector2() / 2;
                }

                this.origin = value;
            }
        }

        public float Rotation { get; set; }

        public float Scale { get; set; }

        public Transform2D Parent
        {
            get
            {
                return this.parent;
            }
            set
            {
                // If changing the parent, detach from the previous parent.
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
            : this(Vector2.Zero, Rectangle.Empty, 1.0f, 1.0f, parentTransform)
        {

        }

        public Transform2D(Vector2 position, Rectangle boundingBox, float scale = 1.0f, float rotation = 0f, Transform2D parent = null)
        {
            this.Position = position;
            this.BoundingBox = boundingBox;
            this.Rotation = rotation;
            this.Parent = parent;
            this.Scale = scale;

            this.PositionChanged += this.UpdateSizePositionWithMovement;
        }

        private Rectangle ScaleIfNeeded(Rectangle inputRectangle)
        {
            Rectangle result = inputRectangle;
            if (this.Scale > 1)
            {
                var width = inputRectangle.Width;
                var height = inputRectangle.Height;
                var scaledWidth = inputRectangle.Width * this.Scale;
                var scaledHeight = inputRectangle.Height * this.Scale;

                var scaledX = this.boundingBox.X - (scaledWidth - width) / 2;
                var scaledY = this.boundingBox.Y - (scaledHeight - height) / 2;

                result = new Rectangle((int)scaledX, (int)scaledY, (int)scaledWidth, (int)scaledHeight);
            }
            else if (this.Scale < 1)
            {
                var width = inputRectangle.Width;
                var height = inputRectangle.Height;
                var scaledWidth = inputRectangle.Width * this.Scale;
                var scaledHeight = inputRectangle.Height * this.Scale;

                var scaledX = this.boundingBox.X + (scaledWidth - width) / 2;
                var scaledY = this.boundingBox.Y + (scaledHeight - height) / 2;

                result = new Rectangle((int)scaledX, (int)scaledY, (int)scaledWidth, (int)scaledHeight);
            }

            return result;
        }

        private void MoveWithParent(Transform2D sender, TransformMovedEventArgs args)
        {
            this.Position -= args.Movement;
        }

        private void UpdateSizePositionWithMovement(Transform2D sender, TransformMovedEventArgs args)
        {
            this.BoundingBox = new Rectangle(this.position.ToPoint(), new Point(this.BoundingBox.Width, this.BoundingBox.Height));
        }
    }
}
