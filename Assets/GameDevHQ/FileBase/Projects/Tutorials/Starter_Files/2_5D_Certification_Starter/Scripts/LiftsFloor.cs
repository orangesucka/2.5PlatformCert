//using GameDevHQ.Filebase.DataModels;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Security.Cryptography;
//using System.Threading;
//using TMPro;
//using TreeEditor;
//using UnityEditor.Rendering;
//using UnityEditor.ShaderGraph;
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
    public bool goingUp2;
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
        if(transform.position.y <= groundFloor.position.y)
        {
            goingDown = false;
            goingUp = true;
        }
        if (goingDown == true)
        {
            transform.position = Vector3.MoveTowards(transform.position, groundFloor.position, liftSpeed * 1 * Time.deltaTime);
        }
        if(transform.position.y >= topFloor.position.y)
        {
            goingDown = true;
            goingUp = false;
            goingUp2 = false;
        }
        if (goingUp == true)
        {
            transform.position = Vector3.MoveTowards(transform.position, topFloor.position, liftSpeed * Time.deltaTime);
        }
        else if (goingUp2 == true)
        {
            transform.position = Vector3.MoveTowards(transform.position, topFloor.position, liftSpeed * Time.deltaTime);
        }
        else if(goingDown == true)
        {
            transform.position = Vector3.MoveTowards(transform.position, groundFloor.position, liftSpeed * Time.deltaTime);
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
                StartCoroutine(ElevatorUpPause());
            }
        }
        else
        {
            if (goingDown == false)
            {
                Gizmos.color = Color.green;
                Gizmos.DrawRay(transform.position + originRight, noAngle * horizontalRaycastDistance);
                goingUp = true;
                goingUp2 = false;
            }
        }
    }
        
    IEnumerator ElevatorUpPause()
    {
        goingUp = false;
        yield return new WaitForSeconds(5f);
        goingUp2 = true;
        yield return new WaitForSeconds(.25f);
        goingUp2 = false;
        
    }

    //Elevator Player Smooth Move
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("PlayerOnBoard");
            other.transform.parent = this.transform;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("PlayerDoneGone");
            other.transform.parent = null;
        }
    }
}
