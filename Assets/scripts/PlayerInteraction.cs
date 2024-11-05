using UnityEngine;
using System.Collections.Generic;

public class PlayerInteraction : MonoBehaviour
{
    private bool canFish = false;
    private List<string> fishTypes = new List<string> { "Bass", "Anchovy", "Broken CD" };

    private CatchMessageUI catchMessageUI;

    private void Start()
    {
        // Find the CatchMessageUI component in the scene
        catchMessageUI = FindObjectOfType<CatchMessageUI>();
    }

    private void Update()
    {
        // Check for left mouse click to fish
        if (canFish && Input.GetMouseButtonDown(0)) // 0 is the left mouse button
        {
            Fish();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if player enters a water area by layer
        if (collision.gameObject.layer == LayerMask.NameToLayer("Water"))
        {
            canFish = true;
            Debug.Log("You can fish here!");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // Disable fishing when player leaves the water area
        if (collision.gameObject.layer == LayerMask.NameToLayer("Water"))
        {
            canFish = false;
            Debug.Log("You can't fish here anymore.");
        }
    }

    private void Fish()
    {
        // Randomly select a fish from the list
        int randomIndex = Random.Range(0, fishTypes.Count);
        string caughtFish = fishTypes[randomIndex];

        // Add the caught fish to the backpack
        GameManager.Instance.AddToBackpack(caughtFish);

        // Display the catch message
        if (catchMessageUI != null)
        {
            catchMessageUI.DisplayCatchMessage(caughtFish);
        }
    }
}
