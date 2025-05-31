using UnityEngine;

namespace Gameplay
{
    public class Slot : MonoBehaviour
    {
        private Cube _cube;

        public bool HasCube => _cube != null;

        public void AddCube(Cube cube)
        {
            _cube = cube;
        }
    }
}