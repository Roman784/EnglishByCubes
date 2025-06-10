using System.Collections;
using TMPro;
using UnityEngine;
using Utils;

namespace UI
{
    public class TaskUI : MonoBehaviour
    {
        [SerializeField] private TMP_Text _sentenceView;

        [Space]

        [SerializeField] private float _startAppearanceDelay;
        [SerializeField] private float _lettersAppearanceDelay;

        public void SetSentence(string sentence)
        {
            _sentenceView.text = "";
            Coroutines.Start(AppearSentenceRoutine(sentence));
        }

        private IEnumerator AppearSentenceRoutine(string sentence)
        {
            yield return new WaitForSeconds(_startAppearanceDelay);

            var text = "";
            foreach (var letter in sentence)
            {
                text += letter;
                _sentenceView.text = text;

                yield return new WaitForSeconds(_lettersAppearanceDelay);
            }
        }
    }
}