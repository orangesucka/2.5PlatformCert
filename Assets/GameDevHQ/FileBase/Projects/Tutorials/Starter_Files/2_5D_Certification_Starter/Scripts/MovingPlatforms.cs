using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatforms : MonoBehaviour
{
    public float platformSpeed;
    public Transform targetA, targetB;
    public bool movingUp;
    

    // Update is called once per frame
    void FixedUpdate()
    {
        if (movingUp == true)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetA.position, platformSpeed * Time.deltaTime);
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, targetB.position, platformSpeed * Time.deltaTime);
        }
        if(transform.position == targetA.position)
        {
            movingUp = false;
        }
        else if(transform.position == targetB.position)
        {
            movingUp = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            other.transform.parent = this.transform;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            other.transform.parent = null;
        }
    }
}
