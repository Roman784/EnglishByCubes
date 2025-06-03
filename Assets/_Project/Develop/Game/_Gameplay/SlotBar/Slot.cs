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

        public void PlaceCube(Cube cube, float duration = 0, Ease ease = Ease.Flash)
        {
            cube.PlaceInSlot(this);
            cube.SetPosition(Position, duration, ease);
        }
    }
}