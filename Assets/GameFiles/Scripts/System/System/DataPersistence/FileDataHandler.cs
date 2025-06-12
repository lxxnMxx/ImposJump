using UnityEngine;
using System;
using System.IO;

public class FileDataHandler
{
    private string _dataDirPath;
    private string _dataFileName;

    private bool _useEncryption;
    private readonly string _encryptionKey = "imposjumpiscool";

    public FileDataHandler(string dataDirPath, string dataFileName, bool useEncryption)
    {
        _dataDirPath = dataDirPath;
        _dataFileName = dataFileName;
        _useEncryption = useEncryption;
    }

    public GameData Load()
    {
        string fullPath = Path.Combine(_dataDirPath, _dataFileName);
        GameData loadedData = null;
        if (File.Exists(fullPath))
        {
            try
            {
                // Load serialized data from file
                string dataToLoad = "";
                using (FileStream stream = new FileStream(fullPath, FileMode.Open))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        dataToLoad = reader.ReadToEnd();
                    }
                }
                // encrypt data (optionally)
                if (_useEncryption) dataToLoad = EncryptDecrypt(dataToLoad);
                
                // deserialize data back from json to gamedata
                loadedData = JsonUtility.FromJson<GameData>(dataToLoad);
            }
            catch (Exception e)
            {
                Debug.Log($"An error occured when trying to save data to the file {fullPath}. \n Error: {e}");
            }
        }
        return loadedData;
    }

    public void Save(GameData gameData)
    {
        // use path combine because different OS's have different path separators
        string fullPath = Path.Combine(_dataDirPath, _dataFileName);

        try
        {
            // create directory if it doesn't exist
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));
            
            // serialize the game data to a Json file
            string dataToStore = JsonUtility.ToJson(gameData, true);

            // encrypt data (optionally)
            if (_useEncryption) dataToStore = EncryptDecrypt(dataToStore);
            
            // write data to file
            using (FileStream stream = new FileStream(fullPath, FileMode.Create))
            {
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.Write(dataToStore);
                }
            }
        }
        catch (Exception e)
        {
            Debug.Log($"An error occured when trying to save data to the file {fullPath}. \n Error: {e}");
        }
    }

    // XOR Encryption
    private string EncryptDecrypt(string data)
    {
        string modifiedData = "";
        for (int i = 0; i < data.Length; i++)
        {
            // actual encrypt/decrypt method
            modifiedData += (char) (data[i] ^ _encryptionKey[i % _encryptionKey.Length]);
        }

        return modifiedData;
    }
}
