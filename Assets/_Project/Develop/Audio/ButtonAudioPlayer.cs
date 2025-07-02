using Configs;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Audio
{
    [RequireComponent(typeof(Button))]
    public class ButtonAudioPlayer : MonoBehaviour
    {
        [Inject]
        private void Construct(AudioProvider audioProvider, IConfigsProvider configsProvider)
        {
            Init(audioProvider, configsProvider.GameConfigs.AudioConfigs.UIConfigs.ButtonClickSound);
        }

        public void Init(AudioProvider audioProvider, AudioClip clip)
        {
            GetComponent<Button>().onClick.AddListener(() =>
            {
                audioProvider.PlaySound(clip);
            });
        }
    }
}