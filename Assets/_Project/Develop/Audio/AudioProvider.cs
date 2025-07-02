using DG.Tweening;
using UnityEngine;
using Utils;
using Zenject;

namespace Audio
{
    public class AudioProvider
    {
        private ObjectPool<AudioSourcer> _sourcers;
        private AudioSourcer _musicSourcer;

        public float Volume { get; private set; }

        [Inject]
        public AudioProvider(AudioSourcer sourcerPrefab)
        {
            _sourcers = new(sourcerPrefab, 5);
            _musicSourcer = GameObject.Instantiate(sourcerPrefab);
        }

        public void PlaySound(AudioClip audioClip)
        {
            var sourcer = _sourcers.GetInstance();

            sourcer.PlayOneShot(audioClip);
            DOVirtual.DelayedCall(audioClip.length, () => _sourcers.ReleaseInstance(sourcer));
        }
    }
}