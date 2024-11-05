using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    private bool canFish = false;

    private void Update()
    {
        if (canFish && Input.GetKeyDown(KeyCode.F)) // Press 'F' to fish
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
        // Add a fish to the GameManager's backpack
        GameManager.Instance.AddToBackpack("Fish");
        Debug.Log("Caught a fish!");
    }
}
