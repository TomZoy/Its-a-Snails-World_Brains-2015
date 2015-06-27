using UnityEngine;
using System.Collections;


public class test : MonoBehaviour {

    bool t = true;

	// Use this for initialization
	void Start () {

        BlockRepresentation1.initialisation();

	}
	
	// Update is called once per frame
	void Update () {

        //Debug.Log("dedede");
        tryRERE();
	}

    void tryRERE ()

    {
        if (t)
        {
            BlockRepresentation1.displayGrid();
            t = false;
            
            Debug.Log("membercount: "+ BlockRepresentation1.countMembers(4));

          //  BlockRepresentation1.openAGateVert(4, 0);
            BlockRepresentation1.openAGateHor(4, 0);
            BlockRepresentation1.displayGrid();
            
        }
    }
}
