using UnityEngine;
using System.Collections;

public class ShootWindowEnemy : MonoBehaviour
{
    public Rigidbody bullet; // The rigidbody of bullet
    public Transform shootWindow; // The transform of the shoot window

    public void Shoot()
    {
        Rigidbody clone;
        // clone a bullet and initialize the speed
        clone = (Rigidbody)Instantiate(bullet, shootWindow.position, shootWindow.rotation);
    }
    
}

