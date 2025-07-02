using Configs;
using DG.Tweening;
using GameState;
using R3;
using System.Collections;
using UnityEngine;
using Utils;
using Zenject;

namespace Audio
{
    public class AudioProvider
    {
        private ObjectPool<AudioSourcer> _sourcers;
        private AudioSourcer _musicSourcer;

        private int _currentMusicIndex;

        private AudioSourcer _sourcerPrefab;
        private IGameStateProvider _gameStateProvider;
        private IConfigsProvider _configsProvider;

        public ReactiveProperty<float> MusicVolume;
        public ReactiveProperty<float> SoundVolume;

        private Coroutine _musicRoutine;

        public MusicAudioConfigs MusicConfigs => _configsProvider.GameConfigs.AudioConfigs.MusicConfigs;

        [Inject]
        public AudioProvider(AudioSourcer sourcerPrefab, IGameStateProvider gameStateProvider, IConfigsProvider configsProvider)
        {
            _sourcerPrefab = sourcerPrefab;
            _gameStateProvider = gameStateProvider;
            _configsProvider = configsProvider;
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

            _currentMusicIndex = Random.Range(0, MusicConfigs.Clips.Count);
        }

        public void PlaySound(AudioClip audioClip)
        {
            var sourcer = _sourcers.GetInstance();

            sourcer.PlayOneShot(audioClip);
            DOVirtual.DelayedCall(audioClip.length, () => _sourcers.ReleaseInstance(sourcer));
        }

        public void PlayMusic()
        {
            if (_musicRoutine != null)
                Coroutines.Stop(_musicRoutine);

            _musicRoutine = Coroutines.Start(PlayMusicRoutine());
        }

        public void SwitchMusic()
        {
            _currentMusicIndex = ++_currentMusicIndex % MusicConfigs.Clips.Count;
            PlayMusic();
        }

        private IEnumerator PlayMusicRoutine()
        {
            while (true)
            {
                var clip = MusicConfigs.Clips[_currentMusicIndex];
                _musicSourcer.PlayLoop(clip);

                yield return new WaitForSeconds(clip.length);

                _currentMusicIndex = ++_currentMusicIndex % MusicConfigs.Clips.Count;
            }
        }
    }
}