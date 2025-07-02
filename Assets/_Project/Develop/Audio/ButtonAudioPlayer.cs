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
            GetComponent<Button>().onClick.AddListener(() =>
            {
                var clip = configsProvider.GameConfigs.AudioConfigs.UIConfigs.ButtonClickSound;
                audioProvider.PlaySound(clip);
            });
        }
    }
}