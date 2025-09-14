using UnityEngine;

[System.Serializable]
public class Skin
{
    public string name;
    public int price;
    public bool isUnlocked;
    public Color color;
    public bool isSelected;

	public Skin(string name, int price, bool isUnlocked, Color color, bool isSelected)
    {
        this.name = name;
        this.price = price;
        this.isUnlocked = isUnlocked;
        this.color = color;
        this.isSelected = isSelected;
	}
}