using Unity.Netcode;
using UnityEngine;
using System.Collections;

public class extraSpeedSpawner : NetworkBehaviour
{
    public GameObject extraSpeedPrefab; // Assign in the Unity editor

    private void Start()
    {
        if (IsServer)
        {
            StartSpawningExtraSpeed();
        }
    }

    private IEnumerator SpawnExtraSpeedRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(10 + Random.Range(0f, 10f)); // Wait for 10 to 20 seconds

            GameObject existingExtraSpeed = GameObject.FindWithTag("extraSpeed");
            if (existingExtraSpeed == null)
            {
                Debug.Log("Spawning new extra speed");
                GameObject speed = Instantiate(extraSpeedPrefab);
                speed.GetComponent<NetworkObject>().Spawn();
            }
            else
            {
                Debug.Log("Extra Speed already exists. Not spawning a new one.");
            }
        }
    }

    public void StartSpawningExtraSpeed()
    {
        StartCoroutine(SpawnExtraSpeedRoutine());
    }
}
