using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class FirstEntrancePopUp : PopUp
    {
        [Space]

        [SerializeField] private Image _fade;
        [SerializeField] private float _fadeValue;
        [SerializeField] private Ease _fadeTransparencyEase;

        public override void Close()
        {
            _pauseProvider.ContinueGame();

            SetTransparency(_fade, 0f, _closeDuration, _fadeTransparencyEase);
            SetScale(_view.transform, 0f, _closeDuration, _closeEase)
                .OnComplete(() => Destroy());
        }

        public class Factory : PopUpFactory<FirstEntrancePopUp>
        {
        }
    }
}