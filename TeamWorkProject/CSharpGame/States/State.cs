using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CSharpGame.States
{
    public abstract class State : GameComponent
    {
        protected State(Game game)
            : base(game)
        {
        }

        public abstract void Update(GameTime gameTime);
        public abstract void Draw(SpriteBatch spriteBatch);
        public abstract void LoadContent();
        public abstract void UnloadContent();
    }
}
