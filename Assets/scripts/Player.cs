using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour 
{
    //variables
    private CharacterController controller;
	private Transform player;
	
	private float walkSpeed = 5f;
	private float runSpeed = 10f;
	private float rotspeed = 10f;

	private Animation anim;

	private UI ui;
	
	
	// Use this for initialization
	void Start () 
	{
        //find the player
        player = this.transform;

        //find the charactercontroller component
		controller = player.GetComponent<CharacterController>();

        //find the animation component
		anim = player.GetComponent<Animation>();

        //find the script of the ui
		ui = GameObject.Find("UI").GetComponent<UI>();
	}
	
	// Update is called once per frame
	void Update () 
	{
        //create plane above the player
        Plane playerPlane = new Plane(Vector3.up, transform.position);

        //create a ray for the mouse position
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		float hitdist = 0.0f;

        //check if the ray hit the plane
        if (playerPlane.Raycast (ray, out hitdist)) {

            //get the target point
			Vector3 targetPoint = ray.GetPoint(hitdist);

            //create a rotation to the targetpoint
			Quaternion targetRotation = Quaternion.LookRotation(targetPoint - transform.position);

            //rotate the player
			transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotspeed * Time.deltaTime);
		}

		//if the space bar is pressed start the walk animation and let the player walk
		if (Input.GetKey (KeyCode.Space)) {
			controller.SimpleMove (player.TransformDirection (Vector3.forward) * walkSpeed);
			anim.CrossFade("walk 1"); 
		}//if the leftshift is pressed start the animation and let the player run
		else if (Input.GetKey (KeyCode.LeftShift)) {
			controller.SimpleMove (player.TransformDirection (Vector3.forward) * runSpeed);
			anim.CrossFade("walk 1");
		}
		else
		{
            //stop the animation
            anim.Stop();
		}
	}

	void  OnControllerColliderHit(ControllerColliderHit hit)
	{
        //check if the charactercontroller hit the kitten
        if (hit.transform.name == "kitten")
		{
			ui.starWithUI = true;
			ui.end = true;
		}
	}
}