using System;
using System.Collections.Generic;

namespace Gameplay
{
    public class CubeBehaviorHandler
    {
        private readonly Cube _cube;

        private Dictionary<Type, CubeBehavior> _behaviorsMap;
        private CubeBehavior _currentBehavior;

        public CubeBehavior CurrentBehavior => _currentBehavior;

        public CubeBehaviorHandler(Cube cube)
        {
            _cube = cube;

            InitBehaviorsMap();
        }

        private void InitBehaviorsMap()
        {
            _behaviorsMap = new();

            _behaviorsMap[typeof(CubeInSlotBehavior)] = new CubeInSlotBehavior(this, _cube);
            _behaviorsMap[typeof(CubeCreationOnFieldBehavior)] = new CubeCreationOnFieldBehavior(this, _cube);
            _behaviorsMap[typeof(CubeDraggingBehavior)] = new CubeDraggingBehavior(this, _cube);
            _behaviorsMap[typeof(CubeRemovingBehavior)] = new CubeRemovingBehavior(this, _cube);
            _behaviorsMap[typeof(CubeOnFieldBehavior)] = new CubeOnFieldBehavior(this, _cube);
            _behaviorsMap[typeof(CubeWordListBehavior)] = new CubeWordListBehavior(this, _cube);
        }

        public void SetInSLotBehavior()
        {
            var behavior = GetBehavior<CubeInSlotBehavior>();
            SetBehavior(behavior);
        }

        public void SetCreationOnFieldBehavior()
        {
            var behavior = GetBehavior<CubeCreationOnFieldBehavior>();
            SetBehavior(behavior);
        }

        public void SetDraggingBehavior()
        {
            var behavior = GetBehavior<CubeDraggingBehavior>();
            SetBehavior(behavior);
        }

        public void SetRemovingBehavior()
        {
            var behavior = GetBehavior<CubeRemovingBehavior>();
            SetBehavior(behavior);
        }

        public void SetOnFieldBehavior()
        {
            var behavior = GetBehavior<CubeOnFieldBehavior>();
            SetBehavior(behavior);
        }

        public void SetWordListBehavior()
        {
            var behavior = GetBehavior<CubeWordListBehavior>();
            SetBehavior(behavior);
        }

        public void SetBehavior(CubeBehavior behavior)
        {
            _currentBehavior?.Exit();

            _currentBehavior = behavior;
            _currentBehavior.Enter();
        }

        private CubeBehavior GetBehavior<T>() where T : CubeBehavior
        {
            return _behaviorsMap[typeof(T)];
        }
    }
}