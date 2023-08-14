using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// the perception system of the enemy
public class Perception_enemy : MonoBehaviour
{
    public GameObject enemy; // enemy
    private float timerPlayerEnter = 0; // timer for send message
    private object[] message = new object[1]; // message to send to the enemy class

    // Update is called once per frame
    void Update()
    {
        timerPlayerEnter -= Time.deltaTime;
    }


    private void OnTriggerStay(Collider other)
    {
        // tell enemy that the player has entered into the shooting area
        if (timerPlayerEnter <= 0) {
            if (other.gameObject.tag == "Player")
            {
                message[0] = "Player";
                enemy.SendMessageUpwards("handlePlayerEnter", message);
                timerPlayerEnter = 0.1f;
            }
        }
    }
}
