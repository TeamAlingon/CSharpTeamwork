namespace LevelEditor.Factory
{
    using System.Collections.Generic;

    using LevelEditor.Data;
    using LevelEditor.Interfaces;
    using LevelEditor.Models;
    using LevelEditor.Models.Level;
    using LevelEditor.Models.UI;
    using LevelEditor.Utils;

    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Content;
    using Microsoft.Xna.Framework.Graphics;

    public static class Factory
    {
        public static void GenerateLevelBuildingPanel(ContentManager content)
        {
            var panelTexture = content.Load<Texture2D>("UiTiles/GrayTileTransparency");
            var spriteFont = content.Load<SpriteFont>("Fonts/impact");
            var buttonTexture = content.Load<Texture2D>("UiTiles/Button");

            var panelPosition = new Vector2(100, 100);
            var panelSize = new Rectangle((int)panelPosition.X, (int)panelPosition.Y, 512, 512);
            var levelBuilderPanel = new Panel(panelPosition, panelSize, panelTexture);

            var levelFiles = FileUtils.GetFilenames("Level");
            var levelSelectorObjects = new List<IDrawableGameObject>();
            foreach (string levelFile in levelFiles)
            {
                var objectTexture = content.Load<Texture2D>(levelFile);
                var texturedGameObject = new TexturedGameObject(objectTexture);
                levelSelectorObjects.Add(texturedGameObject);
            }
            var objectSelectorTransform = new Transform2D(levelBuilderPanel.Transform);
            var level = new Level();
            var objectSelector = new ObjectSelector(levelSelectorObjects, objectSelectorTransform, level);
            
            var nextButton = GenerateButton(spriteFont, buttonTexture, "Next", new Vector2(410, 470));
            nextButton.OnPress += args => objectSelector.SwitchToNextObject();

            var previousButton = GenerateButton(spriteFont, buttonTexture, "Previous", new Vector2(0, 470));
            previousButton.OnPress += args => objectSelector.SwitchToPreviousObject();

            var placeObjectButton = GenerateButton(spriteFont, buttonTexture, "Place", new Vector2(120, 470));
            placeObjectButton.OnPress += args => objectSelector.PlaceGameObjectInLevel();

            levelBuilderPanel.AddChild(objectSelector);
            levelBuilderPanel.AddChild(previousButton);
            levelBuilderPanel.AddChild(nextButton);
            levelBuilderPanel.AddChild(placeObjectButton);

            Repository.GameObjects.Add(level);
            Repository.GameObjects.Add(levelBuilderPanel);
        }

        private static Button GenerateButton(SpriteFont spriteFont, Texture2D buttonTexture, string buttonText, Vector2 buttonPosition)
        {
            var nextButtonTransform = new Transform2D(buttonPosition, Rectangle.Empty);
            var nextButtonText = new Text(buttonText, spriteFont, new Transform2D(Vector2.Zero, Rectangle.Empty));
            var nextButton = new Button(buttonTexture, nextButtonText, nextButtonTransform);
            return nextButton;
        }
    }
}
