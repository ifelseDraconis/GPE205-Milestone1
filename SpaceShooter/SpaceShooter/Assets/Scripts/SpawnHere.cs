using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnHere : MonoBehaviour
{
    public enum SpawnType { playerSpawn, hostileSpawn}
    public SpawnType thisSpawnerMakes;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RespawnPlayer(GameObject playerShip)
    {
        Vector3 playerStart = new Vector3(transform.position.x, transform.position.y + 1.4f, transform.position.z);
        var thePlayer = Instantiate(playerShip, playerStart, Quaternion.identity);
        thePlayer.GetComponent<TankData>().thisBeast = TankData.ShipType.PlayerShip;
    }

    // assigns what type of spawner this is, preventing the spawner from spawning both players and hostiles.
    public void assignSpawnType(SpawnType thisSpawner)
    {
        thisSpawnerMakes = thisSpawner;
    }
}
