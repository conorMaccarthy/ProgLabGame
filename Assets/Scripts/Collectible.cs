using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    public int score;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            ScoreManager.scoreManager.UpdateScore(score);
            Destroy(gameObject);
        }
    }
}
