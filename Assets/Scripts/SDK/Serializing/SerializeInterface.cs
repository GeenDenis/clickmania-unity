using System;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using UnityEngine;

namespace SDK.Serializing
{
    [Serializable, InlineProperty, HideLabel]
    public class SerializeInterface<T> where T : class
    {
        [SerializeField, LabelText("@$property.Parent.NiceName"), OnValueChanged(nameof(ValidateComponent))] 
        private Component component;

        private void ValidateComponent()
        {
            if (component.SafeIsUnityNull()) return;
            if (component.TryGetComponent(out T foundComponent))
            {
                component = foundComponent as Component;
            }
            else
            {
                component = null;
            }
        }

        public T GetComponent()
        {
            return component as T;
        }
    }
}