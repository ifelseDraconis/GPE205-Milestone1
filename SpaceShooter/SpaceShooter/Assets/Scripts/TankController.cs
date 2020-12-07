using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankController : MonoBehaviour
{
    
    private Pawn thisPiece;

    void Start()
    {
        thisPiece = GetComponent<Pawn>();
    }

    public void movePawn (Vector3 movementCommand) 
    {
        thisPiece.makeMove(movementCommand);
    }

    public void rotatePawn (Vector3 rotateCommand)
    {
        thisPiece.makeRotate(rotateCommand);
    }

    public void pawnRotation (Quaternion rotationUpdate)
    {
        thisPiece.doRotation(rotationUpdate);
    }
}
