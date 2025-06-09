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
        private readonly CubesPositionPreviewService _cubesPositionPreviewService;

        private readonly CubeBehaviorHandler _behaviorHandler;

        private List<string> _words;
        private int _curretWordIndex;

        private Camera _camera;
        private Coroutine _dragRoutine;

        public Vector3 Position => _view.GetPosition();

        public Cube(CubeView view, CubeConfigs configs,
                    GameFieldService gameFieldService, CubesPositionPreviewService cubesPositionPreviewService)
        {
            _view = view;
            _configs = configs;
            _gameFieldService = gameFieldService;
            _cubesPositionPreviewService = cubesPositionPreviewService;

            var number = _configs.Number;
            var name = _configs.Name;
            var material = _configs.Material;

            _words = _configs.Words;
            _camera = Camera.main;

            _behaviorHandler = new(this);
            _view.Init(number, name, _words, material);

            _view.OnWordInWordListSelected.AddListener(word =>
            {
                var wordIndex = GetWordIndex(word);
                Rotate(wordIndex);
                _behaviorHandler.SetOnFieldBehavior();
            });

            _view.OnPointerDown.AddListener(() =>
                _behaviorHandler.CurrentBehavior?.OnPointerDown());

            _view.OnPointerUp.AddListener(() =>
                _behaviorHandler.CurrentBehavior?.OnPointerUp());

            _view.OnPointerEnter.AddListener(() =>
                _behaviorHandler.CurrentBehavior?.OnPointerEnter());

            _view.OnPointerExit.AddListener(() =>
                _behaviorHandler.CurrentBehavior?.OnPointerExit());
        }

        public void AddToSlot(Slot slot)
        {
            _behaviorHandler.SetInSLotBehavior();

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

        public void CreateOnField()
        {
            _gameFieldService.CreateCube(_configs);
        }

        public Observable<bool> PlaceOnField(Vector3 position, float scale)
        {
            _behaviorHandler.SetOnFieldBehavior();

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

        public void RemoveFromField()
        {
            _gameFieldService.RemoveCube(this);
        }

        public void SetAccordingPreview()
        {
            _gameFieldService.SetCubesAccordingPreview();
        }

        public void StartDragging()
        {
            StopDragging();
            _dragRoutine = Coroutines.Start(DragRoutine());
        }

        public void StopDragging()
        {
            if (_dragRoutine != null)
                Coroutines.Stop(_dragRoutine);
        }

        public void SetPosition(Vector3 position)
        {
            _view.SetPosition(position);
        }

        public Observable<bool> Select()
        {
            var scale = _configs.DataConfigs.SelectionScale;
            var duration = _configs.DataConfigs.SelectionDuration;
            var ease = _configs.DataConfigs.SelectionEase;

            return _view.SetScale(Vector3.one * scale, duration, ease);
        }

        public Observable<bool> Deselect()
        {
            var duration = _configs.DataConfigs.SelectionDuration;
            var ease = _configs.DataConfigs.SelectionEase;

            return _view.SetScale(Vector3.one, duration, ease);
        }

        public Observable<bool> OpenWordList()
        {
            _behaviorHandler.SetWordListBehavior();

            var duration = _configs.DataConfigs.OpeningWordListDuration;
            var ease = _configs.DataConfigs.OpeningWordListEase;

            return _view.OpenWordList(duration, ease);
        }

        public Observable<bool> CloseWordList()
        {
            var duration = _configs.DataConfigs.ClosingWordListDuration;
            var ease = _configs.DataConfigs.ClosingWordListEase;

            return _view.CloseWordList(duration, ease);
        }

        public void ShowName()
        {
            var duration = _configs.DataConfigs.NameTransparencyChangeDuration;
            var ease = _configs.DataConfigs.NameTransparencyChangeEase;

            _view.ShowName(duration, ease);
        }

        public void HideName()
        {
            var duration = _configs.DataConfigs.NameTransparencyChangeDuration;
            var ease = _configs.DataConfigs.NameTransparencyChangeEase;

            _view.HideName(duration, ease);
        }

        public void RotateToNextSide()
        {
            _curretWordIndex = ++_curretWordIndex % _words.Count();
            Rotate(_curretWordIndex);
        }

        private int GetWordIndex(string word)
        {
            return _words.IndexOf(word);
        }

        private void Rotate(int wordIndex)
        {
            var rotationDuration = _configs.DataConfigs.RotationDuration;
            var fadeDuration = _configs.DataConfigs.FadeDuration;
            var rotationEase = _configs.DataConfigs.RotationEase;
            var fadeEase = _configs.DataConfigs.FadeEase;

            var word = _words[wordIndex];

            _view.Rotate(word, rotationDuration, rotationEase, fadeDuration, fadeEase);
        }

        private IEnumerator DragRoutine()
        {
            _cubesPositionPreviewService.PrepareForPreviewCubePosition(this);

            var offset = _camera.ScreenToWorldPoint(Input.mousePosition) - Position;
            while (true)
            {
                yield return null;

                var position = _camera.ScreenToWorldPoint(Input.mousePosition) - offset;
                position.y = Position.y;

                SetPosition(position);
                _cubesPositionPreviewService.PreviewCubePosition(this);
            }
        }

        public Observable<bool> Destroy()
        {
            _behaviorHandler.SetDestructionBehavior();

            var duration = _configs.DataConfigs.DestructionDuration;
            var ease = _configs.DataConfigs.DestructionEase;

            _view.Disable();
            return _view.Destroy(duration, ease);
        }

        private Observable<bool> Move(Vector3 position,
                         float duration = 0, Ease ease = Ease.Flash)
        {
            return _view.SetPosition(position, duration, ease);
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
    }
}