using System.Collections.Generic;
using TMPro;
using UI;
using UnityEngine;
using UnityEngine.UI;

namespace Template
{
    public class TemplateSlot : MonoBehaviour
    {
        [SerializeField] private Transform _cubePositionPoint;

        [Space]

        [SerializeField] private Image _partView;
        [SerializeField] private TMP_Text _designationView;
        [SerializeField] private TMP_Text _signatureView;

        [Space]

        [SerializeField] private Sprite _leftPart;
        [SerializeField] private Sprite _centerPart;
        [SerializeField] private Sprite _rightPart;

        public Vector3 Position => _cubePositionPoint.position;

        public void Init(TemplateSlotData data, bool isFirstPart = false, bool isLastPart = false)
        {
            var nameMap = new Dictionary<TemplateSlotMode, (string, string)>()
            {
                { TemplateSlotMode.Subject, ("S", "subject")},
                { TemplateSlotMode.Verb, ("V", "verb")},
                { TemplateSlotMode.Object, ("O", "object")},
                { TemplateSlotMode.Adjective, ("A", "adjective")},
                { TemplateSlotMode.Be, ("Be", "be")},
            };

            _partView.sprite = isFirstPart ? _leftPart : isLastPart ? _rightPart : _centerPart;

            _designationView.text = nameMap[data.Mode].Item1;
            _signatureView.text = nameMap[data.Mode].Item2;
        }
    }
}