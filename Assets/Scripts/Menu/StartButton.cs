using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour
{
    public DifficultyOptionsEnum difficulty;
    public GameObject enemyManager;
    
    public enum DifficultyOptionsEnum
    {
        Easy = 0,
        Normal = 1,
        Hard = 3
    }

    public void StartGame()
    {
        enemyManager.GetComponent<EnemyManager>().SetDifficulty((int)difficulty);
        DontDestroyOnLoad(enemyManager);
        SceneManager.LoadScene("Level");
    }

    public void SetDifficulty()
    {
        //enemyManager.CreateEnemyArray();
        //enemyManager.SetEnemyVisionSize();
    }
}
