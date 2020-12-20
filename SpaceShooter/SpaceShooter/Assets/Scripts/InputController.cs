using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    private TankData thisShipData;
    public enum InputScheme { WASD, arrowKeys};
    public InputScheme entryMethod = InputScheme.WASD;


    private float thisPlayerForwardSpeed;
    private float thisPlayerBackSpeed;
    private float thisPlayerRotateSpeed;
    private float thisPlayerFireSpeed;

    private float gravityValue;
    private GameManager myManager;
    public Transform thisTransform;

    private Vector3 controlInput;
    private Vector3 rotateInput;

    private TankController thisJoystick;

    private float lastFired;
    private float fireRate;

    // this loads of the attached variables for a given player controller
    void Start()
    {
        thisShipData = GetComponent<TankData>();
        myManager = thisShipData.thisGameManager;
        thisJoystick = GetComponent<TankController>();
        //gravityValue = 0f;
        thisPlayerForwardSpeed = this.thisShipData.moveSpeed;
        thisPlayerBackSpeed = this.thisShipData.moveSpeed / 2;
        thisPlayerRotateSpeed = this.thisShipData.turnSpeed;
        thisPlayerFireSpeed = this.thisShipData.fireRate;

        // this is the firing cooldown
        lastFired = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        switch(entryMethod)
        {
            case InputScheme.WASD:
                // can only go forward or back
                if(Input.GetKey(KeyCode.W))
                {
                    // code to go faster/ start moving forwards
                    if(Input.GetKey(KeyCode.LeftShift))
                    {
                        controlInput.z = 5.0f;
                    }
                    else
                    {
                        controlInput.z = 1.0f;
                    }

                    controlInput.z = controlInput.z * this.thisPlayerForwardSpeed;

                }
                else if (Input.GetKey(KeyCode.S))
                {
                    // code to slow down/ go backwards
                    controlInput.z = -1.0f * this.thisPlayerBackSpeed;
                }
                else
                {
                    controlInput.z = 0f;
                };
                // can only go left or right
                if (Input.GetKey(KeyCode.A))
                {
                    // code to rotate right
                    rotateInput.y = -1.0f * thisPlayerRotateSpeed * Time.deltaTime;
                }
                else if (Input.GetKey(KeyCode.D))
                {
                    // code to rotate left
                    rotateInput.y = 1.0f * thisPlayerRotateSpeed * Time.deltaTime;
                }
                else
                {
                    rotateInput.y = 0f;
                };

                // this is to fire the weapon on the ship after the other updates have finished
                if (Input.GetKey(KeyCode.Space))
                {
                    // code to fire the lazer if the timer is okay
                    if (Time.time - lastFired > (10.0f / thisPlayerFireSpeed))
                    {
                        thisShipData.createBullet();
                        lastFired = Time.time;
                    }

                }

                controlInput.x = controlInput.x * (thisPlayerForwardSpeed / 2.0f);

                // commented out in order to allow for symmetric development
                //if (Input.GetKey(KeyCode.E))
                //{
                //    // code to turn right
                //    rotateInput.y = 1.0f * thisPlayerRotateSpeed * Time.deltaTime;
                //}
                //else if (Input.GetKey(KeyCode.Q))
                //{
                //    // code to turn left
                //    rotateInput.y = -1.0f * thisPlayerRotateSpeed * Time.deltaTime;
                //}
                //else
                //{
                //    rotateInput.y = 0f;
                //};

                break;

            case InputScheme.arrowKeys:
                // can only go forward or back
                if (Input.GetKey(KeyCode.UpArrow))
                {
                    // code to go faster/ start moving forwards
                    if (Input.GetKey(KeyCode.LeftShift))
                    {
                        controlInput.z = 5.0f;
                    }
                    else
                    {
                        controlInput.z = 1.0f;
                    }

                    controlInput.z = controlInput.z * this.thisPlayerForwardSpeed;
                }
                else if (Input.GetKey(KeyCode.DownArrow))
                {
                    // code to slow down/ go backwards
                    controlInput.z = -1.0f * thisPlayerBackSpeed;
                }
                else
                {
                    controlInput.z = 0f;
                };
                // can only go left or right
                if (Input.GetKey(KeyCode.RightArrow))
                {
                    // code to go right
                    rotateInput.y = 1.0f * thisPlayerRotateSpeed * Time.deltaTime;
                }
                else if (Input.GetKey(KeyCode.LeftArrow))
                {
                    // code to go left
                    rotateInput.y = -1.0f * thisPlayerRotateSpeed * Time.deltaTime;
                }
                else
                {
                    rotateInput.y = 0f;
                };

                if (Input.GetKey(KeyCode.KeypadEnter))
                {
                    // code to fire the lazer if the timer is okay
                    if (Time.time - lastFired > (10.0f / thisPlayerFireSpeed))
                    {
                        thisShipData.createBullet();
                        lastFired = Time.time;
                    }

                }

                controlInput.x = controlInput.x * (thisPlayerForwardSpeed / 2.0f);

                //if (Input.GetKey(KeyCode.Keypad7))
                //{
                //    // code to turn right
                //    rotateInput.y = 1.0f * thisPlayerRotateSpeed;
                //}
                //else if (Input.GetKey(KeyCode.Keypad9))
                //{
                //    // code to turn left
                //    rotateInput.y = -1.0f * thisPlayerRotateSpeed;
                //}
                //else
                //{
                //    rotateInput.y = 0f;
                //};
                break;

        }


        // this covers entering the changes in position onto the connected player ship
        /// moved to controller for pawn movement
        thisJoystick.movePawn(thisTransform.TransformDirection(controlInput) * Time.deltaTime);
        thisJoystick.rotatePawn(rotateInput * Time.deltaTime * 100.0f);

       

        

        // this is just for debugging and proving purposes
        if (Input.GetKeyDown(KeyCode.L))
        {
            myManager.PlayerScore += 200;
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            myManager.RespawnPlayer();
        }
    }
}
