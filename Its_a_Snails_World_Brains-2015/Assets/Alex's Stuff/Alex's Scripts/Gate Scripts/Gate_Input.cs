using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class Gate_Input : MonoBehaviour
{
    //this script will control the input for the gates, it will determine if tehy have been clicked or tapped by the player
    //if they have it will then activate a method that will open the gate

    private UnityEngine.EventSystems.EventSystem myEventSystem;

    public float stayOpenTime = 2.0f;
    public float timerTemp = 0.0f;
    public bool gateHit = false;

    // Use this for initialization
    void Start()
    {
        myEventSystem = GameObject.Find("EventSystem").GetComponent<EventSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (myEventSystem.IsPointerOverGameObject() == false)
            {
                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.transform.gameObject.tag == "Gate")
                    {
                        Debug.Log("hit gate");
                        //Destroy(this.gameObject); <---used to test clicks on touchpad

                        //begin method that will open gate
                        OpenGate();
                    }
                }
            }
        }

        if (gateHit)
        {
            //once opened count down to 0 from x
            if (timerTemp > 0)
            {
                timerTemp -= Time.deltaTime;
            }
            else
            {
                Debug.Log("timer hit 0");
                CloseGate();
            }
        }
    }

    void OpenGate()
    {
        gateHit = true;
        timerTemp = stayOpenTime;
        Debug.Log("gate opened, timer started");
    }

    void CloseGate()
    {
        gateHit = false;

        Debug.Log("closing the gate, timer hit 0");
    }
}
