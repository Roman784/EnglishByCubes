
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
        [SerializeField] private RectTransform _container;

        [Space]

        [SerializeField] private TMP_Text _cubeNameView;

        private void Awake()
        {
            var wordsCount = _cubeConfigs.Words.Count;

            for (int i = 0; i < _cells.Count; i++)
            {
                _cells[i].gameObject.SetActive(i < wordsCount);

                if (i >= _cubeConfigs.Words.Count) continue;

                var color = _cubeConfigs.Material.color;
                var word = _cubeConfigs.Words[i];

                _cells[i].Init(color, word);
            }

            var cubeName = $" Û· {_cubeConfigs.Number}. {_cubeConfigs.Name}";
            _cubeNameView.text = cubeName;

            var cellSize = 172;
            if (wordsCount < 5)
            {
                _container.sizeDelta = new Vector2(_container.sizeDelta.x, cellSize);

                if (wordsCount % 2 == 1)
                {
                    foreach (var cell in _cells)
                    {
                        var rect = cell.GetComponent<RectTransform>();
                        rect.anchoredPosition += new Vector2(cellSize / 2, 0);
                    }
                }
            }
            else if (wordsCount < 6)
            {
                _container.sizeDelta = new Vector2(_container.sizeDelta.x, cellSize * 2);
                foreach (var cell in _cells)
                {
                    var rect = cell.GetComponent<RectTransform>();
                    rect.anchoredPosition = new Vector2(rect.anchoredPosition.x, rect.anchoredPosition.y - cellSize / 2);
                }
            }
            else
            {
                _container.sizeDelta = new Vector2(_container.sizeDelta.x, cellSize * 3);
            }
        }
    }
}