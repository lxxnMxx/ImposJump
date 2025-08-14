using UnityEngine;

[System.Serializable]
public class Skin
{
    public string name;
    public int price;
    public bool isCollected;
    public Color color;
    public bool isSelected;

	public Skin(string name, int price, bool isCollected, Color color, bool isSelected)
    {
        this.name = name;
        this.price = price;
        this.isCollected = isCollected;
        this.color = color;
        this.isSelected = isSelected;
	}
}