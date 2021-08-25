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
    protected Rigidbody rb;
    protected Collider mainCol;
    public Rigidbody[] pieces;
    protected Collider[] pieceCols;

    [SerializeField]
    protected float velToBreak = -1;

    bool isDestroyed = false;


    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        mainCol = GetComponent<Collider>();
        pieces = transform.GetComponentsInChildren<Rigidbody>().Where(r => r != rb).ToArray();
        pieceCols = transform.GetComponentsInChildren<Collider>().Where(r => r != mainCol).ToArray();
    }

    private void Start()
    {
        foreach (Collider c in pieceCols) Physics.IgnoreCollision(mainCol, c);
        ToggleCollapse(false);
    }

    //Collapse is called to enable physics
    protected void ToggleCollapse(bool tog)
    {
        isDestroyed = tog;
        foreach (Rigidbody r in pieces) r.isKinematic = !tog;
        mainCol.enabled = !tog;
    }

    protected virtual void OnCollisionEnter(Collision collision)
    {
        //1. If player collides, toggle collapse
        if(collision.gameObject.tag == "Player")
        {
            if (!isDestroyed)
            {
                Debug.Log("Collapsing " + name +"?");
                Dino_HeadCollision head = collision.gameObject.transform.root.GetComponentInChildren<Dino_HeadCollision>();
                if (head.velocityFloat >= velToBreak)
                {
                    Debug.Log("Collapsing " + name);
                    ToggleCollapse(true);
                }
            }
        }
    }
}
