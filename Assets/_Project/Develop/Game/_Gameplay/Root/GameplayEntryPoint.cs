using GameRoot;
using System.Collections;
using Theme;
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
        private TaskPassingService _taskPassingService;

        [Inject]
        private void Construct(GameplayUI ui,
                               SlotBarFactory slotBarFactory,
                               TaskPassingService taskPassingService)
        {
            _ui = ui;
            _slotBarFactory = slotBarFactory;
            _taskPassingService = taskPassingService;
        }

        public override IEnumerator Run<T>(T enterParams)
        {
            var isLoaded = false;

            var gameConfigs = _configsProvider.GameConfigs;
            var cubesConfigs = gameConfigs.CubesConfigs;
            var tasksConfigs = gameConfigs.TasksConfigs;

            var taskConfigs = tasksConfigs.GetTask(1);
            var taskSentenceRu = taskConfigs.SentenceRu;
            var taskSentenceEn = taskConfigs.SentenceEn;
            var cubeNumbersPool = taskConfigs.CubeNumbersPool.ToArray();

            // Slot bar.
            var slotBar = _slotBarFactory.Create(_slotBarPosition);
            yield return null; // To update slots layout. Forced update does not work.

            // Cubes.
            var cubesConfigsPool = cubesConfigs.GetCubes(cubeNumbersPool);
            slotBar.CreateCubes(cubesConfigsPool);

            // Task passing.
            _taskPassingService.SetCorrectSentence(taskSentenceEn);

            // UI.
            _uiRoot.AttachSceneUI(_ui);
            _ui.SetTaskSentence(taskSentenceRu);
            _ui.InitProgressBar();
            _ui.InitCubeRemoveArea();

            _taskPassingService.OnSentenceMatchingCalculated.AddListener(_ui.FillProgressBar);

            // Theme customization.
            CustomizeTheme();

            isLoaded = true;

            yield return new WaitUntil(() => isLoaded);
        }
    }
}