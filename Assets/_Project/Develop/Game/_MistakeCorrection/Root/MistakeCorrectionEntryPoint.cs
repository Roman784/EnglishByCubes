using Configs;
using Gameplay;
using GameRoot;
using System.Collections;
using UnityEngine;

namespace MistakeCorrection
{
    public class MistakeCorrectionEntryPoint : SceneEntryPoint
    {
        public override IEnumerator Run<T>(T enterParams)
        {
            yield return Run(enterParams.As<MistakeCorrectionEnterParams>());
        }

        private IEnumerator Run(MistakeCorrectionEnterParams enterParams)
        {
            var isLoaded = false;

            

            // Theme customization.
            CustomizeTheme();

            isLoaded = true;

            yield return new WaitUntil(() => isLoaded);
        }
    }
}