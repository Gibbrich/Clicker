using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game
{
    public static class Utils
    {
        public static T GetRandomItem<T>(IList<T> items)
        {
            int id = Random.Range(0, items.Count);
            return items[id];
        }

        public static KeyValuePair<TKey, TValue> GetRandomItem<TKey, TValue>(IDictionary<TKey, TValue> dictionary)
        {
            int id = Random.Range(0, dictionary.Count);
            return dictionary.ElementAt(id);
        }
    }
}