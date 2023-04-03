using System.Collections.Generic;

namespace TheRat.Extensions.DataStructures
{
    public static class DictionaryExtensions
    {
        public static TValue[] ToValueArray<TKey, TValue>(this Dictionary<TKey, TValue> dictionary)
        {
            List<TValue> valueList = new();

            foreach (var item in dictionary)
                valueList.Add(item.Value);

            return valueList.ToArray();
        }
        public static TKey[] ToKeyArray<TKey, TValue>(this Dictionary<TKey, TValue> dictionary)
        {
            List<TKey> keyList = new();

            foreach (var item in dictionary)
                keyList.Add(item.Key);

            return keyList.ToArray();
        }
    }
}