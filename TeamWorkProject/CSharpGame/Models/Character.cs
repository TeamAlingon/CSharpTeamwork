namespace CSharpGame.Models
{
    using System.Collections.Generic;
    using System.Diagnostics;

    using CSharpGame.Enums;
    using CSharpGame.Models.Animations;

    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    public class Character
    {
        private const float MovementSpeed = 5f;

        private const float GroundPosition = 345f;

        private const string ImageFile = "Images/maincharacter";

        private const float DefaultJumpVelocity = MovementSpeed * 3;

        private const float VelocityDampingSpeed = 0.3f;

        private readonly Dictionary<string, Vector2> movements;

        private float currentJumpVelocity = 0;

        private string currentAnimationKey;

        public Character(Dictionary<string, Animation> animations)
        {
            this.Animations = animations;
            this.movements = new Dictionary<string, Vector2>
                                 {
                                     { "right", new Vector2(MovementSpeed, 0) },
                                     { "left", new Vector2(-MovementSpeed, 0) },
                                     { "up", new Vector2(0, -MovementSpeed) },
                                     { "down", new Vector2(0, MovementSpeed) }
                                 };

            this.CurrentAnimationKey = "running";
            this.Orientation = SpriteEffects.None;
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

        public Rectangle BoundingBox => this.Animations[this.CurrentAnimationKey].CurrentFrame;

        public Texture2D Texture => this.Animations[this.CurrentAnimationKey].SpriteSheet;

        public Rectangle CurrentFrame => this.Animations[this.CurrentAnimationKey].CurrentFrame;

        public void Move(GameTime gameTime, params string[] movementParams)
        {
            foreach (var movement in movementParams)
            {
                if ((movement == "right" || movement == "left") && this.State != CharacterState.Jumping)
                {
                    this.State = movement == "right" ? CharacterState.RunningRight : CharacterState.RunningLeft;
                    this.Orientation = movement == "right" ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
                }
                if (movement == "jump" && this.State != CharacterState.Jumping && this.IsGrounded)
                {
                    this.State = CharacterState.Jumping;
                    this.currentJumpVelocity = DefaultJumpVelocity;
                }

                if (this.movements.ContainsKey(movement))
                {
                    this.Position += this.movements[movement];
                }
            }

            if (!this.IsGrounded)
            {
                this.Position += this.movements["down"];
            }

            if (this.State == CharacterState.Jumping)
            {
                this.HandleJumping();
            }

            Debug.WriteLine(this.currentJumpVelocity);
            Debug.WriteLine(this.State);

            this.UpdateCurrentAnimation(gameTime);
        }

        private void HandleJumping()
        {
            this.Position += this.movements["up"] * (this.currentJumpVelocity / 3);
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
    }
}