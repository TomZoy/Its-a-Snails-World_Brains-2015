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
        numbers[4, 1] = 0;
        numbers[4, 2] = 0;
        numbers[4, 3] = 4;
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

    public static void openAGateHor(int row, int col)
    {
        int direction = 10;
        if (col == 0) { direction = 1; }
        else if (col == 6) { direction = 3; }
        else {Debug.LogError("ERROR, invalid direction");}

        //go through the whole row
        for (int i = 1; i < 6; i++)
        {

            //look for block, avoid water tiles
            if (numbers[row, i] != 0)
            {



                //in case theres empty water that way (only if it's a 1x1 block)
                    if (countMembers(numbers[row, i]) == 1) 
                        {
                            if (checkIfCanMove(row, i, direction) == true)  {  doMoveBlock(numbers[row, i], 1);  }
                        }
                //


            }

        }
    }

    static bool checkIfCanMove(int row,int col,int dir)
    { 
        //DIRECTIONS = UP=0; right = 1; down =2; left=3

       


        //detect water
        if (dir == 0) 
        {
            if (row == 1) { return false; }  //refuse block going down from the board
            else if(numbers[row-1, col] == 0) {return true;} {return false;} //if it's water you can go, otherwise not
            // 
        }

        else if (dir == 1) 
        {
            if (col == 5) { return false; }  //refuse block going down from the board
            else if (numbers[row, col+1] == 0) { return true; } { return false; } 
        }

        else if (dir == 2)
        {
            if (row == 5) { return false; }  //refuse block going down from the board
            if (numbers[row + 1, col] == 0) { return true; } { return false; } 
        }

        else if (dir == 3) 
        {
            if (col == 1) { return false; }  //refuse block going down from the board
            if (numbers[row, col-1] == 0) { return true; } { return false; } 
        }

        //to make the function happy .... -.-"
        return false;
    }

    static void doMoveBlock (int ID, int dir)
   
    {
        int[,] direction = new int[,]{{-1,0},{0,1},{1,0},{0,-1}};;



        Debug.Log("MOOVING");

        for (int j = 1; j < 6; j++)
        {
            for (int i = 1; i < 6; i++)
            {
                if (numbers[j, i] == ID) 
                {
                    numbers[(direction[dir,0] + j), direction[dir,1] +i] = numbers[j, i]; 
                    numbers[j, i] = 0;
                    return;
                };
            }
        }
    }
    


}
