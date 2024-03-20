using Unity.Netcode;
using UnityEngine;
using System.Collections;

public class extraLifeSpawner : NetworkBehaviour
{
    public GameObject extraLifePrefab; // Assign in the Unity editor

    private void Start()
    {
        if (IsServer)
        {
            StartSpawningExtraLife();
        }
    }

    private IEnumerator SpawnExtraLifeRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(10 + Random.Range(0f, 10f)); // Wait for 10 to 20 seconds

            GameObject existingExtraLife = GameObject.FindWithTag("extraLife");
            if (existingExtraLife == null)
            {
                Debug.Log("Spawning new extra life");
                GameObject life = Instantiate(extraLifePrefab);
                life.GetComponent<NetworkObject>().Spawn();
            }
            else
            {
                Debug.Log("Extra life already exists. Not spawning a new one.");
            }
        }
    }

    public void StartSpawningExtraLife()
    {
        StartCoroutine(SpawnExtraLifeRoutine());
    }
}
