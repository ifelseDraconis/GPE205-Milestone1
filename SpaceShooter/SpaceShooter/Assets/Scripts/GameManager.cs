using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // the public instance of the game
    public static GameManager thisManager;

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
    public GameObject playerOne;
    public GameObject playerTwo;
    public GameObject BadGuyOne;
    public GameObject BadGuyTwo;
    public GameObject BadGuyThree;

    [Range(1, 6)]
    public int playerLives;
    private int currentLives;

    [Range(0.0f, 1.0f)]
    public float MasterVolume;
    [Range(0.0f, 1.0f)]
    public float MusicVolume;
    [Range(0.0f, 1.0f)]
    public float SFXVolume;

    public AudioClip IntroMusic;
    public AudioClip LevelOneMusic;
    public AudioClip GameOverMusic;

    public enum CurrentScene { StartScreen, LevelScreen, GameOverScreen}
    public CurrentScene thisScene;

    public enum playerCount { OnePlayer, TwoPlayers}
    public playerCount selectedPlayers;

    private MapMaker thisMap;
    private bool MapGenerated = false;
    private bool gameStarted = false;
    private bool playerDefeated = false;

    public static GameObject thisPlayer;
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
        else if (thisManager != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        currentLives = playerLives;
    }

    // Update is called once per frame
    void Update()
    {
        Scene nowScene = SceneManager.GetActiveScene();
        switch(thisScene)
        {
            // this loads the start scene and sets the start state
            case CurrentScene.StartScreen:
                if (!gameStarted)
                {
                    
                    playIntroMusics();
                    gameStarted = true;
                    MapGenerated = false;
                    PlayerScore = 0;
                }

                break;

                // this loads the level screen and sets the level finite state
            case CurrentScene.LevelScreen:
                
                if (nowScene.name == "LevelOne")
                {
                    if (MapGenerated)
                    {
                        thisPlayer = GameObject.FindWithTag("Player");
                        if (thisPlayer == null)
                        {
                            
                            getPlayerSpawn();
                            if (selectedPlayers == playerCount.OnePlayer)
                            {
                                thisPlayer = Instantiate(playerShip, playerStart, Quaternion.identity);
                            }
                            else
                            {
                                thisPlayer = Instantiate(playerOne, playerStart, Quaternion.identity);
                                playerStart.x += 10f;
                                Instantiate(playerTwo, playerStart, Quaternion.identity);
                            }
                            
                        }

                        if (theseEnemies == null)
                        {
                            getHostileSpawn();
                        }
                    }
                    else
                    {
                        playLevelMusics();
                        generateLevel();
                        MapGenerated = true;
                    }
                    
                }                

                break;

                // this loads the game over scene and sets the game over finite state
            case CurrentScene.GameOverScreen:
                if (!playerDefeated)
                {
                    thisMap.AllNew();
                    StageHand.thisHand.LoadGameOver();
                    StageHand.thisHand.setNewHighScore(PlayerScore);
                    playDoomMusics();
                    currentLives = playerLives;
                    playerDefeated = true;
                }

                break;
        }

        
    }

    void getPlayerSpawn()
    {
        GameObject[] playerSpawnList = GameObject.FindGameObjectsWithTag("PlayerSpawner");
        GameObject playerSpawn = null;
        if (playerSpawnList.Length > 1) 
        {
            playerSpawn = playerSpawnList[rollMeRandom(playerSpawnList.Length)];
            playerStart = playerSpawn.transform.position;
        }

        
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

    void generateLevel()
    {
        thisMap = MapMaker.thisMapMaker;
        thisMap.CreateMap();
        getHostileSpawn();
        MapGenerated = true;
        
        
    }

    public void playIntroMusics()
    {
        SoundManager.thisSoundManager.PlayMusic(IntroMusic, MusicVolume * MasterVolume);
    }

    public void playLevelMusics()
    {
        SoundManager.thisSoundManager.PlayMusic(LevelOneMusic, MusicVolume * MasterVolume);
    }

    public void playDoomMusics()
    {
        SoundManager.thisSoundManager.PlayMusic(GameOverMusic, MusicVolume * MasterVolume);
    }

    public void StartGame()
    {
        thisScene = CurrentScene.LevelScreen;        
    }

    public void RestartGame()
    {
        thisScene = CurrentScene.StartScreen;
        StageHand.thisHand.LoadIntro();
        gameStarted = false;
    }

    public void RespawnPlayer()
    {
        currentLives--; 
        if (currentLives < 0)
        {
            thisScene = CurrentScene.GameOverScreen;
        }
    }

    public int GetLives()
    {
        return currentLives;
    }
}
