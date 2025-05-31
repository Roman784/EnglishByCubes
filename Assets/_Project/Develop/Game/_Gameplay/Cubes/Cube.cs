using Configs;
using R3;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Gameplay
{
    public class Cube
    {
        private readonly CubeView _view;
        private readonly CubeConfigs _configs;

        private List<string> _words;
        private int _curretWordIndex;

        public Cube(CubeView view, CubeConfigs configs)
        {
            _view = view;
            _configs = configs;
            _words = _configs.Words;

            _view.Init(_words[0], _configs.Material);

            _view.OnUnpressed.AddListener(() => RotateToNextSide());
        }

        public Observable<bool> Enable()
        {
            return _view.Enable();
        }

        public Observable<bool> Disable(bool instantly = false)
        {
            return _view.Disable(instantly);
        }

        public void SetPosition(Vector3 position)
        {
            _view.SetPosition(position);
        }

        private void RotateToNextSide()
        {
            _curretWordIndex = ++_curretWordIndex % _words.Count();
            var word = _words[_curretWordIndex];

            _view.Rotate(word);
        }
    }
}