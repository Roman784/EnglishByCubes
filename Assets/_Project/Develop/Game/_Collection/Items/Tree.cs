using DG.Tweening;
using UnityEngine;

namespace Collection
{
    public class Tree : MonoBehaviour
    {
        [SerializeField] private Transform _main;
        [SerializeField] private Transform _shadow;

        [Space]

        [SerializeField] private float _appearDuration;
        [SerializeField] private Ease _appearEase;

        private void Awake()
        {
            _main.localScale = Vector2.right;
            _shadow.localScale = Vector2.up;
        }

        public void Appear()
        {
            Appear(_main, _appearDuration, _appearEase);
            Appear(_shadow, _appearDuration, _appearEase);
        }

        private void Appear(Transform view, float dur, Ease ease)
        {
            view.DOScale(Vector2.one, dur).SetEase(ease);
        }
    }
}