using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

/*
 * Code © Bijan Pourmand
 * Authored 8/25/21
 * Base script for destructable objects
 */

public class Destructable : MonoBehaviour
{
    public Rigidbody rb;
    protected Collider[] mainCols;
    public Rigidbody[] pieces;
    public Collider[] pieceCols;

    [SerializeField]
    protected float velToBreak = -1;
    [SerializeField]
    protected int HP = 0;

    bool isDestroyed = false;


    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody>();
        mainCols = GetComponents<Collider>();
        pieces = transform.GetComponentsInChildren<Rigidbody>().Where(r => r != rb).ToArray();
        pieceCols = transform.GetComponentsInChildren<Collider>().Where(c => !mainCols.Contains(c)).ToArray();
    }

    private void Start()
    {
        foreach (Collider c in pieceCols) 
            foreach(Collider mc in mainCols) Physics.IgnoreCollision(mc, c);
        ToggleCollapse(false);
    }

    //Collapse is called to enable physics
    protected void ToggleCollapse(bool tog)
    {
        isDestroyed = tog;
        foreach (Rigidbody r in pieces)
        {
            r.isKinematic = !tog;
            if (r.GetComponent<MeshCollider>() != null && r.GetComponent<MeshCollider>().convex == false)
            {
                r.GetComponent<MeshCollider>().convex = true;
            }
        }
        foreach(Collider c in mainCols) c.enabled = !tog;
        if(tog)foreach (Rigidbody r in pieces) r.transform.parent = null;
        if (tog && rb != null) Destroy(gameObject);
        else if (!tog && rb == null) rb = gameObject.AddComponent<Rigidbody>();
    }

    //HitEvent is called when a valid collision hits the object.
    protected virtual void HitEvent(Collision collision)
    {
        //1. If head velocity > velocity needed to break, subtract hit
        Dino_HeadCollision head = collision.gameObject.GetComponentInChildren<Dino_HeadCollision>();
        if (head != null && head.velocityFloat >= velToBreak)
        {
            HP--;
        }
    }

    protected virtual void OnCollisionEnter(Collision collision)
    {
        Debug.Log(name + " hit " + collision.gameObject.name);
        //1. If player collides, toggle collapse
        if(collision.gameObject.tag == "Player")
        {
            if (!isDestroyed) HitEvent(collision);
            if (HP <= 0) ToggleCollapse(true);
        }
    }
}
