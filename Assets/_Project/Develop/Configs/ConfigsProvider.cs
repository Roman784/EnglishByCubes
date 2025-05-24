using R3;
using UnityEngine;

namespace Configs
{
    public class ConfigsProvider : IConfigsProvider
    {
        private GameConfigs _gameConfigs;

        public GameConfigs GameConfigs => _gameConfigs;

        public Observable<bool> LoadGameConfigs()
        {
            try
            {
                _gameConfigs = Resources.Load<GameConfigs>("GameConfigs");
                return Observable.Return(true);
            }
            catch { return Observable.Return(false); }
        }
    }
}
