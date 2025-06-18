using Configs;
using DG.Tweening;
using System;
using System.Collections.Generic;
using Theme;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class LevelButton : MonoBehaviour
    {
        [SerializeField] private ButtonCustomizer _buttonCustomizer;
        [SerializeField] private ColorCustomizer _iconCustomizer;
        [SerializeField] private Image _iconView;
        [SerializeField] private GameObject _line;

        [Space]

        [SerializeField] private Sprite _theoryIcon;
        [SerializeField] private Sprite _TemplateIcon;
        [SerializeField] private Sprite _practiceIcon;

        private int _number;
        private bool _isLocked;
        private Action<int> _openLevel;

        public void Init(LevelConfigs configs, LevelButtonProgress progress, Action<int> openLevel)
        {
            Dictionary<LevelMode, Sprite> _iconsMap = new()
            {
                { LevelMode.Theory, _theoryIcon },
                { LevelMode.Template, _TemplateIcon },
                { LevelMode.Practice, _practiceIcon },
            };

            Dictionary<LevelButtonProgress, ThemeTags> _themeTagsMap = new()
            {
                { LevelButtonProgress.Completed, ThemeTags.CompletedLevelButton },
                { LevelButtonProgress.Uncompleted, ThemeTags.UncompletedLevelButton },
                { LevelButtonProgress.Current, ThemeTags.CurrentLevelButton },
            };

            _number = configs.Number;
            _isLocked = progress == LevelButtonProgress.Uncompleted;
            _openLevel = openLevel;

            _iconView.sprite = _iconsMap[configs.Mode];
            _iconView.SetNativeSize();

            _buttonCustomizer.SetTag(_themeTagsMap[progress]);
            _iconCustomizer.SetTag(_themeTagsMap[progress]);

            _line.SetActive(configs.Mode == LevelMode.Theory);
        }

        public void OpenLevel()
        {
            if (_isLocked) return;
            _openLevel?.Invoke(_number);
        }
    }
}