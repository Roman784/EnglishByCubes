using DG.Tweening;
using TMPro;
using UnityEngine;

namespace UI
{
    public class TranslationSentence : MonoBehaviour
    {
        [SerializeField] private TMP_Text _ruView;
        [SerializeField] private TMP_Text _enView;

        private string _ruText;

        public void SetRu(string text)
        {
            _ruView.text = text;
        }

        public void SetEn(string text)
        {
            _ruText = text;
        }

        public void Show()
        {
            _enView.text = _ruText;
            _enView.transform.DOPunchScale(Vector3.one * 0.1f, 0.5f, 2).SetEase(Ease.OutQuad);
        }
    }
}