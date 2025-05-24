using R3;
using UnityEngine;

namespace UI
{
    public class UIRootView : MonoBehaviour
    {
        [SerializeField] private LoadingScreen _loadingScreen;

        public Observable<Unit> SetLoadingScreen(bool value)
        {
            return value ?
                _loadingScreen.Show() :
                _loadingScreen.Hide();
        }
    }
}