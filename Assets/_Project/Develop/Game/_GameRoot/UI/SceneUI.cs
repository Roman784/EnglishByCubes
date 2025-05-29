using Configs;
using UnityEngine;
using Zenject;

namespace UI
{
    public class SceneUI : MonoBehaviour
    {
        protected IConfigsProvider _configsProvider;

        [Inject]
        private void Construct(IConfigsProvider configsProvider)
        {
            _configsProvider = configsProvider;
        }
    }
}