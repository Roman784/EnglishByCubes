using R3;
using UnityEngine;

namespace UI
{
    public class LoadingScreen : MonoBehaviour
    {
        [SerializeField] private GameObject _view;

        private void Awake()
        {
            _view.SetActive(false);
        }

        public Observable<Unit> Show()
        {
            _view.SetActive(true);
            return Observable.Return(Unit.Default);
        }

        public Observable<Unit> Hide()
        {
            _view.SetActive(false);
            return Observable.Return(Unit.Default);
        }
    }
}