namespace LevelEditor.Factory
{
    using LevelEditor.Data;
    using LevelEditor.Models;
    using LevelEditor.Models.UI;
    using LevelEditor.Utils;

    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Content;
    using Microsoft.Xna.Framework.Graphics;

    public static class Factory
    {
        public static void GenerateLevelBuildingPanel(ContentManager content)
        {
            var panelTexture = content.Load<Texture2D>("UiTiles/GrayTile");
            var spriteFont = content.Load<SpriteFont>("Fonts/impact");
            var buttonTexture = content.Load<Texture2D>("UiTiles/Button");

            var panelPosition = new Vector2(100, 100);
            var panelSize = new Rectangle((int)panelPosition.X, (int)panelPosition.Y, 512, 512);
            var levelFiles = FileUtils.GetFilenames("Level");

            Panel levelBuildingPanel = new Panel(panelPosition, panelSize, panelTexture);

            var buttonPosition = Vector2.Zero;
            foreach (string levelFile in levelFiles)
            {
                var childTransform = new Transform2D(buttonPosition, Rectangle.Empty);
                var buttonText = new Text(levelFile, spriteFont, new Transform2D(Vector2.Zero, Rectangle.Empty));
                var button = new Button(buttonTexture, buttonText, childTransform);

                levelBuildingPanel.AddChild(button);

                buttonPosition = new Vector2(buttonPosition.X, buttonPosition.Y + button.Texture.Height + 2);
            }

            Repository.GameObjects.Add(levelBuildingPanel);
        }
    }
}
