using System.Collections.Generic;
using Theme;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class LevelButton : MonoBehaviour
    {
        /*[SerializeField] private GameObject _completedButton;
        [SerializeField] private Image _completedIconView;

        [SerializeField] private GameObject _uncompletedButton;
        [SerializeField] private Image _uncompletedIconView;

        [SerializeField] private GameObject _currentButton;
        [SerializeField] private Image _currentIconView;*/

        [SerializeField] private ButtonCustomizer _buttonCustomizer;
        [SerializeField] private ColorCustomizer _iconCustomizer;
        [SerializeField] private Image _iconView;

        [Space]

        [SerializeField] private Sprite _theoryIcon;
        [SerializeField] private Sprite _TemplateIcon;
        [SerializeField] private Sprite _practiceIcon;

        public void Init(LevelButtonMode mode, LevelButtonProgress progress)
        {
            Dictionary<LevelButtonMode, Sprite> _iconsMap = new()
            {
                { LevelButtonMode.Theory, _theoryIcon },
                { LevelButtonMode.Template, _TemplateIcon },
                { LevelButtonMode.Practice, _practiceIcon },
            };

            Dictionary<LevelButtonProgress, ThemeTags> _themeTagsMap = new()
            {
                { LevelButtonProgress.Completed, ThemeTags.CompletedLevelButton },
                { LevelButtonProgress.Uncompleted, ThemeTags.UncompletedLevelButton },
                { LevelButtonProgress.Current, ThemeTags.CurrentLevelButton },
            };

            _iconView.sprite = _iconsMap[mode];
            _iconView.SetNativeSize();

            _buttonCustomizer.SetTag(_themeTagsMap[progress]);
            _iconCustomizer.SetTag(_themeTagsMap[progress]);
        }
    }
}