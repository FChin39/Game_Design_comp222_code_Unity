using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Perception_asteroid : MonoBehaviour
{

    public GameObject asteroid_ai;
    private float timerStateChange; // timer for state change
    private object[] message = new object[2];
    public Rigidbody crystal; // crystal created when broken


    // Update is called once per frame
    void Update()
    {

        timerStateChange -= Time.deltaTime;
    }


    private void OnTriggerEnter(Collider other)
    {

        if (timerStateChange <= 0) {
            // send messge to ai asteroid
            if (other.gameObject.tag == "bullet")
            {
                Vector3 closetPoint = other.bounds.ClosestPoint(transform.position);
                message[0] = closetPoint;
                message[1] = "bullet";
                asteroid_ai.SendMessageUpwards("handlePerceptionCollision", message);
                timerStateChange = 1f;

            }

            if (other.gameObject.tag == "Player")
            {
                message[1] = "Player";
                asteroid_ai.SendMessageUpwards("handlePerceptionCollision", message);
                timerStateChange = 0.3f;

            }
        }
    }
}
