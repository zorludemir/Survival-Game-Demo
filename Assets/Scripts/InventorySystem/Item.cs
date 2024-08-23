using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Item
{
    public int id;
    public string itemName;
    public Sprite icon;
    public int quantity;

    public Item(int id, string itemName, Sprite icon, int quantity)
    {
        this.id = id;
        this.itemName = itemName;
        this.icon = icon;
        this.quantity = quantity;
    }
}
