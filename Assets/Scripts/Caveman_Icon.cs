using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Code © Bijan Pourmand
 * Authored 8/28/21
 * Script for cavemen's emote icon
 */

public class Caveman_Icon : MonoBehaviour
{
    Caveman caveman;
    Transform focus;
    Animator anim;
    public static float offset = 3;

    private void Awake()
    {
        caveman = GetComponentInParent<Caveman>();
        focus = caveman.transform.GetChild(0).GetChild(0).Find("Head");
        anim = GetComponent<Animator>();

    }

    private void Update()
    {
        transform.LookAt(Camera.main.transform);
        transform.position = focus.transform.position + new Vector3(0, offset);
    }

    //SetIcon is called by the caveman script to set icon
    public void SetIcon(string icon)
    {
        anim.SetTrigger(icon);
    }
}
