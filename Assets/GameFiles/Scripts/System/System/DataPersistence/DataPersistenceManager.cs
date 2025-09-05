using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System.Linq;

public class DataPersistence : Singleton<DataPersistence>
{
    public event Action OnAfterGameDataReset;
    
    [Header("File Stuff")]
    [SerializeField] private string fileName;
    [SerializeField] private bool useEncryption;
    
    private GameData _gameData;
    private FileDataHandler _dataHandler;
    private List<IDataPersistence> dataPersistenceObjects;
    
    private void Start()
    {
        _dataHandler = new FileDataHandler(Application.persistentDataPath, fileName, useEncryption);
        dataPersistenceObjects = FindAllDataPersistenceObjects();
        LoadGame();
    }

    private void OnApplicationQuit()
    {
        SaveGame();
    }

    public void NewGame()
    {
        _gameData = new GameData();
    }

    public void LoadGame()
    {
        _gameData = _dataHandler.Load();
        
        // create new game if gameData is null (when _dataHandler haven't found anything)
        if (_gameData == null)
        {
            Debug.Log("No game data found. Initializing new game data.");
            NewGame();
        }
        
        foreach (IDataPersistence obj in dataPersistenceObjects)
        {
            obj.LoadData(_gameData);
        }
    }

    public void SaveGame()
    {
        foreach (IDataPersistence obj in dataPersistenceObjects)
        {
            obj.SaveData(ref _gameData);
        }
        
        _dataHandler.Save(_gameData);
    }

    public void DeleteGameData()
    {
        SaveGame();
        File.Delete($@"{Application.persistentDataPath}/{fileName}");
        LoadGame();
        OnAfterGameDataReset?.Invoke();
    }

    private List<IDataPersistence> FindAllDataPersistenceObjects()
    {
        IEnumerable<IDataPersistence> dataPersistenceObjects = FindObjectsOfType<MonoBehaviour>()
            .OfType<IDataPersistence>();
        return new List<IDataPersistence>(dataPersistenceObjects);
    }
}
