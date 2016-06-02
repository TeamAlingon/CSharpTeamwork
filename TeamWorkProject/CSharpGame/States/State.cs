using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CSharpGame.States
{
    public abstract class State
    {
        public abstract void Update(GameTime gameTime);
        public abstract void Draw(SpriteBatch batch);
        public abstract void LoadContent();
        public abstract void UnloadContent();
    }
}
