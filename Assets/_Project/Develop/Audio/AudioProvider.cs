using DG.Tweening;
using GameState;
using R3;
using UnityEngine;
using Utils;
using Zenject;

namespace Audio
{
    public class AudioProvider
    {
        private ObjectPool<AudioSourcer> _sourcers;
        private AudioSourcer _musicSourcer;

        private AudioSourcer _sourcerPrefab;
        private IGameStateProvider _gameStateProvider;

        public ReactiveProperty<float> MusicVolume;
        public ReactiveProperty<float> SoundVolume;

        [Inject]
        public AudioProvider(AudioSourcer sourcerPrefab, IGameStateProvider gameStateProvider)
        {
            _sourcerPrefab = sourcerPrefab;
            _gameStateProvider = gameStateProvider;
        }

        public void Init()
        {
            var state = _gameStateProvider.GameStateProxy.State;
            MusicVolume = new(state.MusicVolume);
            SoundVolume = new(state.SoundVolume);

            _sourcers = new(_sourcerPrefab, 5);
            _musicSourcer = GameObject.Instantiate(_sourcerPrefab);

            foreach (var sourcer in _sourcers.GetInstances())
                SoundVolume.Subscribe(volume => sourcer.ChangeVolume(volume));
            MusicVolume.Subscribe(volume => _musicSourcer.ChangeVolume(volume));

            MusicVolume.Subscribe(volume => _gameStateProvider.GameStateProxy.SetMusicVolume(volume));
            SoundVolume.Subscribe(volume => _gameStateProvider.GameStateProxy.SetSoundVolume(volume));
        }

        public void PlaySound(AudioClip audioClip)
        {
            var sourcer = _sourcers.GetInstance();

            sourcer.PlayOneShot(audioClip);
            DOVirtual.DelayedCall(audioClip.length, () => _sourcers.ReleaseInstance(sourcer));
        }
    }
}