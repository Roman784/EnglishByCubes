using Configs;
using Gameplay;
using GameRoot;
using System.Collections;
using UI;
using UnityEngine;
using Zenject;

namespace Template
{
    public class TemplateEntryPoint : SceneEntryPoint
    {
        [SerializeField] private Vector3 _slotBarPosition;

        private TemplateUI _ui;

        [Inject]
        private void Construct(TemplateUI ui)
        {
            _ui = ui;
        }

        public override IEnumerator Run<T>(T enterParams)
        {
            yield return Run(enterParams.As<TemplateEnterParams>());
        }

        private IEnumerator Run(TemplateEnterParams enterParams)
        {
            var isLoaded = false;

            var gameConfigs = _configsProvider.GameConfigs;
            var cubesConfigs = gameConfigs.CubesConfigs;
            var levelsConfigs = gameConfigs.LevelsConfigs;

            var taskConfigs = levelsConfigs
                .GetLevel(enterParams.Number)
                .As<TemplateLevelConfigs>();

            /*// Slot bar.
            var slotBar = _slotBarFactory.Create(_slotBarPosition);
            yield return null; // To update slots layout. Forced update does not work.*/

            // UI.
            _uiRoot.AttachSceneUI(_ui);
            _ui.Init(enterParams);
            _ui.SetLevelTitle(taskConfigs);

            // Theme customization.
            CustomizeTheme();

            isLoaded = true;

            yield return new WaitUntil(() => isLoaded);
        }
    }
}