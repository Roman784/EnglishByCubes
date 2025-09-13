using Audio;
using Configs;
using ProceduralAnimations;
using System;
using System.Collections.Generic;
using Theme;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class LevelButton : MonoBehaviour
    {
        [SerializeField] private ButtonCustomizer _buttonCustomizer;
        [SerializeField] private ColorCustomizer _iconCustomizer;
        [SerializeField] private SpriteRenderer _iconView;
        [SerializeField] private TMP_Text _titleView;
        [SerializeField] private Renderer _renderer;
        [SerializeField] private GameObject _line;

        [Space]

        [SerializeField] private Material _completedMat;
        [SerializeField] private Material _uncompletedMat;
        [SerializeField] private Material _currentMat;
        [SerializeField] private Color _uncompletedIconColor;

        [Space]

        [SerializeField] private ButtonAudioPlayer _buttonAudioPlayer;
        [SerializeField] private Image _target;

        private int _number;
        private bool _isLocked;
        private Action<int> _openLevel;

        public void Init(LevelConfigs levelConfigs, LevelButtonsConfigs buttonsConfigs, LevelButtonProgress progress, 
                         AudioProvider audioProvider, AudioClip clickSound,
                         Action<int> openLevel)
        {
            Dictionary<LevelMode, Sprite> _iconsMap = new()
            {
                { LevelMode.Theory, buttonsConfigs.TheoryIcon },
                { LevelMode.Template, buttonsConfigs.TemplateIcon },
                { LevelMode.Practice, buttonsConfigs.PracticeIcon },
                { LevelMode.MistakeCorrection, buttonsConfigs.MistakeCorrectionIcon },
            };

            Dictionary<LevelButtonProgress, Material> _materialsMap = new()
            {
                { LevelButtonProgress.Completed, _completedMat },
                { LevelButtonProgress.Uncompleted, _uncompletedMat },
                { LevelButtonProgress.Current, _currentMat },
            };

            _number = levelConfigs.GlobalNumber;
            _isLocked = progress == LevelButtonProgress.Uncompleted;
            _openLevel = openLevel;

            _iconView.sprite = _iconsMap[levelConfigs.Mode];
            _titleView.text = $"{levelConfigs.LocalNumber}. {levelConfigs.Title}";
            _renderer.material = _materialsMap[progress];

            if (progress == LevelButtonProgress.Uncompleted)
            {
                _iconView.color = _uncompletedIconColor;
                _target.enabled = false;
            }

            _line.SetActive(levelConfigs.Mode == LevelMode.Theory);

            _buttonAudioPlayer.Init(audioProvider, clickSound);
        }

        public void OpenLevel()
        {
            if (_isLocked) return;
            _openLevel?.Invoke(_number);
        }
    }
}


// Old.
/*public void Init(LevelConfigs levelConfigs, LevelButtonsConfigs buttonsConfigs, LevelButtonProgress progress, 
                         AudioProvider audioProvider, AudioClip clickSound,
                         Action<int> openLevel)
        {
            Dictionary<LevelMode, Sprite> _iconsMap = new()
            {
                { LevelMode.Theory, buttonsConfigs.TheoryIcon },
                { LevelMode.Template, buttonsConfigs.TemplateIcon },
                { LevelMode.Practice, buttonsConfigs.PracticeIcon },
                { LevelMode.MistakeCorrection, buttonsConfigs.MistakeCorrectionIcon },
            };

            Dictionary<LevelButtonProgress, ThemeTags> _themeTagsMap = new()
            {
                { LevelButtonProgress.Completed, ThemeTags.CompletedLevelButton },
                { LevelButtonProgress.Uncompleted, ThemeTags.UncompletedLevelButton },
                { LevelButtonProgress.Current, ThemeTags.CurrentLevelButton },
            };

            _number = levelConfigs.GlobalNumber;
            _isLocked = progress == LevelButtonProgress.Uncompleted;
            _openLevel = openLevel;

            _iconView.sprite = _iconsMap[levelConfigs.Mode];
            _iconView.SetNativeSize();

            _titleView.text = $"{levelConfigs.LocalNumber}. {levelConfigs.Title}";

            _buttonCustomizer.SetTag(_themeTagsMap[progress]);
            _iconCustomizer.SetTag(_themeTagsMap[progress]);

            _line.SetActive(levelConfigs.Mode == LevelMode.Theory);

            _buttonAudioPlayer.Init(audioProvider, clickSound);
        }*/