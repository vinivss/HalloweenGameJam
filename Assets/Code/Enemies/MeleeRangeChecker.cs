using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeRangeChecker : MonoBehaviour
{
    public bool InMelee = false;

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.tag == "Player")
        {
            Debug.Log("inMelee");
            InMelee = true;
        }

       
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player"&& InMelee == true)
        {
            InMelee = false;
        }
    }
}
