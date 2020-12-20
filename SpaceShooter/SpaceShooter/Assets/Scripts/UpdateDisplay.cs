using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateDisplay : MonoBehaviour
{
    public enum whatDisplay { Lives, Score, HighestScore}
    public whatDisplay thisDisplay;

    
    // Update is called once per frame
    void FixedUpdate()
    {
        if (thisDisplay == whatDisplay.Lives)
        {
            GetComponent<Text>().text = GameManager.thisManager.GetLives().ToString();
        }

        if (thisDisplay == whatDisplay.Score)
        {
            GetComponent<Text>().text = GameManager.thisManager.PlayerScore.ToString();
        }

        if (thisDisplay == whatDisplay.HighestScore)
        {
            GetComponent<Text>().text = PlayerPrefs.GetInt("HighScore").ToString();
        }
    }
}
