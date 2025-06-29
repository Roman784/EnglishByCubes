using UI;
using UnityEngine;
using Zenject;

namespace Template
{
    public class TemplateInstaller : MonoInstaller
    {
        [SerializeField] private TemplateUI _templateUIPrefab;

        public override void InstallBindings()
        {
            BindUI();
        }

        private void BindUI()
        {
            Container.Bind<TemplateUI>().FromComponentInNewPrefab(_templateUIPrefab).AsSingle();
        }
    }
}