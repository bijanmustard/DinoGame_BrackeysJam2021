using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeckTest : MonoBehaviour
{
    Transform armature;
    Transform n1, n2, n3, n4, n5, n6;
    Transform[] neck;
    Dino_NeckJoint[] nJoints;
    Rigidbody[] rbs;

    float sensitivity = 0.5f;
    float mouseX, mouseY;
    public float mX, mY;
    public static float xAngleMin = -14;
    public static float xAngleMax = 14;
    public static float yAngleMin = -15;
    public static float yAngleMax = 15;
    public float neckSpeed = 50;

    Quaternion rotTo;

    private void Awake()
    {
        armature = transform.Find("Mesh").Find("Armature");
        n1 = armature.GetChild(0);
        n2 = n1.GetChild(0);
        n3 = n2.GetChild(0);
        n4 = n3.GetChild(0);
        n5 = n4.GetChild(0);
        n6 = n5.GetChild(0);
        neck = new Transform[] { n1, n2, n3, n4, n5, n6 };
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
        mX = Mathf.Clamp(mX + mouseX * Time.deltaTime * neckSpeed * sensitivity, xAngleMin, xAngleMax);
        mY = Mathf.Clamp(mY + mouseY * Time.deltaTime * neckSpeed * sensitivity, yAngleMin, yAngleMax);
        foreach (Transform t in neck)
        {
    
            rotTo = Quaternion.identity * Quaternion.Euler(mX, 0, -mY);
            if (t == n1) rotTo = rotTo * Quaternion.Euler(90, 0, 0);
      
            t.localRotation = Quaternion.Slerp(t.localRotation, rotTo, 5f * Time.deltaTime);
        }

    }

    private void FixedUpdate()
    {
        ////1. Set rotTo to current WORLD location offset by mouse 
        //rotTo = nJoints[0].transform.rotation * Quaternion.Euler(mouseX, 0, -mouseY);
        ////2. Start rotation chain
        //nJoints[0].SetVelocity(new Vector3(mouseX, 0, -mouseY) * neckSpeed);
    }

    //ResetNeck resets neck to its default position.
    public void ResetNeck()
    {
        Debug.Log("Neck reset");
        mX = 0;
        mY = 0;
        foreach (Transform t in neck)
        {
            // 1. If t is first neck piece, set to its unique default
            if (t == n1) t.localEulerAngles = new Vector3(90, 0, 0);

            //2. Else, set rotation to 0
            else t.localEulerAngles = Vector3.zero;
        }
    }
}
