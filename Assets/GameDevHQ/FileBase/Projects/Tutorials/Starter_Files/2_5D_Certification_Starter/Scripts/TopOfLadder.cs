using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopOfLadder : MonoBehaviour
{
    public Player player;    
    public void OnTriggerEnter(Collider other)
    {
        /*if (other.CompareTag("Ladder_Grab_Checker"))
        {
            var player = other.transform.parent.GetComponent<Player>();
            
            if (player != null)
            {
                player.OffLadder();
            }
        }*/
    }
}
