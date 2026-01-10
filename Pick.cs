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
            private List<WeightedItem<T>> _items;
            public readonly ReadOnlyCollection<WeightedItem<T>> items;
            public float TotalWeight { get; private set; }

            public WeightedObject(params (T item, float weight)[] tupleItems)
            {
                _items = new List<WeightedItem<T>>();
                for (int i = 0; i < tupleItems.Length; i++)
                {
                    var tuple = tupleItems[i];
                    _items.Add(new WeightedItem<T>(tuple.item, tuple.weight));
                    TotalWeight += tuple.weight;
                }
            }
            public WeightedObject(params WeightedItem<T>[] newItems)
            {
                _items = new List<WeightedItem<T>>();
                for (int i = 0; i < newItems.Length; i++)
                {
                    var wItem = newItems[i];
                    _items.Add(wItem);
                    TotalWeight += wItem.weight;
                }
            }

            public void Add(params WeightedItem<T>[] newItems)
            {
                if (newItems == null || newItems.Length == 0)
                {
                    return;
                }
                for (int i = 0; i < newItems.Length; i++)
                {
                    var wItem = newItems[i];
                    _items.Add(wItem);
                    TotalWeight += wItem.weight;
                }
            }
            public void Add(params (T item, float weight)[] tupleItems)
            {
                if (tupleItems == null || tupleItems.Length == 0)
                {
                    return;
                }
                for (int i = 0; i < tupleItems.Length; i++)
                {
                    var tuple = tupleItems[i];
                    _items.Add(new WeightedItem<T>(tuple.item, tuple.weight));
                    TotalWeight += tuple.weight;
                }
            }

        }

        /// Picking logic
        public static T Weighted<T>(WeightedObject<T> weightedObj)
        {

            if (weightedObj?.items == null || weightedObj.items.Count == 0)
            {
                return default;
            }
            float totalWeight = weightedObj.TotalWeight;

            // Pick a point between 0 and total weight.
            float roll = (float)rng.NextDouble() * totalWeight;
            float cursor = 0;

            var count = weightedObj.items.Count;
            for (int i = 0; i < count; i++)
            {
                var item = weightedObj.items[i];
                cursor += item.weight;
                if (roll <= cursor)
                {
                    return item.item;
                }
            }
            return weightedObj.items[count - 1].item;

        }
        public static T Weighted<T>(params (T item, float weight)[] itemTuples)
        {
            if (itemTuples == null || itemTuples.Length == 0)
            {
                return default;
            }

            var weightedObject = new WeightedObject<T>(itemTuples);
            return Weighted(weightedObject);
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