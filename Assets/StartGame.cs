using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class StartGame : NetworkBehaviour
{

    public MonsterSpawner monsterSpawner;
    public extraLifeSpawner extraLifeSpawner;
    public extraSpeedSpawner extraSpeedSpawner;
    public SurvivalTimer survivalTimer;
    
    void Start()
    {
        
    }

private bool monstersSpawned = false;

void Update()
{
    if (IsServer && !monstersSpawned) // Ensure this runs only once on the server
    {
        if (NetworkManager.Singleton.ConnectedClients.Count == 2)
        {
            // Two players have joined, spawn two monsters once
            
            monsterSpawner.SpawnMonster();
            monsterSpawner.SpawnMonster();
            survivalTimer.enabled = true;
            monstersSpawned = true; // Prevent further monster spawning 
            
        }
    }

    if (monstersSpawned == true) {

    survivalTimer.enabled = true;

    }
    
    if (IsServer) {

        extraLifeSpawner.StartSpawningExtraLife();
        extraSpeedSpawner.StartSpawningExtraSpeed();

    } 

}


}
