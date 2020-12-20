using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class TankHealth : MonoBehaviour
{
    private TankData thisTankData;

    public int thisTankHealth { get; private set; }
    public int thisTankMaxHealth { get; private set; }
    public AudioClip goesBoom;

    private float invlTime;
    private float invlTimeAmount;

    private bool isPlayer;


    void Awake()
    {
        thisTankData = GetComponent<TankData>();
    }

    // Start is called before the first frame update
    void Start()
    {
        // decides which health profile to use based off of which instance profile is implemented
        isPlayer = false;
        invlTime = 0.0f;
        switch (thisTankData.thisBeast)
        {
            case TankData.ShipType.PlayerShip:
                thisTankHealth = thisTankData.thisGameManager.PlayerHealth;
                thisTankMaxHealth = thisTankHealth;
                invlTimeAmount = thisTankData.thisGameManager.PlayerInvlTime;
                isPlayer = true;
                break;

            case TankData.ShipType.HunterShip:
                thisTankHealth = GameManager.thisManager.HunterHealth;
                thisTankMaxHealth = thisTankHealth;
                isPlayer = false;
                break;

            case TankData.ShipType.KamiKazeShip:
                thisTankHealth = GameManager.thisManager.KamiKazeHealth;
                thisTankMaxHealth = thisTankHealth;
                isPlayer = false;
                break;

            case TankData.ShipType.WanderingShip:
                thisTankHealth = GameManager.thisManager.WanderHealth;
                thisTankMaxHealth = thisTankHealth;
                isPlayer = false;
                break;

            case TankData.ShipType.LostShip:
                thisTankHealth = GameManager.thisManager.LostHealth;
                thisTankMaxHealth = thisTankHealth;
                isPlayer = false;
                break;

            default:
                thisTankHealth = GameManager.thisManager.LostHealth;
                thisTankMaxHealth = thisTankHealth;
                isPlayer = false;
                break;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlayer)
        {
            if (invlTime > 0)
            {
                invlTime -= Time.deltaTime;
            }
        }
        
    }

    public void takeHit()
    {
        if (invlTime <= 0) 
        {
            this.thisTankHealth--;
            if (thisTankHealth <= 0)
            {
                AudioSource.PlayClipAtPoint(goesBoom, transform.position);
                GameManager.thisManager.RespawnPlayer();
                transform.position = new Vector3(0.0f, 2.5f, 0.0f);
                //Destroy(gameObject);
            }
            if (isPlayer)
            {
                invlTime = invlTimeAmount;
            }
        }
        
    }

    public void alterTankHealth(int NewHealth)
    {
        thisTankHealth += NewHealth;
        if (thisTankHealth > thisTankMaxHealth)
        {
            thisTankHealth = thisTankMaxHealth;
        }
    }

    public void alterTankMaxHealth(int NewMaxHealth)
    {
        thisTankMaxHealth = NewMaxHealth;
        if (thisTankHealth > thisTankMaxHealth)
        {
            thisTankHealth = thisTankMaxHealth;
        }
    }
}
