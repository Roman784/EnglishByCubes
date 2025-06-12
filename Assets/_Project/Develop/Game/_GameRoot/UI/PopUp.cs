using UnityEngine;

namespace UI
{
    public abstract class PopUp : MonoBehaviour
    {
        public virtual void Open()
        {
            
        }

        public virtual void Close()
        {

        }

        public void Destroy()
        {
            Destroy(gameObject);
        }
    }
}