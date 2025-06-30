using Gameplay;
using GameRoot;
using System.Collections.Generic;
using UnityEngine;

namespace Template
{
    public class TemplateCubesLayoutService : ICubesLayoutService
    {
        private TemplateSlots _slots;

        public void SetSlots(TemplateSlots slots)
        {
            _slots = slots;
        }

        public void LayOut(List<Cube> cubes)
        {
            var positions = GetCubePositions(cubes.Count);
            var scale = GetCubeScale(cubes.Count);

            for (int i = 0; i < cubes.Count; i++)
            {
                var cube = cubes[i];
                if (cube == null) continue;

                var position = positions[i];
                cube.PlaceOnField(position, scale);
            }
        }

        public Vector3 GetLastCubePosition(int cubesCount)
        {
            return GetCubePositions(cubesCount)[cubesCount - 1];
        }

        public List<Vector3> GetCubePositions(int cubesCount)
        {
            var slotPositions = _slots.GetSlotPositions();
            var positions = new List<Vector3>();

            for (int i = 0; i < cubesCount; i++)
            {
                if (i < slotPositions.Count)
                    positions.Add(slotPositions[i]);
                else
                    Debug.LogError("Error getting positions more than template slots!");
            }

            return positions;
        }

        public float GetCubeScale(int _)
        {
            var scale = 1.3f - (_slots.Count - 3) * 0.15f;
            scale = Mathf.Clamp(scale, 0, 1.3f);

            return scale;
        }
    }
}