using UnityEngine;

public interface IDataPersistence
{

    void LoadData(GameData data);
    // here with a reference cause script only changes the existing data
    void SaveData(ref GameData data);
}
