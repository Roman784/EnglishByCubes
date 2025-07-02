using Configs;
using R3;
using System.Collections.Generic;
using System.IO;
using Theme;
using UnityEngine;
using Zenject;

namespace GameState
{
    public class JsonGameStateProvider : IGameStateProvider
    {
        private const string GAME_STATE_KEY = "GAME_STATE";

        private string _savePath;

        private IConfigsProvider _configsProvider;

        public GameStateProxy GameStateProxy { get; private set; }

        [Inject]
        private void Construct(IConfigsProvider configsProvider)
        {
            _configsProvider = configsProvider;
        }

        public JsonGameStateProvider()
        {
            _savePath = GetPath();
        }

        public Observable<bool> LoadGameState()
        {
            try
            {
                if (!File.Exists(_savePath))
                {
                    GameStateProxy = CreateInitalGameState();
                    SaveGameState();
                }
                else
                {
                    var json = File.ReadAllText(_savePath);
                    var state = JsonUtility.FromJson<GameState>(json);

                    GameStateProxy = new GameStateProxy(state, this);
                }

                return Observable.Return(true);
            }
            catch { return Observable.Return(false); }
        }

        public Observable<bool> SaveGameState()
        {
            try
            {
                var json = JsonUtility.ToJson(GameStateProxy.State, true);
                File.WriteAllText(_savePath, json);

                return Observable.Return(true);
            }
            catch { return Observable.Return(false); }
        }

        public Observable<bool> ResetGameState()
        {
            GameStateProxy = CreateInitalGameState();
            return SaveGameState();
        }

        private GameStateProxy CreateInitalGameState()
        {
            var defaultState = _configsProvider.GameConfigs.DefaultGameStateConfigs;

            var gameState = new GameState
            {
                LastCompletedLevelNumber = defaultState.LastCompletedLevelNumber,
                CurrentThemeMode = (int)defaultState.CurrentThemeMode,
                CollectedCollectionItems = new(defaultState.CollectedCollectionItems),
                CurrentCollectionItemFill = defaultState.CurrentCollectionItemFill,
                MusicVolume = defaultState.MusicVolume,
                SoundVolume = defaultState.SoundVolume,
            };

            return new GameStateProxy(gameState, this);
        }

        private string GetPath()
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            return Path.Combine (Application.persistentDataPath, $"{GAME_STATE_KEY}.json");
#else
            return Path.Combine(Application.dataPath, $"{GAME_STATE_KEY}.json");
#endif
        }
    }
}
