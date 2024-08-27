using Extension;
using UnityEngine;

namespace UI.Screens
{
    public abstract class Screen : MonoBehaviour
    {
        public void Open()
        {
            gameObject.Activate();
        }

        public void Close()
        {
            gameObject.Deactivate();
        }
    }
}