using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 *  Code © Bijan Pourmand
 *  Authored 8/22/21
 *  Script for Dino head collision
 */
public class Dino_HeadCollision : MonoBehaviour
{
    Rigidbody rb;
    Vector3 prevPos;
    public Vector3 vel;
    float velocityFloat;
    float velTime;
    float acceleration;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        //1. Set prev position variable
        prevPos = transform.position;

    }

    private void FixedUpdate()
    {
        vel = rb.velocity;
    }

    private void LateUpdate()
    {
        //2. After all move funcs have been called, set velocity
        float prevVel = velocityFloat;
        float prevTime = velTime;

        velocityFloat = PhysLib.GetVelocity(prevPos, transform.position, Time.deltaTime);
        velTime = Time.deltaTime;
        rb.velocity = velocityFloat * (transform.position - prevPos).normalized;

        //Set acceleration
        acceleration = PhysLib.GetAcceleration(prevVel, prevTime, velocityFloat, velTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Add force to collidable obj
        Rigidbody colRB = collision.gameObject.GetComponent<Rigidbody>();
        if (colRB != null)
        {
            //get direction of hit relative to 
            Vector3 hitDir = collision.gameObject.transform.position - collision.contacts[0].point;
            float force = PhysLib.GetForce(acceleration , rb.mass)/5000;
            force = Mathf.Clamp(force, 0, 45);
            Debug.Log(force);
            colRB.AddForce(hitDir * force,ForceMode.Impulse);
            //colRB.AddRelativeTorque(new Vector3(5000, 5000, 1000),ForceMode.Impulse);
        }
    }
}
