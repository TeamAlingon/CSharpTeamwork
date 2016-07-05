namespace CSharpGame.Models
{
    using System.Collections.Generic;

    using Enums;
    using Animations;

    using CSharpGame.Data;
    using CSharpGame.Input;
    using CSharpGame.Models.Foundations;

    using Interfaces;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    public class Character : SpriteGameObject
    {
        private const float MovementSpeed = 5f;
        private const float GroundPosition = 540;
        private const float DefaultJumpVelocity = MovementSpeed * 3;
        private const float VelocityDampingSpeed = 0.3f;
        private readonly Dictionary<string, Vector2> movementVectors;
        private float currentJumpVelocity;
        private Inventory inventory;

        public Character(Dictionary<string, Animation> animations, InputManager inputManager, GameRepository repository)
            : this(Vector2.Zero, animations, inputManager, repository)
        {
        }

        public Character(Vector2 position, Dictionary<string, Animation> animations, InputManager inputManager, GameRepository repository)
            : base(new Transform2D(position, Rectangle.Empty), animations, repository)
        {
            this.movementVectors = new Dictionary<string, Vector2>
                                 {
                                     { "right", new Vector2(MovementSpeed, 0) },
                                     { "left", new Vector2(-MovementSpeed, 0) },
                                     { "up", new Vector2(0, -MovementSpeed) },
                                     { "down", new Vector2(0, MovementSpeed) }
                                 };

            this.inventory=new Inventory();

            inputManager.Jump += this.OnJump;
            inputManager.MoveRight += this.MoveRight;
            inputManager.MoveLeft += this.MoveLeft;
        }

        public int Score { get; set; }

        public bool IsGrounded => this.Position.Y >= GroundPosition;

        public Texture2D Texture => this.Animations[this.CurrentAnimationKey].SpriteSheet;

        public Rectangle CurrentFrame => this.Animations[this.CurrentAnimationKey].CurrentFrame;

        public Rectangle BoundingBox
        {
            get
            {
                return this.Transform.BoundingBox = new Rectangle(this.Position.ToPoint(), this.CurrentFrame.Size);
            }
        }

        private void MoveRight()
        {
            if (this.State != CharacterState.Jumping)
            {
                this.State =  CharacterState.RunningRight;
                this.Orientation = SpriteEffects.None;
            }

            this.Transform.Position += this.movementVectors["right"];
        }

        private void MoveLeft()
        {
            if (this.State != CharacterState.Jumping)
            {
                this.State = CharacterState.RunningLeft;
                this.Orientation = SpriteEffects.FlipHorizontally;
            }

            this.Transform.Position += this.movementVectors["left"];
        }

        private void OnJump()
        {
            if (this.State != CharacterState.Jumping && this.IsGrounded)
            {
                this.State = CharacterState.Jumping;
                this.currentJumpVelocity = DefaultJumpVelocity;
            }
        }

        public override void Update(GameTime gameTime)
        {
            if (!this.IsGrounded)
            {
                this.Transform.Position += this.movementVectors["down"];
            }

            if (this.State == CharacterState.Jumping)
            {
                this.HandleJumping();
            }

            //Debug.WriteLine(this.currentJumpVelocity);
            //Debug.WriteLine(this.State);

            this.UpdateCurrentAnimation(gameTime);
        }

        private void HandleJumping()
        {
            var upVector = new Vector2(0, -MovementSpeed);
            this.Transform.Position += upVector * (this.currentJumpVelocity / 3);
            this.currentJumpVelocity -= VelocityDampingSpeed;

            if (this.IsGrounded)
            {
                this.State = CharacterState.RunningRight;
            }
        }

        public void Collect(ICollectable item)
        {
            this.inventory.Collect(item);
        }

        public Inventory Inventory => this.inventory;

        // Debugging collision, might come in handy again.
        //private Texture2D tex;
        public void Draw(SpriteBatch spriteBatch)
        {
        //    if (this.tex == null)
        //    {
        //        if (!this.BoundingBox.Equals(Rectangle.Empty))
        //        {
        //            this.tex = new Texture2D(
        //            this.GameRepository.GraphicsDevice,
        //            this.BoundingBox.Width,
        //            this.BoundingBox.Height);


        //            Color[] data = new Color[this.BoundingBox.Width * this.BoundingBox.Height];
        //            for (int i = 0; i < data.Length; ++i) data[i] = Color.Chocolate;
        //            this.tex.SetData(data);
        //        }
        //    }
        //    else
        //    {
        //        spriteBatch.Draw(this.tex, this.BoundingBox, Color.White);
        //    }

            spriteBatch.Draw(
                this.Texture,
                new Rectangle(this.Position.ToPoint(), this.BoundingBox.Size),
                this.CurrentFrame,
                Color.White,
                rotation: 0,
                origin: this.Transform.Origin,
                effects: this.Orientation,
                layerDepth: 0f);
        }

    }
}