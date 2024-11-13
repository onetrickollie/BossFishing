using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MarketManager : MonoBehaviour
{
    public GameObject sellPanel; // UI panel to display when interacting with the counter
    public Button sellButton; // Button for selling all items
    public Button exitButton; // Button to close the panel
    public TMP_Text messageText; // Text component for displaying messages
    private bool playerInRange = false;

    private void Start()
    {
        
        if (sellPanel != null) sellPanel.SetActive(false);
        if (sellButton != null) sellButton.onClick.AddListener(SellAllItems);
        if (exitButton != null) exitButton.onClick.AddListener(ClosePanel);
        if (messageText != null) messageText.text = ""; 
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInRange = true;
            if (sellPanel != null)
            {
                sellPanel.SetActive(true); // Show the panel when the player is in range
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInRange = false;
            if (sellPanel != null)
            {
                sellPanel.SetActive(false); // Hide the panel when the player leaves range
            }
        }
    }

   public void SellAllItems()
{
    if (playerInRange && GameManager.Instance != null)
    {
        Debug.Log("Selling all items...");
        List<Item> inventory = GameManager.Instance.GetInventory();

        // Check if there are any fish to sell
        bool hasFish = inventory.Exists(item => item.isFish);
        if (!hasFish)
        {
            if (messageText != null)
            {
                messageText.text = "You have no fish to sell!";
            }
            return; // Exit early if no fish are found
        }

        int fishCount = 0;
        int totalGoldEarned = 0;
        List<Item> itemsToRemove = new List<Item>();

        // Sell all fish items
        foreach (Item item in inventory)
        {
            if (item.isFish)
            {
                totalGoldEarned += item.price;
                fishCount++;
                itemsToRemove.Add(item);
            }
        }

        // Remove sold items
        foreach (Item item in itemsToRemove)
        {
            inventory.Remove(item);
        }

        // Update gold and display message
        GameManager.Instance.AddGold(totalGoldEarned);
        if (messageText != null)
        {
            messageText.text = $"You sold {fishCount} fish and earned {totalGoldEarned} gold!";
        }

        Debug.Log($"Sold {fishCount} fish for {totalGoldEarned} gold.");
        ClosePanel();
    }
}


    public void ClosePanel()
    {
        if (sellPanel != null)
        {
            sellPanel.SetActive(false);
        }

        if (messageText != null)
        {
            messageText.text = ""; // Clear message when closing the panel
        }
    }
}
