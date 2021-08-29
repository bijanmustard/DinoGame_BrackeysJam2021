using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Code © Bijan Pourmand
 * Authored 8/28/21
 * Script for Lava plane
 */

public class Lava : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        //1. Check what entered lava
        //1a. If caveman, tally caveman as destroyed
        if(other.tag == "Caveman")
        {
            other.GetComponentInParent<Caveman>().Perish();
                
        }
    }
}
