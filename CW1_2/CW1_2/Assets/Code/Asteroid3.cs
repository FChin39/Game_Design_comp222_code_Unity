using UnityEngine;

public class Asteroid3 : MonoBehaviour
{
    public Rigidbody crystal; // crystal created when broken
    private int health = 1; // the health of the asteroid, which is the number of times can be shot by bullets.
    private Rigidbody rb; // the rigibody of the current asteroid
    private Vector3 lastDir; // the velocity of the asteroid


    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }


    private void LateUpdate()
    {
        lastDir = rb.velocity; // get the velocity of the asteroid
    }

    private void OnCollisionEnter(Collision collision)
    {
        // when shot by bullets, the health of the asteroid will decrease.
        // If the health has decreased to 0,the asteroid will be destoried. 
        if ((collision.gameObject.tag == "bullet" || collision.gameObject.tag == "enemyBullet") && (--health <= 0))
        {
            Rigidbody clone;
            clone = (Rigidbody)Instantiate(crystal, rb.position, new Quaternion(0, 0, 0, 0));
            Destroy(this.gameObject);
        }

        // Rebound when colliding with walls or other asteroids 
        if (collision.gameObject.tag == "wall" || collision.gameObject.tag == "asteriod")
        {
            Vector3 reflexAngle = Vector3.Reflect(lastDir, collision.contacts[0].normal);
            rb.velocity = reflexAngle.normalized * lastDir.magnitude;
        }
    }
}