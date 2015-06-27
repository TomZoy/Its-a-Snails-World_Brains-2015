using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ConnectionMatrix : MonoBehaviour 
{

    public static int[,] connections = new int[24, 24];
    public List<GameObject> wayPoints = new List<GameObject>();
    public bool[] waypointStates;
    public List<GameObject> openList = new List<GameObject>();
    public List<GameObject> closedList = new List<GameObject>();
    public GameObject currentNode;
    public GameObject goalNode;

	// Use this for initialization
	void Start () 
    {
        foreach (GameObject i in GameObject.FindGameObjectsWithTag("Waypoint"))
        {
            wayPoints.Add(i);
            Debug.Log(wayPoints.Count);
        }
        // cycle through all waypoints and determine if they are active or not
        currentNode = GameObject.Find("START");
        goalNode = GameObject.Find("END");

        closedList.Add(currentNode);
        Debug.Log("closed List" + closedList.Count);
	}
	
	// Update is called once per frame
	void Update () 
    {
        if(Input.GetMouseButtonDown(0))
        {
            NextStep();
        }
	}

    void NextStep()
    {
        Debug.Log("Took a step");

        //search for adjacent tiles
        
        //check each adjacent tile for the closed list

        //if its in there, do nothing, if it is not in there add to open list



    }
}
