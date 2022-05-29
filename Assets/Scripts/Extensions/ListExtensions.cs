using System.Collections.Generic;

namespace Extensions
{
    public static class ListExtensions
    {
        private static int _removeListFirstIndex;
        public static List<T> RemoveList<T>(this List<T> list, List<T> removeList)
        {
            for (var listIndex = _removeListFirstIndex; listIndex < list.Count; listIndex++)
            {
                for (var removeListIndex = 0; removeListIndex < removeList.Count; removeListIndex++)
                {
                    if (!Equals(list[listIndex], removeList[removeListIndex])) continue;
                    
                    list.RemoveAt(listIndex);
                    return list.RemoveList(removeList);
                }
            }

            _removeListFirstIndex = 0;
            return list;
        }
    }
}