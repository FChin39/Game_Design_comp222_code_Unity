using UnityEngine;

public class PlaneIn : MonoBehaviour
{
    // destroy the plane when the spaceship cross it
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player") {
            Destroy(this.gameObject);
        }
    }

}

