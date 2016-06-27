namespace CSharpGame.Models.Animations
{
    using System.Collections.Generic;

    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    public class Animation
    {
        public Animation(
            string name,
            Texture2D spriteSheet,
            List<Rectangle> frameData,
            float timeBetweenFrames = 0.05f,
            int currentFrameIndex = 0)
        {
            this.Name = name;
            this.SpriteSheet = spriteSheet;
            this.FrameData = frameData;
            this.CurrentFrameIndex = currentFrameIndex;
            this.TimeBetweenFrames = timeBetweenFrames;
        }

        public string Name { get; set; }

        public float TimeBetweenFrames { get; set; }

        public Texture2D SpriteSheet { get; }

        private int CurrentFrameIndex { get; set; }

        private float TimeSinceLastFrame { get; set; }

        private List<Rectangle> FrameData { get; }

        public Rectangle CurrentFrame => this.FrameData[this.CurrentFrameIndex];

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

        public void Reset()
        {
            this.CurrentFrameIndex = 0;
        }

        private void IncrementCurrentFrame()
        {
            this.CurrentFrameIndex++;

            if (this.CurrentFrameIndex >= this.FrameData.Count)
            {
                this.CurrentFrameIndex = 0;
            }
        }
    }
}