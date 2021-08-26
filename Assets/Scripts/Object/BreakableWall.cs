using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Code © Bijan Pourmand
 * Authored 8/25/21
 * Script for breakable walls, extends from Destructable
 */

public class BreakableWall : Destructable
{
    //Vars & Refs
    protected Material material;

    protected override void HitEvent(Collision collision)
    {
        //1. Check head velocity
        Dino_HeadCollision head = collision.gameObject.transform.GetComponent<Dino_HeadCollision>();
        if (head != null && head.velocityFloat >= velToBreak) HP--;
    }
}
