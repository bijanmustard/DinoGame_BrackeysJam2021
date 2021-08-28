using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Code © Bijan Pourmand
 * Authored 8/27/21
 * Script for catapult object
 */

public class Catapult : Destructable
{
    protected GameObject mesh;
    Rigidbody arm;
    Rigidbody projectile;
    Transform ballistic_pt;
    Animator anim;

    [SerializeField]
    float launchForce = 2000f;

    protected override void Awake()
    {
        base.Awake();
        mesh = transform.Find("Catapult_Mesh").gameObject;
        arm = mesh.transform.Find("Arm").GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        arm.centerOfMass = transform.TransformPoint(arm.transform.localPosition);
        ballistic_pt = arm.transform.GetChild(0);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.B)) LoadProjectile(GameObject.Find("Ball"));
        if (Input.GetKeyDown(KeyCode.Mouse0)) anim.SetTrigger("Launch");
    }

    //LoadProjectile is called to load a projectile
    public void LoadProjectile(GameObject gObj)
    {
        Rigidbody rb = gObj.GetComponent<Rigidbody>();
        if(rb != null)
        {
            projectile = rb;
            projectile.isKinematic = true;
            gObj.transform.parent = ballistic_pt;
            gObj.transform.localPosition = Vector3.zero;
            gObj.transform.rotation = ballistic_pt.rotation;
            
        }
    }

    //Launch

    //FireProjectile is called to fire the loaded projectile.
    public void FireProjectile()
    {
        //1. If a projectile is loaded...
        if(projectile != null)
        {
            //2. Detach projectile and launch forward
            projectile.transform.parent = null;
            projectile.isKinematic = false;
            foreach(Collider mc in mainCols) Physics.IgnoreCollision(projectile.GetComponent<Collider>(), mc);
            projectile.AddForce(projectile.transform.up * launchForce, ForceMode.Force);
            anim.SetTrigger("Retract");
        }
    }

 
}
