using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // The transform of the spaceship
    private Vector3 offset; // The position offset between the spaceship and the camera
    private float timer = 3.0f;
    // Start is called before the first frame update
    void Start()
    {
        // Lock the FPS to 60
        Application.targetFrameRate = 60;
        // compute the initial offset between the spaceship and the camera
        offset = target.position - this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // update the offset between the spaceship and the camera
        this.transform.position = target.position - offset;
        // update the timer
        timer -= Time.deltaTime;

        // raise the camera
        if (timer < 0)
        {
            if (this.transform.position.y < 32.8) {
                offset.y -= (float)0.05;
            }
        }

    }
}