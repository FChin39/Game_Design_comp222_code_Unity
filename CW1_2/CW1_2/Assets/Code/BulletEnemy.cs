using UnityEngine;

public class BulletEnemy : MonoBehaviour
{
    public Transform target; // The transform of the spaceship
    private float bulletTimer = 8f; // Timer for bullets to destroy themselves.
    private float speed = 3f; // bullet speed

    // Update is called once per frame
    void Update()
    {
        // update the timer and destroy bullets whose timers are ended.
        bulletTimer -= Time.deltaTime;
        if (bulletTimer <= 0)
        {
            Destroy(this.gameObject);
        }
        // move to the position of the player
        target = GameObject.FindGameObjectWithTag("Player").transform;
        float step = speed * Time.deltaTime;
        gameObject.transform.localPosition = Vector3.MoveTowards(this.transform.position, target.position, step);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "asteroid" || collision.gameObject.tag == "bullet")
        {
            
            Destroy(this.gameObject);
        }
            

    }
}
