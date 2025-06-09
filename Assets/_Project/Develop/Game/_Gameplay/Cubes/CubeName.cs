using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Gameplay
{
    public class CubeName : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _view;
        [SerializeField] private TMP_Text _nameView;

        private Tweener _transparencyTweener;

        private void Awake()
        {
            _view.alpha = 0f;
        }

        public void SetName(int number, string name)
        {
            var fullName = $" Û· {number}. {name}";
            _nameView.text = fullName;
        }

        public void Show(float duration, Ease ease)
        {
            SetTransparency(1f, duration, ease);
        }

        public void Hide(float duration, Ease ease)
        {
            SetTransparency(0f, duration, ease);
        }

        private void SetTransparency(float value, float duration, Ease ease)
        {
            _transparencyTweener?.Kill(false);
            _transparencyTweener = _view.DOFade(value, duration).SetEase(ease);
        }
    }
}