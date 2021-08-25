using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Code © Bijan Pourmand
 * Authored 8/22/21
 * Script for Player neck movement
 */

public class Dino_Neck : MonoBehaviour
{
    Transform armature;
    Transform n1, n2, n3, n4, n5, n6;
    Transform[] neck;

    float mouseX, mouseY;
    float mX, mY;
    protected float xAngleMin = -14;
    protected float xAngleMax = 14;
    protected float yAngleMin = -15;
    protected float yAngleMax = 15;
    public float neckSpeed = 50;
    

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
    }


    private void Update()
    {
        //1. Get mouse input
        mouseX = Input.GetAxis("Mouse X");
        mouseY = Input.GetAxis("Mouse Y");
        mX = Mathf.Clamp(mX + mouseX * Time.deltaTime * neckSpeed, xAngleMin, xAngleMax);
        mY = Mathf.Clamp(mY + mouseY * Time.deltaTime * neckSpeed, yAngleMin, yAngleMax);

        foreach (Transform t in neck)
        {
            if (t != n1)
            {
                t.localEulerAngles = new Vector3(mX, 0, -mY);
            }
            else t.localEulerAngles = new Vector3(mX, 0, -mY) + new Vector3(90, 0, 0);

            //t.rotation = Quaternion.Slerp(t.rotation, Quaternion.Euler(new Vector3(mX, 0, -mY)), 0.1f);

        }
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
            if (t == n1) t.localEulerAngles = new Vector3(90,0,0);

            //2. Else, set rotation to 0
            else t.localEulerAngles = Vector3.zero;
        }
    }
}
