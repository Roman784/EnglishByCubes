using GameRoot;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

namespace Gameplay
{
    public class GameplayLevelPassingService : ILevelPassingService
    {
        private string _correctSentence;

        public UnityEvent<float> OnSentenceMatchingCalculated { get; private set; } = new();

        public void SetCorrectSentence(string sentence)
        {
            _correctSentence = sentence;
        }

        public void CalculateSentenceMatching(string playerSentence)
        {
            string correct = _correctSentence.ToLower().Replace(" ", "").Trim();
            string player = playerSentence.ToLower().Replace(" ", "").Trim();

            int levenshteinDistance = CalculateLevenshteinDistance(correct, player);
            int maxLength = Mathf.Max(correct.Length, player.Length);

            if (maxLength == 0)
            {
                OnSentenceMatchingCalculated.Invoke(1f);
                return;
            }

            float similarity = (1f - (float)levenshteinDistance / maxLength);
            OnSentenceMatchingCalculated.Invoke(similarity);
        }

        private int CalculateLevenshteinDistance(string a, string b)
        {
            int[,] matrix = new int[a.Length + 1, b.Length + 1];

            for (int i = 0; i <= a.Length; i++)
                matrix[i, 0] = i;

            for (int j = 0; j <= b.Length; j++)
                matrix[0, j] = j;

            for (int i = 1; i <= a.Length; i++)
            {
                for (int j = 1; j <= b.Length; j++)
                {
                    int cost = (a[i - 1] == b[j - 1]) ? 0 : 1;
                    matrix[i, j] = Mathf.Min(
                        matrix[i - 1, j] + 1,
                        matrix[i, j - 1] + 1,
                        matrix[i - 1, j - 1] + cost
                    );
                }
            }

            return matrix[a.Length, b.Length];
        }
    }
}