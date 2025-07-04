using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Theory
{
    public class TheoryCubeCell : MonoBehaviour
    {
        [SerializeField] private Image _view;
        [SerializeField] private TMP_Text _textView;

        public void Init(Color color, string word)
        {
            _view.color = color;
            _textView.text = word;
        }
    }
}