using GameRoot;
using LevelMenu;
using System.Collections;
using UnityEngine;

namespace Collection
{
    public class CollectionEntryPoint : SceneEntryPoint
    {
        public override IEnumerator Run<T>(T enterParams)
        {
            yield return Run(enterParams.As<CollectionEnterParams>());
        }

        private IEnumerator Run(CollectionEnterParams enterParams)
        {
            var isLoaded = false;

            //UI.
            

            // Theme customization.
            CustomizeTheme();

            isLoaded = true;

            yield return new WaitUntil(() => isLoaded);
        }
    }
}