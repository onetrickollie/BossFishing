using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]

// a data structure to hold item data
public class Item
{
    public string itemName;
    public int quantity;
    public Sprite itemSprite;
    [TextArea][SerializeField]private string itemDescription;

    public Item(string name, int qty, Sprite sprite)
    {
        itemName = name;
        quantity = qty;
        itemSprite = sprite;
    }
}