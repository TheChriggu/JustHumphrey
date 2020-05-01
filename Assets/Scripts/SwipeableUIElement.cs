using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwipeableUIElement : MonoBehaviour
{
    public GameObject NextObject;
    public int MaxPosition = 1500;
    public float Speed = 50;
    private Vector3 fp;   //First touch position
    private Vector3 lp;   //Last touch position
    private float dragDistance;  //minimum distance for a swipe to be registered
    bool isMoving = false;
    
 
    void Start()
    {
        dragDistance = Screen.width * 15 / 100; //dragDistance is 15% width of the screen
        StartCoroutine("MoveIn");
    }
 
    void Update()
    {
#if UNITY_ANDROID
        if (Input.touchCount == 1 && !isMoving) // user is touching the screen with a single touch
        {
            Touch touch = Input.GetTouch(0); // get the touch
            if (touch.phase == TouchPhase.Began) //check for the first touch
            {
                fp = touch.position;
                lp = touch.position;
            }
            else if (touch.phase == TouchPhase.Moved) // update the last position based on where they moved
            {
                lp = touch.position;
            }
            else if (touch.phase == TouchPhase.Ended) //check if the finger is removed from the screen
            {
                lp = touch.position;  //last touch position. Ommitted if you use list
 
                //Check if drag distance is greater than 20% of the screen height
                if (Mathf.Abs(lp.x - fp.x) > dragDistance || Mathf.Abs(lp.y - fp.y) > dragDistance)
                {//It's a drag
                 //check if the drag is vertical or horizontal
                    if (Mathf.Abs(lp.x - fp.x) > Mathf.Abs(lp.y - fp.y))
                    {   //If the horizontal movement is greater than the vertical movement...
                        if ((lp.x > fp.x))  //If the movement was to the right)
                        {   //Right swipe
                        }
                        else
                        {   //Left swipe
                            
                            
                            if (NextObject != null)
                            {
                                StartCoroutine("MoveOut");
                                NextObject.SetActive(true);
                            }
                            else
                            {
                                StartCoroutine("Finish");
                            }
 
                        }
                    }
                }
            }
        }
#endif
#if !UNITY_ANDROID
        if(Input.GetKeyDown("left") && !isMoving)
        {
               
            if (NextObject != null)
            {
                StartCoroutine("MoveOut");   
                NextObject.SetActive(true); 
            }
            else
            {
                StartCoroutine("Finish");
            }      
        }
#endif
    }

    public IEnumerator MoveIn() 
    {
        isMoving = true;
        Vector3 startPosition = transform.position;
        startPosition.x += MaxPosition;
        transform.position = startPosition;

        for (float pos = MaxPosition; pos > 0; pos -= Speed) 
        {
            Vector3 newPos = transform.position;
            newPos.x -= Speed;
            transform.position = newPos;
            yield return new WaitForSeconds(0.005f);
        }

        StartCoroutine("ShowTimer");

        isMoving = false;
    }

    IEnumerator MoveOut() 
    {
        isMoving = true;
        for (float pos = 0; pos < MaxPosition; pos += Speed) 
        {
            Vector3 newPos = transform.position;
            newPos.x -= Speed;
            transform.position = newPos;
            yield return new WaitForSeconds(0.005f);
        }        
        gameObject.SetActive(false);
    }

    IEnumerator Finish() 
    {
        isMoving = true;
        for (float pos = 0; pos < MaxPosition; pos += Speed) 
        {
            Vector3 newPos = transform.position;
            newPos.x -= Speed;
            transform.position = newPos;
            yield return new WaitForSeconds(0.005f);
        }        
        SceneManager.LoadScene("StartScreen");
    }

    IEnumerator ShowTimer()
    {
        yield return new WaitForSeconds(4);
        if(!isMoving)
        {
            if (NextObject != null)
            {
                StartCoroutine("MoveOut");   
                NextObject.SetActive(true);  
            }
            else
            {
                StartCoroutine("Finish");
            }      
        }
    } 


}
