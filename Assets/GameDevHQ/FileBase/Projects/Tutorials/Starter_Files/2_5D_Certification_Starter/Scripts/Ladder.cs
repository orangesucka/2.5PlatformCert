using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladder : MonoBehaviour
{
    public Vector3 ladderFeetPos, ladderHandPos;
    public Player player;
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ladder_Grab_Checker"))
        {
            var player = other.transform.parent.GetComponent<Player>();

            if (player != null)
            {
                player.GrabLadder(ladderHandPos, this);
            }
        }
    }
    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Ladder_Grab_Checker"))
        {
            var player = other.transform.parent.GetComponent<Player>();

            if (player != null)
            {
                player.UnGrabLadder(ladderHandPos, this);
            }
        }
    }
    public Vector3 StandingLadderPosition()
    {
        //Debug.Log(player.transform.position);
        Debug.Log("FeetPosBeingCalled");
        player.transform.position = ladderFeetPos;
        player.chrtrCntrlr.enabled = true;
        player.activeLadder = null;
        return ladderFeetPos;
    }
}
