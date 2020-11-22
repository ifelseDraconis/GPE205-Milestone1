using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{
    private GameObject[] playerTargetList;
    private GameObject playerTarget;
    private Transform thisShipTransform;

    // Start is called before the first frame update
    void Start()
    {
        thisShipTransform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerTarget == null)
        {
            playerTarget = FindPlayer();
        }

        if (playerTarget != null)
        {
            // code to send the enemies after the player
        }
    }

    GameObject FindPlayer()
    {
        playerTargetList = GameObject.FindGameObjectsWithTag("Player");
        playerTarget = null;
        float distance = Mathf.Infinity;
        Vector3 position = this.thisShipTransform.position;
        foreach (GameObject target in playerTargetList)
        {
            Vector3 diff = target.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if(curDistance < distance)
            {
                playerTarget = target;
                distance = curDistance;
            }
        }

        return playerTarget;
    }
}
