using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Transform targetA, targetB, targetC;
    public bool moveTowardsA, moveTowardsB, moveTowardsC;
    public float platformSpeed;
    public Player player; 
       // Update is called once per frame
    void Update()
    {
        PlatformMovement();
    }

    public void PlatformMovement()
    {
        if (moveTowardsA == true)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetA.position, platformSpeed * Time.deltaTime);
        }
        else if(moveTowardsB == true)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetB.position, platformSpeed * Time.deltaTime);
        }
        else if(moveTowardsC == true)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetC.position, platformSpeed * Time.deltaTime);
        }
        if(transform.position == targetC.position)
        {
            moveTowardsA = true;
            moveTowardsB = false;
            moveTowardsC = false;
        }
        if(transform.position == targetA.position)
        {
            moveTowardsA = false;
            moveTowardsB = true;
            moveTowardsC = false;
        }
        if(transform.position == targetB.position)
        {
            moveTowardsA = false;
            moveTowardsB = false;
            moveTowardsC = true;
        }
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Youre part of the Platform");
            other.transform.parent = this.transform;
        }
    }
    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Youre not Part of the Platform");
            other.transform.parent = null;
        }
    }
}
