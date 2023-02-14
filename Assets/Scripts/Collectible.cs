using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    public int healthRestored;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            GameObject.Find("Character").GetComponent<CharacterStats>().Heal(healthRestored);
            Destroy(gameObject);
        }
    }
}
