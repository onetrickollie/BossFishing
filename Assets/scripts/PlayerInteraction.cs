using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    private bool canFish = false;

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
        // Simulate catching a fish (for now, just a debug message)
        Debug.Log("Caught a fish!");
        GameManager.Instance.AddToBackpack("Fish"); // Add fish to backpack
    }
}
