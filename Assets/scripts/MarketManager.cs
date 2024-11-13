using System.Collections.Generic;
using UnityEngine;

public class MarketManager : MonoBehaviour
{
    public void SellItem(Item itemToSell)
    {
        if (GameManager.Instance != null)
        {
            List<Item> inventory = GameManager.Instance.GetInventory();

            if (inventory.Contains(itemToSell))
            {
                inventory.Remove(itemToSell);
                Debug.Log($"Sold {itemToSell.itemName}. It has been removed from the inventory.");

                // Optionally update the UI to reflect the change
                // You could call InventoryManager.UpdateInventoryUI() here if needed
            }
            else
            {
                Debug.LogWarning($"Item {itemToSell.itemName} not found in inventory.");
            }
        }
    }
}

