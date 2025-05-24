using R3;
using System.Threading.Tasks;

namespace Configs
{
    public interface IConfigsProvider
    {
        public GameConfigs GameConfigs { get; }
        public Observable<bool> LoadGameConfigs();
    }
}
