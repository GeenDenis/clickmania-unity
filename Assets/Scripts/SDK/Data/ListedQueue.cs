using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace SDK.Data
{
    [Serializable, HideLabel]
    public class ListedQueue<T>
    {
        [SerializeField, LabelText("@$property.Parent.NiceName")] private List<T> elements;

        public List<T> Elements => new List<T>(elements);
        public T this[int key] => elements[key];
        public int Count => elements.Count;

        public ListedQueue(IEnumerable<T> elements)
        {
            this.elements = new List<T>(elements);
        }
        
        public ListedQueue(int length)
        {
            elements = new List<T>(length);
        }
        
        public void Clear() => elements.Clear();

        public bool Contains(T item) => elements.Contains(item);

        public void Enqueue(T item)
        {
            elements.Add(item);
        }
        
        public T Dequeue()
        {
            T returnElement = default;

            if (elements.Count == 0) return returnElement;
            
            returnElement = elements[0];
            elements.RemoveAt(0);

            return returnElement;
        }

        public T Peek()
        {
            return elements.Count == 0 ? default : elements[0];
        }

        public bool TryDequeue(out T result)
        {
            if (TryPeek(out var peekResult))
            {
                elements.RemoveAt(0);
                result = peekResult;
                return true;
            }
            
            result = peekResult;
            return false;
        }

        public bool TryPeek(out T result)
        {
            result = default;

            if (elements.Count == 0)
            {
                return false;
            }
            
            result = elements[0];
            return true;
        }
    }
}