namespace CSharpGame.Input
{
    using CSharpGame.EventHandlers;

    using Microsoft.Xna.Framework;

    public abstract class CharacterInput : GameComponent
    {
        public event PointerEventHandler PointerPress;

        public event PointerEventHandler PointerDrag;

        public event PointerEventHandler PointerRelease;

        public event GameActionEventHandler MoveRight;

        public event GameActionEventHandler MoveLeft;

        public event GameActionEventHandler Jump;

        public event GameActionEventHandler Attack;

        public CharacterInput(Game game)
            : base(game)
        {
        }

        protected void InvokePointerPress(Vector2 currentPointerPosition, Vector2 delta)
        {
            this.PointerPress?.Invoke(new PointerEventDataArgs(currentPointerPosition, delta));
        }

        protected void InvokePointerDrag(Vector2 currentPointerPosition, Vector2 delta)
        {
            this.PointerDrag?.Invoke(new PointerEventDataArgs(currentPointerPosition, delta));
        }

        protected void InvokePointerRelease(Vector2 currentPointerPosition, Vector2 delta)
        {
            this.PointerRelease?.Invoke(new PointerEventDataArgs(currentPointerPosition, delta));
        }

        protected void InvokeMoveRight()
        {
            this.MoveRight?.Invoke();
        }

        protected void InvokeMoveLeft()
        {
            this.MoveLeft?.Invoke();
        }

        protected void InvokeJump()
        {
            this.Jump?.Invoke();
        }

        protected void InvokeAttack()
        {
            this.Attack?.Invoke();
        }
    }
}
