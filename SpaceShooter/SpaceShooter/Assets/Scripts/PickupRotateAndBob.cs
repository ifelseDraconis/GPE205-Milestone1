using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupRotateAndBob : MonoBehaviour
{
    private Transform thisPickupTransform;
    private float moveMe = 0.0f;
    [Range(0.1f, 12.0f)]
    public float bounceSpeed;
    private bool rise = false;

    // Start is called before the first frame update
    void Start()
    {
        thisPickupTransform = transform;
    }

    // Update is called once per frame
    void Update()
    {
        moveMe += 180.0f * Time.deltaTime;

        Quaternion target = Quaternion.Euler(0.0f, moveMe, 0.0f);
        

        if (thisPickupTransform.position.y <= 2.0f)
        {
            rise = true;
            
        }
        else if (thisPickupTransform.position.y >= 4.5f)
        {
            rise = false;
        }

        if (rise)
        {
            thisPickupTransform.Translate(Vector3.up * Time.deltaTime * bounceSpeed, Space.World);
        }
        else
        {
            thisPickupTransform.Translate(-Vector3.up * Time.deltaTime * bounceSpeed, Space.World);
        }


        thisPickupTransform.rotation = target;
    }
}
