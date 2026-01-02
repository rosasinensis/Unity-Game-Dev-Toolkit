using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MKUtil
{
    public class LazyStaticInstance<T> : MonoBehaviour where T : MonoBehaviour
    {

        // Primarily used for Databases and making sure they're
        // loaded in the correct order.

        // These instances must manually be Initialized (because load order matters).

        public bool IsInitialized { get; private set; }

        private static T _instance;
        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    StaticInstanceInitialize();
                }
                return _instance;
            }
        }

        public static void StaticInstanceInitialize()
        {

            // Make sure the Instance exists.

            if (_instance != null)
            {
                return;
            }

            T[] allObjects = FindObjectsOfType<T>();

            if (allObjects.Length > 1)
            {
                Debug.LogWarning($"[MKUtil] Multiple instances of {typeof(T)} found. Cleaning up.");
                _instance = allObjects[0];
                var length = allObjects.Length;
                for (int i = 1; i < length; i++)
                {
                    Destroy(allObjects[i].gameObject);
                }
            }
            else if (allObjects.Length == 1)
            {
                _instance = allObjects[0];
            }
            else
            {
                var go = new GameObject($"[Static] {typeof(T).Name}");
                _instance = go.AddComponent<T>();
            }

            DontDestroyOnLoad(_instance.gameObject);

        }

        public virtual void LoadData()
        {

            // Load 'safe' data which doesn't require information from other Instances yet.
            // Load mods here, but don't necessarily communicate with everything yet.

            // Example:
            // Make a blueprint of the DarkSword item to place into the ItemDatabase, which
            // uses a modded Element "Chaos" from the ElementDatabase.

        }

        public void Initialize()
        {
            if (IsInitialized)
            {
                return;
            }

            // Finally Initialize everything else which requires communication between
            // the Instances. Usually building the objects itself is done here.

            // Example:
            // Item "Dark Sword" requires blueprint from ItemDatabase, which requires
            // ElementDatabase to fetch "Chaos" element from its database.

            StartInitialization();
            IsInitialized = true;
        }
        public virtual void StartInitialization() { }

    }


}