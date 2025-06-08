namespace Gameplay
{
    public abstract class CubeBehavior
    {
        protected readonly Cube _cube;

        public CubeBehavior(Cube cube)
        {
            _cube = cube;
        }

        public abstract void Enter();
        public virtual void Exit() { }
    }
}