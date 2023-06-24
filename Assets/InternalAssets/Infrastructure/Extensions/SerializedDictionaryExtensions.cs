using System.Collections.Generic;
using AYellowpaper.SerializedCollections;

namespace SouthBasement.Extensions.DataStructures
{
    public static class SerializedDictionaryExtensions
    {
        public static Dictionary<TKey, TValue> ToDictionary<TKey, TValue>(this SerializedDictionary<TKey, TValue> serializedDictionary)
        {
            Dictionary<TKey, TValue> dictionary = new();

            foreach (var pair in serializedDictionary)
                dictionary.Add(pair.Key, pair.Value);

            return dictionary;
        }
    }
}