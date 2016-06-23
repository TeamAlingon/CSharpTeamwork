namespace CSharpGame.Models
{
    using System.Collections.Generic;

    using CSharpGame.Models.Animations;

    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    public class Character
    {
        private const float Speed = 5;

        private const string ImageFile = "Images/maincharacter";

        private readonly Dictionary<string, Vector2> movements;

        private string CurrentAnimationKey { get; set; }

        private Dictionary<string, Animation> Animations { get; }

        public SpriteEffects Orientation { get; private set; }

        public Vector2 Position { get; private set; }

        public Rectangle BoundingBox
            => new Rectangle(this.Position.ToPoint(), new Point(this.GetCurrentFrame().Height, this.GetCurrentFrame().Width));

        public Character(Dictionary<string, Animation> animations)
        {
            this.Animations = animations;
            this.movements = new Dictionary<string, Vector2>
                                 {
                                     { "right", new Vector2(Speed, 0) },
                                     { "left", new Vector2(-Speed, 0) },
                                     { "up", new Vector2(0, -Speed) },
                                     { "down", new Vector2(0, Speed) },
                                     { "jump", new Vector2(0, -Speed * 2) }
                                 };

            this.CurrentAnimationKey = "running";
            this.Orientation = SpriteEffects.None;
        }

        public Texture2D GetTexture()
        {
            return this.Animations[this.CurrentAnimationKey].SpriteSheet;
        }

        public Rectangle GetCurrentFrame()
        {
            return this.Animations[this.CurrentAnimationKey].GetCurrentFrame();
        }

        public void Move(string direction, GameTime gameTime)
        {
            this.Position += this.movements[direction];

            if (direction == "right" || direction == "left")
            {
                this.CurrentAnimationKey = "running";
                this.Orientation = direction == "right" ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
            }
            else if (direction == "jump")
            {
                this.CurrentAnimationKey = "runningJump";
            }

            this.UpdateCurrentAnimation(gameTime);
        }

        private void UpdateCurrentAnimation(GameTime gameTime)
        {
            this.Animations[this.CurrentAnimationKey].Play(gameTime);
        }
    }
}