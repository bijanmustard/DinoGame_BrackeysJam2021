using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Code © Bijan Pourmand
 * Authored 8/23/21
 * Script handing bite move
 */

public class Dino_Bite : MonoBehaviour
{
    //Vars & Refs
    Dino_Move pMove;
    Dino_HeadCollision headCol;
    public List<GameObject> grabbableObjs = new List<GameObject>();
    public GameObject nextToGrab = null;
    public GameObject grabbed = null;
    protected Transform grabbed_parent = null;
    bool canGrab = true;
  
    

    private void Awake()
    {
        pMove = GetComponentInParent<Dino_Move>();
        headCol = transform.parent.GetComponentInChildren<Dino_HeadCollision>();
     
    }

    private void Update()
    {
        //Scrub array for null refs
        foreach (GameObject obj in grabbableObjs) if (obj == null) grabbableObjs.Remove(obj);
            //1. Set next to grab based on which is closest to the radius
            if (grabbableObjs.Count < 1) nextToGrab = null;
        else nextToGrab = GetNextToGrab();
        
        //2. Get Input
        if (canGrab)
        {
            //2a. If bite button down and grabbable obj is nearby, grab obj
            if (Input.GetButtonDown("Bite"))
            {
                if (nextToGrab != null && grabbed == null) GrabObj(nextToGrab); 
            }
            //2b. else if bite button up and obj is grabbed, release obj
            else if (Input.GetButtonUp("Bite")){
                if (grabbed != null) ReleaseGrabbed();
            }

        }



    }

    //SetNextToGrab is called to set nextToGrab.
    GameObject GetNextToGrab()
    {
        
        
            GameObject closest = null;
            foreach (GameObject gObj in grabbableObjs)
            {
                if (closest == null) closest = gObj;
                else
                {
                    if (Vector3.Distance(gObj.transform.position, transform.position)
                        < Vector3.Distance(closest.transform.position, transform.position)) closest = gObj;
                }
            }
            return closest;
        
    }

    //GrabObj is called to grab a grabbable obj.
    protected void GrabObj(GameObject obj)
    {
        //1. Set grabbed ref
        grabbed = obj;

        //2. If rigidbody is present, lock constraints
        if (obj.tag == "Caveman")
        {
            Caveman cMan = obj.GetComponentInParent<Caveman>();
            cMan.ToggleRagdoll(true); 
        }
        Rigidbody rb = grabbed.GetComponent<Rigidbody>();
        if(rb != null)
        {
            rb.constraints = RigidbodyConstraints.FreezeRotation;
            rb.isKinematic = true;
        }
        //3. Parent to bite
        grabbed_parent = grabbed.transform.parent;
        grabbed.transform.parent = transform;

    }

    //ReleaseObj is called to release the current grabbed obj.
    public void ReleaseGrabbed()
    {
        //1. De-parent grabbed
        grabbed.transform.parent = grabbed_parent;
        grabbed_parent = null;
        //2. If rigidbody, release constraints
        Rigidbody rb = grabbed.GetComponent<Rigidbody>();
        if(rb != null)
        {
            foreach (Dino_HeadCollision dhc in FindObjectsOfType<Dino_HeadCollision>())
            {
                Physics.IgnoreCollision(dhc.GetComponent<Collider>(), grabbed.GetComponent<Collider>());
            }
            rb.constraints = RigidbodyConstraints.None;
            rb.isKinematic = false;
            //2a. Add force
            float force = PhysLib.GetForce(headCol.rb.mass, headCol.Acceleration) / Dino_HeadCollision.forceDivisor;
            force = Mathf.Clamp(force, 0, 45);

            rb.AddForce(force * headCol.vel.normalized, ForceMode.Impulse);
            rb.AddTorque(force * headCol.vel.normalized, ForceMode.Impulse);
          
        }
        //3. Set grabbed to null
        grabbed = null;
    }

    private void OnTriggerEnter(Collider other)
    {
        //if object is grabbable and canGrab...
        Rigidbody rb = other.gameObject.GetComponent<Rigidbody>();
        if (rb != null)
        {
            //.. add to grabbable obj list
            if (!grabbableObjs.Contains(other.gameObject)) grabbableObjs.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //If obj is in grabbable list, remove list
        Rigidbody rb = other.gameObject.GetComponent<Rigidbody>();
        if(rb != null)
        {
            if (grabbableObjs.Contains(other.gameObject)) grabbableObjs.Remove(other.gameObject);
        }
    }

}
