using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Utils
{
    [RequireComponent(typeof(TMP_Text))]
    public class OpenHyperlink : MonoBehaviour, IPointerClickHandler
    {
        private TMP_Text m_textMeshPro;
        private Camera m_Camera;

        void Start()
        {
            m_textMeshPro = GetComponent<TMP_Text>();

            Canvas canvas = GetComponentInParent<Canvas>();
            if (canvas != null && canvas.renderMode == RenderMode.ScreenSpaceOverlay)
            {
                m_Camera = null;
            }
            else
            {
                m_Camera = canvas != null ? canvas.worldCamera : Camera.main;
            }
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            int linkIndex = TMP_TextUtilities.FindIntersectingLink(m_textMeshPro, Input.mousePosition, m_Camera);
            if (linkIndex != -1)
            {
                TMP_LinkInfo linkInfo = m_textMeshPro.textInfo.linkInfo[linkIndex];
                Application.OpenURL(linkInfo.GetLinkID());
            }
        }
    }
}