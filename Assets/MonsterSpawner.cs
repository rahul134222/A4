using Unity.Netcode;
using UnityEngine;

public class MonsterSpawner : NetworkBehaviour
{
    public GameObject monsterPrefab;

    public void SpawnMonster()
    {

        Debug.Log("Monster Spawn"); 
        //GameObject monsterObj = Instantiate(monsterPrefab);
        //monsterObj.GetComponent<NetworkObject>().Spawn(); // Spawn the monster across the network


        if (IsServer)
        {
            Debug.Log("In server"); 
            GameObject monster = Instantiate(monsterPrefab);
            monster.GetComponent<NetworkObject>().Spawn(); // Spawn the monster across the network
        }
    }
}
