
using Configs;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Theory
{
    public class TheoryCubeUnfolding : MonoBehaviour
    {
        [SerializeField] private CubeConfigs _cubeConfigs;
        [SerializeField] private List<TheoryCubeCell> _cells;

        [Space]

        [SerializeField] private TMP_Text _cubeNameView;

        private void Awake()
        {
            for (int i = 0; i < _cells.Count; i++)
            {
                _cells[i].gameObject.SetActive(i < _cubeConfigs.Words.Count);

                if (i >= _cubeConfigs.Words.Count) continue;

                var color = _cubeConfigs.Material.color;
                var word = _cubeConfigs.Words[i];

                _cells[i].Init(color, word);
            }

            var cubeName = $" Û· {_cubeConfigs.Number}. {_cubeConfigs.Name}";
            _cubeNameView.text = cubeName;
        }
    }
}