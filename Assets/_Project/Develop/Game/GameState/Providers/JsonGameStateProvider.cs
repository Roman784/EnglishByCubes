using R3;
using System.IO;
using UnityEngine;
using Zenject;

namespace GameState
{
    public class JsonGameStateProvider : IGameStateProvider
    {
        private const string GAME_STATE_KEY = "GAME_STATE";

        private string _savePath;

        public GameStateProxy GameStateProxy { get; private set; }

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
            var gameState = new GameState
            {
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
