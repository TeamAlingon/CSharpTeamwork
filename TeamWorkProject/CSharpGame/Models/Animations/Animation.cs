namespace CSharpGame.Models.Animations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    public class Animation
    {
        public string Name { get; set; }

        public float TimeBetweenFrames { get; set; }

        public Texture2D SpriteSheet { get; }

        private int CurrentFrame { get; set; }

        private float TimeSinceLastFrame { get; set; }

        private List<Rectangle> FrameData { get; }

        public Animation(
            string name,
            Texture2D spriteSheet,
            List<Rectangle> frameData,
            float timeBetweenFrames = 0.05f,
            int currentFrame = 0)
        {
            this.Name = name;
            this.SpriteSheet = spriteSheet;
            this.FrameData = frameData;
            this.CurrentFrame = currentFrame;
            this.TimeBetweenFrames = timeBetweenFrames;
        }

        public Rectangle GetCurrentFrame()
        {
            return this.FrameData[this.CurrentFrame];
        }

        public void Play(GameTime gameTime)
        {
            var deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            this.TimeSinceLastFrame += deltaTime;

            if (this.TimeSinceLastFrame >= this.TimeBetweenFrames)
            {
                this.IncrementCurrentFrame();
                this.TimeSinceLastFrame = 0;
            }
        }

        private void IncrementCurrentFrame()
        {
            this.CurrentFrame++;

            if (this.CurrentFrame >= this.FrameData.Count)
            {
                this.CurrentFrame = 0;
            }
        }
    }
}