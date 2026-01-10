using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MKUtil
{
    public static class Pick
    {
        // Pick or shuffle from items. Handles weighted stuff, too.
        // For loot tables, weighted spawning, randomization...

        // Example: 
        // var rareFish = Pick.Weighted(fishList);
        // var randomDialogueChoice = Pick.RandomFrom(choices);
        // Pick.Shuffle(inventory);


        public static System.Random rng = new System.Random();
        // Using System.Random for seeds.

        public static void SetSeed(int seed) => rng = new System.Random(seed);

        public struct WeightedItem<T>
        {
            public T item;
            public float weight;

            public WeightedItem(T item, float weight = 1.0f)
            {
                this.item = item;
                this.weight = weight;
            }
        }

        public class WeightedObject<T>
        {
            public WeightedItem<T>[] items;
            public int Count => items.Length;

            public WeightedObject(params WeightedItem<T>[] newItems)
            {
                items = newItems;
            }

            public float GetTotalWeight()
            {
                float total = 0f;
                for (int i = 0; i < items.Length; i++)
                {
                    total += items[i].weight;
                }
                return total;
            }
        }

        /// Picking logic

        public static T Weighted<T>(params (T item, float weight)[] itemTuples)
        {

            if (itemTuples == null || itemTuples.Length == 0)
            {
                return default;
            }

            float totalWeight = 0;

            for (int i = 0; i < itemTuples.Length; i++)
            {
                totalWeight += itemTuples[i].weight;
            }

            float roll = (float)rng.NextDouble() * totalWeight;
            float cursor = 0;

            for (int i = 0; i < itemTuples.Length; i++)
            {
                var tuple = itemTuples[i];
                cursor += tuple.weight;
                if (roll <= cursor)
                {
                    return tuple.item;
                }
            }
            return itemTuples[itemTuples.Length - 1].item;
        }


        public static T Weighted<T>(WeightedObject<T> weightedObj)
        {

            if (weightedObj == null || weightedObj.Count == 0)
            {
                return default;
            }

            float totalWeight = weightedObj.GetTotalWeight();
            // Pick a point between 0 and total weight.
            float roll = (float)rng.NextDouble() * totalWeight;

            float cursor = 0;
            for (int i = 0; i < weightedObj.Count; i++)
            {
                var item = weightedObj.items[i];
                cursor += item.weight;
                if (roll <= cursor)
                {
                    return item.item;
                }
            }
            return weightedObj.items[weightedObj.Count - 1].item;

        }

        /// Shuffle logic
        public static void Shuffle<T>(IList<T> collection)
        {
            // Something like scrambling the contents of something inside.
            // Pick.Shuffle(chest)

            // Uses Fisher-Yates shuffle.

            int n = collection.Count;

            for (int i = 0; i < n; i++)
            {
                int r = i + (int)(rng.NextDouble() * (n - i));
                T temp = collection[r];
                collection[r] = collection[i];
                collection[i] = temp;
            }
        }

        public static int RandomInt(int size)
        {
            return rng.Next(size);
        }
        public static T RandomFrom<T>(IList<T> collection)
        {
            // Picks a random item from a collection.
            return collection[RandomInt(collection.Count)];
        }
    }
}