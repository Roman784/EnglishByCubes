using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class GameCompletedPopUp : PopUp
    {
        [Space]

        [SerializeField] private Image _fade;
        [SerializeField] private float _fadeValue;
        [SerializeField] private Ease _fadeTransparencyEase;

        private new void Awake()
        {
            SetTransparency(_fade, 0f, 0, 0);
            SetScale(_view.transform, 0f, 0, 0);
        }

        public override void Open()
        {
            _themeProvider.Customize(gameObject);

            SetScale(_view.transform, 1f, _openDuration, _openEase);
            SetTransparency(_fade, _fadeValue, _openDuration, _fadeTransparencyEase);
        }

        public override void Close()
        {
            SetTransparency(_fade, 0f, _closeDuration, _fadeTransparencyEase);
            SetScale(_view.transform, 0f, _closeDuration, _closeEase)
                .OnComplete(() => Destroy());
        }

        public class Factory : PopUpFactory<GameCompletedPopUp>
        {
        }
    }
}