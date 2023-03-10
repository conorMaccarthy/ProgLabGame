using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    EnemyManager enemyManager;

    void OnEnable()
    {
        enemyManager = GameObject.Find("EnemyManager").GetComponent<EnemyManager>();
        enemyManager.CreateEnemyArray();
        enemyManager.SetEnemyVisionSize();
    }
}
