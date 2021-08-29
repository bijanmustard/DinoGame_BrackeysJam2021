using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Code © Bijan Pourmand
 * Authored 8/28/21
 * Script allowing caveman body col to interact with caveman script
 */

public class Caveman_BodyCol : MonoBehaviour
{
    Caveman myCaveman;
    private void Awake()
    {
        myCaveman = GetComponentInParent<Caveman>();
    }

    private void OnCollisionEnter(Collision collision)
    {

        if (myCaveman.isRagdoll && collision.gameObject.tag == "Floor") myCaveman.PlaySound(myCaveman.hit);
    }
}
