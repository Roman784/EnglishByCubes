using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Gameplay
{
    public class CubeWord : MonoBehaviour
    {
        [SerializeField] private TMP_Text _wordView;
        [SerializeField] private Button _selectButton;

        private CubeWordList _wordList;
        private string _word;

        private void Awake()
        {
            _selectButton.onClick.AddListener(SelectWord);
        }

        public void Init(CubeWordList wordList, string word)
        {
            _wordList = wordList;
            _word = word;

            _wordView.text = _word;
        }

        private void SelectWord()
        {
            _wordList.SelectWord(_word);
        }
    }
}