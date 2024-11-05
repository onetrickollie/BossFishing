using UnityEngine;
using System.Collections.Generic;

public class PlayerInteraction : MonoBehaviour
{
    private bool canFish = false;
    public List<FishData> fishTypes; // List of fish types in the Inspector

    private CatchMessageUI catchMessageUI;

    private void Start()
    {
        catchMessageUI = FindObjectOfType<CatchMessageUI>();
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
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Water"))
        {
            canFish = false;
        }
    }

    private void Fish()
    {
        int randomIndex = Random.Range(0, fishTypes.Count);
        FishData caughtFish = fishTypes[randomIndex];

        GameManager.Instance.AddToBackpack(caughtFish);

        if (catchMessageUI != null)
        {
            catchMessageUI.DisplayCatchMessage(caughtFish.itemName);
        }
    }
}
