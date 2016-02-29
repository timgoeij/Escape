using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour {

    //variables
    public Texture startImage;
	public Texture tutImage;
	public Texture failImage;
	public Texture endImage;

	public bool starWithUI = true;

	bool start = true;
	bool tutorial = false;
	public bool fail = false;
	public bool end = false;

	int tries = 0;

	// Use this for initialization
	void Start () {

        //if tries is bigger than 0, destroy the double ui gameobjects
        if (tries > 0)
		{
			GameObject[] uis = GameObject.FindGameObjectsWithTag("ui");

			foreach(GameObject ui in uis)
			{
				if(tries == 0 || tries > 2)
				{
					Destroy(ui.gameObject);
				}
			}
		}
	}
	
	// Update is called once per frame
	void Update () {

        //destroy the ui not while loading the new scene
        DontDestroyOnLoad(this.gameObject);
	}

	void OnGUI()
	{
        // if start with ui is true, start the game with menu
        if (starWithUI)
		{
			GUI.Window(0, new Rect(0,0, Screen.width, Screen.height), startUiFunction, "");
		}
	}

	void startUiFunction(int id)
	{
		if(start)
		{
            //if start is true, let see the startscreen
            GUI.Box(new Rect(0,0, Screen.width, Screen.height), startImage);

            //go to the tutorial
            if (GUI.Button(new Rect((Screen.width / 2) - 50, Screen.height - 100, 100, 50), "Volgende"))
			{
				start = false;
				tutorial = true;
			}
		}
		else if(tutorial)
		{
            //if tutorial is true, let see the tutorial of the game
            GUI.Box(new Rect(0,0, Screen.width, Screen.height), tutImage);

            //start the game
            if (GUI.Button(new Rect((Screen.width / 2) - 50, Screen.height - 100, 100, 50), "Start"))
			{
				tutorial = false;
				starWithUI = false;
			}
		}
		else if(fail && !end)
		{
			GUI.Box(new Rect(0,0, Screen.width, Screen.height), failImage);

            //try the game again
            if (GUI.Button(new Rect((Screen.width / 2) - 75, Screen.height - 150, 100, 50), "Probeer opnieuw"))
			{
				tries += 1;
				fail = false;
				starWithUI = false;
				SceneManager.LoadScene(0);
			}
		}
		else if(end)
		{
            //if end is true, let see the endscreen

            GUI.Box(new Rect(0,0, Screen.width, Screen.height), endImage);

            //start the game again
            if (GUI.Button(new Rect((Screen.width / 2) - 100, Screen.height - 200, 100, 50), "Speel nog een keer"))
			{
				tries = 0;
				end = false;
				start = true;
				SceneManager.LoadScene(0);
			}
		}
	}
}
