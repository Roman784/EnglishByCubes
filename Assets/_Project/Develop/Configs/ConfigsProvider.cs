using R3;
using UnityEngine;

namespace Configs
{
    public class ConfigsProvider : IConfigsProvider
    {
        private GameConfigs _gameConfigs;

        public GameConfigs GameConfigs => _gameConfigs;

        public Observable<GameConfigs> LoadGameConfigs()
        {
            _gameConfigs = Resources.Load<GameConfigs>("GameConfigs");
            return Observable.Return(_gameConfigs);
        }
    }
}
