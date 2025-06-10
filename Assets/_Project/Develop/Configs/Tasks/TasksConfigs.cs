using System.Collections.Generic;
using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = "TasksConfigs", menuName = "Game Configs/Task/New Tasks Configs")]
    public class TasksConfigs : ScriptableObject
    {
        [field: SerializeField] public List<TaskConfigs> Tasks { get; private set; }

        public TaskConfigs GetTask(int number)
        {
            foreach (var task in Tasks)
            {
                if (task.Number == number)
                    return task;
            }

            Debug.LogError($"Task number {number} was not found!");
            return null;
        }

        [ContextMenu("Set Task Numbers")]
        private void SetTaskNumbers()
        {
            for (int i = 0; i < Tasks.Count; i++)
            {
                Tasks[i].SetNumber(i + 1);
            }
        }

        private void OnValidate()
        {
            ValidateTaskNumbers();
        }

        private void ValidateTaskNumbers()
        {
            for (int i = 0; i < Tasks.Count; i++)
            {
                var number = Tasks[i].Number;
                for (int j = i + 1; j < Tasks.Count; j++)
                {
                    if (number == Tasks[j].Number)
                        Debug.LogError($"Task numbers {number} are repeated!");
                }
            }
        }
    }
}