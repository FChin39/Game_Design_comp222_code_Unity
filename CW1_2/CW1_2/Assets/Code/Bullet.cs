using UnityEngine;

public class Bullet : MonoBehaviour
{

    private float bulletTimer = 3f; // Timer for bullets to destroy itself.

    // Update is called once per frame
    void Update()
    {
        // update the timer and destroy bullets whose timers are ended.
        bulletTimer -= Time.deltaTime;
        if (bulletTimer <= 0)
        {
            Destroy(this.gameObject);
        }

    }
    private void OnCollisionEnter(Collision collision)
    {
        // If the bullet hits walls, it will destory itself.
        if (!((collision.gameObject.tag == "Player")||(collision.gameObject.tag == "PlanePlaying"))) {
            
            Destroy(this.gameObject);
        }
            

    }
}
