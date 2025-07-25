using Audio;
using Configs;
using DG.Tweening;
using GameRoot;
using R3;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using Utils;

namespace Gameplay
{
    public class Cube
    {
        private readonly CubeView _view;
        private readonly CubeConfigs _configs;
        private readonly IGameFieldService _gameFieldService;
        private readonly CubesPositionPreviewService _cubesPositionPreviewService;
        private readonly AudioProvider _audioProvider;
        private readonly IConfigsProvider _configsProvider;

        private readonly CubeBehaviorHandler _behaviorHandler;

        private List<string> _words;
        private int _curretWordIndex;

        private Camera _camera;
        private Coroutine _dragRoutine;

        private SlotBar _slotBar;

        public int Number => _configs.Number;
        public string CurrentWord => _words[_curretWordIndex];
        public Vector3 Position => _view.GetPosition();
        public bool IsInRemoveArea { get; private set; }
        public UnityEvent OnRotated { get; private set; } = new();
        public UnityEvent<bool> OnDragged { get; private set; } = new();

        private CubeAudioConfigs AudioConfigs => _configsProvider.GameConfigs.AudioConfigs.CubeConfigs;

        public Cube(CubeView view, CubeConfigs configs,
                    IGameFieldService gameFieldService,
                    CubesPositionPreviewService cubesPositionPreviewService,
                    AudioProvider audioProvider, IConfigsProvider configsProvider)
        {
            _view = view;
            _configs = configs;
            _gameFieldService = gameFieldService;
            _cubesPositionPreviewService = cubesPositionPreviewService;
            _audioProvider = audioProvider;
            _configsProvider = configsProvider;

            var number = _configs.Number;
            var name = _configs.Name;
            var material = _configs.Material;

            _words = _configs.Words;
            _camera = Camera.main;

            _behaviorHandler = new(this);
            _view.Init(number, name, _words, material);

            _view.OnInRemoveAreaEnter.AddListener(() => IsInRemoveArea = true);
            _view.OnInRemoveAreaExit.AddListener(() => IsInRemoveArea = false);

            _view.OnWordInWordListSelected.AddListener(word =>
            {
                var wordIndex = GetWordIndex(word);
                _curretWordIndex = wordIndex;
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

        public void AddToSlot(SlotBar slotBar, Slot slot)
        {
            _behaviorHandler.SetInSLotBehavior();

            _slotBar = slotBar;

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
            PlayRotationSound();

            _slotBar.RemoveCube(this);
            var cube = _gameFieldService.CreateCube(_configs);

            cube.SetSlotBar(_slotBar);
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

        public void SetSlotBar(SlotBar slotBar)
        {
            _slotBar = slotBar;
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

            OnDragged.Invoke(false);
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

            PlayRotationSound();

            OnRotated.Invoke();
        }

        private IEnumerator DragRoutine()
        {
            _cubesPositionPreviewService.PrepareForPreviewCubePosition(this);

            var startPosition = _camera.ScreenToWorldPoint(Input.mousePosition);
            var offset = startPosition - Position;
            var onDraggedTrigged = false;

            while (true)
            {
                yield return null;

                var position = _camera.ScreenToWorldPoint(Input.mousePosition) - offset;
                position.y = Position.y;

                SetPosition(position);
                _cubesPositionPreviewService.PreviewCubePosition(this);

                if (!onDraggedTrigged && Vector3.Distance(startPosition, position + offset) > 0.2f)
                {
                    onDraggedTrigged = true;
                    OnDragged.Invoke(true);
                }
            }
        }

        public Observable<bool> Destroy()
        {
            _slotBar.RestoreCube(Number);

            PlayDestructionSound();

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

        private void PlayRotationSound()
        {
            var clip = AudioConfigs.RotationSounds[Random.Range(0, AudioConfigs.RotationSounds.Count)];
            _audioProvider.PlaySound(clip);
        }

        private void PlayMoveSound()
        {
            var clip = AudioConfigs.MoveSound;
            _audioProvider.PlaySound(clip);
        }

        private void PlayDestructionSound()
        {
            var clip = AudioConfigs.DestructionSound;
            _audioProvider.PlaySound(clip);
        }
    }
}