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
    Dino_Neck neck;
    Dino_Bite bite;
    GameObject grabbed = null;

    public float h, v;
    public Vector3 moveDir;
    Vector3 turnDir;
    [SerializeField]
    float speed = 35f;
    float turnSpeed = 70f;

    bool isStrafe = false;
    bool isBite = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        neck = GetComponent<Dino_Neck>();
        bite = GetComponentInChildren<Dino_Bite>();
    }

    // Update is called once per frame
    void Update()
    {
        //1. Get input
        //1a. Horizontal/Vertical
        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");
        //1b. Strafe
        if (Input.GetButtonDown("Strafe")) isStrafe = true;
        if (Input.GetButtonUp("Strafe")) isStrafe = false;
        //1c. Biting
        if (Input.GetButtonDown("Bite"))
        {
            anim.SetTrigger("BiteDown");
            isBite = true;
        }
        if (Input.GetButtonUp("Bite"))
        {
            anim.SetTrigger("BiteUp");
            isBite = false;
        }
        //1d. Neck reset
        if (Input.GetButton("ResetNeck")) neck.ResetNeck();

        //2. Set moveDir & turnDir
        moveDir = transform.forward * v * speed;
        if (isStrafe) moveDir += transform.right * h * speed;
        moveDir.y = rb.velocity.y;
        turnDir = new Vector3(0, h * turnSpeed, 0);
    }

    private void FixedUpdate()
    {
        rb.velocity = moveDir;
        if(!isStrafe)rb.MoveRotation(Quaternion.Euler(transform.eulerAngles + turnDir * Time.fixedDeltaTime));
    }

    //BiteObj is called to hold onto a given object
    public void BiteObj(GameObject obj)
    {
        //1. Set obj to grabbed
        grabbed = obj;
    }
}
