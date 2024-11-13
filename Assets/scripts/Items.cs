using Unity.VisualScripting;
using UnityEngine;

// [System.Serializable]
// A data structure to hold item data
public class Item
{
    public string itemName;
    public int quantity;
    public Sprite itemSprite;
    [TextArea][SerializeField] private string itemDescription;

    public FishData linkedFishData; // Optional reference to a ScriptableObject

    // Constructor that takes 4 parameters
    public Item(string name, int qty, Sprite sprite, string description)
    {
        itemName = name;
        quantity = qty;
        itemSprite = sprite;
        itemDescription = description;
    }

    // Property to retrieve the correct description (editable if linked to FishData)
    public string Description => linkedFishData != null ? linkedFishData.fishDescription : itemDescription;
}