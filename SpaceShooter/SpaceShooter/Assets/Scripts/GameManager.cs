using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // the public instance of the game
    public static GameManager thisManager { get; private set; }

    // public variables that the designer can change in the inspector
    public int PlayerHealth;
    public int EnemyHealth;

    public float PlayerFireRate;
    public float PlayerForwardSpeed;
    public float PlayerBackwardSpeed;
    public float PlayerRotateSpeed;

    public float EnemySpeed;
    public float EnemyRotateSpeed;
    public float EnemyFireRate;
    public float LevelGravity;

    public float BulletSpeed;
    public float BulletDuration;

    public GameObject playerShip;

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
        Vector3 playerStart = new Vector3(0f, 0f, 0f);
        Instantiate(playerShip, playerStart, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
