namespace CSharpGame.Models
{
    using System.Collections.Generic;
    using System.Diagnostics;

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
        private const float DefaultSpeed = 7f;
        private const float DefaultGroundPosition = 630;
        private const float DefaultJumpVelocity = 4f;
        private const float DefaultVelocityDampingSpeed = 0.1f;
        private readonly Dictionary<string, Vector2> movementVectors;

        private float currentJumpVelocity;

        public Character(Dictionary<string, Animation> animations, InputManager inputManager, GameRepository repository)
            : this(Vector2.Zero, animations, inputManager, repository)
        {
        }

        public Character(Vector2 position, Dictionary<string, Animation> animations, InputManager inputManager, GameRepository repository)
            : base(new Transform2D(position, Rectangle.Empty), animations, repository)
        {
            this.movementVectors = new Dictionary<string, Vector2>
                                 {
                                     { "right", new Vector2(1, 0) },
                                     { "left", new Vector2(-1, 0) },
                                     { "up", new Vector2(0, -1) },
                                     { "down", new Vector2(0, 1) }
                                 };

            this.Inventory = new Inventory();
            this.CurrentSpeed = DefaultSpeed;

            inputManager.Jump += this.OnJump;
            inputManager.MoveRight += this.MoveRight;
            inputManager.MoveLeft += this.MoveLeft;
        }

        public Inventory Inventory { get; }

        public int ScoreCoins { get; set; }

        public float CurrentSpeed { get; set; }

        public bool IsGrounded => this.Position.Y >= DefaultGroundPosition;

        public Texture2D Texture => this.Animations[this.CurrentAnimationKey].SpriteSheet;

        public Rectangle CurrentFrame => this.Animations[this.CurrentAnimationKey].CurrentFrame;

        public Rectangle BoundingBox => new Rectangle(this.Position.ToPoint(), this.CurrentFrame.Size);

        private void MoveRight()
        {
            if (this.State != CharacterState.Jumping)
            {
                this.State =  CharacterState.RunningRight;
                this.Orientation = SpriteEffects.None;
            }

            this.Transform.Position += this.movementVectors["right"] * this.CurrentSpeed;
        }

        private void MoveLeft()
        {
            if (this.State != CharacterState.Jumping)
            {
                this.State = CharacterState.RunningLeft;
                this.Orientation = SpriteEffects.FlipHorizontally;
            }

            this.Transform.Position += this.movementVectors["left"] * this.CurrentSpeed;
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
            if (!this.IsGrounded && this.State != CharacterState.Jumping)
            {
                this.Transform.Position += this.movementVectors["down"] * this.CurrentSpeed;
            }

            if (this.State == CharacterState.Jumping)
            {
                this.HandleJumping();
            }

            Debug.WriteLine(this.currentJumpVelocity);
            Debug.WriteLine(this.State);

            this.Inventory.Update(gameTime, this);
            this.UpdateCurrentAnimation(gameTime);
        }

        private void HandleJumping()
        {
            this.Transform.Position += this.movementVectors["up"] * DefaultSpeed * this.currentJumpVelocity;
            this.currentJumpVelocity -= DefaultVelocityDampingSpeed;

            if (this.IsGrounded)
            {
                this.State = CharacterState.RunningRight;
            }
        }

        public void Collect(ICollectable item)
        {
            item.GetCollected(this);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            this.DebugCollision(spriteBatch);

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

        // Debugging collision, might come in handy again.
        private Texture2D tex;
        private void DebugCollision(SpriteBatch spriteBatch)
        {
            if (this.tex == null)
            {
                if (!this.BoundingBox.Equals(Rectangle.Empty))
                {
                    this.tex = new Texture2D(
                    this.GameRepository.GraphicsDevice,
                    this.BoundingBox.Width,
                    this.BoundingBox.Height);


                    Color[] data = new Color[this.BoundingBox.Width * this.BoundingBox.Height];
                    for (int i = 0; i < data.Length; ++i) data[i] = Color.Chocolate;
                    this.tex.SetData(data);
                }
            }
            else
            {
                spriteBatch.Draw(
                    texture: this.tex, 
                    destinationRectangle: this.BoundingBox, 
                    color: Color.White, 
                    origin: this.Transform.Origin);
            }
        }

    }
}