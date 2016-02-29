using UnityEngine;
using System.Collections;

public class MinigameFields : MonoBehaviour 
{
    //variables
    public Transform text;
	public bool switchNumber = false;

    //set the number of the field
    public void setNumber(int number)
	{
		text.GetComponent<TextMesh>().text = number.ToString();
	}

    //get the number of the field
    public int getNumber()
	{
		return int.Parse(text.GetComponent<TextMesh>().text);
	}

    //if the button of the mouse is up, set switch number at true
    void OnMouseUp()
	{
		switchNumber = true;
	}
}
