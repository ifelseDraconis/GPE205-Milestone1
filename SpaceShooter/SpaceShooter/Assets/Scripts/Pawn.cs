using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : MonoBehaviour
{
    private CharacterController thisController;
    private Transform thisShipTransform;

    // Start is called before the first frame update
    void Start()
    {
        thisController = GetComponent<CharacterController>();
        thisShipTransform = GetComponent<Transform>();

    }

    public void makeMove(Vector3 movementAction)
    {
        thisController.Move(movementAction);
    }

    public void makeRotate(Vector3 rotateAction)
    {
        thisShipTransform.Rotate(rotateAction);
    }

    public void doRotation(Quaternion thisRotation)
    {
        thisShipTransform.rotation = thisRotation;
    }
}
