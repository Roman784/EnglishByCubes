using TMPro;
using UnityEngine;

namespace Gameplay
{
    public class CubeSide : MonoBehaviour
    {
        [field: SerializeField] public CanvasGroup View { get; private set; } 
        [field: SerializeField] public TMP_Text WordView { get; private set; } 
        [field: SerializeField] public Vector3 Rotation { get; private set; }

        public void SetWord(string word)
        {
            WordView.text = word;
        }

        [ContextMenu("Fast Setup")]
        private void FastSetup()
        {
            try
            {
                View = GetComponent<CanvasGroup>();
                WordView = transform.GetChild(0).GetComponent<TMP_Text>();
            }
            catch { Debug.LogError($"Failed to Fast Setup cube: {gameObject.name}"); }
        }
    }
}