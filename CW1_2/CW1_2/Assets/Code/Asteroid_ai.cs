using UnityEngine;

public class Asteroid_ai : MonoBehaviour
{
    public Rigidbody asteroid3; // asteroids of next sizes
    public Rigidbody crystal; // crystal created when broken
    private float speed = 1.4f; // the initial velocity of the current asteroid
    private int health = 5; // the health of the asteroid, which is the number of times can be shot by bullets.
    private float nextSpeed = 4; // the initial velocity of the asteroid of next sizes
    private Rigidbody rb; // the rigibody of the current asteroid
    private Vector3 lastDir; // the velocity of the asteroid
    private int state = 0; // ai state
    public Transform target; // The transform of the spaceship
    private float chaseSpeed = 2.8f; // chase state speed
    private float stateTimer = 0; // timer for state change


    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = new Vector3(1, 0, 1) * speed;
    }

    public void handlePerceptionCollision(object[] message) {
        Material material = new Material(Shader.Find("Transparent/Diffuse"));
        state = (string)message[1] == "Player" ? 1 : 2;
        if (state == 2)
        {  // state to escape from the bullet
            material.color = Color.yellow;
            GetComponent<Renderer>().material = material;
            Vector3 closetPoint = (Vector3)message[0];
            Vector3 reflexAngle = (Vector3.Reflect(lastDir, new Vector3(closetPoint.x, 0.4f, closetPoint.z)));
            rb.velocity = 6f * reflexAngle.normalized;
        }
        else if (state == 1) {
            // state to chase the player
            material.color = Color.red;
            GetComponent<Renderer>().material = material;
            rb.velocity = 0 * new Vector3(0,0,0);
        }

        stateTimer = 4f;
    }

    private void LateUpdate()
    {
        lastDir = rb.velocity; // get the velocity of the asteroid
        stateTimer -= Time.deltaTime;
        if (state != 0) {
            if (stateTimer <= 0)
            {
                Material material = new Material(Shader.Find("Transparent/Diffuse"));
                material.color = Color.white;
                GetComponent<Renderer>().material = material;
                rb.velocity = new Vector3(-Random.Range(-1,0), 0, -Random.Range(-1, 0)) * speed;
                state = 0;
            }
        }
        if (state == 1) {
            // update the offset between the spaceship and the asteroid
            float step = chaseSpeed * Time.deltaTime;
            gameObject.transform.localPosition = Vector3.MoveTowards(this.transform.position, target.position, step);

        }
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
