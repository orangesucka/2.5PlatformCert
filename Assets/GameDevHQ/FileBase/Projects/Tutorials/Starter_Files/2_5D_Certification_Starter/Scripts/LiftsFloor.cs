using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Security.Cryptography;
using UnityEngine;

public class LiftsFloor : MonoBehaviour
{
    public float liftSpeed;
    public float horizontalRaycastDistance;
    public float verticalRaycastDistance;
    public float rayAngle;
    public Transform trans;
    public Transform groundFloor;
    public Transform topFloor;
    public Vector3 originRight;
    public bool goingUp;
    public bool goingDown;

    // Start is called before the first frame update
    void Start()
    {
        goingUp = true;
        trans = GetComponent<Transform>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        ElevatorMovement();
    }
    public void ElevatorMovement()
    {
        if(goingUp == true)
        {
            transform.position = Vector3.MoveTowards(transform.position, topFloor.position, liftSpeed * Time.deltaTime);
        }
        else
        {
            if(goingDown == true)
            transform.position = Vector3.MoveTowards(transform.position, groundFloor.position, liftSpeed * Time.deltaTime);
        }
        if(transform.position == topFloor.position)
        {
            StartCoroutine(ElevatorUpPause());
            goingUp = false;

        }
        if(transform.position == groundFloor.position)
        {
            StartCoroutine(ElevatorDownPause());
            goingDown = false;
        }

    }
    public void OnDrawGizmos()
    {
        
        //Quaternion spreadAngle = Quaternion.AngleAxis(rayAngle, new Vector3(1, 0, 0));
        Vector3 noAngle = transform.forward;
        //Vector3 nintyAngle = transform.up*(-1);

        RaycastHit hitDraw;
        
        bool horizontalDraw = Physics.Raycast(transform.position + originRight, noAngle, out hitDraw, horizontalRaycastDistance);
        if (horizontalDraw)
        {
            if (goingDown == false)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawRay(transform.position + originRight, noAngle * hitDraw.distance);
            }
        }
        else
        {
            if (goingDown == false)
            {
                Gizmos.color = Color.green;
                Gizmos.DrawRay(transform.position + originRight, noAngle * horizontalRaycastDistance);
            }
        }
    }
        
    IEnumerator ElevatorUpPause()
    {
        yield return new WaitForSeconds(5f);
        goingDown = true;        
    }
    IEnumerator ElevatorDownPause()
    {
        yield return new WaitForSeconds(5f);
        goingUp = true;
    }
    //Elevator Player Smooth Move
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("PlayerOnBoard");
            other.transform.parent = this.transform;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("PlayerDoneGone");
            other.transform.parent = null;
        }
    }
}
