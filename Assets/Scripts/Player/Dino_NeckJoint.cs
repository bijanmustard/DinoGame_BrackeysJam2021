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
    Dino_Neck neck;
    public Rigidbody rb;
    public Vector3 localPos;

    private void Awake()
    {
        //1. Get ref to rigidbody
        rb = GetComponent<Rigidbody>();
        neck = GetComponentInParent<Dino_Neck>();
        //2. Set cetner of mass to pivot
        rb.centerOfMass = transform.parent.TransformPoint(transform.localPosition);
        localPos = transform.localPosition;
    }

    private void Update()
    {
        rb.centerOfMass = transform.parent.TransformPoint(transform.localPosition);
        Debug.DrawRay(rb.centerOfMass, Vector3.up * 10f, Color.red);
        
    }
    //SetRotation sets the rotation of the rigidbody
    public void SetRotation(Quaternion rot)
    {
        //1. Set self rotation
        rb.MoveRotation(rot.normalized);
        //2. If next segment is neck segment, rotate it
        Dino_NeckJoint next = transform.GetChild(0).GetComponent<Dino_NeckJoint>();
        if (next != null) next.SetRotation(rot);
    }

    public void SetVelocity(Vector3 vel)
    {
        rb.velocity = vel;
        Dino_NeckJoint next = transform.GetChild(0).GetComponent<Dino_NeckJoint>();
        if (next != null) next.SetVelocity(vel);
    }

    private void OnCollisionEnter(Collision collision)
    {
        neck.isCollision = true;
    }
    private void OnCollisionStay(Collision collision)
    {
        neck.isCollision = true;
    }



}
