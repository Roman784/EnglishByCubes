using R3;
using System.Collections;
using UnityEngine;

namespace UI
{
    public class UIRoot : MonoBehaviour
    {
        [SerializeField] private LoadingScreen _loadingScreen;
        [SerializeField] private Transform _sceneUIContainer;

        public IEnumerator SetLoadingScreen(bool value)
        {
            bool isCompleted = false;

            (value ? _loadingScreen.Show() : _loadingScreen.Hide())
                .Subscribe(_ => isCompleted = true);

            yield return new WaitUntil(() => isCompleted);
        }

        public void AttachSceneUI(SceneUI sceneUI)
        {
            ClearContainer(_sceneUIContainer);
            sceneUI.transform.SetParent(_sceneUIContainer, false);
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