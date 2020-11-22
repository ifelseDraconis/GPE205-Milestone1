using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankData : MonoBehaviour
{
    public GameManager thisGameManager;
    public TankHealth thisShipHealth;
    public TankPhysic thisShipPhysic;
    public TankController thisShipController;
    public GameObject myBulletShot;

    private Transform thisShipTrans;


    // Start is called before the first frame update
    void Start()
    {
        thisGameManager = GameManager.thisManager;
        thisShipHealth = GetComponent<TankHealth>();
        thisShipPhysic = GetComponent<TankPhysic>();
        thisShipController = GetComponent<TankController>();
    }

    public void createBullet()
    {
        FireBullet();
    }

    void Update()
    {
        thisShipTrans = GetComponent<Transform>();
    }

    void FireBullet()
    {
        // this produces and then fires a bullet at a specific point in space with the same rotation as the ship
        
        Vector3 launchPoint = new Vector3(thisShipTrans.position.x, thisShipTrans.position.y, thisShipTrans.position.z);
        Instantiate(myBulletShot, launchPoint, new Quaternion(thisShipTrans.localRotation.x, thisShipTrans.localRotation.y, thisShipTrans.localRotation.z, 1));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Bullet")
        {
            thisShipHealth.takeHit();
        }
        
    }
}
