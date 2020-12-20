using UnityEngine;
using System.Collections;

public class Room : MonoBehaviour {

	public GameObject doorNorth;
	public GameObject doorSouth;
	public GameObject doorEast;
	public GameObject doorWest;

	public enum DoorSwitch { NorthWest, North, NorthEast, West, SouthWest, South, SouthEast, East, Center}
	public DoorSwitch doorSettings;

	public void blowDoors()
    {
		switch(doorSettings)
        {
			case DoorSwitch.NorthWest:
				blowSouth();
				blowEast();
				break;

			case DoorSwitch.North:
				blowSouth();
				blowEast();
				blowWest();
				break;

			case DoorSwitch.NorthEast:
				blowSouth();
				blowWest();
				break;

			case DoorSwitch.West:
				blowNorth();
				blowSouth();
				blowEast();
				break;

			case DoorSwitch.SouthWest:
				blowNorth();
				blowEast();
				break;

			case DoorSwitch.SouthEast:
				blowNorth();
				blowWest();
				break;

			case DoorSwitch.South:
				blowNorth();
				blowEast();
				blowWest();
				break;

			case DoorSwitch.East:
				blowNorth();
				blowSouth();
				blowWest();
				break;

			case DoorSwitch.Center:
				blowNorth();
				blowEast();
				blowWest();
				blowSouth();
				break;

			default:
				// do nothing
				break;
		}

		void blowNorth()
        {
			Destroy(doorNorth);
        }

		void blowSouth()
        {
			Destroy(doorSouth);
		}

		void blowEast()
        {
			Destroy(doorEast);
		}

		void blowWest()
        {
			Destroy(doorWest);
		}
    }

}
