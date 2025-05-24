using R3;
using UnityEngine;

namespace Configs
{
    public class ConfigsProvider : IConfigsProvider
    {
        private GameConfigs _gameConfigs;

        public GameConfigs GameSettings => _gameConfigs;

        public Observable<GameConfigs> LoadGameConfigs()
        {
            _gameConfigs = Resources.Load<GameConfigs>("Configs/GameConfigs");
            return Observable.Return(_gameConfigs);
        }
    }
}
