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
        public virtual void OnPressed() { }
        public virtual void OnUnpressed() { }
        public virtual void Exit() { }
    }
}