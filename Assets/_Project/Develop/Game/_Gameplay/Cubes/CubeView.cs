using UnityEngine;
using UnityEngine.Events;

namespace Gameplay
{
    public class CubeView : MonoBehaviour
    {
        public UnityEvent OnPressed { get; private set; }
        public UnityEvent OnUnpressed { get; private set; }

        private void OnMouseDown() => OnPressed.Invoke();
        private void OnMouseUp() => OnUnpressed.Invoke();

        public void Rotate()
        {

        }
    }
}