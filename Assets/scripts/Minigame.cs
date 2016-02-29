using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Minigame : MonoBehaviour {

	// Use this for initialization
    //variables
	public int[] numbers;
	public int totalOfSum;

	public Transform[] numberFields;
	public Transform totalField;

	public bool complete = false;

	private List<Transform> switchNumbers = new List<Transform>();

	void Start () 
	{
        //give all fields of the minigame a number
        for (int i = 0; i < numbers.Length; i++)
		{
			numberFields[i].GetComponent<MinigameFields>().setNumber(numbers[i]);
		}

        //give the totalfield a number to solve
        totalField.GetComponent<MinigameFields>().setNumber(totalOfSum);
	}

	// Update is called once per frame
	void Update () 
	{
        //loop through all minigame fields
        for (int i = 0; i < numberFields.Length; i++)
		{

            //if switch number is true set the color of the field to blue
            if (numberFields[i].GetComponent<MinigameFields>().switchNumber)
			{
				numberFields[i].GetComponent<Renderer>().material.color = Color.blue;

                //check if switchnumbers is equals to 0 and add the field
                if (switchNumbers.Count == 0)
				{
					switchNumbers.Add(numberFields[i]);
				}
				else
				{
                    //check if switch numbers is smaller than 2
					if(switchNumbers.Count < 2)
					{
                        //check if the number of the field is not equal than the number in the list
                        if (numberFields[i].GetInstanceID() != switchNumbers[0].GetInstanceID())
						{
                            //add the field to the list
                            switchNumbers.Add(numberFields[i]);
						}
					}
				}
			}
			else
			{
                //change the color back to red
                numberFields[i].GetComponent<Renderer>().material.color = Color.red;
			}
		}

        //if the list count is equal to 2 switch the numbers
		if(switchNumbers.Count == 2)
		{
			numberswitch();
		}

        //check if three fields horizontal of the number is equal to the totalnumber and set complete at true
		if((numberFields[0].GetComponent<MinigameFields>().getNumber() + numberFields[1].GetComponent<MinigameFields>().getNumber() +
		    numberFields[2].GetComponent<MinigameFields>().getNumber()) == totalOfSum)
		{
			numberFields[0].GetComponent<Renderer>().material.color = Color.green;
			numberFields[1].GetComponent<Renderer>().material.color = Color.green;
			numberFields[2].GetComponent<Renderer>().material.color = Color.green;

			complete = true;
		}
		else if((numberFields[3].GetComponent<MinigameFields>().getNumber() + numberFields[4].GetComponent<MinigameFields>().getNumber() +
		   numberFields[5].GetComponent<MinigameFields>().getNumber()) == totalOfSum)
		{
			numberFields[3].GetComponent<Renderer>().material.color = Color.green;
			numberFields[4].GetComponent<Renderer>().material.color = Color.green;
			numberFields[5].GetComponent<Renderer>().material.color = Color.green;

			complete = true;
		}
		else if((numberFields[6].GetComponent<MinigameFields>().getNumber() + numberFields[7].GetComponent<MinigameFields>().getNumber() +
		   numberFields[8].GetComponent<MinigameFields>().getNumber()) == totalOfSum)
		{
			numberFields[6].GetComponent<Renderer>().material.color = Color.green;
			numberFields[7].GetComponent<Renderer>().material.color = Color.green;
			numberFields[8].GetComponent<Renderer>().material.color = Color.green;

			complete = true;
		}
	}

	void numberswitch()
	{
        //put the number of the fields in the variables
        int A = switchNumbers[0].GetComponent<MinigameFields>().getNumber();
		int B = switchNumbers[1].GetComponent<MinigameFields>().getNumber();

        //switch the numbers and set switch number at false
        switchNumbers[0].GetComponent<MinigameFields>().setNumber(B);
		switchNumbers[0].GetComponent<MinigameFields>().switchNumber = false;
		switchNumbers[1].GetComponent<MinigameFields>().setNumber(A);
		switchNumbers[1].GetComponent<MinigameFields>().switchNumber = false;

        //remove the fields of the list
        switchNumbers.RemoveRange(0, 2);
	}
}
