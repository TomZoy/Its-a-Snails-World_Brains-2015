using UnityEngine;
using System.Collections;

public class BlockRepresentation1  {

    public static int[,] numbers = new int[6,6]; // declare numbers as an int array of any size int[] myArray1 = new int [5];

    static string test;

 

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


}
