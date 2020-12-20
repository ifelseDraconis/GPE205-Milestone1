using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Text>().text = GameManager.thisManager.PlayerScore.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
