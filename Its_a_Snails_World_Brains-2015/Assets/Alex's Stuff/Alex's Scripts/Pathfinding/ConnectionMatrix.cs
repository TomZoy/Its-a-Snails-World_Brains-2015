using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ConnectionMatrix : MonoBehaviour 
{
    public static int[,] placeholderArray = new int[7,7];

    public List<GameObject> wayPoints = new List<GameObject>();
    public List<Vector3> waypointPositions = new List<Vector3>();
    public List<Vector2> winningPath = new List<Vector2>();
    static float timepersqr = 1.0f;
    private bool moving = false;

    public bool[] waypointStates;

	// Use this for initialization
	void Start () 
    {
        initialisePlaceholderArray();

        foreach (GameObject i in GameObject.FindGameObjectsWithTag("Waypoint"))
        {
            wayPoints.Add(i);
            waypointPositions.Add(i.gameObject.transform.position);
            Debug.Log(wayPoints.Count);
        }
	}
	
    void initialisePlaceholderArray()
    {
        // row - column
        placeholderArray[1, 1] = 0;
        placeholderArray[1, 2] = 0;
        placeholderArray[1, 3] = 0;
        placeholderArray[1, 4] = 0;
        placeholderArray[1, 5] = 0;

        // row - column
        placeholderArray[2, 1] = 0;
        placeholderArray[2, 2] = 0;
        placeholderArray[2, 3] = 0;
        placeholderArray[2, 4] = 1;
        placeholderArray[2, 5] = 1;

        // row - column
        placeholderArray[3, 1] = 1;
        placeholderArray[3, 2] = 1;
        placeholderArray[3, 3] = 1;
        placeholderArray[3, 4] = 1;
        placeholderArray[3, 5] = 0;

        // row - column
        placeholderArray[4, 1] = 0;
        placeholderArray[4, 2] = 0;
        placeholderArray[4, 3] = 0;
        placeholderArray[4, 4] = 0;
        placeholderArray[4, 5] = 0;

        // row - column
        placeholderArray[5, 1] = 0;
        placeholderArray[5, 2] = 0;
        placeholderArray[5, 3] = 0;
        placeholderArray[5, 4] = 0;
        placeholderArray[5, 5] = 0;
    }

	// Update is called once per frame
	void Update () 
    {
        bool canUpdatePosition = false;

        if(Input.GetMouseButtonDown(0))
        {
            winningPath = FindPath(2, 5, 3, 1);
            if (winningPath.Count > 0)
                Debug.Log("we found the goal");
            else
                Debug.Log("no goal found");
            Debug.Log(winningPath.Count);
        }
        if (Input.GetMouseButtonDown(1))
        {
            canUpdatePosition = !canUpdatePosition;
        }

        if (canUpdatePosition)
        {
            canUpdatePosition = false;
            StartCoroutine(callCoroutine());
        }
	}

    List<Vector2> FindPath(int startX, int startY, int endX, int endY)
    {
        List<Vector2> up = new List<Vector2>();
        List<Vector2> down = new List<Vector2>();
        List<Vector2> left = new List<Vector2>();
        List<Vector2> right = new List<Vector2>();
        List<Vector2> solution = new List<Vector2>();
        List<Vector2> closedList = new List<Vector2>();
        Debug.Log("created empty lists");

        Vector2 start = new Vector2(startX, startY);
        Vector2 end = new Vector2(endX, endY);
        Debug.Log("created start and end vectors");

        //if can go up
        if (placeholderArray[startX, startY + 1].Equals(1))
        {
            Debug.Log("can go up in findpath");

            up.Add(start);
            up.Add(new Vector2(startX, startY + 1));
        }
        else
        {
            Debug.Log("could not go up in findpath");
        }

        //if can go down
        if (placeholderArray[startX, startY - 1].Equals(1))
        {
            Debug.Log("can go down in findpath");
            down.Add(start);
            down.Add(new Vector2(startX, startY - 1));
        }
        else
        {
            Debug.Log("could not go down in findpath");
        }

        //if can go left
        if (placeholderArray[startX - 1, startY].Equals(1))
        {
            Debug.Log("can go left in findpath");
            left.Add(start);
            left.Add(new Vector2(startX - 1, startY));
        }
        else
        {
            Debug.Log("could not go left in findpath");
        }

        //if can go right
        if (placeholderArray[startX + 1, startY].Equals(1))
        {
            Debug.Log("can go right in findpath");
            right.Add(start);
            right.Add(new Vector2(startX + 1, startY));
        }
        else
        {
            Debug.Log("could not go right in findpath");
        }
        
        //if up count is greater than 0
        //search
        //if  result count > 0 return that result list
        if (up.Count > 0)
        {
            Debug.Log("up was greater than 0");
             //do the dfs
            List<Vector2> result = DFSSearch(up, end);
            if (result.Count > 0)
            {
                return result;
            }
        }

            //else if down count is greater than 0
            //search
            //if  result count > 0 return that result list
            else if (down.Count > 0)
            {
                Debug.Log("down was greater than 0");
                //do the dfs
                List<Vector2> result = DFSSearch(down, end);
                if (result.Count > 0)
                {
                    return result;
                }
            }

                //else if left count is greater than 0
                //search
                //if  result count > 0 return that result list
                else if (left.Count > 0)
                {
                    Debug.Log("left was greater than 0");
                    //do the dfs
                    List<Vector2> result = DFSSearch(left, end);
                    if (result.Count > 0)
                    {
                        return result;
                    }
                }

                    //else if right count is greater than 0
                    //search
                    //if  result count > 0 return that result list
                    else if (right.Count > 0)
                    {
                        Debug.Log("right was greater than 0");
                        //do the dfs
                        List<Vector2> result = DFSSearch(right, end);
                        if (result.Count >0)
                        {
                            return result;
                        }
                    }

        //temp return
        List<Vector2> returner = new List<Vector2>();
        //Debug.Log(returner.Count);
        return returner;
    }

    List<Vector2>DFSSearch(List<Vector2> listIn, Vector2 end)
    {

        List<Vector2> returner = new List<Vector2>();

        Debug.Log("entered the proper dfs search");
        foreach (Vector2 vec in listIn)
        {
            Debug.Log("coords:" + vec.x + "  " + vec.y);
        }

        Vector2 currVec = listIn[listIn.Count - 1];
        int currx = (int)currVec.x;
        int curry = (int)currVec.y;
        Debug.Log(currVec.x + "   " + currVec.y + "   " + "current vecs");

        if (currVec == end)
        {
            Debug.Log("end node found");
            return listIn;
        }

        //if can go up
        if (placeholderArray[currx, curry + 1].Equals(1))
        {
            Debug.Log("can go up in dfs");

            List<Vector2> passedInList = new List<Vector2>();
            passedInList = listIn;

            bool addToList = CheckIfContains(passedInList, new Vector2(currVec.x, currVec.y + 1));
            if (!addToList)
                {
                    passedInList.Add(new Vector2(currVec.x, currVec.y + 1));
                    Debug.Log("added to the input list");

                    List<Vector2> result = DFSSearch(passedInList, end);
                    if (result.Count > 0)
                    {
                        return result;
                    }
                }
            else 
            {
                Debug.Log("terminating search, no path here");
            }
        }
        //if can go down
        if (placeholderArray[currx, curry - 1].Equals(1))
        {
            Debug.Log("can go down in dfs");

            List<Vector2> passedInList = new List<Vector2>();
            passedInList = listIn;

            bool addToList = CheckIfContains(passedInList, new Vector2(currVec.x, currVec.y - 1));
            if (!addToList)
            {
                passedInList.Add(new Vector2(currVec.x, currVec.y - 1));
                Debug.Log("added to the input list");

                List<Vector2> result = DFSSearch(passedInList, end);
                if (result.Count > 0)
                {
                    return result;
                }
            }
            else
            {
                Debug.Log("terminating search, no path here");
            }
        }

        //if can go left
        if (placeholderArray[currx - 1, curry].Equals(1))
        {
            Debug.Log("can go left in dfs");

            List<Vector2> passedInList = new List<Vector2>();
            passedInList = listIn;

            bool addToList = CheckIfContains(passedInList, new Vector2(currVec.x - 1, currVec.y));
            if (!addToList)
            {
                passedInList.Add(new Vector2(currVec.x - 1, currVec.y));
                Debug.Log("added to the input list");

                List<Vector2> result = DFSSearch(passedInList, end);
                if (result.Count > 0)
                {
                    return result;
                }
            }
            else
            {
                Debug.Log("terminating search, no path here");
            }
        }

        //if can go right
        if (placeholderArray[currx + 1, curry].Equals(1))
        {
            Debug.Log("can go right in dfs");

            List<Vector2> passedInList = new List<Vector2>();
            passedInList = listIn;

            bool addToList = CheckIfContains(passedInList, new Vector2(currVec.x + 1, currVec.y));
            if (!addToList)
            {
                passedInList.Add(new Vector2(currVec.x + 1, currVec.y));
                Debug.Log("added to the input list");

                List<Vector2> result = DFSSearch(passedInList, end);
                if (result.Count > 0)
                {
                    return result;
                }
            }
            else
            {
                Debug.Log("terminating search, no path here");
            }
        }

        return returner;
    }

    bool CheckIfContains(List<Vector2> inList, Vector2 comparison)
    {
        foreach(Vector2 vec in inList)
        {
            if (comparison == vec)
            {
                Debug.Log("the value was found inside the list already");
                return true;
                
            }
        }
        return false;
    }

    void FollowPath(List<Vector2> listIn)
    {
        //this method is going to assign each 2d array location to a waypoint position
        //it will then make the snail go through the final list one by one till it gets to the goal
        Debug.Log("started to follow path");

        foreach(Vector2 vec in listIn)
        {
            //find the player object
            //move to next postion in list
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            Debug.Log("get position vector returned as " + GetPosition(vec));
            StartCoroutine(moveToCoroutine(vec, player));
            Debug.Log("player position" + player.transform.position);
        }
    }

    Vector3 GetPosition(Vector2 vecIn)
    {
        Vector3 returnVec = new Vector3(1, 1, 1);

        if (vecIn == new Vector2(1,1))
        {
            //input the positions in these
            returnVec = new Vector3(1, 1, 1);
        }
        else if (vecIn == new Vector2(1, 2))
        {
            //input the positions in these
            returnVec = new Vector3(1, 1, 1);
        }
        else if (vecIn == new Vector2(1, 3))
        {
            //input the positions in these
            returnVec = new Vector3(1, 1, 1);
        }
        else if (vecIn == new Vector2(1, 4))
        {
            //input the positions in these
            returnVec = new Vector3(1, 1, 1);
        }
        else if (vecIn == new Vector2(1, 5))
        {
            //input the positions in these
            returnVec = new Vector3(1, 1, 1);
        }
        else if (vecIn == new Vector2(2, 1))
        {
            //input the positions in these
            returnVec = new Vector3(1, 1, 1);
        }
        else if (vecIn == new Vector2(2, 2))
        {
            //input the positions in these
            returnVec = new Vector3(1, 1, 1);
        }
        else if (vecIn == new Vector2(2, 3))
        {
            //input the positions in these
            returnVec = new Vector3(1, 1, 1);
        }
        else if (vecIn == new Vector2(2, 4))
        {
            //input the positions in these
            returnVec = new Vector3(1, 1, 1);
        }
        else if (vecIn == new Vector2(2, 5))
        {
            //input the positions in these
            returnVec = new Vector3(1, 1, 1);
        }
        else if (vecIn == new Vector2(3, 1))
        {
            //input the positions in these
            returnVec = new Vector3(1, 1, 1);
        }
        else if (vecIn == new Vector2(3, 2))
        {
            //input the positions in these
            returnVec = new Vector3(2, 1, 1);
        }
        else if (vecIn == new Vector2(3, 3))
        {
            //input the positions in these
            returnVec = new Vector3(3, 1, 1);
        }
        else if (vecIn == new Vector2(3, 4))
        {
            //input the positions in these
            returnVec = new Vector3(4, 1, 1);
        }
        else if (vecIn == new Vector2(3, 5))
        {
            //input the positions in these
            returnVec = new Vector3(5, 1, 1);
        }
        else if (vecIn == new Vector2(4, 1))
        {
            //input the positions in these
            returnVec = new Vector3(1, 1, 1);
        }
        else if (vecIn == new Vector2(4, 2))
        {
            //input the positions in these
            returnVec = new Vector3(1, 1, 1);
        }
        else if (vecIn == new Vector2(4, 3))
        {
            //input the positions in these
            returnVec = new Vector3(1, 1, 1);
        }
        else if (vecIn == new Vector2(4, 4))
        {
            //input the positions in these
             returnVec = new Vector3(1, 1, 1);
        }
        else if (vecIn == new Vector2(4, 5))
        {
            //input the positions in these
            returnVec = new Vector3(1, 1, 1);
        }
        else if (vecIn == new Vector2(5, 1))
        {
            //input the positions in these
            returnVec = new Vector3(1, 1, 1);
        }
        else if (vecIn == new Vector2(5, 2))
        {
            //input the positions in these
            returnVec = new Vector3(1, 1, 1);
        }
        else if (vecIn == new Vector2(5, 3))
        {
            //input the positions in these
            returnVec = new Vector3(1, 1, 1);
        }
        else if (vecIn == new Vector2(5, 4))
        {
            //input the positions in these
            returnVec = new Vector3(1, 1, 1);
        }
        else if (vecIn == new Vector2(5, 5))
        {
            //input the positions in these
            returnVec = new Vector3(1, 1, 1);
        }
        else
        {
            returnVec = new Vector3(2, 2, 2);
        }


        return returnVec;
    }

    public IEnumerator moveToCoroutine(Vector3 target, GameObject player)
    {
        moving = true;
        Vector3 v3 = player.transform.position;
        float totalTime = (target - player.transform.position).magnitude * timepersqr;
        float currentTime = 0;
        while (currentTime < totalTime)
        {
            currentTime += Time.deltaTime;
            player.transform.position = Vector3.Lerp(v3, target, currentTime / totalTime);

            yield return 0;

        }

        player.transform.position = target;
        moving = false;
    }

    public IEnumerator callCoroutine()
    {
        int i = 0;

        while (i < winningPath.Count)
        {
            if (!moving)
            {
                StartCoroutine(moveToCoroutine(winningPath[i], GameObject.FindGameObjectWithTag("Player")));
                i++;
            }
           yield return 0;
        }
    }
}
