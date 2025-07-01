using System.Collections.Generic;

namespace Pause
{
    public class PauseProvider
    {
        private List<IPaused> _pausedObjs = new();

        public void StopGame()
        {
            foreach (var obj in _pausedObjs)
            {
                obj.Pause();
            }
        }

        public void ContinueGame()
        {
            foreach (var obj in _pausedObjs)
            {
                obj.Unpause();
            }
        }

        public void Add(IPaused obj)
        {
            _pausedObjs.Add(obj);
            obj.Unpause();
        }

        public void Remove(IPaused obj)
        {
            _pausedObjs.Remove(obj);
            obj.Unpause();
        }
    }
}