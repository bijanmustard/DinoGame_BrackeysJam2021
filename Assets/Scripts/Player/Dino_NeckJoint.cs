using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * COde © Bijan Pourmand
 * Authored 8/26/21
 * Script for Dino neck joints, to be controlled by physics
 */

public class Dino_NeckJoint : MonoBehaviour
{
    public Rigidbody rb;

    private void Awake()
    {
        //1. Get ref to rigidbody
        rb = GetComponent<Rigidbody>();
        //2. Set cetner of mass to pivot
        rb.centerOfMass = transform.parent.TransformPoint(transform.localPosition);
    }

    private void Update()
    {
        Debug.DrawRay(rb.centerOfMass, Vector3.up * 5f, Color.red);
        ;
    }
    //SetRotation sets the rotation of the rigidbody
    public void SetRotation(Quaternion rot)
    {
        ////1. Set self rotation
        //rb.MoveRotation(rot);
        ////2. If next segment is neck segment, rotate it
        //Dino_NeckJoint next = transform.GetChild(0).GetComponent<Dino_NeckJoint>();
        //if (next != null) next.SetRotation(rot);
    }

    public void SetVelocity(Vector3 vel)
    {
        rb.velocity = vel;
        Dino_NeckJoint next = transform.GetChild(0).GetComponent<Dino_NeckJoint>();
        //if (next != null) next.SetVelocity(vel);
    }

}
