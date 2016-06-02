using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CSharpGame.States
{
    public class GameStateManager
    {
        private Stack<State> states;

        public GameStateManager()
        {
            this.states = new Stack<State>();
        }

        public void Push(State state)
        {
            this.states.Push(state);
        }

        public void Pop()
        {
            this.states.Pop().UnloadContent();
        }

        public void Set(State state)
        {
            this.states.Pop();
            this.states.Push(state);
        }

        public State Peek()
        {
            return this.states.Peek();
        }

        public void Update(GameTime gameTime)
        {
            states.Peek().Update(gameTime);
        }

        public void Draw(SpriteBatch batch)
        {
            states.Peek().Draw(batch);
        }
    }
}
