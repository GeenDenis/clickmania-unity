using Sirenix.OdinInspector;

namespace SDK.Data
{
    [System.Serializable]
    public class KeyValueContainer<T1, T2>
    {
        [TableColumnWidth(120, Resizable = false)]
        public T1 Key;
		
        public T2 Value;
    }
}