using System.Collections;
using TMPro;
using UnityEngine;
using Utils;

namespace UI
{
    public class GameplayUI : SceneUI
    {
        [SerializeField] private TaskUI _taskUI;

        public void SetTaskSentence(string sentence)
        {
            _taskUI.SetSentence(sentence);
        }
    }
}