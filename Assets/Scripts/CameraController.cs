using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Code © Bijan Pourmand
 * Authored 8/25/21
 * Script for camera controller
 */

public class CameraController : MonoBehaviour
{
    //Vars & Refs
    Dino_HeadCollision head;
    Dino_Neck neck;

    Vector3 basePos_playerLocal = new Vector3(0, 2.39f, -2.59f);
    Vector3 basePos_thirdPerson = new Vector3(0, 6.15f, -10.07f);
    Vector3 baseRot_thirdPerson = new Vector3(10, 0, 0);
    Vector3 moveTo;
    float transform_minX = -3.5f;
    float transform_maxX = 3.5f;
    float transform_minY;
    float transform_maxY;
    float transform_xOff;
    float transform_yOff;
    float rotation_xOff;
    float rotation_yOff;
    float camSpeed = 20f;
    public Vector2 dist;

    float mouseX, mouseY;

    private void Awake()
    {
        head = FindObjectOfType<Dino_HeadCollision>();
        neck = transform.parent.GetComponentInChildren<Dino_Neck>();
    }

    private void Update()
    {
       
        
    }

    private void LateUpdate()
    {
        //1. Get mouseX, mouseY
        mouseX = neck.mX;
        mouseY = neck.mY;

        //1. Get moveTo pos

        moveTo = basePos_thirdPerson + new Vector3(mouseX/7.5f, -mouseY/3f);
        Vector3 rotTo = baseRot_thirdPerson + new Vector3(-mouseY * 1.5f, mouseX * 1.5f, 0);
        Quaternion local = transform.localRotation;
        Quaternion quatDest = Quaternion.Euler(baseRot_thirdPerson) * Quaternion.Euler(-mouseY * 1.5f, mouseX * 1.5f, 0);
        transform.localPosition = Vector3.Lerp(transform.localPosition, moveTo, camSpeed * Time.deltaTime);
        transform.localRotation = Quaternion.Lerp(local, quatDest, camSpeed * Time.deltaTime);
    }




}
