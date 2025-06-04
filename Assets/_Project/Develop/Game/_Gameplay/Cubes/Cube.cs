using Configs;
using DG.Tweening;
using R3;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Utils;

namespace Gameplay
{
    public class Cube
    {
        private readonly CubeView _view;
        private readonly CubeConfigs _configs;
        private readonly GameFieldService _gameFieldService;

        private List<string> _words;
        private int _curretWordIndex;

        private Vector3 _dragOffset;
        private Camera _camera;

        private bool _isInSlot;
        private bool _isPressed;
        private bool _isDestroyed;

        public Vector3 Position => _view.GetPosition();

        public Cube(CubeView view, CubeConfigs configs, GameFieldService gameFieldService)
        {
            _view = view;
            _configs = configs;
            _gameFieldService = gameFieldService;

            _words = _configs.Words;
            _camera = Camera.main;

            _view.Init(_words[0], _configs.Material);

            _view.OnUnpressed.AddListener(() => 
            {
                if (_isDestroyed) return;

                if (!_isInSlot)
                    RotateToNextSide();
                else
                    CreateOnField();

                _isPressed = false;
            });

            _view.OnPressed.AddListener(() =>
            {
                if (_isPressed || _isInSlot || _isDestroyed) return;
                _isPressed = true;
                _dragOffset = _camera.ScreenToWorldPoint(Input.mousePosition) - Position;
                Coroutines.Start(Drag());
            });
        }

        public Observable<bool> PlaceInSlot(Slot slot, bool instantly = false)
        {
            var duration = instantly ? 0f : _configs.DataConfigs.RescaleDuration;
            var ease = _configs.DataConfigs.RescaleEase;

            _isInSlot = true;
            SetScale(slot.Scale);
            return SetPosition(slot.Position, duration, ease);
        }

        public Observable<bool> PlaceOnField(Vector3 position, float scale)
        {
            var placementDuration = _configs.DataConfigs.FieldPlacementDuration;
            var switchingDuration = _configs.DataConfigs.SwitchingOnFieldDuration;
            var placementEase = _configs.DataConfigs.FieldPlacementEase;
            var switchingEase = _configs.DataConfigs.SwitchingOnFieldEase;

            SetScale(scale);
            SetPosition(position, switchingDuration, switchingEase);

            return _view.Enable(placementDuration, placementEase);
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

        public void SetScale(float scale)
        {
            _view.SetScale(scale);
        }

        public IEnumerator Drag()
        {
            while (_isPressed)
            {
                yield return null;

                var position = _camera.ScreenToWorldPoint(Input.mousePosition) - _dragOffset;
                position.y = Position.y;

                SetPosition(position);

                if (Position.z < -3.5)
                {
                    _isPressed = false;
                    _gameFieldService.RemoveCube(this);
                }
            }
        }

        public Observable<bool> Destroy()
        {
            _isDestroyed = true;

            var duration = _configs.DataConfigs.DestructionDuration;
            var ease = _configs.DataConfigs.DestructionEase;

            return _view.Destroy(duration, ease);
        }

        private void CreateOnField()
        {
            _gameFieldService.CreateCube(_configs);
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