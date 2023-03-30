using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class FileHandler
{
    private string _dataDirPath;
    private string _dataFileName;
    string dataFullPath;

    private bool _useEncryption = false;
    private readonly string encryptionCode = "encryptWord";

    public FileHandler(string dataDirPath, string dataFileName, bool useEncryption)
    {
        _dataDirPath = dataDirPath;
        _dataFileName = dataFileName;
        _useEncryption = useEncryption;
        dataFullPath = Path.Combine(_dataDirPath, _dataFileName);
    }

    public void Save(GameData data)
    {
        try
        {
            Directory.CreateDirectory(Path.GetDirectoryName(dataFullPath));

            string dataToStore = JsonUtility.ToJson(data, true);

            if (_useEncryption)
            {
                dataToStore = EncryptDecrypt(dataToStore);
            }

            using (FileStream stream = new FileStream(dataFullPath, FileMode.Create))
            {
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.Write(dataToStore);
                }
            }
        }
        catch(Exception e)
        {
            Debug.LogError("Error occured when trying to save data to file: " + dataFullPath + "\n" + e);
        }
    }

    public GameData Load()
    {
        GameData loadedData = null;

        if (File.Exists(dataFullPath))
        {
            try
            {
                string dataToLoad = "";

                using (FileStream stream = new FileStream(dataFullPath, FileMode.Open))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        dataToLoad = reader.ReadToEnd();
                    }
                }

                if (_useEncryption)
                {
                    dataToLoad = EncryptDecrypt(dataToLoad);
                }

                loadedData = JsonUtility.FromJson<GameData>(dataToLoad);
            }
            catch(Exception e)
            {
                Debug.LogError("Error occured when trying to load data to file: " + dataFullPath + "\n" + e);
            }
        }
        return loadedData;
    }

    private string EncryptDecrypt(string data)
    {
        string modifiedData = "";

        for (int i = 0; i < data.Length; i++)
        {
            modifiedData += (char)(data[i] ^ encryptionCode[i % encryptionCode.Length]);
        }

        return modifiedData;
    }
}
