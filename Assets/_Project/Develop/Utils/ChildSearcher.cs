using System.Collections.Generic;
using UnityEngine;

namespace Utils
{
    public class ChildSearcher
    {
        public static List<T> GetAllChilds<T>(GameObject root) where T : Component
        {
            List<T> components = new();
            Search(root, components);

            return components;
        }

        private static void Search<T>(GameObject current, List<T> components) where T : Component
        {
            T[] currentComponents = current.GetComponents<T>();

            if (currentComponents != null && currentComponents.Length > 0)
                components.AddRange(currentComponents);

            foreach (Transform child in current.transform)
            {
                Search(child.gameObject, components);
            }
        }
    }
}