using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BlockRepresentation1  {

    public static int[,] numbers = new int[7,7]; // row and col 0 and 6 are for the gates! (gate 0 means closed 1 means open) others are tileIDs

    static string test;

    public static List<Vector3> BlocksToMove = new List<Vector3>();

    public static List<GameObject> Blocks = new List<GameObject>();

    public static GameObject pickedCube;

    public static Vector2 lastPlace;

    public static  bool isInputAllowed = true;

    //arayist can store the number of elements assigned to an ID

   
    /*
    
    public static void initialisation()
    {
        // row - column
        numbers[1,1] = 1;
        numbers[1,2] = 0;
        numbers[1,3] = 0;
        numbers[1,4] = 0;
        numbers[1,5] = 2;

        // row - column
        numbers[2, 1] = 0;
        numbers[2, 2] = 0;
        numbers[2, 3] = 0;
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
        numbers[4, 3] = 0;
        numbers[4, 4] = 0;
        numbers[4, 5] = 0;

        // row - column
        numbers[5, 1] = 3;
        numbers[5, 2] = 0;
        numbers[5, 3] = 0;
        numbers[5, 4] = 0;
        numbers[5, 5] = 4;
    }
    */

    public static void initialisation()
    {
        // row - column
        numbers[1, 1] = 0;
        numbers[1, 2] = 1;
        numbers[1, 3] = 2;
        numbers[1, 4] = 0;
        numbers[1, 5] = 0;

        // row - column
        numbers[2, 1] = 3;
        numbers[2, 2] = 0;
        numbers[2, 3] = 0;
        numbers[2, 4] = 4;
        numbers[2, 5] = 0;

        // row - column
        numbers[3, 1] = 0;
        numbers[3, 2] = 5;
        numbers[3, 3] = 0;
        numbers[3, 4] = 0;
        numbers[3, 5] = 0;

        // row - column
        numbers[4, 1] = 6;
        numbers[4, 2] = 0;
        numbers[4, 3] = 7;
        numbers[4, 4] = 0;
        numbers[4, 5] = 8;

        // row - column
        numbers[5, 1] = 0;
        numbers[5, 2] = 0;
        numbers[5, 3] = 9;
        numbers[5, 4] = 0;
        numbers[5, 5] = 0;
    }
    public static void displayGrid()
    {


        for (int j = 0; j < 7; j++)
        {
            test = "";
            for (int i = 0; i < 7; i++)
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

    public static Vector2 TransLateAxes(int zx, int zy)

    {
        int x;
        int y;

        x = zy;

        y= -(zx -1);
        return new Vector2 (x,y);
    }

    public static void openAGate(int row, int col)
    {
        //DIRECTIONS = UP=0; right = 1; down =2; left=3

        if ((row == 0) || (row == 6)) { openAGateVert(row, col); }
        else if ((col == 0) || (col == 6)) { openAGateHor(row, col); }
        else { Debug.Log("ERROR, INVALID VALUE"); };
    }

    public static void openAGateVert(int row, int col)
    {

        bool terminteLoop = false;
        int direction = 10;
        int i = 0;
        int b = 0;


        if (row == 0) { direction = 2; i = 1; b = 6; };
        if (row == 6) { direction = 0; i = 6; b = 1; };

        //go through the whole col
        while ((i != b) && (terminteLoop == false))
        { //while statement is true
          //  Debug.Log(i + " " + col);

            //look for block, avoid water tiles
            if (numbers[i, col] != 0)
            {
                foreach (GameObject gameobby in Blocks)
                {
                    Transform trans = gameobby.transform;
                    Debug.Log("before " + i +" , " + col);
                    Vector2 V2 = TransLateAxes(i,col);
                    Debug.Log("after " + V2.x + " , " + V2.y);
                    if (trans.transform.position == new Vector3(V2.x, 0, V2.y)) { pickedCube = gameobby; };
                }

                //in case theres empty water that way (only if it's a 1x1 block)
                if (countMembers(numbers[i, col]) == 1)
                {
                    if (checkIfCanMove(i, col, direction) == true) { doMoveBlock(numbers[i, col], direction);  } 
                    else { terminteLoop = true; }
                }
                


            }

            if (direction == 2) { i++; } else { i--; }
        }

        //update real world ojects
        pushCube PC = pickedCube.GetComponent<pushCube>();
        if (PC != null)
        {

         //   PC.destination = new Vector3(lastPlace.x, 0, lastPlace.y);
         //   PC.run = true;

           PC.StartCoroutine(PC.moveToCoroutine(new Vector3(lastPlace.x, 0, lastPlace.y)));
        }    
    }

    public static void openAGateHor(int row, int col)
    {
        int direction=10;
        int i=0;
        int b=0;
        bool terminteLoop = false;

        if (col == 0) { direction = 1;  i = 1; b = 6; };
        if (col == 6) { direction = 3;  i = 6; b = 1; };
        



        //go through the whole row
        while ((i != b) && (terminteLoop==false))
        { //while statement is true

            //look for block, avoid water tiles
            if (numbers[row, i] != 0)
            {

                foreach (GameObject gameobby in Blocks)
                {

                    Transform trans = gameobby.transform;
                    Debug.Log("before " + row + " , " + i);
                    Vector2 V2 = TransLateAxes(row, i);
                    Debug.Log("after " + V2.x + " , " + V2.y);

                    

                    if (trans.transform.position == new Vector3(V2.x, 0, V2.y)) { pickedCube = gameobby; };
                }


                //in case theres empty water that way (only if it's a 1x1 block)
                if (countMembers(numbers[row, i]) == 1)
                {
                    if (checkIfCanMove(row, i, direction) == true) { doMoveBlock(numbers[row, i], direction);  } 
                    else { terminteLoop = true; }
                }
                


            }

            if (direction == 1) { i++; } else {i--;}
        }

        //update real world ojects
        //    pickedCube.gameObject.SendMessage("moveTo2", new Vector3(lastPlace.x, 0, lastPlace.y));
        pushCube PC = pickedCube.GetComponent<pushCube>();
        if (PC != null)
        {

          //  PC.destination = new Vector3(lastPlace.x, 0, lastPlace.y);
          //  PC.run = true;

            PC.StartCoroutine(PC.moveToCoroutine(new Vector3(lastPlace.x, 0, lastPlace.y)));
        }
       // pickedCube.GetComponent<pushCube>().moveTo2(new Vector3(3, 2, 1));
    }

    static bool checkIfCanMove(int row,int col,int dir)
    { 
        //DIRECTIONS = UP=0; right = 1; down =2; left=3

       


        //detect water
        if (dir == 0) 
        {
            if (row == 1) { return false; }  //refuse block going down from the board
            else if (numbers[row - 1, col] == 0) { return true; }
            else { return false; } //if it's water you can go, otherwise not
            // 
        }

        else if (dir == 1) 
        {
            if (col == 5) { return false; }  //refuse block going down from the board
            else if (numbers[row, col+1] == 0) { return true; } 
            else { return false; } 
        }

        else if (dir == 2)
        {
            if (row == 5) { return false; }  //refuse block going down from the board
            if (numbers[row + 1, col] == 0) { return true; } { return false; } 
        }

        else if (dir == 3) 
        {
            if (col == 1) { return false; }  //refuse block going down from the board
            if (numbers[row, col - 1] == 0) { return true;  } { return false; } 
        }

        //to make the function happy .... -.-"
        return false;
    }

    static void doMoveBlock (int ID, int dir)
   
    {
        int[,] direction = new int[,]{{-1,0},{0,1},{1,0},{0,-1}};



        Debug.Log("MOOVING");


        //build a list of object to move
        for (int j = 1; j < 6; j++)
        {
            for (int i = 1; i < 6; i++)
            {
                if (numbers[j, i] == ID) 
                {
                    numbers[(direction[dir,0] + j), (direction[dir,1] +i)] = numbers[j, i];

                    Vector2 V2 = TransLateAxes((direction[dir, 0] + j), (direction[dir, 1] + i));

                    lastPlace = V2; //new Vector2((direction[dir, 1] + i), (direction[dir, 0] + j));
                    Debug.Log(lastPlace.ToString());

                    numbers[j, i] = 0;
                    return;
                };
            }
        }




    }
    
    // BUILD A LIST FOR THE BLOCKS THAT ABOUT TO MOVE !!!

}
