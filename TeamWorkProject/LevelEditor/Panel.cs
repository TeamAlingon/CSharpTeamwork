namespace LevelEditor
{
    using System.Collections.Generic;

    using LevelEditor.Utils;

    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    public class Panel
    {
        public Vector2 Position { get; set; }

        public Rectangle Placement { get; set; }

        public Texture2D PanelSize { get; set; }

        public SpriteFont SpriteFont { get; set; }

        private IEnumerable<string> LevelTileMapFilenames { get; set; }

        public Panel(Vector2 position, Rectangle placement, Texture2D panelSize, SpriteFont spriteFont)
        {
            this.Position = position;
            this.Placement = placement;
            this.PanelSize = panelSize;
            this.SpriteFont = spriteFont;

            this.LevelTileMapFilenames = FileUtils.GetFilenames("Level");
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.LinearWrap, null, null);
            spriteBatch.Draw(
                this.PanelSize,
                this.Position,
                this.Placement,
                Color.White,
                0,
                Vector2.Zero,
                1f,
                SpriteEffects.None,
                0);

            var x = this.Position.X + 5;
            var y = this.Position.Y - 19;

            foreach (string levelTileMapFilename in LevelTileMapFilenames)
            {
                spriteBatch.DrawString(this.SpriteFont, levelTileMapFilename, new Vector2(x, y += 19), Color.Black);
            }

            spriteBatch.End();
        }
    }
}
