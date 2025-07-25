using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace UI
{
    public class TheoryPageLink : MonoBehaviour
    {
        [SerializeField] private TMP_Text _view;

        private int _pageIdx = -1;

        public UnityEvent<int> OnFollow = new();

        public void Init(int pageIdx, string title)
        {
            _pageIdx = pageIdx;
            _view.text = title;
        }

        public void Follow()
        {
            OnFollow.Invoke(_pageIdx);
        }
    }
}