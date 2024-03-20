using UnityEngine;
using Unity.Netcode;

public class CollectibleManager : NetworkBehaviour
{
    public GameObject lifeCollectiblePrefab; // Assign this in the Unity editor
    public float minX = -5f; // Set the bounds for where collectibles can appear
    public float maxX = 5f;
    public float minY = -5f;
    public float maxY = 5f;
    private GameObject currentCollectible = null; // Track the current collectible

    void Start()
    {
        // Start spawning collectibles at random intervals
        InvokeRepeating("TrySpawnLifeCollectible", 2f, 10f); // Start after 2 seconds, repeat every 10 seconds

    }

    void TrySpawnLifeCollectible()
    {
        // Only spawn a new collectible if there isn't one already
        if (currentCollectible == null)
        {
            currentCollectible = SpawnLifeCollectible();
        }
    }

    public GameObject SpawnLifeCollectible()
    {
        Vector2 spawnPosition = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
        return Instantiate(lifeCollectiblePrefab, spawnPosition, Quaternion.identity);
    }

    public void CollectibleWasCollected()
    {
        // Call this method when the collectible is collected
        currentCollectible = null;
    }

    public void NetworkStart()
    {
        // Call this method when the collectible is collected
    }

}
