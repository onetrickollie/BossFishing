using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public GameObject InventoryMenu;
    private bool menuActivated;
    public List<Item> items = new List<Item>();
    public ItemSlot[] itemSlot; // Assuming you have slots to display items

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            menuActivated = !menuActivated;
            InventoryMenu.SetActive(menuActivated);
            Time.timeScale = menuActivated ? 0 : 1;
        }
    }

    public void AddItem(Item newItem)
    {
        // Ensure we check each slot and find the first available one
        for (int i = 0; i < itemSlot.Length; i++)
        {
            if (!itemSlot[i].isFull)
            {
                items.Add(newItem); // Add the new item to the items list
                itemSlot[i].AddItem(newItem); // Update the item slot UI
                itemSlot[i].isFull = true; // Mark the slot as full
                Debug.Log($"Added {newItem.itemName} to the inventory.");
                return;
            }
        }
        Debug.LogWarning("Inventory is full. Cannot add more items.");
    }

    public void DeselectAllSlots()
    {
        for (int i = 0; i < itemSlot.Length; i++)
        {
            itemSlot[i].selectedShader.SetActive(false);
            itemSlot[i].thisItemSelected = false;
        }
    }

    public void DisplayItemDetails(Item item)
    {
        if (item == null)
        {
            Debug.LogWarning("DisplayItemDetails was called with a null item.");
            return;
        }

        Debug.Log($"Displaying details for item: {item.itemName}");

        foreach (var slot in itemSlot)
        {
            if (slot.thisItemSelected)
            {
                if (slot.itemDescriptionImage != null)
                {
                    slot.itemDescriptionImage.sprite = item.itemSprite;
                }
                else
                {
                    Debug.LogWarning("Item description image is null in the selected slot.");
                }

                if (slot.itemDescriptionNameText != null)
                {
                    slot.itemDescriptionNameText.text = item.itemName;
                }
                else
                {
                    Debug.LogWarning("Item description name text is null in the selected slot.");
                }

                if (slot.itemDescriptionText != null)
                {
                    slot.itemDescriptionText.text = item.Description;
                }
                else
                {
                    Debug.LogWarning("Item description text is null in the selected slot.");
                }
            }
        }
    }
}
