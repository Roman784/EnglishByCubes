using Configs;
using DG.Tweening;
using R3;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Gameplay
{
    public class Cube
    {
        private readonly CubeView _view;
        private readonly CubeConfigs _configs;

        private List<string> _words;
        private int _curretWordIndex;

        private bool _isInSlot;

        public Cube(CubeView view, CubeConfigs configs)
        {
            _view = view;
            _configs = configs;
            _words = _configs.Words;

            _view.Init(_words[0], _configs.Material);

            _view.OnUnpressed.AddListener(() => 
            {
                if (!_isInSlot)
                    RotateToNextSide();
            });
        }

        public void PlaceInSlot(Slot slot)
        {
            _isInSlot = true;
            _view.SetScale(slot.Scale);
        }

        public Observable<bool> Enable()
        {
            var duration = _configs.DataConfigs.RescaleDuration;
            var ease = _configs.DataConfigs.RescaleEase;

            return _view.Enable(duration, ease);
        }

        public Observable<bool> Disable(bool instantly = false)
        {
            var duration = _configs.DataConfigs.RescaleDuration;
            var ease = _configs.DataConfigs.RescaleEase;

            return _view.Disable(duration, ease, instantly);
        }

        public Observable<bool> SetPosition(Vector3 position, 
                                            float duration = 0, Ease ease = Ease.Flash)
        {
            return _view.SetPosition(position, duration, ease);
        }

        private void RotateToNextSide()
        {
            var rotationDuration = _configs.DataConfigs.RotationDuration;
            var fadeDuration = _configs.DataConfigs.FadeDuration;
            var rotationEase = _configs.DataConfigs.RotationEase;
            var fadeEase = _configs.DataConfigs.FadeEase;

            _curretWordIndex = ++_curretWordIndex % _words.Count();
            var word = _words[_curretWordIndex];

            _view.Rotate(word, rotationDuration, rotationEase, fadeDuration, fadeEase);
        }
    }
}