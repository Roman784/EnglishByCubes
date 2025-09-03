using Configs;
using Gameplay;
using GameRoot;
using System.Collections;
using UI;
using UnityEngine;
using Zenject;

namespace MistakeCorrection
{
    public class MistakeCorrectionEntryPoint : SceneEntryPoint
    {
        private MistakeCorrectionUI _ui;

        [Inject]
        private void Construct(MistakeCorrectionUI ui)
        {
            _ui = ui;
        }

        public override IEnumerator Run<T>(T enterParams)
        {
            yield return Run(enterParams.As<MistakeCorrectionEnterParams>());
        }

        private IEnumerator Run(MistakeCorrectionEnterParams enterParams)
        {
            var isLoaded = false;

            // UI.
            _uiRoot.AttachSceneUI(_ui);
            _ui.Init(enterParams);
            //_ui.SetLevelTitle();

            // Theme customization.
            CustomizeTheme();

            isLoaded = true;

            yield return new WaitUntil(() => isLoaded);
        }
    }
}