using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    //touch manager for gameplay phase
    //assuming SWIPE GAMEPLAY
    public Vector2 startPos;
    public Vector2 direction;
    public Vector2 swipe;

    public BallRoll ball;
    public GameManager manager;
    // public LineRenderer trail;
    public Camera cam;

    private void Awake()
    {
        // ball = GameObject.FindObjectOfType<BallRoll>();
        manager = GameObject.FindObjectOfType<GameManager>();
        // trail = GameObject.FindObjectOfType<LineRenderer>();
        // trail.useWorldSpace = false;
        cam = GameObject.FindObjectOfType<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        #region Touch Controls
        if(Input.touchCount > 0)
        {
            //if we are touching, proceed
            Touch touch = Input.GetTouch(0); //get the first touch
            //Evaluate movement based on touch phase
            switch (touch.phase)
            {
                //touch is first detected -- get starting position!!
                case TouchPhase.Began:
                    // trail.positionCount = 0;
                    startPos = touch.position;
                    // Debug.Log("Start touch!");
                    break;

                //determine where we're moving
                case TouchPhase.Moved:
                    //compare current touch position to beginning position
                    direction = touch.position - startPos;
                    // trail.positionCount++;

                    // Debug.Log("Touch is moving. Current Direction: " + direction);
                    break;
                
                //we are lifting off from the touch. Send final direction as swipe direction!
                case TouchPhase.Ended:
                    swipe = direction.normalized;
                    if(manager.allowInput)
                        SendVector();
                    // Debug.Log("End touch!");
                    break;
            }
        }
        #endregion

        #region Mouse Controls
        if(Input.GetMouseButtonDown(0))
        {
            //getting initial left click
            startPos = Input.mousePosition;
            
            // Debug.Log("Start touch!");
        }
        if(Input.GetMouseButton(0))
        {
            //getting initial left click
            direction = (Vector2)Input.mousePosition - startPos;
            // trail.positionCount++;
            // trail.SetPosition((trail.positionCount-1),cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 1)));
            // Debug.Log("Touch is moving. Current Direction: " + direction);
        }
        if(Input.GetMouseButtonUp(0))
        {
            //getting initial left click
            swipe = direction.normalized;
            // trail.positionCount = 0;
            // RectTransform trans = trail.gameObject.GetComponent<RectTransform>();
            // trans.position = Vector3.zero;
            if(manager.allowInput)
                SendVector();
            // Debug.Log("End touch!");
        }
        #endregion
    }

    public void SendVector()
    {
        ball.ApplyForce(swipe);
    }
}
