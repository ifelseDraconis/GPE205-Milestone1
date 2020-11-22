using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankController : MonoBehaviour
{
    public InputController thisController;
    public AIController thisAIMaster;

    void Start()
    {
        thisController = GetComponent<InputController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
