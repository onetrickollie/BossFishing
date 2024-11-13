using UnityEngine;
using System.Collections.Generic;

public class PlayerInteraction : MonoBehaviour
{
    private bool canFish = false;

    [SerializeField]
    private List<FishData> fishList = new List<FishData>(); // List of fish data (ScriptableObjects)
    [SerializeField]
    private InventoryManager inventoryManager;
    private CatchMessageUI catchMessageUI;

    private void Start()
    {
        catchMessageUI = FindObjectOfType<CatchMessageUI>();
        if (inventoryManager == null)
        {
            Debug.LogWarning("No InventoryManager found in the scene. Make sure to add one to the scene.");
        }
    }

    private void Update()
    {
        if (canFish && Input.GetMouseButtonDown(0))
        {
            Fish();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Water"))
        {
            canFish = true;
            Debug.Log("You can fish here!");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Water"))
        {
            canFish = false;
            Debug.Log("You can't fish here anymore.");
        }
    }

    private void Fish()
    {
        int randomIndex = Random.Range(0, fishList.Count);
        FishData caughtFish = fishList[randomIndex];

        // Display the catch message
        if (catchMessageUI != null)
        {
            catchMessageUI.DisplayCatchMessage(caughtFish.fishName);
        }

        // Create an Item and add to inventory with price
        if (inventoryManager != null)
        {
            int fishPrice = caughtFish.price;
            Item fishItem = new Item(caughtFish.fishName, 1, caughtFish.fishSprite, caughtFish.fishDescription, fishPrice, true);
            inventoryManager.AddItem(fishItem);
        }
    }
}
