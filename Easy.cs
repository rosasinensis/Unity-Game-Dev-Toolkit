using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MKUtil
{
    public static class Easy
    {
        // Example:
        // if (Easy.Chance(0.9f)) { /* 90% success rate */ }
        public static bool Chance(float amount)
        {
            return (Random.value < amount);
        }
        // Example:
        // Easy.FiftyFifty()
        public static bool FiftyFifty()
        {
            return Chance(0.5f);
        }

        // -- Extensions --

        // Example:
        // var caughtFish = fishList.Pick()
        public static T Pick<T>(this IList<T> list)
        {
            if (list == null || list.Count == 0)
            {
                return default(T);
            }
            return list[Random.Range(0, list.Count)];
        }

        // Example:
        // itemDatabase.Get(moddedApple, apple);
        public static TValue Get<TKey, TValue>(this Dictionary<TKey, TValue> dict, TKey key, TValue defaultValue)
        {
            return (dict.TryGetValue(key, out TValue result)) ? result : defaultValue;
        }
        // Example:
        // itemDatabase.Get(apple)
        public static TValue Get<TKey, TValue>(this Dictionary<TKey, TValue> dict, TKey key)
        {
            return (dict.TryGetValue(key, out TValue result)) ? result : default(TValue);
        }

        // Example:
        // playerObject.Grab(out HealthBar healthBar)
        public static bool Grab<T>(this GameObject go, out T result)
        {
            result = go.GetComponent<T>();
            return (result != null);
        }
    }
}