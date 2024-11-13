using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerInteraction : MonoBehaviour
{
    private bool canFish = false;
    private bool isFishing = false; // To prevent multiple fishing attempts

    [SerializeField]
    private List<FishData> fishList = new List<FishData>(); // List of fish data (ScriptableObjects)
    [SerializeField]
    private InventoryManager inventoryManager;
    [SerializeField]
    private AudioSource fishingSound; // Reference to an AudioSource for playing fishing sound
    [SerializeField]
    private GameObject exclamationMark; // Reference to the exclamation mark GameObject

    private CatchMessageUI catchMessageUI;

    private void Start()
    {
        catchMessageUI = FindObjectOfType<CatchMessageUI>();
        if (inventoryManager == null)
        {
            Debug.LogWarning("No InventoryManager found in the scene. Make sure to add one to the scene.");
        }

        if (exclamationMark != null)
        {
            exclamationMark.SetActive(false); // Ensure the exclamation mark is hidden initially
        }
    }

    private void Update()
    {
        if (canFish && Input.GetMouseButtonDown(0) && !isFishing)
        {
            StartCoroutine(FishingRoutine()); // Start the fishing coroutine
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

    private IEnumerator FishingRoutine()
    {
        isFishing = true;

        // Play fishing sound
        if (fishingSound != null)
        {
            fishingSound.Play();
        }

        // Wait for a random duration before a fish is on the hook
        float waitTime = Random.Range(2f, 5f); // Random wait between 2 to 5 seconds
        yield return new WaitForSeconds(waitTime);

        // Display exclamation mark to indicate a fish is on the hook
        if (exclamationMark != null)
        {
            exclamationMark.SetActive(true);
        }

        // Wait for the player to click again to catch the fish
        yield return new WaitUntil(() => Input.GetMouseButtonDown(0));

        // Hide the exclamation mark
        if (exclamationMark != null)
        {
            exclamationMark.SetActive(false);
        }

        // Catch the fish
        Fish();

        isFishing = false;
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

        // Create an Item and add to inventory
        if (inventoryManager != null)
        {
            int fishPrice = caughtFish.price;
            Item fishItem = new Item(caughtFish.fishName, 1, caughtFish.fishSprite, caughtFish.fishDescription, fishPrice, true);
            inventoryManager.AddItem(fishItem);
        }
    }
}
