using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    public Vector3 rotAxis = Vector3.up;
    public float speed = 20f;
    private void Update()
    {
        transform.Rotate(rotAxis * speed * Time.fixedDeltaTime);
    }
}
