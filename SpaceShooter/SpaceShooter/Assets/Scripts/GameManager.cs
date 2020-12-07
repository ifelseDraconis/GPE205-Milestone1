using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // the public instance of the game
    public static GameManager thisManager { get; private set; }

    // public variables that the designer can change in the inspector
    public int mapSeed;

    public int PlayerHealth;
    public float PlayerInvlTime;

    public int KamiKazeHealth;
    public int HunterHealth;
    public int WanderHealth;
    public int LostHealth;

    public float PlayerFireRate;
    public float PlayerForwardSpeed;
    public float PlayerBackwardSpeed;
    public float PlayerRotateSpeed;

    public float EnemySpeed;
    public float EnemyRotateSpeed;
    public float EnemyFireRate;
    public float EnemyEngagementDistance;
    [Range(0.5f, 3.0f)]
    public float AvoidBlockTime;

    [Range(0,6)]
    public int NumberOfHostilesToSpawn;

    public float waypointCloseDistance;

    public float LevelGravity;

    public float EnemyHearingDistance;
    public float EnemyViewingRadius;
    [Range(0,360)]
    public float EnemyViewingAngle;
    [Range(0.1f, 4.0f)]
    public float peekHowOften;
    [Range(0.01f, 120.0f)]
    public float diveDistance;
    public float differenceFactor;

    public float BulletSpeed;
    public float BulletDuration;

    public LayerMask thesePlayers;
    public LayerMask theseHurddles;


    [Range(0.1f, 12.0f)]
    public float powerUpRespawnTimer;
    [Range(0.1f, 15.0f)]
    public float PowerUpspeedModifier;
    [Range(1,12)]
    public int PowerUphealthModifier;
    [Range(1,12)]
    public int PowerUpmaxHealthModifier;
    [Range(0.1f, 12.0f)]
    public float PowerUpfireRateModifier;
    [Range(100, 3000)]
    public int PowerUpscoreBonus;

    [Range(1.5f, 15.0f)]
    public float PowerUpduration;


    public int PlayerScore;
    public GameObject playerShip;
    public GameObject BadGuyOne;
    public GameObject BadGuyTwo;
    public GameObject BadGuyThree;

    private MapMaker thisMap;

    private GameObject thisPlayer;
    private Vector3 playerStart = new Vector3(0f, 2.5f, 0f);

    private List<GameObject> theseEnemies = new List<GameObject>();
    private Vector3 enemySpawn;

    // makes sure something is assigned to the Game Manager on wake
    void Awake()
    {
        if (thisManager == null) 
        { 
            thisManager = this;
        } 
        else
        {
            
        }

    }

    // Start is called before the first frame update
    void Start()
    {        
        thisPlayer = Instantiate(playerShip, playerStart, Quaternion.identity);
        thisPlayer.GetComponent<TankData>().thisBeast = TankData.ShipType.PlayerShip;
        thisMap = MapMaker.thisMapMaker;
        getHostileSpawn();
    }

    // Update is called once per frame
    void Update()
    {
        if (thisPlayer == null)
        {
            getPlayerSpawn();
            thisPlayer = Instantiate(playerShip, playerStart, Quaternion.identity);
            thisPlayer.GetComponent<TankData>().thisBeast = TankData.ShipType.PlayerShip;
        }

        if (theseEnemies == null)
        {
            
        }
    }

    void getPlayerSpawn()
    {
        GameObject[] playerSpawnList = GameObject.FindGameObjectsWithTag("PlayerSpawner");
        GameObject playerSpawn = null;
        playerSpawn = playerSpawnList[rollMeRandom(playerSpawnList.Length)];
        playerStart = playerSpawn.transform.position;
    }

    void getHostileSpawn()
    {
        GameObject[] hostileSpawnList = GameObject.FindGameObjectsWithTag("PlayerSpawner");
        GameObject hostileSpawn = null;
        GameObject newBoogie = null;
        int x = 0;
        while (x < NumberOfHostilesToSpawn)
        {
            hostileSpawn = hostileSpawnList[rollMeRandom(hostileSpawnList.Length)];
            enemySpawn = hostileSpawn.transform.position;
            newBoogie = Instantiate(BadGuyOne, enemySpawn, Quaternion.identity);
            newBoogie.GetComponent<AIController>().currentAIChoice = AIController.AIBehaviorChoice.DiveBomber;
            theseEnemies.Add(newBoogie);
            x++;
        }
        
    }

    int rollMeRandom(int thisMax)
    {
        int result = Random.Range(1, thisMax);
        return result;
    }
}
