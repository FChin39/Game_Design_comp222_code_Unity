using UnityEngine;

public class Asteroid2 : MonoBehaviour
{
    public Rigidbody asteroid3; // asteroids of next sizes
    public Rigidbody crystal; // crystal created when broken
    private int health = 3; // the health of the asteroid, which is the number of times can be shot by bullets.
    private float nextSpeed = 4; // the initial velocity of the asteroid of next sizes
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

            Vector3 positions = rb.position;
            Rigidbody clone, clone2, clone3;
            // Clone two asteroids of next size.
            clone = (Rigidbody)Instantiate(asteroid3, new Vector3(positions.x + 0.3f, positions.y + 0.4f, positions.z), rb.rotation);
            clone2 = (Rigidbody)Instantiate(asteroid3, new Vector3(positions.x - 0.4f, positions.y - 0.3f, positions.z), rb.rotation);

            // Clone a crystal
            clone3 = (Rigidbody)Instantiate(crystal, rb.position, new Quaternion(0, 0, 0, 0));

            // initilize the velocity of two asteroids generated
            clone.velocity = transform.TransformDirection(Vector3.back * nextSpeed);
            clone2.velocity = transform.TransformDirection(Vector3.left * nextSpeed);

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
