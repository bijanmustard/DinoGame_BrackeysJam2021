using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Code © Bijan Pourmand
 * Authored 8/25/21
 * Script for TNT Barrels
 */

public class TNT : MonoBehaviour
{
    //Vars & Refs
    Rigidbody rb;
    bool fuseLit = false;
    static float explosionForce = 7500f;
    static float explosionRadius = 20f;

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1,0,0,0.5f);
        Gizmos.DrawSphere(transform.position, explosionRadius);
        Gizmos.color = new Color(1, 1, 1, 0.5f);
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) Detonate();
    }

    //Detonate is called to explode the barrel.
    public void Detonate()
    {
        Debug.Log("BOOM!");
        //1. Get nearby colliders
        Collider[] toBoom = Physics.OverlapSphere(transform.position, explosionRadius);
        //2. Apply explosion force to RBs
        foreach(Collider col in toBoom)
        {
            Rigidbody r = col.GetComponent<Rigidbody>();
            if(r != null) r.AddExplosionForce(explosionForce, transform.position, explosionRadius);
        }
       
    }



}
