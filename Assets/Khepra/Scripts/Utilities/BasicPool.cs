using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Utilities
{
    public class BasicPool : MonoBehaviour
    {
        /// <summary>
        /// The dictionary that contains the pool game objects;
        /// </summary>
        [SerializeField] private StringListDictionary poolDictionary = new();

        
        /// <summary>
        /// Public function that spawns a clone of the given prefab GameObject and sets it as a child of the given container Transform.
        /// </summary>
        /// <param name="prefab">The prefab GameObject to clone.</param>
        /// <param name="container">The Transform to set as the parent of the cloned GameObject.</param>
        /// <returns>A GameObject that is a clone of the given prefab GameObject, and is set as a child of the given container Transform.</returns>
        public GameObject SpawnClone(GameObject prefab, Transform container)
        {
            // Check if the prefab is already in the pool of objects.
            if (!poolDictionary.ContainsKey(prefab.name))
            {
                // If it is not in the pool, create a new list for the prefab and add a new clone of the prefab to the list.
                poolDictionary.Add(prefab.name, new List<GameObject>());
                return Instantiate(prefab, container);
            }

            // Check if there are any clones of the prefab available in the pool.
            if (poolDictionary[prefab.name].Count == 0) 
                // If there are not, instantiate a new clone of the prefab and return it.
                return Instantiate(prefab, container);
            
            // Get the last clone from the list of clones for the prefab.
            var clone = poolDictionary[prefab.name].Last();
            // Remove the clone from the list of clones for the prefab.
            poolDictionary[prefab.name].Remove(clone);
            // Set the clone to be active.
            clone.SetActive(true);
            // Set the parent Transform of the clone to the container Transform.
            clone.transform.SetParent(container);
            // Return the clone.
            return clone;

        }

        private GameObject CreateInstance(GameObject prefab, Transform container)
        {
            GameObject instance = Instantiate(prefab, container);
            poolDictionary[prefab.name].Add(instance);
            
            return instance;
        }
        
        /// <summary>
        /// Despawns the given clone.
        /// </summary>
        /// <param name="prefabName">The prefab name of the clone to despawn.</param>
        /// <param name="clone">The GameObject clone to despawn.</param>
        /// <param name="pool">The Transform pool to place the despawned clone in.</param>
        public void DeSpawnClone(string prefabName, GameObject clone, Transform pool)
        {
            poolDictionary[prefabName].Add(clone);
            StartCoroutine(DisableClones(clone, pool));
        }

        /// <summary>
        /// Disables the given clone and places it in the given pool.
        /// </summary>
        /// <param name="clone">The GameObject clone to disable and place in the pool.</param>
        /// <param name="pool">The Transform pool to place the disabled clone in.</param>
        /// <returns>An IEnumerator that can be used to yield until the clone has been disabled and placed in the pool.</returns>
        private IEnumerator DisableClones(GameObject clone, Transform pool)
        {
            yield return null;
            clone.transform.SetParent(pool);
            clone.SetActive(false);
        }
    }
}
