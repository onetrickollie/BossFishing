using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    
    public Vector3 playerPosition; // To save the player's position
    public List<string> backpackItems = new List<string>(); // Backpack inventory

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

    // Method to add item to backpack
    public void AddToBackpack(string item)
    {
        backpackItems.Add(item);
    }
}
