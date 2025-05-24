using System.Collections;
using UnityEngine;

namespace Utils
{
    public sealed class Coroutines : MonoBehaviour
    {
        private static Coroutines _instance;
        private static Coroutines instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new GameObject("[COROUTINES]").AddComponent<Coroutines>();
                    DontDestroyOnLoad(_instance.gameObject);
                }
                return _instance;
            }
        }

        private void OnDestroy()
        {
            instance.StopAllCoroutines();
        }

        public static Coroutine Start(IEnumerator enumerator)
        {
            return instance.StartCoroutine(enumerator);
        }

        public static void Stop(Coroutine routine)
        {
            if (routine != null)
                instance.StopCoroutine(routine);
        }

        public static void StopAll()
        {
            instance.StopAllCoroutines();
        }
    }
}
