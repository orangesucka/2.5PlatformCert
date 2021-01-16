using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Rendering;
using UnityEngine.VFX;

public class Player : MonoBehaviour
{
    public float speed;
    public float ladderSpeed;
    public float jumpHeight;
    public float gravity;
    public float verticalSpeed;
    public float rayHeight;
    public float rayAngle;
    public float rayCastRightDistance;
    public float rayCastDownDistance;
    public bool rayBool;
    public bool onLedge;
    public bool onLadder;
    private bool offLadder;
    public int spheres;
    public Vector3 startingPosition;
    public Vector3 direction;
    public Vector3 velocity;
    public Vector3 topOfLadder;
    public CharacterController chrtrCntrlr;

    public Animator charAnimator;
    public Ledge activeLedge;
    public UIManager uiManager;
    public Ladder activeLadder;
    //public TopOfLadder topOfLadderScript;

    // Start is called before the first frame update
    void Start()
    {
        uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        chrtrCntrlr = GetComponent<CharacterController>();
        charAnimator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();
        Climbing();
        ClimbingLadder();
        GetOffLadder();
        NinjaRoll();
        Death();
    }

    public void CalculateMovement()
    { 
        float h = Input.GetAxisRaw("Horizontal");
        if (chrtrCntrlr.isGrounded == true)
        {
            direction = new Vector3(0, 0, h);
            charAnimator.SetFloat("Speed", /*Mathf.Abs*/(h));
            charAnimator.SetBool("RunJumpForward", false);
            charAnimator.SetBool("RunJumpBack", false);
            charAnimator.SetBool("UpJump", false);
            charAnimator.SetBool("ClimbUp", false);
            //charAnimator.SetBool("ClimbingOffLadder", false);
            velocity = direction * speed;
            onLedge = false;

            if (Input.GetKeyDown(KeyCode.Space) && /*Mathf.Abs*/(h) > 0.1f)
            {
                verticalSpeed = jumpHeight;
                //Trigger forward jump animation
                charAnimator.SetBool("RunJumpForward", true);
            }

            if (Input.GetKeyDown(KeyCode.Space) && h < 0f)
            {
                verticalSpeed = jumpHeight;
                //Trigger back jump animation
                charAnimator.SetBool("RunJumpBack", true);
            }
            else if (Input.GetKeyDown(KeyCode.Space))
            {
                verticalSpeed = jumpHeight;
                charAnimator.SetBool("UpJump", true);
            }
            //Use if flipping direction
            /*if (h != 0)
            {
                Vector3 facing = transform.localEulerAngles;
                facing.y = direction.z > 0 ? 0 : 180;
                transform.localEulerAngles = facing;
            }
            */
        }
        else
        {
            verticalSpeed -= gravity;
        }
        if (chrtrCntrlr.enabled == true)
        {
            velocity.y = verticalSpeed;
            chrtrCntrlr.Move(velocity * Time.deltaTime);
        }
    }
    public void NinjaRoll()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            Debug.Log("Left Shift has been pressed");
            charAnimator.SetBool("NinjaRollBool", true);
        }
        else
        {
            charAnimator.SetBool("NinjaRollBool", false);
        }
    }
    public void GrabLadder(Vector3 ladderHandPos, Ladder currentLadder)
    {
        Debug.Log("OnLadder is true");
        onLadder = true;
        activeLadder = currentLadder;
        gravity = 0;
    }
    
    public void UnGrabLadder(Vector3 ladderHandPos, Ladder currentLadder)
    {
        onLadder = false;
        offLadder = true;
        chrtrCntrlr.enabled = true;
        gravity = 1;
        direction = Vector3.zero;
        verticalSpeed -= gravity;
        velocity.y = verticalSpeed;
        chrtrCntrlr.Move(velocity * Time.deltaTime);
        Debug.Log(chrtrCntrlr.isGrounded);
        Debug.Log("Onladder is false");
    }

    public void GetOffLadder()
    {
        if (offLadder == true)
        {

            //chrtrCntrlr.enabled = false;  //This Breaks the game

            charAnimator.SetBool("ClimbingOffLadder", true);

            //Turn this back on maybe?
            //charAnimator.SetBool("ClimbingLadder", false);
        }
    }

    public void GrabLedge(Vector3 handPosition, Ledge currentLedge)
    {
        chrtrCntrlr.enabled = false;
        charAnimator.SetBool("UpJump", false);
        charAnimator.SetBool("GrabLedge", true);
        charAnimator.SetFloat("Speed", 0.0f);
        charAnimator.SetBool("RunJumpForward", false);

        onLedge = true;
        transform.position = handPosition;
        activeLedge = currentLedge;
    }

    public void ClimbUpComplete()
    {
        //Debug.Log("ClimbUpComplete()");
        transform.position = activeLedge.StandingPosition();
        charAnimator.SetBool("GrabLedge", false);
        chrtrCntrlr.enabled = true;
    }
    public void ClimbUpLadderComplete()
    {
        Debug.Log("ClimbUpComplete()");
        //onLadder = false;
        transform.position = activeLadder.StandingLadderPosition();
        
        // This maybe needs to go back off
        charAnimator.SetBool("ClimbingLadder", false);
        chrtrCntrlr.enabled = true;
    }

    public void Climbing()
    {
        if (onLedge == true)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("You pressed the E key");
                charAnimator.SetBool("ClimbUp", true);
            }
        }
    }
    public void ClimbingLadder()
    {
        if (onLadder == true)
        {
            charAnimator.SetFloat("Speed", 0);   
            float v = Input.GetAxisRaw("Vertical");
            direction = new Vector3(0, v, 0);
            velocity = direction * ladderSpeed;
            verticalSpeed = velocity.y;
            chrtrCntrlr.Move(velocity * ladderSpeed);
            
            if (velocity.y > .1f)
            {
                charAnimator.speed = 1;
                charAnimator.SetBool("ClimbingLadder", true);
                charAnimator.SetBool("ClimbDownLadder", false);
            }
            else if(charAnimator.GetBool("ClimbingLadder") == true && velocity.y == 0)
            {
                charAnimator.speed = 0;
            }
            if (velocity.y < -.1f)
            {
                charAnimator.speed = 1;
                charAnimator.SetBool("ClimbDownLadder", true);
                charAnimator.SetBool("ClimbingLadder", false);
            }
            else if (charAnimator.GetBool("ClimbDownLadder") == true && velocity.y == 0)
            {
                charAnimator.speed = 0;
            }
        }
    }

    public void OnDrawGizmos()
    {
        Vector3 noAngle = transform.forward;
        Quaternion spreadAngle = Quaternion.AngleAxis(rayAngle, new Vector3(1, 0, 0));
        Vector3 newVector = spreadAngle * noAngle;
        Vector3 newOrigin = new Vector3(0, rayHeight, 0);

        RaycastHit hitDraw;

        bool horizontalDraw = Physics.Raycast(transform.position + newOrigin, newVector, out hitDraw, rayCastRightDistance);
        if (horizontalDraw)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawRay(transform.position + newOrigin, newVector * hitDraw.distance);
            rayBool = true;
        }
        else
        {
            Gizmos.color = Color.green;
            Gizmos.DrawRay(transform.position + newOrigin, newVector * rayCastRightDistance);
            rayBool = false;
        }

        bool downDraw = Physics.Raycast(transform.position, transform.up * -1, out hitDraw, rayCastDownDistance);
        if (downDraw)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawRay(transform.position, transform.up * -1 * hitDraw.distance);
        }
        else
        {
            Gizmos.color = Color.green;
            Gizmos.DrawRay(transform.position, transform.up * -1 * rayCastDownDistance);
        }
    }
    public void Death()
    {
        if (transform.position.y <= 0f)
        {
            transform.position = startingPosition;
        }
    }

    public void addSpheres()
    {
        spheres++;
        uiManager.UpdateSpheresDisplay(spheres);
    }

    public int SphereCount()
    {
        return spheres;
    }
}
