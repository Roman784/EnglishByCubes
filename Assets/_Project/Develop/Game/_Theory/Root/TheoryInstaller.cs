using UI;
using UnityEngine;
using Zenject;

namespace Theory
{
    public class TheoryInstaller : MonoInstaller
    {
        [SerializeField] private TheoryUI _theoryUIPrefab;

        public override void InstallBindings()
        {
            BindUI();
        }

        private void BindUI()
        {
            Container.Bind<TheoryUI>().FromComponentInNewPrefab(_theoryUIPrefab).AsSingle();
        }
    }
}