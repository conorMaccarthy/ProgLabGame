using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    GameObject[] enemies;
    int _difficulty;

    public void SetDifficulty(int difficulty)
    {
        _difficulty = difficulty;
    }
    
    public void CreateEnemyArray()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
    }

    public void SetEnemyVisionSize()
    {
        switch (_difficulty)
        {
            case 0:
                foreach (GameObject enemy in enemies)
                {
                    if (enemy.GetComponent<SphereCollider>() != null)
                    {
                        enemy.GetComponent<SphereCollider>().radius = 6;
                    }
                }
                break;
            case 1:
                foreach (GameObject enemy in enemies)
                {
                    if (enemy.GetComponent<SphereCollider>() != null)
                    {
                        enemy.GetComponent<SphereCollider>().radius = 8;
                    }
                }
                break;
            case 2:
                foreach (GameObject enemy in enemies)
                {
                    if (enemy.GetComponent<SphereCollider>() != null)
                    {
                        enemy.GetComponent<SphereCollider>().radius = 11;
                    }
                }
                break;
        }
    }
}
