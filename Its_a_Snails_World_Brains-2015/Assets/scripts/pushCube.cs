using UnityEngine;
using System.Collections;

public class pushCube : MonoBehaviour {

   public  bool run=false;
    public float cubeMoveSpeed = 0.1f;

    public Vector3 destination;

    static float timepersqr = 5f;

    public float smoothFactor = 5;

	// Use this for initialization
	void Start () 
    {
        Debug.Log(this.gameObject.transform.position);

        BlockRepresentation1.Blocks.Add(this.gameObject);
	}
	
	// Update is called once per frame
	void Update () {

        if (run)
        {
           // moveTo2(destination);

            //run = false;
        }
	}

    void moveTo(Vector3 target)

    {
       // Vector3 movingVector = (target-transform.position);
       // movingVector.Normalize();

        transform.position += target * Time.deltaTime * cubeMoveSpeed; //(Vector3.right *Time.deltaTime * cubeMoveSpeed, Space.World); //movingVector
        if ((transform.position.x > (target.x * 0.99f)) && (transform.position.x < (target.x * 1.05f))) { Debug.Log("bleeee"); run = false; };
    }
      

   public void moveTo2(Vector3 target)
    {
        transform.position = Vector3.Lerp(transform.position, target, Time.deltaTime * smoothFactor * cubeMoveSpeed);

    }

   public IEnumerator moveToCoroutine(Vector3 target)
   {
       BlockRepresentation1.isInputAllowed = false;

       Vector3 startposition = transform.position;

       float totalTime = (target - startposition).magnitude * timepersqr;
       float currentTime = 0;
       while (currentTime < totalTime)

       {
           currentTime += Time.deltaTime;
           transform.position = Vector3.Lerp(transform.position, target, currentTime/totalTime);


           BlockRepresentation1.isInputAllowed = true;
           yield return 0;
           
       }

       transform.position = target;
   }

    void moveTo3(Vector3 target)
    {
        transform.position = Vector3.MoveTowards(transform.position, target, cubeMoveSpeed);
    }

    void moveTo4(Vector3 target)
    {

        float step = cubeMoveSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, target, step);
    }


    void moveTo5(Vector3 target)
    { 
        

     float cubeMoveSpeed = 1.0F;
     float startTime;
     float journeyLength;

        startTime = Time.time;
        journeyLength = Vector3.Distance(transform.position, target);


        float distCovered = (Time.time - startTime) * cubeMoveSpeed;
        float fracJourney = distCovered / journeyLength;
        transform.position = Vector3.Lerp(transform.position, target, fracJourney);
    
    }

    }
