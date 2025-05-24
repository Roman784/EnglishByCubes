using R3;
using System.Threading.Tasks;

namespace Configs
{
    public interface IConfigsProvider
    {
        public GameConfigs GameSettings { get; }
        public Observable<GameConfigs> LoadGameConfigs();
    }
}
