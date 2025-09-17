using UnityEngine;

[System.Serializable]
public class Skin
{
    public string name;
    public int price;
    public Color color;
    public SkinState state;

	public Skin(string name, int price, bool isUnlocked, Color color, bool isSelected, SkinState state)
    {
        this.name = name;
        this.price = price;
        this.color = color;
        this.state = state;
    }
}