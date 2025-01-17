﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public enum DoorType
    {
        //swipe doors will slide the door open in a direction and slide back when closed
        SwipeUP,
        SwipeDown,
        SwipeLeft,
        SwipeRight,
        SwipeFront,
        SwipeBack,
        //hinge doors rotate using the models rotation, center the models axis in a 3d modeling program to be over the hinge position of your door
        //set the hinge distance at most to 175 - higher than this may miss the code - 180 and up will not work - hinges cannot do a 360 degree turn because they check a distance value that only goes to 180
        HingeUp,
        HingeDown,
        HingeLeft,
        HingeRight,
        HingeFront,
        HingeBack,
        //Waypoint Door will move to the transform of a game object you choose to be the waypoint
        MoveToWaypoint,
    }
    public GameObject player; //player distance check is made using this, attach this to the player
    public DoorType type;
    public GameObject waypoint; //when using MoveToWaypoint set the object that will be the waypoint
    public GameObject activateTarget; //if using a different gameobject as the activation target place it here, this is helpful for centering a hinge gate's activationrange    
    public bool distanceTrigger = true;//if set to false the door will need to be opened by a trigger, setting openNow = true will open the door
    public bool showOpenRange;//show range (red) in scene window
    public float openRange;//set the size of the door opening trigger 
    public bool showCloseRange;//show range (blue) in scene window
    public float closeRange;// if close range is set to 0 then the door will not close. any other value will have the door close based on player distance from trigger
    public float swipeDistance;// set how far the door will move when using a swipe door
    public float movementSpeed = 20; // set how fast the door will move
    [Range(0.0f, 178f)] public float HingeDistance = 90; //pick within this range for hinged door open angle
    public float swayBuffer = 2.4f;//how much the hinge door will move when settling in to position
    public AudioClip[] openSounds; //set of sounds to be picked from to play the door opening sound


    AudioClip openAudio; //reference to deathAudio selection
    float state;
    Vector3 s_Distance;
    Vector3 originalPosition;
    Quaternion originalRotation;
    Quaternion currentRotation;
    bool openNow = false;
    bool swipingDoor;
    //float Distance;
    // Use this for initialization
    void Start()
    {
        //remember original location
        originalPosition = transform.position;
        originalRotation = transform.rotation;
        //Pick a Random sound from the set
        if (openSounds.Length > 0 ) {
            openAudio = openSounds[Random.Range(0, openSounds.Length)];
        }

        //create moveToward position
        if (type == DoorType.SwipeDown)
        {
            s_Distance = new Vector3(transform.position.x, (transform.position.y - swipeDistance), transform.position.z);
            swipingDoor = true;
        }
        if (type == DoorType.SwipeUP)
        {
            s_Distance = new Vector3(transform.position.x, (transform.position.y + swipeDistance), transform.position.z);
            swipingDoor = true;
        }
        if (type == DoorType.SwipeBack)
        {
            s_Distance = new Vector3(transform.position.x, transform.position.y, (transform.position.z - swipeDistance));
            swipingDoor = true;
        }
        if (type == DoorType.SwipeFront)
        {
            s_Distance = new Vector3(transform.position.x, transform.position.y, (transform.position.z + swipeDistance));
            swipingDoor = true;
        }
        if (type == DoorType.SwipeRight)
        {
            s_Distance = new Vector3((transform.position.x + swipeDistance), transform.position.y, transform.position.z);
            swipingDoor = true;
        }
        if (type == DoorType.SwipeLeft)
        {
            s_Distance = new Vector3((transform.position.x - swipeDistance), transform.position.y, transform.position.z);
            swipingDoor = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //   if (Input.GetAxis("Fire1") == 1)
        //    {
        // print("xxxxxxxxxxx");
        //print(Quaternion.Angle(originalRotation, currentRotation));
       // print(HingeDistance + "HD");
        print(state + "state");
        // print("xxxxxxxxxxx");
        // print(Quaternion.Angle(originalRotation, currentRotation) > HingeDistance);
        //   }
        if (distanceTrigger == true)
        {
            if (state == 0)
            {
                if (activateTarget != null)
                {
                    if (Vector3.Distance(activateTarget.transform.position, player.transform.position) <= openRange)
                    {
                        if (openSounds.Length > 0)
                        {
                            AudioSource.PlayClipAtPoint(openAudio, transform.position); // play door open sound
                        }
                        state = 1;

                    }
                }
                else
                {
                    if (Vector3.Distance(transform.position, player.transform.position) <= openRange)
                    {
                        if (openSounds.Length > 0)
                        {
                            AudioSource.PlayClipAtPoint(openAudio, transform.position); // play door open sound
                        }
                        state = 1;
                    }
                }
            }
        }
        else { WaitForTrigger(); }
        //print(state);
        if (state == 1)
        {
            //           print(Vector3.Distance(transform.position, originalLocation));
            //          print(swipeDistance);

            //open door
            if (type == DoorType.MoveToWaypoint)
            {
                float speed = movementSpeed * Time.deltaTime;
                transform.position = Vector3.MoveTowards(transform.position, waypoint.transform.position, speed);
                if (Vector3.Distance(transform.position, waypoint.transform.position) <= 0)
                {
                    state = 2;
                }
                return; //dont check other door types
            }

            if (swipingDoor == true)
            {
                float swipe = movementSpeed * Time.deltaTime;
                transform.position = Vector3.MoveTowards(transform.position, s_Distance, swipe);
                if (Vector3.Distance(transform.position, originalPosition) >= (swipeDistance - 1))
                {
                    state = 2;
                }
            }
            else
            {
                //hinge like turning - requires the model to have its axis placed in the hinge location(this would be done in modeling program) FYI - if the axis is in the center it will spin from there
                currentRotation = transform.rotation;
                if (type == DoorType.HingeRight)
                {
                    transform.Rotate(Vector3.right, movementSpeed * Time.deltaTime);//have item spin on X rotation (1,0,0)
                }
                if (type == DoorType.HingeLeft)
                {
                    transform.Rotate(Vector3.left, movementSpeed * Time.deltaTime);//have item spin on X rotation (-1,0,0)
                }
                if (type == DoorType.HingeUp)
                {
                    transform.Rotate(Vector3.up, movementSpeed * Time.deltaTime);//have item spin on Y rotation (0,1,0)
                }
                if (type == DoorType.HingeDown)
                {
                    transform.Rotate(Vector3.down, movementSpeed * Time.deltaTime);//have item spin on Y rotation (0,-1,0)
                }
                if (type == DoorType.HingeFront)
                {
                    transform.Rotate(Vector3.forward, movementSpeed * Time.deltaTime);//have item spin on z rotation (0,0,1)
                }
                if (type == DoorType.HingeBack)
                {
                    transform.Rotate(Vector3.back, movementSpeed * Time.deltaTime);//have item spin on Z rotation (0,0,-1)
                }

                if (Quaternion.Angle(originalRotation, currentRotation) > HingeDistance + swayBuffer)
                {
                    state = 1.5f;
                }
            }
        }
        if (state == 1.5f)
        {
            currentRotation = transform.rotation;

            if (type == DoorType.HingeRight)
            {
                transform.Rotate(Vector3.left, movementSpeed / 2 * Time.deltaTime);//have item spin on X rotation (-1,0,0)
            }
            if (type == DoorType.HingeLeft)
            {
                transform.Rotate(Vector3.right, movementSpeed / 2 * Time.deltaTime);//have item spin on X rotation (-1,0,0)
            }
            if (type == DoorType.HingeUp)
            {
                transform.Rotate(Vector3.down, movementSpeed / 2 * Time.deltaTime);//have item spin on Y rotation (0,1,0)
            }
            if (type == DoorType.HingeDown)
            {
                transform.Rotate(Vector3.up, movementSpeed / 2 * Time.deltaTime);//have item spin on Y rotation (0,-1,0)
            }
            if (type == DoorType.HingeFront)
            {
                transform.Rotate(Vector3.back, movementSpeed / 2 * Time.deltaTime);//have item spin on z rotation (0,0,1)
            }
            if (type == DoorType.HingeBack)
            {
                transform.Rotate(Vector3.forward, movementSpeed / 2 * Time.deltaTime);//have item spin on Z rotation (0,0,-1)
            }

            if (Quaternion.Angle(originalRotation, currentRotation) <= HingeDistance - swayBuffer)
            {
                state = 1.8f;
                //state = 0;
            }
        }
        if (state == 1.8f)
        {
            //add door swing back and forth
            currentRotation = transform.rotation;
            if (type == DoorType.HingeRight)
            {
                transform.Rotate(Vector3.right, movementSpeed / 6 * Time.deltaTime);//have item spin on X rotation (1,0,0)
            }
            if (type == DoorType.HingeLeft)
            {
                transform.Rotate(Vector3.left, movementSpeed / 6 * Time.deltaTime);//have item spin on X rotation (-1,0,0)
            }
            if (type == DoorType.HingeUp)
            {
                transform.Rotate(Vector3.up, movementSpeed / 6 * Time.deltaTime);//have item spin on Y rotation (0,1,0)
            }
            if (type == DoorType.HingeDown)
            {
                transform.Rotate(Vector3.down, movementSpeed / 6 * Time.deltaTime);//have item spin on Y rotation (0,-1,0)
            }
            if (type == DoorType.HingeFront)
            {
                transform.Rotate(Vector3.forward, movementSpeed / 6 * Time.deltaTime);//have item spin on z rotation (0,0,1)
            }
            if (type == DoorType.HingeBack)
            {
                transform.Rotate(Vector3.back, movementSpeed / 6 * Time.deltaTime);//have item spin on Z rotation (0,0,-1)
            }


            if (Quaternion.Angle(originalRotation, currentRotation) >= HingeDistance)
            {
                state = 2;
            }
        }
        if (state == 2)
        {
            //check player is out of open range
            if (closeRange <= 0) return;//dont use close range if it is set to zero, stop the script here
            if (activateTarget != null)
            {
                if (Vector3.Distance(activateTarget.transform.position, player.transform.position) >= closeRange)
                {
                    if (openSounds.Length > 0)
                    {
                        AudioSource.PlayClipAtPoint(openAudio, transform.position);
                    }
                    state = 3;


                }
            }
            else
            {
                if (Vector3.Distance(transform.position, player.transform.position) >= closeRange)
                {
                    if (openSounds.Length > 0)
                    {
                        AudioSource.PlayClipAtPoint(openAudio, transform.position);
                    }
                    state = 3;


                }
            }
        }
        if (state == 3)
        {
            // close door
            if (type == DoorType.MoveToWaypoint)
            {
                float speed = movementSpeed * Time.deltaTime;
                transform.position = Vector3.MoveTowards(transform.position, originalPosition, speed);
                if (Vector3.Distance(transform.position, originalPosition) <= 0)
                {
                    state = 0;
                }
                return; //dont check other door types
            }
            if (swipingDoor == true)
            {
                float swipe = movementSpeed * Time.deltaTime;
                transform.position = Vector3.MoveTowards(transform.position, originalPosition, swipe);
                if (transform.position == originalPosition)
                {
                    state = 0;
                }
            }
            else
            {
                //close hinge door
                currentRotation = transform.rotation;

                if (type == DoorType.HingeRight)
                {
                    transform.Rotate(Vector3.left, movementSpeed * Time.deltaTime);//have item spin on X rotation (-1,0,0)
                }
                if (type == DoorType.HingeLeft)
                {
                    transform.Rotate(Vector3.right, movementSpeed * Time.deltaTime);//have item spin on X rotation (-1,0,0)
                }
                if (type == DoorType.HingeUp)
                {
                    transform.Rotate(Vector3.down, movementSpeed * Time.deltaTime);//have item spin on Y rotation (0,1,0)
                }
                if (type == DoorType.HingeDown)
                {
                    transform.Rotate(Vector3.up, movementSpeed * Time.deltaTime);//have item spin on Y rotation (0,-1,0)
                }
                if (type == DoorType.HingeFront)
                {
                    transform.Rotate(Vector3.back, movementSpeed * Time.deltaTime);//have item spin on z rotation (0,0,1)
                }
                if (type == DoorType.HingeBack)
                {
                    transform.Rotate(Vector3.forward, movementSpeed * Time.deltaTime);//have item spin on Z rotation (0,0,-1)
                }

                if (Quaternion.Angle(originalRotation, currentRotation) <= 5)//set to 5 here as a buffer so the .Angle isnt missed
                {
                    state = 4;
                    //   originalRotation = currentRotation;
                    //   state = 0;
                }
            }
        }
        if (state == 4)
        {
            //add door swing back and forth
            currentRotation = transform.rotation;
            if (type == DoorType.HingeRight)
            {
                transform.Rotate(Vector3.right, movementSpeed / 2 * Time.deltaTime);//have item spin on X rotation (1,0,0)
            }
            if (type == DoorType.HingeLeft)
            {
                transform.Rotate(Vector3.left, movementSpeed / 2 * Time.deltaTime);//have item spin on X rotation (-1,0,0)
            }
            if (type == DoorType.HingeUp)
            {
                transform.Rotate(Vector3.up, movementSpeed / 2 * Time.deltaTime);//have item spin on Y rotation (0,1,0)
            }
            if (type == DoorType.HingeDown)
            {
                transform.Rotate(Vector3.down, movementSpeed / 2 * Time.deltaTime);//have item spin on Y rotation (0,-1,0)
            }
            if (type == DoorType.HingeFront)
            {
                transform.Rotate(Vector3.forward, movementSpeed / 2 * Time.deltaTime);//have item spin on z rotation (0,0,1)
            }
            if (type == DoorType.HingeBack)
            {
                transform.Rotate(Vector3.back, movementSpeed / 2 * Time.deltaTime);//have item spin on Z rotation (0,0,-1)
            }


            if (Quaternion.Angle(originalRotation, currentRotation) > HingeDistance / 10)
            {
                state = 5;
            }
        }
        if (state == 5)
        {
            currentRotation = transform.rotation;

            if (type == DoorType.HingeRight)
            {
                transform.Rotate(Vector3.left, movementSpeed / 4 * Time.deltaTime);//have item spin on X rotation (-1,0,0)
            }
            if (type == DoorType.HingeLeft)
            {
                transform.Rotate(Vector3.right, movementSpeed / 4 * Time.deltaTime);//have item spin on X rotation (-1,0,0)
            }
            if (type == DoorType.HingeUp)
            {
                transform.Rotate(Vector3.down, movementSpeed / 4 * Time.deltaTime);//have item spin on Y rotation (0,1,0)
            }
            if (type == DoorType.HingeDown)
            {
                transform.Rotate(Vector3.up, movementSpeed / 4 * Time.deltaTime);//have item spin on Y rotation (0,-1,0)
            }
            if (type == DoorType.HingeFront)
            {
                transform.Rotate(Vector3.back, movementSpeed / 4 * Time.deltaTime);//have item spin on z rotation (0,0,1)
            }
            if (type == DoorType.HingeBack)
            {
                transform.Rotate(Vector3.forward, movementSpeed / 4 * Time.deltaTime);//have item spin on Z rotation (0,0,-1)
            }

            if (Quaternion.Angle(originalRotation, currentRotation) <= 5)//set to 5 here as a buffer so the .Angle isnt missed
            {
                state = 0;
                //state = 0;
            }
        }
    }

    void WaitForTrigger()
    {
        //setting openNow to true elsewhere will set off trigger
        if (openNow == true)
        {
            distanceTrigger = true;
            openRange = 99999;//this will hopefully catch all ranges for player distance to activate the door...add more 9's if not.
        }

    }

    //void Open()
    //{
    //    if (type == DoorType.SwipeDown)
    //    {
    //       float swipe = swipeSpeed* Time.deltaTime;
    //       // Vector3 s_Distance = new Vector3(transform.position.x, (transform.position.y - swipeDistance));
    //        transform.position = Vector3.MoveTowards(transform.position,s_Distance, swipe);
    //    }
    //}

    void OnDrawGizmos()
    {
        if (type == DoorType.MoveToWaypoint)
        {
            Gizmos.color = new Color(1, 1, 0, 0.6f);//yelllow
            Gizmos.DrawSphere(waypoint.transform.position, 1); //changing the 1 here will change the size of the waypoint in your scene
        }
            if (showOpenRange == true)
        {
            if (openRange <= 0) return;
            Gizmos.color = new Color(1, 0, 0, 0.1f);//red
            if (activateTarget != null)
            {
                Gizmos.DrawSphere(activateTarget.transform.position, openRange);
            }
            else
            {
                Gizmos.DrawSphere(transform.position, openRange);
            }
        }
        if (showCloseRange == true)
        {
            if (closeRange <= 0) return;
            Gizmos.color = new Color(0, 0, 1, 0.1f);//blue
            if (activateTarget != null)
            {
                Gizmos.DrawSphere(activateTarget.transform.position, closeRange);
            }
            else
            {
                Gizmos.DrawSphere(transform.position, closeRange);
            }
        }
    }
}

