using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    public Vector3 rotAxis = Vector3.up;
    public float speed = 20f;
    Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        //transform.Rotate(rotAxis * speed * Time.fixedDeltaTime);
        rb.AddTorque(rotAxis * speed * 100 * Time.fixedDeltaTime, ForceMode.Force);
    }
}
