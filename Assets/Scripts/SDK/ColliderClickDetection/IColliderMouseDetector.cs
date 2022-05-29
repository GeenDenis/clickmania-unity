using System;

namespace SDK.ColliderClickDetection
{
    public interface IColliderMouseDetector
    {
        event Action OnMouseDownEvent;
        event Action OnMouseDragEvent;
        event Action OnMouseEnterEvent;
        event Action OnMouseExitEvent;
        event Action OnMouseOverEvent;
        event Action OnMouseUpEvent;
        event Action OnMouseUpAsButtonEvent;

        void RemoveAllSubscribers();
    }
}