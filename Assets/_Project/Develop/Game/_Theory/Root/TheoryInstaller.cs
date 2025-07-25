using UI;
using UnityEngine;
using Zenject;

namespace Theory
{
    public class TheoryInstaller : MonoInstaller
    {
        [SerializeField] private TheoryUI _theoryUIPrefab;

        [Space]

        [SerializeField] private TheoryTableOfContentsPopUp _tableOfContentsPopUpPrefab;

        public override void InstallBindings()
        {
            BindUI();
        }

        private void BindUI()
        {
            Container.Bind<TheoryUI>().FromComponentInNewPrefab(_theoryUIPrefab).AsSingle();

            BindPopUps();
        }

        private void BindPopUps()
        {
            Container.Bind<TheoryPopUpProvider>().AsTransient();

            Container.BindFactory<TheoryTableOfContentsPopUp, TheoryTableOfContentsPopUp.Factory>().FromComponentInNewPrefab(_tableOfContentsPopUpPrefab);
        }
    }
}