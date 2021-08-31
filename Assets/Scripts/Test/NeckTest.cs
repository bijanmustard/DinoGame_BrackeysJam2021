using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeckTest : Dino_Neck
{
    //Transform armature;
    //Transform n1, n2, n3, n4, n5, n6;
    Transform n7;
    //Transform[] neck;
    Dino_NeckJoint[] nJoints;
    //Rigidbody[] rbs;

    //float sensitivity = 0.5f;
    //float mouseX, mouseY;


    private void Awake()
    {
        armature = transform.Find("Mesh").Find("Armature");

        n1 = armature.GetChild(0);
        n2 = n1.GetChild(0);
        n3 = n2.GetChild(0);
        n4 = n3.GetChild(0);
        n5 = n4.GetChild(0);
        n6 = n5.GetChild(0);
        n7 = n6.GetChild(0);
        neck = new Transform[] { n1, n2, n3, n4, n5, n6, n7 };
        nJoints = GetComponentsInChildren<Dino_NeckJoint>();
        rbs = new Rigidbody[]{ n1.GetComponent<Rigidbody>(), n2.GetComponent<Rigidbody>(), n3.GetComponent<Rigidbody>(),
        n4.GetComponent<Rigidbody>(), n5.GetComponent<Rigidbody>(), n6.GetComponent<Rigidbody>()};

        foreach (Rigidbody r1 in rbs)
        {
            foreach (Rigidbody r2 in rbs)
            {
                Physics.IgnoreCollision(r1.GetComponent<Collider>(), r2.GetComponent<Collider>());
            }
        }
    }


    private void Update()
    {
        //1. Get mouse input
        mouseX = Input.GetAxis("Mouse X");
        mouseY = Input.GetAxis("Mouse Y");
        mX += mouseX * neckSpeed * sensitivity * Time.deltaTime;
        mY += mouseY * neckSpeed * sensitivity * Time.deltaTime;
        mX = Mathf.Clamp(mX, xAngleMin, xAngleMax);
        mY = Mathf.Clamp(mY, yAngleMin, yAngleMax);

        if (isCollision)
        {
            Debug.Log("Neck collide");
            isCollision = false;
        }

        foreach (Transform t in neck)
        {

            rotTo = Quaternion.identity * Quaternion.Euler(mX, 0, -mY);
            if (t == n1) rotTo = rotTo * Quaternion.Euler(90, 0, 0);

            t.localRotation = Quaternion.Slerp(t.localRotation, rotTo, 5f * Time.deltaTime);
        }




    }

    private void FixedUpdate()
    {
  

    }


}
