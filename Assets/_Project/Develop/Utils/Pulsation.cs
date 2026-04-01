using UnityEngine;

namespace Utils
{
    public class Pulsation : MonoBehaviour
    {
        private Vector2 _initialScale;

        private void Start()
        {
            _initialScale = transform.localScale;
        }

        private void Update()
        {
            transform.localScale = _initialScale + Vector2.one * Mathf.Sin(Time.time * 2f) * 0.075f;
        }
    }
}