using UnityEngine;
using System.Collections;

public class camera : MonoBehaviour 
{
    //variables
    GameObject player;
	Transform cam;


	// Use this for initialization
	void Start () {

        //find the player
        player = GameObject.FindGameObjectWithTag("Player");

        //find the camera
        cam = this.transform;
	}
	
	// Update is called once per frame
	void Update () 
	{
        //update the position of the camera
        cam.position = new Vector3(player.transform.position.x, 20, player.transform.position.z);
	}
}
