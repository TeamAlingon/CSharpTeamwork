namespace CSharpGame.Models
{
    using System.Collections.Generic;
    using Enums;
    using Animations;
    
    using Input;

    using Interfaces;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    public class Character
    {
        private const float MovementSpeed = 5f;
        private const float GroundPosition = 345f;
        private const string ImageFile = "Images/maincharacter";
        private float DefaultJumpVelocity = MovementSpeed * 3;
        private const float VelocityDampingSpeed = 0.3f;
        private  Dictionary<string, Vector2> movementVectors;
        private float currentJumpVelocity;
        private string currentAnimationKey;
        private float currentRunVelocity;
        private Inventory inventory;

        public Character(Dictionary<string, Animation> animations, InputManager inputManager)
        {
            this.currentRunVelocity = MovementSpeed;
            this.Animations = animations;
            this.movementVectors = new Dictionary<string, Vector2>
                                 {
                                     { "right", new Vector2(MovementSpeed, 0) },
                                     { "left", new Vector2(-MovementSpeed, 0) },
                                     { "up", new Vector2(0, -MovementSpeed) },
                                     { "down", new Vector2(0, MovementSpeed) }
                                 };

            this.CurrentAnimationKey = "running";
            this.Orientation = SpriteEffects.None;
            this.inventory=new Inventory();

            inputManager.Jump += this.OnJump;
            inputManager.MoveRight += this.MoveRight;
            inputManager.MoveLeft += this.MoveLeft;
        }

        private string CurrentAnimationKey
        {
            get
            {
                return this.currentAnimationKey;
            }
            set
            {
                if (this.currentAnimationKey != null)
                {
                    this.Animations[this.currentAnimationKey].Reset();
                }

                this.currentAnimationKey = value;
            }
        }

        private Dictionary<string, Animation> Animations { get; }

        public bool IsGrounded => this.Position.Y >= GroundPosition;

        public CharacterState State { get; private set; }

        public SpriteEffects Orientation { get; private set; }

        public Vector2 Position { get; private set; }

        public Texture2D Texture => this.Animations[this.CurrentAnimationKey].SpriteSheet;

        public Rectangle CurrentFrame => this.Animations[this.CurrentAnimationKey].CurrentFrame;

        public Rectangle BoundingBox => this.CurrentFrame;

        private void MoveRight()
        {
            if (this.State != CharacterState.Jumping)
            {
                this.State =  CharacterState.RunningRight;
                this.Orientation = SpriteEffects.None;
            }

            this.Position += this.movementVectors["right"];
        }

        private void MoveLeft()
        {
            if (this.State != CharacterState.Jumping)
            {
                this.State = CharacterState.RunningLeft;
                this.Orientation = SpriteEffects.FlipHorizontally;
            }

            this.Position += this.movementVectors["left"];
        }

        private void OnJump()
        {
            if (this.State != CharacterState.Jumping && this.IsGrounded)
            {
                this.State = CharacterState.Jumping;
                this.currentJumpVelocity = this.DefaultJumpVelocity;
            }
        }

        public void Update(GameTime gameTime)
        {
            if (!this.IsGrounded)
            {
                this.Position += this.movementVectors["down"];
            }

            if (this.State == CharacterState.Jumping)
            {
                this.HandleJumping();
            }

            

            if (this.inventory.SpeedUp)
            {
                this.currentRunVelocity +=0.2f;
                this.movementVectors = new Dictionary<string, Vector2>
                                 {
                                     { "right", new Vector2(currentRunVelocity, 0) },
                                     { "left", new Vector2(-currentRunVelocity, 0) },
                                     { "up", new Vector2(0, -currentRunVelocity) },
                                     { "down", new Vector2(0, currentRunVelocity) }
                                 };
            }
            //Debug.WriteLine(this.currentJumpVelocity);
            //Debug.WriteLine(this.State);

            this.UpdateCurrentAnimation(gameTime);
        }

        private void HandleJumping()
        {
            var upVector = new Vector2(0, -MovementSpeed);
            this.Position += upVector * (this.currentJumpVelocity / 3);
            this.currentJumpVelocity -= VelocityDampingSpeed;

            if (this.IsGrounded)
            {
                this.State = CharacterState.RunningRight;
            }
        }

        private void UpdateCurrentAnimation(GameTime gameTime)
        {
            if ((this.State == CharacterState.RunningLeft || this.State == CharacterState.RunningRight)
                && this.CurrentAnimationKey != "running")
            {
                this.CurrentAnimationKey = "running";
            }
            else if (this.State == CharacterState.Jumping 
                && this.CurrentAnimationKey != "runningJump")
            {
                this.CurrentAnimationKey = "runningJump";
            }
            else if (this.State == CharacterState.Attacking)
            {
                // TODO: Attack animation.
            }
            else if (this.State == CharacterState.Idle)
            {
                // TODO: Idle animation
            }

            this.Animations[this.CurrentAnimationKey].Play(gameTime);
        }

        public void Collect(ICollectable item)
        {
            this.inventory.Collect(item);
        }

        public Inventory Inventory => this.inventory;

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(
                this.Texture,
                new Rectangle(this.Position.ToPoint(), new Point(100, 150)),
                this.CurrentFrame,
            Color.White,
                rotation: 0,
                origin: new Vector2(),
                effects: this.Orientation,
                layerDepth: 0f);
        }

    }
}