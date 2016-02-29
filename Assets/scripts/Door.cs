using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour {
    // Use this for initialization

    //variables
    private Transform door;
	public bool open = false;
	
	void Start () 
	{
        //find the door
        door = this.transform;
	}

	void Update()
	{
        // if true, open the door, if false, close the door
        if (open)
		{
			openDoor();
		}
		else
		{
			closeDoor();
		}
	}

	void openDoor()
	{
        //rotate the door to open the door
        if (door.tag == "vert_door")
			door.rotation = Quaternion.Slerp(door.rotation,Quaternion.Euler(0,180,0), 0.1f);	
		else
			door.rotation = Quaternion.Slerp(door.rotation,Quaternion.Euler(0,90,0), 0.1f);
	}

	void closeDoor()
	{
        //rotate the door to close the door
        if (door.tag == "vert_door")
			door.rotation = Quaternion.Slerp(door.rotation,Quaternion.Euler(0,90,0), 0.1f);
		else
			door.rotation = Quaternion.Slerp(door.rotation,Quaternion.Euler(0,0,0), 0.1f);
	}
}