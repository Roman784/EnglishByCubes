using System.Collections.Generic;
using Theme;
using UnityEngine;

namespace UI
{
    public class ProgressBarDot : MonoBehaviour
    {
        [SerializeField] private ColorCustomizer _view;

        public void Highlight()
        {
            _view.SetTag(ThemeTags.HighlightInTheoryProgress);
        }

        public void RemoveHighlight()
        {
            _view.SetTag(ThemeTags.MainFill);
        }
    }
}