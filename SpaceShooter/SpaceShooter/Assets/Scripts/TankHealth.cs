using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankHealth : MonoBehaviour
{
    public TankData thisTankData;

    private int thisTankHealth;
    private int thisTankMaxHealth;

    // Start is called before the first frame update
    void Start()
    {
        thisTankHealth = thisTankData.thisGameManager.PlayerHealth;
        thisTankHealth = thisTankData.thisGameManager.PlayerHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void takeHit()
    {
        thisTankHealth--;
        if (thisTankHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}
