namespace Gameplay
{
    public abstract class CubeBehavior
    {
        protected readonly CubeBehaviorHandler _handler;
        protected readonly Cube _cube;

        public CubeBehavior(CubeBehaviorHandler handler, Cube cube)
        {
            _handler = handler;
            _cube = cube;
        }

        public abstract void Enter();
        public virtual void OnPointerDown() { }
        public virtual void OnPointerUp() { }
        public virtual void OnPointerEnter() { }
        public virtual void OnPointerExit() { }
        public virtual void Exit() { }
    }
}