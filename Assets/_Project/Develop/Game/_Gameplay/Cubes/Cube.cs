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
        private bool _isDragged;

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
                _isPressed = false;

                if (_isInSlot)
                    CreateOnField();
                else if (!_isDragged)
                    RotateToNextSide();
            });

            _view.OnPressed.AddListener(() =>
            {
                if (_isPressed || _isInSlot || _isDestroyed) return;
                _isPressed = true;
                _dragOffset = _camera.ScreenToWorldPoint(Input.mousePosition) - Position;
                Coroutines.Start(Drag());
            });
        }

        public void AddToSlot(Slot slot)
        {
            _isInSlot = true;
            SetPosition(slot.Position);
            DisableInSlots(true);
        }

        public Observable<bool> PlaceInSlot(Slot slot)
        {
            var duration = _configs.DataConfigs.RescaleDuration;
            var ease = _configs.DataConfigs.RescaleEase;

            _view.Enable();
            _view.SetScale(Vector3.one * slot.Scale);
            Enlarge();

            return Move(slot.Position, duration, ease);
        }

        public Observable<bool> DisableInSlots(bool instantly = false)
        {
            _view.Disable();
            return Shrink(instantly);
        }

        public Observable<bool> PlaceOnField(Vector3 position, float scale)
        {
            var placementDuration = _configs.DataConfigs.FieldPlacementDuration;
            var switchingDuration = _configs.DataConfigs.SwitchingOnFieldDuration;
            var placementEase = _configs.DataConfigs.FieldPlacementEase;
            var switchingEase = _configs.DataConfigs.SwitchingOnFieldEase;

            _view.SetViewScale(Vector3.one * scale, placementDuration, placementEase);
            return Move(position, switchingDuration, switchingEase);
        }

        public void DisableOnField()
        {
            Shrink(true);
        }

        public void SetPosition(Vector3 position)
        {
            _view.SetPosition(position);
        }

        public Observable<bool> Move(Vector3 position,
                         float duration = 0, Ease ease = Ease.Flash)
        {
            return _view.SetPosition(position, duration, ease);
        }

        public Observable<bool> Destroy()
        {
            _isDestroyed = true;

            var duration = _configs.DataConfigs.DestructionDuration;
            var ease = _configs.DataConfigs.DestructionEase;

            _view.Disable();
            return _view.Destroy(duration, ease);
        }

        private Observable<bool> Select()
        {
            var scale = _configs.DataConfigs.SelectionScale;
            var duration = _configs.DataConfigs.SelectionDuration;
            var ease = _configs.DataConfigs.SelectionEase;

            return _view.SetScale(Vector3.one * scale, duration, ease);
        }

        private Observable<bool> Deselect()
        {
            var duration = _configs.DataConfigs.SelectionDuration;
            var ease = _configs.DataConfigs.SelectionEase;

            return _view.SetScale(Vector3.one, duration, ease);
        }

        private Observable<bool> Enlarge()
        {
            var duration = _configs.DataConfigs.RescaleDuration;
            var ease = _configs.DataConfigs.RescaleEase;

            return _view.SetViewScale(Vector3.one, duration, ease);
        }

        private Observable<bool> Shrink(bool instantly = false)
        {
            if (instantly)
            {
                _view.SetViewScale(Vector3.zero);
                return Observable.Return(true);
            }

            var duration = _configs.DataConfigs.RescaleDuration;
            var ease = _configs.DataConfigs.RescaleEase;

            return _view.SetViewScale(Vector3.zero, duration, ease);
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

        private IEnumerator Drag()
        {
            _isDragged = true;
            Select();

            _gameFieldService.PrepareForPreviewCubePosition(this);

            while (_isPressed)
            {
                yield return null;

                var position = _camera.ScreenToWorldPoint(Input.mousePosition) - _dragOffset;
                position.y = Position.y;

                SetPosition(position);
                _gameFieldService.PreviewCubePosition(this);

                if (Position.z < -3.5)
                {
                    _isPressed = false;
                    _gameFieldService.RemoveCube(this);
                }
            }

            _isDragged = false;
            Deselect();

            _gameFieldService.SwapCubesAccordingPreview();
        }
    }
}