using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyManager : MonoBehaviour
{
    GameObject[] enemies;
    int _difficulty;

    public int Difficulty
    {
        set { _difficulty = value; }
    }
    
    void CreateEnemyArray()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
    }

    void SetEnemyVisionSize()
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

    public void StartSetDifficulty()
    {
        StartCoroutine(SetDifficulty());
    }

    IEnumerator SetDifficulty()
    {
        yield return new WaitUntil(LevelSceneActive);
        Debug.Log("Waited");
        CreateEnemyArray();
        SetEnemyVisionSize();
        Debug.Log("Enemies set");
    }

    private bool LevelSceneActive()
    {
        return SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Level");
    }
}
