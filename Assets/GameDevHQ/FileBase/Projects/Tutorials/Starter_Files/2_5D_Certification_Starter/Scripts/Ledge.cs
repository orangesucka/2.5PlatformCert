using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ledge : MonoBehaviour
{
    private Vector3 handPosition, feetPosition;
    public Vector3 handPositionFineTuning, feetPositionFineTuning;
    public Player player;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ledge_Grab_Checker"))
        {
            var player = other.transform.parent.GetComponent<Player>();

            if(player != null)
            {
                handPosition = player.transform.position + handPositionFineTuning;
                player.GrabLedge(handPosition, this);  
            }
        }
    }

    public Vector3 StandingPosition()
    {
        feetPosition = player.transform.position + feetPositionFineTuning;
        return feetPosition;
    }
}
