using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour
{
    public DifficultyOptionsEnum difficulty;
    public EnemyManager enemyManager;
    
    public enum DifficultyOptionsEnum
    {
        Easy = 0,
        Normal = 1,
        Hard = 3
    }

    public void StartGame()
    {
        enemyManager.Difficulty = (int)difficulty;
        Debug.Log(difficulty);
        enemyManager.StartSetDifficulty();
        DontDestroyOnLoad(enemyManager.gameObject);
        SceneManager.LoadScene("Level");
        Debug.Log("Scene loaded");
    }
}
