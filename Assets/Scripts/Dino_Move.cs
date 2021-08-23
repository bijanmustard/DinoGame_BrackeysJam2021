using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Code © Bijan Pourmand
 * Authored 8/22/21
 * Script for Player movement.
 */


public class Dino_Move : MonoBehaviour
{

    //Vars & Refs
    Rigidbody rb;
    Animator anim;

    public float h, v;
    public Vector3 moveDir;
    Vector3 turnDir;
    float speed = 50f;
    float turnSpeed = 100f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //1. Get input
        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");
        //2. Set moveDir & turnDir
        moveDir = transform.forward * v * speed;
        moveDir.y = rb.velocity.y;
        turnDir = new Vector3(0, h * turnSpeed, 0);
        //3. Move player
        if (Input.GetButtonDown("Bite"))
        {
            //anim.SetBool("IsBite", true);
            anim.SetTrigger("BiteDown");
        }
        else if (Input.GetButtonUp("Bite"))
        {
            //anim.SetBool("IsBite", false);
            anim.SetTrigger("BiteUp");
        }


    }

    private void FixedUpdate()
    {
        rb.velocity = moveDir;
        rb.MoveRotation(Quaternion.Euler(transform.eulerAngles + turnDir * Time.fixedDeltaTime));
    }
}
