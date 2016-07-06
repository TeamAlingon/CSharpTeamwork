namespace CSharpGame.Models.Foundations
{
    using System.Collections.Generic;

    using CSharpGame.Data;
    using CSharpGame.Enums;
    using CSharpGame.Models.Animations;

    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    public class SpriteGameObject : GameObject
    {
        private string currentAnimationKey;

        public SpriteGameObject(Transform2D transform, Dictionary<string, Animation> animations, GameRepository repository)
            : base(transform, repository)
        {
            this.Animations = animations;

            this.CurrentAnimationKey = "running";
            this.Orientation = SpriteEffects.None;
        }
        
        protected Dictionary<string, Animation> Animations { get; }

        protected string CurrentAnimationKey
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


        public CharacterState State { get; protected set; }

        public SpriteEffects Orientation { get; protected set; }

        protected void UpdateCurrentAnimation(GameTime gameTime)
        {
            if ((this.State == CharacterState.RunningLeft || this.State == CharacterState.RunningRight)
                && this.CurrentAnimationKey != "running")
            {
                this.CurrentAnimationKey = "running";
            }
            else if (this.State == CharacterState.Jumping
                && this.CurrentAnimationKey != "jumping")
            {
                this.CurrentAnimationKey = "jumping";
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
