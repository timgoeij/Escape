using UnityEngine;
using System.Collections;

public class DoorTrigger : MonoBehaviour {

    //variables
    public Transform door;
	public Transform minigame;
    private bool isPlayer = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        //check if isplayer is true
        if (isPlayer)
        {
            //check if the door has a minigame
            if (minigame != null)
            {
                //check if the minigame is not complete and make the minigame visible
                if (!minigame.GetComponent<Minigame>().complete)
                {
                    foreach (Transform child in minigame)
                    {
                        child.GetComponent<Renderer>().enabled = true;

                        foreach (Transform gchild in child)
                        {
                            gchild.GetComponent<Renderer>().enabled = true;
                        }
                    }
                }
                else
                {
                    //make the minigame invisible and open the door
                    foreach (Transform child in minigame)
                    {
                        child.GetComponent<Renderer>().enabled = false;

                        foreach (Transform gchild in child)
                        {
                            gchild.GetComponent<Renderer>().enabled = false;
                        }
                    }

                    door.GetComponent<Door>().open = true;
                }
            }
            else //if the minigame is complete open the door
            {
                door.GetComponent<Door>().open = true;
            }
        }
	}

	void OnTriggerEnter(Collider collide)
	{
        //check if the enemy hits the trigger and open the door else set isPlayer to true
        if (collide.transform.tag != "Player")
		{
			door.GetComponent<Door>().open = true;
		}
		else
		{
            isPlayer = true;
		}
	}

	void OnTriggerExit(Collider collide)
	{

        //check if the player hits the trigger and hide the minigame
        if (collide.transform.tag == "Player" && minigame != null)
		{
			foreach(Transform child in minigame)
			{
				child.GetComponent<Renderer>().enabled = false;

				foreach(Transform gchild in child)
				{
					gchild.GetComponent<Renderer>().enabled = false;
				}
			}
		}

        //set isPlayer to false
        isPlayer = false;
        //close the door
        door.GetComponent<Door>().open = false;
	}
}
