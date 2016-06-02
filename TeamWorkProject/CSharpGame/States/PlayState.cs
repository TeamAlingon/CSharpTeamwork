using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CSharpGame.States
{
    public class PlayState : State
    {
        private GameStateManager stateManager;

        public PlayState(GameStateManager stateManager)
        {
            this.stateManager = stateManager;
        }

        public override void Update(GameTime gameTime)
        {
            throw new NotImplementedException();
        }

        public override void Draw(SpriteBatch batch)
        {
            throw new NotImplementedException();
        }

        public override void LoadContent()
        {
            throw new NotImplementedException();
        }

        public override void UnloadContent()
        {
            throw new NotImplementedException();
        }
    }
}
