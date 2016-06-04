namespace LevelEditor.Factory
{
    using LevelEditor.Models;
    using LevelEditor.Models.UI;
    using LevelEditor.Utils;

    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Content;
    using Microsoft.Xna.Framework.Graphics;

    public class PanelFactory
    {
        private ContentManager Content { get; }

        public PanelFactory(ContentManager content)
        {
            this.Content = content;
        }

        public Panel GenerateMapTileSelectorPanel()
        {
            var panelPosition = new Vector2(100, 100);
            var panelSize = new Rectangle((int)panelPosition.X, (int)panelPosition.Y, 512, 512);
            var panelTexture = this.Content.Load<Texture2D>("UiTiles/GrayTile");
            var spriteFont = this.Content.Load<SpriteFont>("Fonts/impact");
            var levelFiles = FileUtils.GetFilenames("Level");

            Panel mapTilePanel = new Panel(panelPosition, panelSize, panelTexture);

            var textPosition = Vector2.Zero;
            foreach (string levelFile in levelFiles)
            {
                var childTransform = new Transform2D(textPosition, Rectangle.Empty);
                mapTilePanel.AddChild(new Text(levelFile, spriteFont, childTransform));

                textPosition = new Vector2(textPosition.X, textPosition.Y + 20);
            }

            return mapTilePanel;
        }
    }
}
