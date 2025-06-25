using R3;
using System.Collections;
using UnityEngine;

namespace UI
{
    public class UIRoot : MonoBehaviour
    {
        [SerializeField] private LoadingScreen _loadingScreen;
        [SerializeField] private PopUpsRoot _popUpsRoot;
        [SerializeField] private Transform _sceneUIContainer;

        public PopUpsRoot PopUpsRoot => _popUpsRoot;

        public IEnumerator SetLoadingScreen(bool value)
        {
            bool isCompleted = false;

            (value ? _loadingScreen.Show() : _loadingScreen.Hide())
                .Subscribe(_ => isCompleted = true);

            yield return new WaitUntil(() => isCompleted);
        }

        public void AttachSceneUI(SceneUI sceneUI)
        {
            sceneUI.transform.SetParent(_sceneUIContainer, false);
        }

        public void ClearAllContainers()
        {
            ClearContainer(_sceneUIContainer);
            _popUpsRoot.DestroyAllPopUps();
        }

        private void ClearContainer(Transform container)
        {
            var childCount = container.childCount;
            for (int i = 0; i < childCount; i++)
            {
                Destroy(container.GetChild(i).gameObject);
            }
        }
    }
}