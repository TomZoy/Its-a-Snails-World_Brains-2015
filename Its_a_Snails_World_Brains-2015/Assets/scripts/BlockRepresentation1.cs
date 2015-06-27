using UnityEngine;
using System.Collections;

public class BlockRepresentation1  {

    public static int[,] numbers = new int[7,7]; // row and col 0 and 6 are for the gates! (gate 0 means closed 1 means open) others are tileIDs

    static string test;

    //arayist can store the number of elements assigned to an ID

 

	// Use this for initialization
	void Start () {


	}
	
	// Update is called once per frame
	void Update () {
   
	}



    public static void initialisation()
    {
        // row - column
        numbers[1,1] = 1;
        numbers[1,2] = 1;
        numbers[1,3] = 2;
        numbers[1,4] = 0;
        numbers[1,5] = 0;

        // row - column
        numbers[2, 1] = 3;
        numbers[2, 2] = 0;
        numbers[2, 3] = 2;
        numbers[2, 4] = 0;
        numbers[2, 5] = 0;

        // row - column
        numbers[3, 1] = 0;
        numbers[3, 2] = 0;
        numbers[3, 3] = 0;
        numbers[3, 4] = 0;
        numbers[3, 5] = 0;

        // row - column
        numbers[4, 1] = 4;
        numbers[4, 2] = 0;
        numbers[4, 3] = 0;
        numbers[4, 4] = 0;
        numbers[4, 5] = 0;

        // row - column
        numbers[5, 1] = 0;
        numbers[5, 2] = 0;
        numbers[5, 3] = 5;
        numbers[5, 4] = 5;
        numbers[5, 5] = 0;
    }

    public static void displayGrid()
    {


        for (int j = 1; j < 6; j++)
        {
            test = "";
            for (int i = 1; i < 6; i++)
            {
                test += numbers[j, i];
                test += ", ";
            }
            Debug.Log(test);
        }
    }

    public static int countMembers(int ID)
    {
        int count = 0;
        for (int j = 1; j < 6; j++)
        {
        
            for (int i = 1; i < 6; i++)
            {
                if (numbers[j, i] == ID) { count++; };
            }
        }

        return count;
    }

    public static void openAGateVert(int row, int col)
    {

        bool isGoingRight;
        if (col == 0) { isGoingRight = true; } else { isGoingRight = false; };

        for (int i = 1; i < 5; i++)
        {

            if (numbers[row, i] != 0)
            {

                //in case theres empty water that way (only if it's a 1x1 block)
                if (numbers[row, i + 1] == 0)
                {
                    if (countMembers(numbers[row, i]) == 1) { doMoveBlock(numbers[row, i], 1); }
                };


            }

        }
    }

    static void doMoveBlock (int ID, int direction)
   
    {
        Debug.Log("in here");
        for (int j = 1; j < 6; j++)
        {
            for (int i = 1; i < 6; i++)
            {
                if (numbers[j, i] == ID) 
                {
                    numbers[j, i+1] = numbers[j, i]; 
                    numbers[j, i] = 0;
                    return;
                };
            }
        }
    }
    


}
