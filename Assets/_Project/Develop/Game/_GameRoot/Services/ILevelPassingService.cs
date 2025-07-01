using UnityEngine.Events;

namespace GameRoot
{
    public interface ILevelPassingService
    {
        public void CalculateSentenceMatching(string playerSentence);
    }
}