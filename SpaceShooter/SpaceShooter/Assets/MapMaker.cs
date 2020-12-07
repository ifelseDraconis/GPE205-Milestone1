using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapMaker : MonoBehaviour
{
    // makes it visible to all other classes
    public static MapMaker thisMapMaker;

    public GameObject RoomOne;
    public GameObject RoomTwo;
    public GameObject RoomThree;
    public int[] SeedNumber;
    public int onNumber;
    public bool BasedByDay;

    public Vector3 placementLocation;
    public float displacementSize;

    public List<GameObject> theseRooms =  new List<GameObject>();

    [Range(1, 20)]
    public int RoomRows;
    [Range(1, 20)]
    public int RoomColumns;

    void Awake()
    {
        if (thisMapMaker == null)
        {
            thisMapMaker = this;
        } 
        else
        {

        }

        int x = 0;
        int RoomChoice = 0;
        int currentCount = 0;
        x = RoomRows * RoomColumns;

        if (BasedByDay)
        {
            DayOfWeek wk = DateTime.Today.DayOfWeek;
            onNumber = Convert.ToInt32(wk);
        }
        
        // this just iterates through the seed listing, building a room based off of what the seed is.
        while (currentCount < x)
        {
            RoomChoice = chooseRoom();
            addRoom(RoomChoice);
            currentCount++;
        }

        placeRooms();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Adds the room to the list, still abstract
    void addRoom(int thisRoomChoice)
    {
        switch(thisRoomChoice)
        {
            case 2:
                theseRooms.Add(RoomOne);
                break;

            case 3:
                theseRooms.Add(RoomTwo);
                break;

            case 4:
                theseRooms.Add(RoomThree);
                break;

            case 5:
                theseRooms.Add(RoomOne);
                break;

            case 6:
                theseRooms.Add(RoomTwo);
                break;

            case 7:
                theseRooms.Add(RoomThree);
                break;

            case 9:
                theseRooms.Add(RoomOne);
                break;

            default:
                theseRooms.Add(RoomOne);
                break;
        }
    }

    // chooses what room to add
    int chooseRoom()
    {
        if (onNumber < SeedNumber.Length - 1)
        {
            onNumber++;
        }
        else
        {
            onNumber = 0;
        }

        return SeedNumber[onNumber];
    }

    // places the rooms in the map for real by first deciding where they are located, what doors are open, and then creates a real copy of the prefab
    void placeRooms()
    {
        int xMax = theseRooms.Count - 1;
        int x = 0;
        int RowNum = 1;
        int ColNum = 1;

        while (x < xMax)
        {
            if (RowNum == RoomRows)
            {
                RowNum = 1;
                ColNum++;
                placementLocation.x = 0.0f;
                placementLocation.z += displacementSize;
            }
            theseRooms[x].transform.position = placementLocation;
            placementLocation.x += displacementSize;
            if (ColNum == 1)
            {
                if (RowNum > 1 & RowNum < RoomRows - 1)
                {
                    theseRooms[x].GetComponent<Room>().doorSettings = Room.DoorSwitch.South;
                }
                else if (RowNum == 1)
                {
                    theseRooms[x].GetComponent<Room>().doorSettings = Room.DoorSwitch.SouthWest;
                }
                else
                {
                    theseRooms[x].GetComponent<Room>().doorSettings = Room.DoorSwitch.SouthEast;
                }
                
            }
            else if(ColNum > 1 & ColNum < RoomColumns + 1)
            {
                if (RowNum > 1 & RowNum < RoomRows - 1)
                {
                    theseRooms[x].GetComponent<Room>().doorSettings = Room.DoorSwitch.Center;
                }
                else if (RowNum == 1)
                {
                    theseRooms[x].GetComponent<Room>().doorSettings = Room.DoorSwitch.West;
                }
                else
                {
                    theseRooms[x].GetComponent<Room>().doorSettings = Room.DoorSwitch.East;
                }
            }
            else
            {
                if (RowNum > 1 & RowNum < RoomRows - 1)
                {
                    theseRooms[x].GetComponent<Room>().doorSettings = Room.DoorSwitch.North;
                }
                else if (RowNum == 1)
                {
                    theseRooms[x].GetComponent<Room>().doorSettings = Room.DoorSwitch.NorthWest;
                }
                else
                {
                    theseRooms[x].GetComponent<Room>().doorSettings = Room.DoorSwitch.NorthEast;
                }
            }
            theseRooms[x] = Instantiate(theseRooms[x], theseRooms[x].transform.position, Quaternion.identity);
            theseRooms[x].GetComponent<Room>().blowDoors();
            x++;
            RowNum++;
        }

    }
}
