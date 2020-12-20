using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{
    public enum AIBehaviorChoice { HostileGunner, SuicideRunner, DiveBomber, JohnnyFootballHelmet, FireBad}
    public AIBehaviorChoice currentAIChoice = AIBehaviorChoice.JohnnyFootballHelmet;

    private AIBehaviorChoice previousBehavior;

    private GameManager thisGameManager;

    private GameObject[] playerTargetList;
    private GameObject playerTarget;
    private Transform thisShipTransform;
    private GameObject thisHostile;
    private TankHealth thisHostileHealth;

    public float HearingDistance { get; private set; }
    public float viewingRadius { get; private set; }
    public float viewingAngle { get; private set; }
    public float looksThisOften { get; private set; }

    private LayerMask targetMask;
    private LayerMask obstacleMask;

    public enum WaypointSet { SetA, SetB, SetC, SetD, SetE};
    public WaypointSet currentTravelPath;
    private GameObject[] waypointList;
    private int currentWaypoint;

    public enum LoopType { Stop, Loop, Pingpong};
    public LoopType patrolMethod;

    private Vector3 afterThem;
    private bool isLeaving;

    private int avoidanceStage;
    private float avoidanceTime;
    private float exitTime;

    private TankData thisTankNexus;
    private float lastShot;

    private bool isForward;

    private float diveDistance;   


    // Start is called before the first frame update
    void Start()
    {
        thisGameManager = GameManager.thisManager;
        thisShipTransform = GetComponent<Transform>();
        HearingDistance = thisGameManager.EnemyHearingDistance;        
        thisTankNexus = GetComponent<TankData>();
        avoidanceTime = thisTankNexus.avoidanceTime;
        lastShot = Time.time;
        isForward = true;
        isLeaving = false;
        diveDistance = thisGameManager.diveDistance;
        thisHostile = gameObject;
        thisHostileHealth = gameObject.GetComponent<TankHealth>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (playerTarget == null)
        {
            playerTarget = FindPlayer();
        }

        switch (currentAIChoice)
        {
            // shoots at the player when they get close
            case AIBehaviorChoice.HostileGunner:
                if(hearPlayer())
                {
                    doSomeShooting();
                }
                else
                {
                    //currentAIChoice = AIBehaviorChoice.JohnnyFootballHelmet;
                }
                break;

            case AIBehaviorChoice.SuicideRunner:
                if (avoidanceStage != 0)
                {
                    DoAvoidance();
                }
                else
                {
                    if (hearPlayer())
                    {
                        DoChase();
                    }
                }                
                break;

            case AIBehaviorChoice.DiveBomber:
                if (avoidanceStage != 0)
                {
                    DoAvoidance();
                }
                else
                {
                    if (ShallDiveBomb())
                    {
                        doSomeShooting();
                    }
                    else
                    {
                        if (hearPlayer())
                        {

                        }
                        else
                        {
                            DoChase();
                        }
                    }
                }                
                break;

            case AIBehaviorChoice.JohnnyFootballHelmet:
                // when the ship is generally just patrolling or loitering
                // switches to being an immobile gunner if it has no waypoints
                if (myFootballHelmet())
                {
                    // does something if following waypoints
                    progressWaypoint();
                }
                else
                {
                    currentAIChoice = AIBehaviorChoice.HostileGunner;
                }
                break;

            case AIBehaviorChoice.FireBad:
                if (avoidanceStage != 0)
                {
                    DoAvoidance();
                }
                else
                {
                    beginFleeing();
                }
                
                
                break;

            default:
                // if the AI state somehow gets set to an non-existant state
                currentAIChoice = AIBehaviorChoice.JohnnyFootballHelmet;
                break;
        }

        if (thisHostileHealth.thisTankHealth <= 2)
        {
            currentAIChoice = AIBehaviorChoice.FireBad;
        }
    }

    void beginFleeing()
    {
        // case logic for making the AI run away from the player with obstacle avoidance
        if (playerTarget != null)
        {
            if (isLeaving)
            {
                if (Vector3.SqrMagnitude(playerTarget.transform.position - thisShipTransform.position) < (thisTankNexus.waypointCloseness * thisTankNexus.waypointCloseness * 2.0f))
                {
                    afterThem = findUnblockedPath(lookAway(playerTarget.transform), (thisTankNexus.waypointCloseness / 2.0f));
                    isLeaving = true;
                }
            }
            else
            {

            }
            
        }
        else
        {
            currentAIChoice = previousBehavior;
        }
    }

    bool ShallDiveBomb()
    {
        Vector3 position = this.thisShipTransform.position;
        Vector3 diff = playerTarget.transform.position - position;
        float differentDis = diff.sqrMagnitude;
        if (differentDis <= diveDistance)
        {            
            beginFleeing();
            return false;
        }        
        
        return true;
    }

    Quaternion lookAway(Transform target)
    {
        // this holds the vector that leads towards the target
        Vector3 vectorTowardsTarget;

        // this loads the vector to the given target, preparing to send the ship at that target
        vectorTowardsTarget = target.position - this.thisShipTransform.position;

        // this let us know what direction is away from the player
        vectorTowardsTarget = -1 * vectorTowardsTarget;

        vectorTowardsTarget.Normalize();
        afterThem = vectorTowardsTarget;

        // Find what the Quaternion for the look rotation that the ship would need
        Quaternion targetRotation = Quaternion.LookRotation(vectorTowardsTarget);

        // check the rotation to see if we are already looking that direction
        if (this.thisShipTransform.rotation == targetRotation)
        {
            // there wasn't a rotation, so we are already looking in the direction
            return targetRotation;
        }

        // pushes a rotation step to the Pawn to shift the rotation direction of the ship
        thisTankNexus.thisShipController.pawnRotation(Quaternion.RotateTowards(this.thisShipTransform.rotation, targetRotation, thisTankNexus.turnSpeed * Time.deltaTime));

        // there was a rotation, so return true
        return targetRotation;
    }

    void doSomeShooting()
    {
        if (playerTarget != null)
        {
            if (RotateTowards(playerTarget.transform))
            {
                // the ship isn't looking at the target yet
            }
            else
            {
                fireThatCannon();
            }
        }

        if (playerTarget == null)
        {
            playerTarget = FindPlayer();
            currentAIChoice = AIBehaviorChoice.JohnnyFootballHelmet;
        }
    }

    // this code looks for the nearest instance of a player object
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

    void ChasePlayer ()
    {
        

        if (playerTarget != null)
        {
            // code to send the enemies after the player
            Vector3 diff = playerTarget.transform.position - this.thisShipTransform.position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance <= thisTankNexus.thisGameManager.EnemyEngagementDistance)
            {
                if (RotateTowards(playerTarget.transform))
                {
                    // do nothing
                }
                else
                {
                    // begin following the player
                    thisTankNexus.thisShipController.movePawn(new Vector3(0.0f, 0.0f, thisTankNexus.moveSpeed * Time.deltaTime));

                    // code to fire the lazer, maybe
                    fireThatCannon();
                }
            }
            
        }

        if (playerTarget == null)
        {
            currentAIChoice = AIBehaviorChoice.JohnnyFootballHelmet;
        }
    }

    bool FollowWaypoints()
    {
        if (waypointList[currentWaypoint] != null)
        {
            // code to send the enemies after the player
            if (RotateTowards(waypointList[currentWaypoint].transform))
            {
                // do nothing
            }
            else
            {
                // begin following the path to the waypoint
                thisTankNexus.thisShipController.movePawn(new Vector3(0.0f, 0.0f, thisTankNexus.moveSpeed * Time.deltaTime));
            }

            if (Vector3.SqrMagnitude(waypointList[currentWaypoint].transform.position - thisShipTransform.position) < (thisTankNexus.waypointCloseness * thisTankNexus.waypointCloseness))
            {
                progressWaypoint();
            }

            return true;
        }

        return false;
    }

    bool hearPlayer()
    {        
        Vector3 position = this.thisShipTransform.position;
        Vector3 diff = playerTarget.transform.position - position;
        float differentDis = diff.sqrMagnitude;
        if(differentDis <= HearingDistance)
        {
            currentAIChoice = AIBehaviorChoice.HostileGunner;
        }

        return false;
    }

    bool myFootballHelmet()
    {
        //this code is just for loitering or going to waypoints

        if (waypointList != null)
            {
                return FollowWaypoints();
            }

        return false;
    }

    void progressWaypoint()
    {
        switch (patrolMethod)
        {
            case LoopType.Stop:
                // code for going through the waypoints once and then stopping
                if (currentWaypoint < waypointList.Length - 1)
                {
                    if (canMove(thisTankNexus.moveSpeed))
                    {
                        currentWaypoint++;
                        DoAvoidance();
                    }
                    else
                    {
                        if (avoidanceStage == 0)
                        {
                            avoidanceStage = 1;
                        }
                        else
                        {
                            DoAvoidance();
                        }
                    }
                }
                break;

            case LoopType.Loop:
                // code for going through the waypoints repeatedly
                if (currentWaypoint < waypointList.Length - 1)
                {
                    if (canMove(thisTankNexus.moveSpeed))
                    {
                        currentWaypoint++;
                        DoAvoidance();
                    }
                    else
                    {
                        if (avoidanceStage == 0)
                        {
                            avoidanceStage = 1;
                        }
                        else
                        {
                            DoAvoidance();
                        }
                    }
                }
                else
                {
                    currentWaypoint = 0;
                }
                break;

            case LoopType.Pingpong:
                // code for going back and forth through the waypoints
                if (isForward)
                {
                    // advance to the next waypoint if it exists
                    if (currentWaypoint < waypointList.Length - 1)
                    {
                        if (canMove(thisTankNexus.moveSpeed))
                        {
                            currentWaypoint++;
                            DoAvoidance();
                        }
                        else
                        {
                            if (avoidanceStage == 0)
                            {
                                avoidanceStage = 1;
                            }
                            else
                            {
                                DoAvoidance();
                            }
                        }
                    } 
                    else
                    {
                        if (canMove(thisTankNexus.moveSpeed))
                        {
                            currentWaypoint--;
                            isForward = false;
                            DoAvoidance();
                        }
                        else
                        {
                            if (avoidanceStage == 0)
                            {
                                avoidanceStage = 1;
                            }
                            else
                            {
                                DoAvoidance();
                            }
                        }
                    }
                } 
                else
                {
                    if (currentWaypoint > 0)
                    {
                        currentWaypoint--;
                    } 
                    else
                    {
                        currentWaypoint++;
                        isForward = true;
                    }
                }
                break;

            default:
                // this resets the patrol method to stop so that the system doesn't give me problems.
                patrolMethod = LoopType.Stop;
                break;
        }
    }

    bool RotateTowards(Transform target)
    {
        // this holds the vector that leads towards the target
        Vector3 vectorTowardsTarget;

        // this loads the vector to the given target, preparing to send the ship at that target
        vectorTowardsTarget = target.position - this.thisShipTransform.position;

        // Find what the Quaternion for the look rotation that the ship would need
        Quaternion targetRotation = Quaternion.LookRotation(vectorTowardsTarget);

        // check the rotation to see if we are already looking that direction
        if (this.thisShipTransform.rotation == targetRotation)
        {
            // there wasn't a rotation, so we are already looking in the direction
            return false;
        }

        // pushes a rotation step to the Pawn to shift the rotation direction of the ship
        thisTankNexus.thisShipController.pawnRotation(Quaternion.RotateTowards(this.thisShipTransform.rotation, targetRotation, thisTankNexus.turnSpeed * Time.deltaTime));

        // there was a rotation, so return true
        return true;
    }

    void fireThatCannon()
    {
        // finds the current distance between the hostile ship and the player
        Vector3 diff = playerTarget.transform.position - this.thisShipTransform.position;
        float curDistance = diff.sqrMagnitude;
        if (curDistance >= thisTankNexus.thisGameManager.EnemyEngagementDistance)
        {
            // code to fire the lazer if the timer is okay
            if (Time.time - lastShot > (10.0f / thisTankNexus.fireRate))
            {
                thisTankNexus.createBullet();
                lastShot = Time.time;
            }
        }
    }

    public void everyBaddieForThemselves()
    {
        previousBehavior = currentAIChoice;
        currentAIChoice = AIBehaviorChoice.FireBad;
    }

    // this method is going to use raycasts to check if the way ahead is clear in a closing cone in order to see which path is better.
    Vector3 findUnblockedPath(Quaternion desiredPoint, float castingDistance)
    {
        Vector3 wayForward = new Vector3(0.0f, 0.0f, 0.0f);
        wayForward = afterThem + playerTarget.transform.position;


        return wayForward;
    }

    
    public bool canMove (float speed)
    {
        // this checks to see if there is something in front of the ship
        RaycastHit hit;
        if (Physics.Raycast(thisShipTransform.position, thisShipTransform.forward, out hit, thisTankNexus.moveSpeed))
        {
            if (!hit.collider.CompareTag("Player"))
            {
                return false;
            }
        }
        // TODO check if we can move forward by "Speed" units
        return true;
    }

    void DoChase()
    {
        //check to see if the ship can move, and then make it move
        if (canMove(thisTankNexus.moveSpeed))
        {
            thisTankNexus.thisShipController.movePawn(thisShipTransform.forward * thisTankNexus.moveSpeed);
        }
        else
        {
            // manuver around to get out of the way of the obstacle
            avoidanceStage = 1;
        }
    }

    void DoAvoidance()
    {
        //TODO Write avoidance function
        if (avoidanceStage == 1)
        {
            //Rotate left
            thisTankNexus.thisShipController.rotatePawn(-thisShipTransform.right * thisTankNexus.moveSpeed);

            // if it can move forward then move on to stage 2
            if (canMove(thisTankNexus.moveSpeed))
            {
                avoidanceStage = 2;

                exitTime = avoidanceTime;
            }

            // otherwise turn left again next turn
        }
        else if (avoidanceStage == 2)
        {
            // if it can move forward, go ahead and do it
            if (canMove(thisTankNexus.moveSpeed))
            {
                // subtract from the timer and move
                exitTime -= Time.deltaTime;

                // move forward
                thisTankNexus.thisShipController.movePawn(-thisShipTransform.forward * thisTankNexus.moveSpeed);

                if(exitTime <= 0)
                {
                    avoidanceStage = 0;
                }
            }
            else
            {
                // can't move forward, so go ahead and go back to stage 1
                avoidanceStage = 1;
            }
        }
    }

    void nearestNextWaypoint(Transform currentWaypoint, Transform previousWaypoint)
    {
        //Transform nextWaypoint;

        //GameObject[] Waypoints;
        //GameObject nearestWaypoint;
    }
    
}
