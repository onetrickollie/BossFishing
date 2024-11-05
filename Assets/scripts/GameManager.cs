using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public Vector3 playerPosition; // To save the player's position
    public List<ItemData> backpackItems = new List<ItemData>(); // Backpack inventory

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Method to save player's position
    public void SavePlayerPosition(Vector3 position)
    {
        playerPosition = position;
    }

    // Method to add item to backpack (generalized for any item type)
    public void AddToBackpack(ItemData item)
    {
        backpackItems.Add(item);
    }
}
