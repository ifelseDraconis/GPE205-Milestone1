using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerCreator : MonoBehaviour
{
    public GameObject whatToSpawn;
    public GameObject thisChildPowerup;

    public GameObject[] thisPowerup;

    private float powerUpRespawnTimer;
    private float countDownTimer;

    // Start is called before the first frame update
    void Start()
    {
        powerUpRespawnTimer = GameManager.thisManager.powerUpRespawnTimer;
        countDownTimer = powerUpRespawnTimer;
        createMyBaby();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (thisChildPowerup == null)
        {
            countDownTimer -= Time.deltaTime;
            if (countDownTimer <= 0.0f)
            {
                decideWhatToSpawn();
                createMyBaby();
            }
        }
    }

    void createMyBaby()
    {
        if (whatToSpawn == null)
        {
            decideWhatToSpawn();
        }
        countDownTimer = powerUpRespawnTimer;
        GameObject thisSpawn = Instantiate(whatToSpawn, gameObject.transform.position, Quaternion.identity);
        thisSpawn.transform.parent = gameObject.transform;
        thisChildPowerup = thisSpawn;
    }

    void decideWhatToSpawn()
    {
        int thisChoice = Random.Range(1, 4);

        switch(thisChoice)
        {
            case 1:
                whatToSpawn = thisPowerup[0];
                break;
            case 2:
                whatToSpawn = thisPowerup[1];
                break;
            case 3:
                whatToSpawn = thisPowerup[2];
                break;
            default:
                whatToSpawn = thisPowerup[0];
                break;
        }
    }

    public void someoneAteIt()
    {
        thisChildPowerup = null;
    }
}
