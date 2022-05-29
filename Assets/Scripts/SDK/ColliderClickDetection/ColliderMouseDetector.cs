using System;
using System.Diagnostics.SymbolStore;
using UnityEngine;

namespace SDK.ColliderClickDetection
{
    public class ColliderMouseDetector : MonoBehaviour
    {
        public event Action OnMouseDownEvent = delegate { };
        public event Action OnMouseDragEvent = delegate { };
        public event Action OnMouseEnterEvent = delegate { };
        public event Action OnMouseExitEvent = delegate { };
        public event Action OnMouseOverEvent = delegate { };
        public event Action OnMouseUpEvent = delegate { };
        public event Action OnMouseUpAsButtonEvent = delegate { };

        public bool IsActive { get; private set; }

        private void Awake()
        {
            IsActive = gameObject.activeInHierarchy;
        }

        private void OnMouseDown() => OnMouseDownEvent.Invoke();

        private void OnMouseDrag() => OnMouseDragEvent.Invoke();

        private void OnMouseEnter() => OnMouseEnterEvent.Invoke();

        private void OnMouseExit() => OnMouseExitEvent.Invoke();

        private void OnMouseOver() => OnMouseOverEvent.Invoke();

        private void OnMouseUp() => OnMouseUpEvent.Invoke();

        private void OnMouseUpAsButton() => OnMouseUpAsButtonEvent.Invoke();

        public void RemoveAllSubscribers()
        {
            OnMouseDownEvent = delegate { };
            OnMouseDragEvent = delegate { };
            OnMouseEnterEvent = delegate { };
            OnMouseExitEvent = delegate { };
            OnMouseOverEvent = delegate { };
            OnMouseUpEvent = delegate { };
            OnMouseUpAsButtonEvent = delegate { };
        }

        public void SetActiveInput(bool active)
        {
            gameObject.SetActive(active);
        }
    }
}