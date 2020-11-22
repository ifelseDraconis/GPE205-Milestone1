using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public PlayerController playerControls { get; private set; }
    private long PlayerScore;
    public long currentScore;

    void Awake()
    {
        if (playerControls == null)
        {
            playerControls = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // pushes an update into the field on a given player character's ship
        currentScore = getPlayerScore();
    }

    // protected access methods for the scoring system
    public long getPlayerScore()
    {
        return PlayerScore;
    }

    // a way to increase the score from outside of the game object
    public void addToPlayerScore(long newMoney)
    {
        PlayerScore += newMoney;
    }
}
