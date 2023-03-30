using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DataManager : MonoBehaviour
{
    public static DataManager manager;

    [SerializeField] private string fileName;
    [SerializeField] private bool useEncryption;

    private FileHandler fileHandler;
    private GameData gameData;
    private List<IDataPersistance> dataPersistanceObjects;

    private void Awake()
    {
        if (manager == null)
        {
            manager = this;
        }
        else
        {
            Debug.Log("More than 1 Data Manager in scene.");
        }
    }

    private void Start()
    {
        fileHandler = new FileHandler(Application.persistentDataPath, fileName, useEncryption);
        dataPersistanceObjects = FindAllDataPersistanceObjects();
        LoadGame();
    }

    public void SaveGame()
    {
        foreach (IDataPersistance dataPersistanceObj in dataPersistanceObjects)
        {
            dataPersistanceObj.SaveData(ref gameData);
        }
        
        fileHandler.Save(gameData);
        Debug.Log("Saved position = " + gameData.position);
    }

    public void LoadGame()
    {
        gameData = fileHandler.Load();

        if (gameData == null)
        {
            Debug.Log("No saved data found. Loading new game.");
            NewGame();
        }

        foreach (IDataPersistance dataPersistanceObj in dataPersistanceObjects)
        {
            dataPersistanceObj.LoadData(gameData);
        }

        Debug.Log("Data has been loaded successfully");
    }

    public void NewGame()
    {
        gameData = new GameData();
    }

    private void OnApplicationQuit()
    {
        SaveGame();
    }

    private List<IDataPersistance> FindAllDataPersistanceObjects()
    {
        IEnumerable<IDataPersistance> dataPersistanceObjects = FindObjectsOfType<MonoBehaviour>().OfType<IDataPersistance>();

        return new List<IDataPersistance>(dataPersistanceObjects);
    }
}
