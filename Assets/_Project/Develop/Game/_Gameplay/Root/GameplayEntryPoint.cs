using GameRoot;
using System.Collections;
using UI;
using UnityEngine;
using Zenject;

namespace Gameplay
{
    public class GameplayEntryPoint : SceneEntryPoint
    {
        [SerializeField] private Vector3 _slotBarPosition;

        private GameplayUI _ui;
        private SlotBarFactory _slotBarFactory;

        [Inject]
        private void Construct(GameplayUI ui,
                               SlotBarFactory slotBarFactory)
        {
            _ui = ui;
            _slotBarFactory = slotBarFactory;
        }

        public override IEnumerator Run<T>(T enterParams)
        {
            var isLoaded = false;

            var gameConfigs = _configsProvider.GameConfigs;
            var cubesConfigs = gameConfigs.CubesConfigs;
            var tasksConfigs = gameConfigs.TasksConfigs;

            var taskConfigs = tasksConfigs.GetTask(1);
            var taskSentence = taskConfigs.Sentence;
            var cubeNumbersPool = taskConfigs.CubeNumbersPool.ToArray();

            // Slot bar.
            var slotBar = _slotBarFactory.Create(_slotBarPosition);
            yield return null; // To update slots layout. Forced update does not work.

            // Cubes.
            var cubesConfigsPool = cubesConfigs.GetCubes(cubeNumbersPool);
            slotBar.CreateCubes(cubesConfigsPool);

            // UI.
            _uiRoot.AttachSceneUI(_ui);
            _ui.CustomizeTheme();
            _ui.SetTaskSentence(taskSentence);
            _ui.InitProgressBar();

            isLoaded = true;

            yield return new WaitUntil(() => isLoaded);
        }
    }
}