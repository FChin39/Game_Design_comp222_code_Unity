using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public Transform target; // The transform of the spaceship
    private float timer = 4f; // timer for enemy to activate
    private float shootingTimer = 0.1f; // timer for enemy to keep shoot mode
    private float speed = 1.5f; // the initial speed of the enemy
    private float attackTimer = 3.0f; // the attack timer for the enemy
    private float attackInterval = 3.0f; // the attack interval of the enemy
    private float asteroidTimer = 0; // the timer for the enemy to be hitted by asteroids
    private int health = 25; // the health of the enemy, which is the number of times can be shot by bullets.
    private Rigidbody rb; // the rigibody of the enemy
    public Rigidbody crystal; // crystal created when broken
    private int shootingFlag = 0; // the mode of the enemy: flag 1 -  attack mode; 
    Material material; // the material of the enemy
    public GameObject shootWindow; // the shoot window of the enemy
    public Text healthText; // the health display text

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        material = new Material(Shader.Find("Transparent/Diffuse"));
    }

    public void handlePlayerEnter(object[] message)
    {
        shootingTimer = 0.3f;
        shootingFlag = 1;
    }

    // Update is called once per frame
    void Update()
    {
        healthText.text = "enemy health\n" + health + " / 25";

        if (health<10) {
            speed = 1.8f;
            attackInterval = 1.2f;
            healthText.color = Color.red;
            material.color = new Color(1, 0, 0, 0.8f);
            GetComponent<Renderer>().material = material;
        }

        if (shootingTimer <= 0) {
            shootingFlag = 0;
        }

        if (shootingFlag == 1 && attackTimer <= 0) {
            shootWindow.SendMessageUpwards("Shoot");
            attackTimer = attackInterval;
        }

        if (timer <= 0) {
            float step = speed * Time.deltaTime;
            gameObject.transform.localPosition = Vector3.MoveTowards(this.transform.position, target.position, step);
        }

        // update the timer
        timer -= Time.deltaTime;
        shootingTimer -= Time.deltaTime;
        attackTimer -= Time.deltaTime;
        asteroidTimer -= Time.deltaTime; 
    }


    private void OnCollisionEnter(Collision collision)
    {
        // when shot by bullets, the health of the asteroid will decrease.
        // If the health has decreased to 0,the asteroid will be destoried. 


        if ((collision.gameObject.tag == "bullet" || collision.gameObject.tag == "enemyBullet"))
            health--;

        if (collision.gameObject.tag == "asteroid" && asteroidTimer<=0) {
            health -= 8;
            asteroidTimer = 0.4f;
        }
            

        if (health <= 0)
        {

            Vector3 positions = rb.position;
            Rigidbody clone;

            // Clone a crystal
            clone = (Rigidbody)Instantiate(crystal, rb.position, new Quaternion(0, 0, 0, 0));
            clone = (Rigidbody)Instantiate(crystal, new Vector3(rb.position.x-0.2f, rb.position.y, rb.position.z - 0.2f), new Quaternion(0, 0, 0, 0));
            clone = (Rigidbody)Instantiate(crystal, new Vector3(rb.position.x+0.2f, rb.position.y, rb.position.z + 0.2f), new Quaternion(0, 0, 0, 0));
            clone = (Rigidbody)Instantiate(crystal, new Vector3(rb.position.x + 0.2f, rb.position.y, rb.position.z - 0.2f), new Quaternion(0, 0, 0, 0));
            clone = (Rigidbody)Instantiate(crystal, new Vector3(rb.position.x - 0.2f, rb.position.y, rb.position.z + 0.2f), new Quaternion(0, 0, 0, 0));
            clone = (Rigidbody)Instantiate(crystal, new Vector3(rb.position.x, rb.position.y, rb.position.z + 0.2f), new Quaternion(0, 0, 0, 0));
            Destroy(healthText);
            Destroy(this.gameObject);
        }
    }


}
