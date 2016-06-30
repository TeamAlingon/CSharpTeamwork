namespace CSharpGame.States
{
    using System;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using Microsoft.Xna.Framework.Input;

    public class MenuState : State
    {
        private GameStateManager stateManager;
        public MenuState(GameStateManager stateManager)
        {
            this.stateManager = stateManager;
        }

        public override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Enter))
            {
                this.stateManager.Set(new PlayState(this.stateManager));
            }
        }

        public override void Draw(SpriteBatch batch)
        {
            throw new NotImplementedException();
        }

        public override void LoadContent()
        {
        }


        public override void UnloadContent()
        {
            throw new NotImplementedException();
        }
    }
}
