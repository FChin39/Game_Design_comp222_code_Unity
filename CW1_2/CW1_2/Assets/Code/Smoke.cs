using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Smoke : MonoBehaviour
{
    private float speed = 1;
    private Rigidbody rb;
    private Vector3 lastDir;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = new Vector3(1, 0, 1) * speed;
    }

    private void LateUpdate()
    {
        lastDir = rb.velocity;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "wall")
        {
            Vector3 reflexAngle = Vector3.Reflect(lastDir, collision.contacts[0].normal);
            rb.velocity = reflexAngle.normalized * lastDir.magnitude;
        }
    }
}
