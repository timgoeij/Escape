using UnityEngine;
using System.Collections;

public class Guard : MonoBehaviour 
{
    //variables
    private Transform guard;
	private NavMeshAgent agent;
	private GameObject[] waypoints;
	private Animation anim;
	private int point = 0;

	Ray middleRay;
	Ray leftRay;
	Ray rightRay;

	private Vector3 raypos;
	private bool followPlayer;

	private Transform player;

	private UI ui;


	// Use this for initialization
	void Start () {

        //find the guard
        guard = this.transform;

        //find the NavMeshAgent component
        agent = guard.GetComponent<NavMeshAgent>();

        //find all the waypoints
        waypoints = GameObject.FindGameObjectsWithTag("waypoint");

        //find the animation component
        anim = guard.GetComponent<Animation>();

        //find the player
        player = GameObject.FindGameObjectWithTag("Player").transform;

        //find the script of the ui
        ui = GameObject.Find("UI").GetComponent<UI>();
	}
	
	// Update is called once per frame
	void Update () {

        //start the animation
        anim.CrossFade("Take 001");

        //intialize rays
        raypos = new Vector3(guard.position.x, guard.position.y + 2.5f, guard.position.z);
		
		middleRay = new Ray(raypos, guard.TransformDirection(Vector3.forward));
		leftRay = new Ray(raypos, guard.TransformDirection(new Vector3(1,0,1)));
		rightRay = new Ray(raypos, guard.TransformDirection(new Vector3(-1,0,1)));

		RaycastHit hit;

        //if one of the rays hit the player set followPlayer at true
        if (Physics.Raycast(leftRay, out hit, 75) || Physics.Raycast(middleRay, out hit, 75)
		   || Physics.Raycast(rightRay, out hit, 75))
		{
			if(hit.transform.CompareTag(player.tag))
			{
				followPlayer = true;
			}
		}

		if(!followPlayer)
		{
            //set the destination of the agent to the position of a waypoint
            agent.SetDestination(waypoints[point].transform.position);

            //check if the path is complete
            if (pathComplete())
			{
                //is the player at the last waypoint? go to the first waypoint
                if (point >= waypoints.Length - 1)
				{
					point = 0;
				}
				else
				{
                    //go to the next waypoint
                    point++;
				}
			}
		}
		else
		{
            //set the destination of the agent to the position of the player
            agent.SetDestination(player.position);

            //if the distance between the player and the guard set followplayer at false
            if (agent.remainingDistance > 20.0f)
				followPlayer = false;
		}
	}

	private bool pathComplete()
	{
        //check if the guard isn't walking
        if (!agent.pathPending)
		{
            //check if the stopdistance is smaller of equal to the remaining distance
            if (agent.remainingDistance <= agent.stoppingDistance)
			{
                //check if the agent hasn't a path or has no velocity
                if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
				{
                    return true;
				}
			}
		}

		return false;
	}

	private void OnTriggerEnter(Collider collide)
	{
        //check if the player hits the trigger of the guard
        if (collide.transform.tag == "Player")
		{
			ui.fail = true;
			ui.starWithUI = true;
		}
	}
}
