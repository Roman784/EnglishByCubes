using UI;
using UnityEngine;
using Zenject;

namespace LevelMenu
{
    public class LevelMenuInstaller : MonoInstaller
    {
        [SerializeField] private LevelMenuUI _levelMenuUIPrefab;

        public override void InstallBindings()
        {
            BindUI();
        }

        private void BindUI()
        {
            Container.Bind<LevelMenuUI>().FromComponentInNewPrefab(_levelMenuUIPrefab).AsSingle();
        }
    }
}