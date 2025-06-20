using Configs;
using R3;
using UnityEngine;
using UnityEngine.UI;

namespace Theory
{
    public class TheoryPage : MonoBehaviour
    {
        [SerializeField] private VerticalLayoutGroup _container;

        public void CreateContent(TheoryPageConfigs configs)
        {
            foreach (var content in configs.Contents)
            {
                var newContent = Instantiate(content.Content);
                newContent.transform.SetParent(_container.transform, false);
            }

            _container.CalculateLayoutInputVertical();
            _container.SetLayoutVertical();
        }

        public Observable<bool> Show()
        {
            gameObject.SetActive(true);
            return Observable.Return(true);
        }

        public Observable<bool> Hide(bool instantly = false)
        {
            //gameObject.SetActive(false);
            return Observable.Return(true);
        }
    }
}