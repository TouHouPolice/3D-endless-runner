using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chaser : MonoBehaviour
{

    public Transform Catch;
    public Transform Close;
    public Transform Far;
    public bool isFar = true;
    public  bool isClose = false;
    public  bool isCaught = false;
    public  float dangerousDuration = 10f;
    public  float dangerousTimeRemaining;

    public float toCloseDurationOfLerp = 1.5f;
    public float toFarDurationOfLerp = 2f;
    public float toCatchDurationOfLerp = 1f;
    public  bool isLerping = false;
    public  float timeStarted;
    public  Vector3 startPos;
    
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (isClose == true && dangerousTimeRemaining > 0)
        {
            dangerousTimeRemaining -= Time.deltaTime;
        }

        if (dangerousTimeRemaining <= 0 && isClose == true)//lerp to far
        {
            isLerping = true;
            isClose = false;
            isFar = true;
            
        }
        if (isLerping)
        {
            
            if (isCaught)
            {
                LerpToCatch();
            }
            if (isFar)
            {
                LerpToFar();
            }
            if (isClose)
            {
                LerpToClose();

            }
        }
        else if (isLerping == false)
        {
            if (isFar)
            {
                transform.position = Far.transform.position;

            }
            if (isClose)
            {
                transform.position = Close.transform.position;
            }
            if (isCaught)
            {
                transform.position = Catch.transform.position;
            }
        }
        //placeholder for now
        ;
    }


    public void LerpToClose()
    {

       
        
        
        if (isLerping)
        {
            float timeSinceStarted = Time.time - timeStarted;
            float percentageComplete = timeSinceStarted / toCloseDurationOfLerp;
            
                Vector3 newPos = Vector3.Lerp(startPos, Close.transform.position, percentageComplete);
                transform.position = new Vector3(transform.position.x, transform.position.y, newPos.z);

                if (percentageComplete >= 1.0f)
                {
                    isLerping = false;

                }
            
        }
    }

    public void LerpToFar()
    {
            
        if (isLerping)
        {
            float timeSinceStarted = Time.time - timeStarted;
            float percentageComplete = timeSinceStarted / toFarDurationOfLerp;

            Vector3 newPos = Vector3.Lerp(startPos, Far.transform.position, percentageComplete);
            transform.position = new Vector3(transform.position.x, transform.position.y, newPos.z);

            if (percentageComplete >= 1.0f)
            {
                isLerping = false;

            }
        }
    }

    public void LerpToCatch()
    {
       
        
        //Catch.transform.parent = null;

         if (isLerping)
         {

             {
                 float timeSinceStarted = Time.time - timeStarted;
                 float percentageComplete = timeSinceStarted / toCatchDurationOfLerp;
                
                
                     Vector3 newPos = Vector3.Lerp(startPos, Catch.transform.position, percentageComplete);
                
                     transform.position = new Vector3(transform.position.x, transform.position.y, newPos.z);
                
                     if (percentageComplete >= 1.0f)
                     {
                         isLerping = false;
                     }

             }

         }
        /*float speed = 20f;
        Vector3.MoveTowards(transform.position, playerLastPosition, speed * Time.deltaTime);*/
    }
}
    



