using System;
using UnityEngine;
using UnityEngine.UI;

public class Skin : MonoBehaviour, IDataPersistence
{
    public int price;
    public bool isCollected;

    [SerializeField] private GameObject player;

    private Color _skinColor;
    
    private void Start()
    {
        _skinColor = gameObject.GetComponent<Image>().color;
    }

    public void CollectSkin()
    {
        transform.GetChild(0).gameObject.SetActive(false); // deactivate the Image that grays out the skin
        isCollected = true;
        SelectSkin();
    }

    public void SelectSkin()
    {
        player.GetComponent<SpriteRenderer>().color = _skinColor;
    }
    
    public void LoadData(GameData data)
    {
        print("Data loaded");
    }

    public void SaveData(ref GameData data)
    {
        print("Data saved");
    }
}