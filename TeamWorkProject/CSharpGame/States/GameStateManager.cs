namespace CSharpGame.States
{
    using System.Collections.Generic;

    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

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
            this.Pop();
            this.states.Push(state);
        }

        public State Peek()
        {
            return this.states.Peek();
        }

        public void Update(GameTime gameTime)
        {
            this.Peek().Update(gameTime);
        }

        public void Draw(SpriteBatch batch)
        {
            this.Peek().Draw(batch);
        }
    }
}