using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spe : MonoBehaviour
{
    public Rigidbody body;
    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        body.AddForce(new Vector3(h,0, v));
    }
}
