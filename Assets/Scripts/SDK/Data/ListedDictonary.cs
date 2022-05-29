using System.Collections.Generic;
using System.Linq;
using SDK.GameCore.Interfaces;
using Sirenix.OdinInspector;
using UnityEngine;

namespace SDK.Data
{
	[System.Serializable] [InlineProperty] [HideLabel]
	public class ListedDictionary<T1, T2> : IEditorInitable
	{
		[SerializeField] [LabelText("@$property.Parent.NiceName")] [TableList]
		private List<KeyValueContainer<T1, T2>> list = new List<KeyValueContainer<T1, T2>>();
		

		public List<T1> Keys
		{
			get
			{
				var keys = new List<T1>();

				for (var i = 0; i < list.Count; i++)
				{
					keys.Add(list[i].Key);
				}

				return keys;
			}
		}
		
		public List<T2> Values
		{
			get
			{
				var values = new List<T2>();

				for (var i = 0; i < list.Count; i++)
				{
					values.Add(list[i].Value);
				}

				return values;
			}
		}
		
		
		public T2 this[T1 key]
		{
			get => TryGetValue(key, out var value) ? value : default;
			set => SetValue(key, value);
		}

		public void Add(T1 key, T2 value)
		{
			if (ContainsKey(key) == false)
			{
				list.Add(new KeyValueContainer<T1, T2>
				{
					Key = key,
					Value = value
				});
			}
		}

		public void Remove(T1 key)
		{
			for (var i = 0; i < list.Count; i++)
			{
				if (Equals(list[i].Key, key))
				{
					list.RemoveAt(i);
					i--;
				}
			}
		}
		
		public bool ContainsKey(T1 key)
		{
			for (var i = 0; i < list.Count; i++)
			{
				if (Equals(list[i].Key, key))
				{
					return true;
				}
			}

			return false;
		}
		
		public bool ContainsValue(T2 value)
		{
			for (var i = 0; i < list.Count; i++)
			{
				if (Equals(list[i].Value, value))
				{
					return true;
				}
			}

			return false;
		}
		
		public bool TryGetValue(T1 key, out T2 value)
		{
			for (var i = 0; i < list.Count; i++)
			{
				if (Equals(list[i].Key, key))
				{
					value = list[i].Value;

					return true;
				}
			}

			value = default;
			
			return false;
		}

		public void SetValue(T1 key, T2 value)
		{
			for (var i = 0; i < list.Count; i++)
			{
				if (Equals(list[i].Key, key))
				{
					list[i].Value = value;

					return;
				}
			}
		}


#if UNITY_EDITOR
		public void E_Init()
		{
			list = list
				.GroupBy(container => container.Key)
				.Select(grouping => grouping.First())
				.ToList();
		}
#endif
	}
}