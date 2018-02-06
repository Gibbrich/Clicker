using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public static class Utils
    {
        public static T GetRandomItem<T>(T[] items)
        {
            int id = Random.Range(0, items.Length);
            return items[id];
        }
    }
}