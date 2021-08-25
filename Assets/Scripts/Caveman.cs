using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Code © Bijan Pourmand
 * Authored 8/24/21
 * Script for Basic Cavemen who run away
 */
public class Caveman : MonoBehaviour
{
    //Vars & Refs
    Rigidbody rb;
    Dino_Move player;
    Collider capsule;

    public Vector3 moveDir;
    float speed = 500f;
    float mass = 20f;
    public float distToPlayer;
    float triggerDist = 30f;
    public bool isRunning = false;
    protected bool isRagdoll = false;

    private void Awake()
    {
        //1. Set refs
        rb = GetComponent<Rigidbody>();
        player = FindObjectOfType<Dino_Move>();
        capsule = GetComponent<Collider>();
        // 2. Set capsule to ignore collision with ragdoll colliders
        foreach(Collider col in transform.GetChild(0).GetComponentsInChildren<Collider>())
        {
            Physics.IgnoreCollision(capsule, col);
        }
        //3. Ignore capsule to ignore collision with bite radius
        Physics.IgnoreCollision(capsule, FindObjectOfType<Dino_Bite>().GetComponent<Collider>());

        ToggleRagdoll(false);
    }

    private void Update()
    {
        //1. Get distance from player
        distToPlayer = Vector3.Distance(transform.position, player.transform.position);
        //2. If caveman is within range, run away from player
        if (isRagdoll) isRunning = false;
        else
        {
            if (distToPlayer <= triggerDist)
            {
                isRunning = true;
            }
            else isRunning = false;
        }

        if (Input.GetKeyDown(KeyCode.R)) ToggleRagdoll(true);

    }

    private void FixedUpdate()
    {
        if (isRunning)
        {
            moveDir = (transform.position - player.transform.position).normalized;
            moveDir.y = 0;
            Debug.DrawRay(transform.position, moveDir * 10f, Color.red);
            transform.forward = moveDir;
            //rb.MovePosition(transform.position + moveDir * speed * Time.fixedDeltaTime);
            rb.velocity = transform.forward * speed * Time.fixedDeltaTime;

        }
    }

    //ToggleRagdoll is called to toggle the ragdoll physics.
    public void ToggleRagdoll(bool tog)
    {
        //1. Toggle main capsule collider
        GetComponent<Collider>().enabled = !tog;
        //1.5. Toggle capsule rigidbody
        if (tog && rb != null) Destroy(rb);
        else if(!tog && rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody>();
            rb.mass = mass;
        }
        //2. Toggle isKinematic for each ragdoll collider
        foreach (Rigidbody r in transform.GetChild(0).GetComponentsInChildren<Rigidbody>())
        {
            r.isKinematic = !tog;
        }
    
        isRagdoll = tog;
        Debug.Log("Ragdoll is " + isRagdoll);

    }


}
