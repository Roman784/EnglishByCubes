using GameRoot;
using System.Collections;
using UnityEngine;

namespace Theory
{
    public class TheoryEntryPoint : SceneEntryPoint
    {
        public override IEnumerator Run<T>(T enterParams)
        {
            yield return Run(enterParams.As<TheoryEnterParams>());
        }

        private IEnumerator Run(TheoryEnterParams enterParams)
        {
            var isLoaded = false;

            Debug.Log(enterParams.LevelNumber);

            //UI.
            //_uiRoot.AttachSceneUI(_ui);

            // Theme customization.
            CustomizeTheme();

            isLoaded = true;

            yield return new WaitUntil(() => isLoaded);
        }
    }
}