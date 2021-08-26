using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Code © Bijan Pourmand
 * Authored 8/25/21
 * Script for DebugUI
 */

public class DBGUI_HeadVel : DBGUI
{
    Dino_HeadCollision head;

    private void Start()
    {
        head = FindObjectOfType<Dino_HeadCollision>();
    }
    private void Update()
    {
        myText.text = string.Format("Head Velocity: {0}",head.velocityFloat);
    }
}
