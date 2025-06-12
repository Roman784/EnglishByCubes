using Configs;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Gameplay
{
    public class CubeRemoveArea : MonoBehaviour
    {
        [SerializeField] private Image _view;

        [Space]

        private float _firstHighlightLevel;
        private float _secondHighlightLevel;

        private float _highlightDuration;
        private Ease _highlightEase;

        private Tweener _highlightTween;

        public void Init(CubeRemoveAreaConfigs configs, GameFieldService gameFieldService)
        {
            _firstHighlightLevel = configs.FirstHighlightLevel;
            _secondHighlightLevel = configs.SecondHighlightLevel;
            _highlightDuration = configs.HighlightDuration;
            _highlightEase = configs.HighlightEase;

            var color = _view.color;
            color.a = 0;
            _view.color = color;

            gameFieldService.OnCubeCreated.AddListener(cube =>
            {
                cube.OnDragged.AddListener(res =>
                {
                    if (res)
                        Highlight(_firstHighlightLevel);
                    else
                        Highlight(0);
                });
            });
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<CubeView>(out CubeView cube))
            {
                cube.OnInRemoveAreaEnter.Invoke();
                Highlight(_secondHighlightLevel);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent<CubeView>(out CubeView cube))
            {
                cube.OnInRemoveAreaExit.Invoke();
                Highlight(_firstHighlightLevel);
            }
        }

        private void Highlight(float transparency)
        {
            _highlightTween?.Kill(false);
            _highlightTween = _view.DOFade(transparency, _highlightDuration)
                .SetEase(_highlightEase);
        }
    }
}