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
    public Transform trans;
    public Transform groundFloor;
    public Transform topFloor;
    public bool goingUp;
    public bool goingDown;

    // Start is called before the first frame update
    void Start()
    {
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
            {
                transform.position = Vector3.MoveTowards(transform.position, groundFloor.position, liftSpeed * Time.deltaTime);
            }
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
