using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTest1 : MonoBehaviour
{
    Rigidbody rb;
    public float h, v;
    float mouseX, mouseY, scroll;
    float speed = 2;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");
        mouseX = Input.GetAxis("Mouse X");
        mouseY = Input.GetAxis("Mouse Y");
        scroll = Input.GetAxis("Mouse ScrollWheel");
       
    }

    private void FixedUpdate()
    {
        //rb.MovePosition(Vector3.Lerp(transform.position, transform.position += new Vector3(mouseX, mouseY, scroll) * speed,
        //   2f * Time.deltaTime));
        rb.AddForce(new Vector3(h,v, 0) * 100f, ForceMode.Force);
    }
}
