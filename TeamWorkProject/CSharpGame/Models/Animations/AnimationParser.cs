namespace CSharpGame.Models.Animations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    public class AnimationParser
    {
        public static Dictionary<string, Animation> ReadSpriteSheetData(
            Texture2D animationSpriteSheet,
            IEnumerable<string> frameData)
        {
            var animationFrames = new Dictionary<string, List<Rectangle>>();
            foreach (string element in frameData)
            {
                var animationParameters = element.Split(new[] { '_' }, StringSplitOptions.RemoveEmptyEntries).ToArray();
                var animationName = animationParameters[0];

                if (!animationFrames.ContainsKey(animationName))
                {
                    animationFrames.Add(animationName, new List<Rectangle>());
                }

                var startPosition = element.IndexOf("Rect(", StringComparison.Ordinal) + "Rect(".Length;
                var length = element.LastIndexOf(");", StringComparison.Ordinal) - startPosition;

                var frameParameters =
                    element.Substring(startPosition, length)
                        .Split(new[] { ", " }, StringSplitOptions.RemoveEmptyEntries)
                        .Select(int.Parse)
                        .ToArray();

                var rectangle = new Rectangle(
                    frameParameters[0],
                    animationSpriteSheet.Height - frameParameters[1] - frameParameters[3],
                    frameParameters[2],
                    frameParameters[3]);

                animationFrames[animationName].Add(rectangle);
            }

            var animations = new Dictionary<string, Animation>();
            foreach (var animationFrame in animationFrames)
            {
                var animationName = animationFrame.Key;
                var animationFrameData = animationFrame.Value;

                animations.Add(
                    animationFrame.Key,
                    new Animation(animationName, animationSpriteSheet, animationFrameData));
            }

            return animations;
        }
    }
}