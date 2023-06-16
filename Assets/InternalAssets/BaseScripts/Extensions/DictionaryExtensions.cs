using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace TheRat.Extensions.DataStructures
{
    public static class DictionaryExtensions
    {
        public static TValue[] ToValueArray<TKey, TValue>(this Dictionary<TKey, TValue> dictionary)
        {
            List<TValue> valueList = new();

            foreach (var pair in dictionary)
            {
                if (pair.Value != null)
                {
                    valueList.Add(pair.Value);
                }
                else
                {
                    Debug.LogWarning("Null value defined");
                }
            }

            return valueList.ToArray();
        }
        public static TKey[] ToKeyArray<TKey, TValue>(this Dictionary<TKey, TValue> dictionary)
        {
            List<TKey> keyList = new();

            foreach (var pair in dictionary)
            {
                if (pair.Key != null)
                {
                    keyList.Add(pair.Key);
                }
                else
                {
                    Debug.LogWarning("Null key defined");
                }
            }

            return keyList.ToArray();
        }
    }
}