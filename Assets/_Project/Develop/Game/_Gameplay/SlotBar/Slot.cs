using DG.Tweening;
using UnityEngine;

namespace Gameplay
{
    public class Slot : MonoBehaviour
    {
        [SerializeField] private Vector3 _positionOffset;
        [SerializeField] private float _cubeScale;

        public Vector3 Position => transform.position + _positionOffset;
        public float Scale => _cubeScale;
    }
}