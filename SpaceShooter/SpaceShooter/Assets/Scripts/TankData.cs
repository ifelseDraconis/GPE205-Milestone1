using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankData : MonoBehaviour
{
    public enum ShipType { PlayerShip, PlayerOne, PlayerTwo, KamiKazeShip, HunterShip, WanderingShip, LostShip};
    public ShipType thisBeast;    

    public GameManager thisGameManager { get; private set; }
    public TankHealth thisShipHealth { get; private set; }
    public TankPhysic thisShipPhysic { get; private set; }
    public TankController thisShipController { get; private set; }

    public CameraFollow thisCamera;

    public GameObject thisShip;
    private GameObject myCannon;

    private Transform thisShipTrans;
    private CannonShot thisPewPew;

    private float invlTime;

    public float waypointCloseness { get; private set; }

    public float turnSpeed { get; private set; }
    public float moveSpeed { get; private set; }
    public float fireRate { get; private set; }
    public float avoidanceTime { get; private set; }

    public List<PowerUp> powerUps;    

    void Awake()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        thisGameManager = GameManager.thisManager;
        thisShipHealth = GetComponent<TankHealth>();
        thisShipPhysic = GetComponent<TankPhysic>();
        thisShipController = GetComponent<TankController>();
        waypointCloseness = thisGameManager.waypointCloseDistance;
        avoidanceTime = thisGameManager.AvoidBlockTime;
        
        powerUps = new List<PowerUp>();
        thisShipTrans = GetComponent<Transform>();

        switch (thisBeast)
        {
            case ShipType.PlayerShip:
                myCannon = gameObject.transform.Find("ShootyPoint").gameObject;
                moveSpeed = thisGameManager.PlayerForwardSpeed;
                turnSpeed = thisGameManager.PlayerRotateSpeed;
                fireRate = thisGameManager.PlayerFireRate;
                thisPewPew = myCannon.GetComponent<CannonShot>();
                break;

            case ShipType.PlayerOne:
                myCannon = gameObject.transform.Find("ShootyPoint").gameObject;
                moveSpeed = thisGameManager.PlayerForwardSpeed;
                turnSpeed = thisGameManager.PlayerRotateSpeed;
                fireRate = thisGameManager.PlayerFireRate;
                thisPewPew = myCannon.GetComponent<CannonShot>();
                break;

            case ShipType.PlayerTwo:
                myCannon = gameObject.transform.Find("ShootyPoint").gameObject;
                moveSpeed = thisGameManager.PlayerForwardSpeed;
                turnSpeed = thisGameManager.PlayerRotateSpeed;
                fireRate = thisGameManager.PlayerFireRate;
                thisPewPew = myCannon.GetComponent<CannonShot>();
                break;

            case ShipType.HunterShip:
                myCannon = gameObject.transform.Find("ShootyPoint").gameObject;
                moveSpeed = thisGameManager.EnemySpeed;
                turnSpeed = thisGameManager.EnemyRotateSpeed;
                fireRate = thisGameManager.EnemyFireRate;
                thisPewPew = myCannon.GetComponent<CannonShot>();
                break;

            case ShipType.KamiKazeShip:
                myCannon = gameObject.transform.Find("ShootyPoint").gameObject;
                moveSpeed = thisGameManager.EnemySpeed;
                turnSpeed = thisGameManager.EnemyRotateSpeed;
                fireRate = thisGameManager.EnemyFireRate;
                thisPewPew = myCannon.GetComponent<CannonShot>();
                break;

            case ShipType.WanderingShip:
                myCannon = gameObject.transform.Find("ShootyPoint").gameObject;
                moveSpeed = thisGameManager.EnemySpeed;
                turnSpeed = thisGameManager.EnemyRotateSpeed;
                fireRate = thisGameManager.EnemyFireRate;
                thisPewPew = myCannon.GetComponent<CannonShot>();
                break;

            case ShipType.LostShip:
                myCannon = gameObject.transform.Find("ShootyPoint").gameObject;
                moveSpeed = thisGameManager.EnemySpeed;
                turnSpeed = thisGameManager.EnemyRotateSpeed;
                fireRate = thisGameManager.EnemyFireRate;
                thisPewPew = myCannon.GetComponent<CannonShot>();
                break;

            default:
                myCannon = gameObject.transform.Find("ShootyPoint").gameObject;
                moveSpeed = thisGameManager.EnemySpeed;
                turnSpeed = thisGameManager.EnemyRotateSpeed;
                fireRate = thisGameManager.PlayerFireRate;
                thisPewPew = myCannon.GetComponent<CannonShot>();
                break;
        }        
        
        
    }

    public void createBullet()
    {
        FireBullet();
    }

    void Update()
    {
        List<PowerUp> expiredPowerUps = new List<PowerUp>();
        if (invlTime > 0)
        {
            invlTime -= Time.deltaTime;
        }

        

        // run through the list of powerups attached to this game object
        foreach (PowerUp power in powerUps)
        {
            power.duration -= Time.deltaTime;

            // check out the duration amount on the powerup, and remove it if the duration is finished
            if(power.duration <= 0)
            {                
                expiredPowerUps.Add(power);                
            }
        }

        // then goes back through and removes the various items that expired, rather than trying to remove them mid stream
        foreach (PowerUp expiredPower in expiredPowerUps)
        {
            expiredPower.DeactivatePower(thisShip.GetComponent<TankData>());
            try
            {
                powerUps.Remove(expiredPower);
            }
            catch
            {

            }
        }

        expiredPowerUps.Clear();
    }

    public void adjustShipSpeed(float newShipSpeedDelta)
    {
        moveSpeed += newShipSpeedDelta;
        turnSpeed += newShipSpeedDelta;
    }

    public void addShipHealth(int thisNewHealth)
    {
        thisShipHealth.alterTankHealth(thisNewHealth);
    }

    public void alterShipTotalHealth(int thisNewMaxHealth, int thisNewHealth)
    {
        if (thisNewMaxHealth > thisShipHealth.thisTankMaxHealth)
        {            
            thisShipHealth.alterTankMaxHealth(thisNewMaxHealth);
            thisShipHealth.alterTankHealth(thisNewHealth);
        }
        else
        {
            thisShipHealth.alterTankMaxHealth(thisNewMaxHealth);            
        }
        
    }

    public void addToScore(int newPoints)
    {
        thisGameManager.PlayerScore += newPoints;
    }

    void FireBullet()
    {
        thisPewPew.fireTheCannon(thisShipTrans);
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.tag == "Bullet")
        {
                thisShipHealth.takeHit();
                invlTime = thisGameManager.PlayerInvlTime;
        }

        if (other.tag == "PowerUp")
        {
            this.AddPowerUp(other.gameObject.GetComponent<PowerUp>());
        }        
        
    }

    public void AddPowerUp(PowerUp powerUp)
    {
        // activates the effect of the powerup
        powerUp.ActivatePower(thisShip.GetComponent<TankData>());

        // doesn't add the power up to the list, so it can't be removed
        if (!powerUp.isPermanent)
        {
            powerUps.Add(powerUp);
        }
    }
}
