using Configs;
using R3;
using UnityEngine;

namespace Theory
{
    public class TheoryPage : MonoBehaviour
    {
        public void CreateContent(TheoryPageConfigs configs)
        {

        }

        public Observable<bool> Show()
        {
            gameObject.SetActive(true);
            return Observable.Return(true);
        }

        public Observable<bool> Hide(bool instantly = false)
        {
            gameObject.SetActive(false);
            return Observable.Return(true);
        }
    }
}