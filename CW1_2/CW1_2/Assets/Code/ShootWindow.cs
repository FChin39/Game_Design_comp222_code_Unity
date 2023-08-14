using UnityEngine;
using System.Collections;

public class ShootWindow : MonoBehaviour
{
    public AudioClip shoot; // The audio clip of shooting
    public AudioSource sound; // The audio source of shooting
    public Rigidbody bullet; // The rigidbody of bullet
    public Transform shootWindow; // The transform of the shoot window
    private float bspeed = 30f; // the speed of the bullet
    private float timerShoot; // // Timer for users to vaildly operate the spaceship to shoot

    void Start()
    {
        // initialization
        timerShoot = 5f;
        sound.clip = shoot;

    }

    void Update()
    {
        timerShoot -= Time.deltaTime;
        if (timerShoot <= 0)
        {
            // shoot operation
            if (Input.GetKeyDown(KeyCode.J)|| Input.GetKeyDown(KeyCode.Space))
            {
                sound.Play();
                Rigidbody clone;
                // clone a bullet and initialize the speed
                clone = (Rigidbody)Instantiate(bullet, shootWindow.position, shootWindow.rotation);
                clone.velocity = transform.TransformDirection(Vector3.forward * bspeed);
                timerShoot = 0.1f;
            }
        }
    }
    
}