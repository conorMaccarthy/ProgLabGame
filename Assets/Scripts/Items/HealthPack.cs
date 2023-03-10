using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPack : MonoBehaviour
{
    Collectible healthPack;

    void Awake()
    {
        healthPack = new Collectible("healthPack", 25, 0);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            healthPack.RestoreHealth();
            Destroy(gameObject);
        }
    }
}
