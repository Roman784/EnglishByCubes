using DG.Tweening;
using UnityEngine;

namespace Gameplay
{
    public class Slot : MonoBehaviour
    {
        [SerializeField] private Vector3 _positionOffset;
        [SerializeField] private float _cubeScale;

        public Vector3 Position => transform.position + _positionOffset;

        public void PlaceCube(Cube cube, float duration = 0, Ease ease = Ease.Flash)
        {
            cube.SetPosition(Position, duration, ease);
            cube.SetScale(_cubeScale);
        }
    }
}