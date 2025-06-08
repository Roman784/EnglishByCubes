using TMPro;
using UnityEngine;

namespace Gameplay
{
    public class CubeWord : MonoBehaviour
    {
        [SerializeField] private TMP_Text _wordView;

        public void SetWord(string text)
        {
            _wordView.text = text;
        }
    }
}