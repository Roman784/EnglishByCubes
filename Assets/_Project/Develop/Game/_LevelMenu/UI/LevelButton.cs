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

        private int _number;
        private bool _isLocked;
        private Action<int> _openLevel;

        public void Init(LevelConfigs levelConfigs, LevelButtonsConfigs buttonsConfigs, 
                         LevelButtonProgress progress, Action<int> openLevel)
        {
            Dictionary<LevelMode, Sprite> _iconsMap = new()
            {
                { LevelMode.Theory, buttonsConfigs.TheoryIcon },
                { LevelMode.Template, buttonsConfigs.TemplateIcon },
                { LevelMode.Practice, buttonsConfigs.PracticeIcon },
            };

            Dictionary<LevelButtonProgress, ThemeTags> _themeTagsMap = new()
            {
                { LevelButtonProgress.Completed, ThemeTags.CompletedLevelButton },
                { LevelButtonProgress.Uncompleted, ThemeTags.UncompletedLevelButton },
                { LevelButtonProgress.Current, ThemeTags.CurrentLevelButton },
            };

            _number = levelConfigs.Number;
            _isLocked = progress == LevelButtonProgress.Uncompleted;
            _openLevel = openLevel;

            _iconView.sprite = _iconsMap[levelConfigs.Mode];
            _iconView.SetNativeSize();

            _buttonCustomizer.SetTag(_themeTagsMap[progress]);
            _iconCustomizer.SetTag(_themeTagsMap[progress]);

            _line.SetActive(levelConfigs.Mode == LevelMode.Theory);
        }

        public void OpenLevel()
        {
            if (_isLocked) return;
            _openLevel?.Invoke(_number);
        }
    }
}