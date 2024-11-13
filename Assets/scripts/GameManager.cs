using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public Vector3 playerPosition = Vector3.zero;
    private Dictionary<string, Vector3> sceneSpawnPoints = new Dictionary<string, Vector3>(); // Store spawn points for each scene
    public string currentScene; // Track the current scene
    public List<Item> inventory = new List<Item>(); // Persistent inventory list
    public int playerGold = 0; // Wallet system to track gold

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

    // Method to set the spawn point for a specific scene
    public void SetSpawnPointForScene(string sceneName, Vector3 spawnPoint)
    {
        if (sceneSpawnPoints.ContainsKey(sceneName))
        {
            sceneSpawnPoints[sceneName] = spawnPoint;
        }
        else
        {
            sceneSpawnPoints.Add(sceneName, spawnPoint);
        }
    }

    // Method to get the spawn point for the current scene
    public Vector3 GetSpawnPointForScene(string sceneName)
    {
        if (sceneSpawnPoints.ContainsKey(sceneName))
        {
            return sceneSpawnPoints[sceneName];
        }
        return Vector3.zero; // Default position if no specific spawn point is set
    }

    public void SetCurrentScene(string sceneName)
    {
        currentScene = sceneName;
    }

    // Inventory management methods
    public void AddItemToInventory(Item item)
    {
        if (!inventory.Contains(item))
        {
            inventory.Add(item);
            Debug.Log($"Added {item.itemName} to the inventory.");
        }
    }

    public List<Item> GetInventory()
    {
        return inventory;
    }

    // Wallet methods
    public void AddGold(int amount)
    {
        playerGold += amount;
        Debug.Log($"Gold added. Current balance: {playerGold}");
    }

    public void DeductGold(int amount)
    {
        playerGold = Mathf.Max(0, playerGold - amount); // Ensure gold does not go negative
        Debug.Log($"Gold deducted. Current balance: {playerGold}");
    }

    // Save player position
    public void SavePlayerPosition(Vector3 position)
    {
        playerPosition = position;
        Debug.Log($"Player position saved: {playerPosition}");
    }
}
